using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Diagnostics.Runtime;

namespace AnalyzeDump
{
   internal class Program
   {
      private const string TargetType = "LargeMemoryUsage.D";
      private const string TargetProcessName = "LargeMemoryUsage.exe";

      private static void Main(string[] args)
      {
         DataTarget dataTarget;
         Process targetProcess = null;
         if (args.Length > 0)
         {
            if (!File.Exists(args[0]))
            {
               Console.WriteLine($"{args[0]} could not be found");
               return;
            }

            dataTarget = DataTarget.LoadDump(args[0]);
         }
         else
         {
            // Let's create our own process to test with
            Console.WriteLine("Starting target process");
            var startInfo = new ProcessStartInfo(TargetProcessName);
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            targetProcess = Process.Start(startInfo);
            Thread.Sleep(1000);
            dataTarget = DataTarget.AttachToProcess(targetProcess.Id, true);
         }


         try
         {
            PrintDumpInfo(dataTarget);

            // Let's download the correct DAC for this CLR
            var dacFile = GetDacFile(dataTarget.ClrVersions[0], dataTarget);

            var clr = dataTarget.ClrVersions[0].CreateRuntime(dacFile);

            PrintHeapInfo(clr);

            PrintSegmentInfo(clr);

            PrintGen0Objects(clr);

            PrintLOHObjects(clr);

            PrintGen1ObjectsByHeapSegment(clr);

            PrintRootsOfObjects(clr);

            PrintObjectSize(clr);

            PrintFinalizableObjects(clr);

            PrintBoxedObjects(clr);

            PrintTop10BiggestMethods(clr);
         }
         finally
         {
            dataTarget.Dispose();
            targetProcess?.Kill();
         }
      }

      private static string GetDacFile(ClrInfo clrInfo, DataTarget target)
      {
         var location = clrInfo.ModuleInfo.FileName;
         /*if (string.IsNullOrEmpty(location) || !File.Exists(location))
         {
             // try to download from symbol server
             ModuleInfo dacInfo = clrInfo.ModuleInfo;
             try
             {
                 location = target.SymbolLocator.FindBinary(dacInfo);
             }
             catch (WebException)
             {
                 return null;
             }
         }*/
         return location;
      }

      private static void PrintSegmentInfo(ClrRuntime clr)
      {
         PrintHeader("Segment Info");
         foreach (var segment in clr.Heap.Segments)
         {
            Console.WriteLine($"0x{segment.Start:x} - 0x{segment.End:x} ({segment.Length:N0} bytes)");
            /*Console.WriteLine($"\tEphemeral: {segment.IsEphemeral}, Large:{segment.IsLarge}");
            Console.WriteLine($"\tProcessor Affinity: {segment.ProcessorAffinity}");
            Console.WriteLine($"\tGen2: 0x{segment.Gen2Start:x} ({segment.Gen2Length:N0}), Gen1: 0x{segment.Gen1Start:x} ({segment.Gen1Length:N0}), Gen0: 0x{segment.Gen0Start:x} ({segment.Gen0Length:N0})");*/
            Console.WriteLine();
         }
      }

      private static void PrintObjectSize(ClrRuntime clr)
      {
         PrintHeader("Object Size");

         var obj = FindObjectOfType(clr, TargetType);
         Console.WriteLine($"0x{obj.Address:x} - {obj.Type.Name}");
         var heap = clr.Heap;
         // Evaluation stack
         var stack = new Stack<ulong>();

         var considered = new HashSet<ulong>();

         var count = 0;
         ulong size = 0;
         stack.Push(obj.Address);

         while (stack.Count > 0)
         {
            var objAddr = stack.Pop();
            if (considered.Contains(objAddr))
            {
               continue;
            }

            considered.Add(objAddr);

            var type = heap.GetObjectType(objAddr);
            if (type == null)
            {
               continue;
            }

            count++;
            /*size += type.GetSize(objAddr);
            type.EnumerateRefsOfObject(objAddr, delegate (ulong child, int offset)
            {
                if (child != 0 && !considered.Contains(child))
                    stack.Push(child);
            });*/
         }

         Console.WriteLine($"Object Size: {obj.Size}");
         Console.WriteLine($"Full size: {size}");
      }

      private static void PrintFinalizableObjects(ClrRuntime clr)
      {
         PrintHeader("Finalizable Objects");
         foreach (var objAddr in clr.Heap.EnumerateFinalizableObjects())
         {
            var type = clr.Heap.GetObjectType(objAddr);
            if (type == null)
            {
               continue;
            }

            var obj = new ClrObject(objAddr, type);
            Console.WriteLine($"0x{objAddr:x} - {obj.Type.Name}");
         }
      }

      private static void PrintBoxedObjects(ClrRuntime clr)
      {
         PrintHeader("Boxed Objects");

         const int MaxObjects = 10;
         var objectsPrinted = 0;
         foreach (var obj in clr.Heap.EnumerateObjects())
         {
            if (obj.IsBoxedValue)
            {
               Console.WriteLine($"0x{obj.Address:x} - {obj.Type.Name}");
               if (++objectsPrinted >= MaxObjects)
               {
                  break;
               }
            }
         }
      }

      private static void PrintGen0Objects(ClrRuntime clr)
      {
         /*PrintHeader("Gen0 Objects (limit:10)");
         var heap = clr.Heap;
         const int MaxObjects = 10;
         int objectsPrinted = 0;
         foreach(var obj in heap.EnumerateObjects())
         {
             int gen = heap.GetGeneration(obj.Address);
             if (gen == 0)
             {
                 Console.WriteLine($"0x{obj.Address:x} - {obj.Type.Name}");
                 if (++objectsPrinted >= MaxObjects)
                 {
                     break;
                 }
             }
         }*/
      }

      private static void PrintLOHObjects(ClrRuntime clr)
      {
         PrintHeader("LOH Objects (limit:10)");

         var objectCount = 0;
         const int MaxObjectCount = 10;
         if (clr.Heap.CanWalkHeap)
         {
            foreach (var segment in clr.Heap.Segments)
            {
               /*if (segment.IsLarge)
               {
                   for (ulong objAddr = segment.FirstObject; objAddr != 0; objAddr = segment.NextObject(objAddr))
                   {
                       var type = clr.Heap.GetObjectType(objAddr);
                       if (type == null)
                       {
                           continue;
                       }
                       var obj = new ClrObject(objAddr, type);
                       if (++objectCount > MaxObjectCount)
                       {
                           break;
                       }
                       Console.WriteLine($"{obj.Address} {obj.Type.Name}");
                   }
               }*/
            }
         }
      }

      private static void PrintGen1ObjectsByHeapSegment(ClrRuntime clr)
      {
         PrintHeader("Gen1 Objects by Heap Segment");
         var objectCount = 0;
         const int MaxObjectCount = 10;
         if (clr.Heap.CanWalkHeap)
         {
            foreach (var segment in clr.Heap.Segments)
            {
               // Only the ephemeral segment contains gen0 and gen1
               /*if (segment.IsEphemeral)
               {
                   //get range of gen 1
                   ulong start = segment.Gen1Start;
                   ulong end = start + segment.Gen1Length;
                   Console.WriteLine($"Segment Info: Start: {start}, End {end}");

                   for (ulong objAddr = segment.FirstObject; objAddr != 0; objAddr = segment.NextObject(objAddr))
                   {
                       if (objAddr >= start && objAddr < end)
                       {
                           var type = clr.Heap.GetObjectType(objAddr);
                           if (type == null)
                           {
                               continue;
                           }
                           var obj = new ClrObject(objAddr, type);
                           if (++objectCount > MaxObjectCount)
                           {
                               break;
                           }
                           Console.WriteLine($"{obj.Address} {obj.Type.Name}");
                       }
                   }

                   break;
               }*/
            }
         }
      }

      private static void PrintRootsOfObjects(ClrRuntime clr)
      {
         PrintHeader("Roots of Object");

         var childToParents = new Dictionary<ulong, ClrObject>();
         var heap = clr.Heap;

         // Find an arbitrary object for demo purposes
         var targetObject = FindObjectOfType(clr, TargetType);

         if (targetObject.Address == 0)
         {
            Console.WriteLine($"Could not find any objects of type {TargetType}");
            return;
         }

         // Analyze all objects, build up reference map
         foreach (var obj in heap.EnumerateObjects())
         {
            foreach (var objRef in obj.EnumerateReferences())
            {
               childToParents[objRef.Address] = obj;
            }
         }

         // Walk up the chain of references
         var currentObj = targetObject;
         var indentSize = 0;
         while (true)
         {
            Console.Write(new string(' ', indentSize));
            Console.WriteLine($"0x{currentObj.Address:x} - {currentObj.Type.Name}");

            ClrObject parentObject;
            if (!childToParents.TryGetValue(currentObj.Address, out parentObject))
            {
               break;
            }

            currentObj = parentObject;
            indentSize += 4;
         }
      }

      private static void PrintTop10BiggestMethods(ClrRuntime clr)
      {
         PrintHeader("Top 10 Methods");
         var methods = new List<MethodSize>();

         foreach (var module in clr.EnumerateModules())
         {
            // Only look at our own methods                
            if (!module.Name.EndsWith(TargetProcessName))
            {
               continue;
            }

            var filename = Path.GetFileName(module.Name);
            /*foreach (var type in module.EnumerateTypes())
            {
                foreach(var method in type.Methods)
                {
                    ulong ilSize = 0;
                    ulong nativeSize = 0;

                    // See https://github.com/Microsoft/clrmd/blob/master/Documentation/MachineCode.md
                    // for a description of how to calculate machine code size
                    if (method.IL != null)
                    {
                        ilSize += (ulong)method.IL.Length;

                        if (method.ILOffsetMap != null)
                        {
                            for (var iOffset = 0; iOffset < method.ILOffsetMap.Length; iOffset++)
                            {
                                var entry = method.ILOffsetMap[iOffset];
                                var size = entry.EndAddress - entry.StartAddress;
                                nativeSize += size;
                            }
                        }
                    }
                    var methodSize = new MethodSize()
                    {
                        Module = filename,
                        TypeName = type.Name,
                        Name = method.Name,
                        ILSize = ilSize,
                        NativeSize = nativeSize
                    };
                    methods.Add(methodSize);
                }
            }    */
         }

         methods.Sort((a, b) => { return -a.NativeSize.CompareTo(b.NativeSize); });

         Console.WriteLine("Module, Type, Method, IL Size, Native Size");
         Console.WriteLine("------------------------------------------");
         for (var i = 0; i < Math.Min(10, methods.Count); i++)
         {
            var method = methods[i];
            Console.WriteLine(
               $"{method.Module}, {method.TypeName}, {method.Name}, {method.ILSize}, {method.NativeSize}");
         }
      }

      private static void PrintHeapInfo(ClrRuntime clr)
      {
         PrintHeader("Heap Info");
         //Console.WriteLine($"Heap Count: {clr.Heap.Count}");
         var heap = clr.Heap;
         //Console.WriteLine($"Size: {heap.TotalHeapSize:N0} bytes");
         Console.WriteLine($"Segment Count: {heap.Segments.Length}");
         Console.WriteLine($"Can Walk Heap? {heap.CanWalkHeap}");
      }

      private static void PrintDumpInfo(DataTarget target)
      {
         PrintHeader("Target Info");

         /*Console.WriteLine($"Architecture: {target.Architecture}");
         Console.WriteLine($"Pointer Size: {target.PointerSize}");*/
         Console.WriteLine("CLR Versions:");
         foreach (var clr in target.ClrVersions)
         {
            Console.WriteLine($"\t{clr.Version}");
         }
      }

      private static ClrObject FindObjectOfType(ClrRuntime clr, string typeName)
      {
         foreach (var obj in clr.Heap.EnumerateObjects())
         {
            if (obj.Type.Name == TargetType)
            {
               return obj;
            }
         }

         return new ClrObject();
      }

      private static void PrintHeader(string value)
      {
         Console.WriteLine();
         Console.WriteLine(value);
         Console.WriteLine(new string('=', value.Length));
      }

      private class MethodSize
      {
         public string Module { get; }
         public string TypeName { get; }
         public string Name { get; }
         public ulong ILSize { get; }
         public ulong NativeSize { get; }
      }
   }
}