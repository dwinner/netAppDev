using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RefReturn
{
    struct Point3d
    {
        public double x;
        public double y;
        public double z;

        public string Name { get; set; }
        //public string Name2 { get; set; }

        //public string Name3 { get; set; }
    }

    class Vector
    {
        private Point3d location;
        public Point3d Location { get; set; }
        public ref Point3d RefLocation { get { return ref this.location; } }

        private int magnitude;

        public int Magnitude
        {
            get { return this.magnitude; }
            set { this.magnitude = value; }
        }

        public ref int RefMagnitude { get { return ref this.magnitude; } }
    }

    class Program
    {
        private const int ArraySize = 5;
        private const int NumIterations = 10000000;

        private static readonly Random rand = new Random();        
        
        static void Main(string[] args)
        {
            var arr = GenerateRandomArray(ArraySize);
            ZeroMiddleValue(arr);

            RefZeroMiddleValue(arr);

            // We can also now do things like assigning values to methods
            GetRefToMiddle(arr) = 0;
                        
            var vector = new Vector { Location = new Point3d { x = 1, y = 2, z = 3 }, Magnitude = 2 };

            SetVectorToOrigin(vector);
            RefSetVectorToOrigin(vector);

            Console.WriteLine("Benchmarks:");

            BenchmarkSetVectorToOrigin();

            BenchmarkRefSetVectorToOrigin();            
        }

        #region Ref Array Access
        private static void ZeroMiddleValue(int[] arr)
        {
            int midIndex = GetMidIndex(arr);
            arr[midIndex] = 0;
        }

        private static int GetMidIndex(int[] arr)
        {
            return arr.Length / 2;
        }

        private static void RefZeroMiddleValue(int[] arr)
        {
            ref int middle = ref GetRefToMiddle(arr);
            middle = 0;
        }

        private static ref int GetRefToMiddle(int[] arr)
        {
            return ref arr[arr.Length / 2];
        }

        private static int[] GenerateRandomArray(int arraySize)
        {
            var arr = new int[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                arr[i] = rand.Next();
            }
            return arr;
        }
        #endregion

        #region Ref Struct Access
        private static void SetVectorToOrigin(Vector vector)
        {            
            Point3d pt = vector.Location;
            pt.x = 0;
            pt.y = 0;
            pt.z = 0;
            vector.Location = pt;            
        }

        private static void RefSetVectorToOrigin(Vector vector)
        {            
            ref Point3d location = ref vector.RefLocation;
            location.x = 0;
            location.y = 0;
            location.z = 0;            
        }

        private static void BenchmarkSetVectorToOrigin()
        {
            var vector = new Vector { Location = new Point3d { x = 1, y = 2, z = 3 }, Magnitude = 2 };

            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < NumIterations; i++)
            {
                SetVectorToOrigin(vector);
            }
            watch.Stop();
            Console.WriteLine($"SetVectorsToOrigin: {watch.ElapsedMilliseconds}ms");
        }

        private static void BenchmarkRefSetVectorToOrigin()
        {
            var vector = new Vector { Location = new Point3d { x = 1, y = 2, z = 3 }, Magnitude = 2 };

            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < NumIterations; i++)
            {                
                RefSetVectorToOrigin(vector);
            }
            watch.Stop();

            Console.WriteLine($"RefSetVectorsToOrigin: {watch.ElapsedMilliseconds}ms");
        }
        #endregion  

        
    }
}
