
// Copyright Christophe Bertrand.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UniversalSerializerLib3;

namespace UniversalSerializer
{

   /// <summary>
   /// Serializations methods for WPF.
   /// </summary>
   public class UniversalSerializerWpf : UniversalSerializerLib3.UniversalSerializer
   {
      /// <summary>
      /// Modifiers for WPF types.
      /// This class will be found by reflexion and set in a ModifiersAssembly.
      /// </summary>
      public class WpfCustomModifiers : CustomModifiers
      {
         /// <summary>
         /// Inits WPF modifiers.
         /// </summary>
         public WpfCustomModifiers()
            : base(
            Containers: new ITypeContainer[] {
               new XamlValueSerializerContainer(),
               new DependencyPropertyContainer()
                 },
            FilterSets: new FilterSet[1] { new FilterSet() {
               DefaultConstructorTestCleaner = WpfDefaultConstructorTestCleaner
            } }
            )
         {
         }
      }

      // - - - -

      /// <summary>
      /// Prepare a serializer and deserializer following your parameters and modifiers.
      /// </summary>
      /// <param name="stream">The stream that stores, or will store, the serialized data.</param>
      public UniversalSerializerWpf(
         Stream stream)
         : base(CheckParameters(new Parameters() { Stream = stream }))
      {
      }

      // - - - -

      /// <summary>
      /// Prepare a serializer and deserializer following your parameters and modifiers.
      /// Parameters.Stream must be defined.
      /// </summary>
      /// <param name="parameters"></param>
      public UniversalSerializerWpf(
         Parameters parameters)
         : base(CheckParameters(parameters))
      {
      }

      // - - - -

      /// <summary>
      /// Prepare a serializer and deserializer that work on a file.
      /// Do not forget to call Dispose() when you release UniversalSerializer, or to write using(). Otherwize an IO exception could occure when trying to access the file for the second time, saying the file can not be open (in fact the file would have not be closed by this first instance of UniversalSerializer).
      /// </summary>
      /// <param name="fileName">The name of the file that will be open or created.</param>
      public UniversalSerializerWpf(
         string fileName)
         : this(new Parameters() { Stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite) })
      {
         this.FileStreamCreatedByConstructorOnly = (FileStream)this.parameters.Stream; // For Dispose().
      }

      // - - - -

      static bool WpfDefaultConstructorTestCleaner(object instance)
      {
         // Serializer instanciates the default constructor (when available) once to ensure it does not throw an exception. Unfortunately, that instanciation can lead to problems. Example: each WPF Window have to be closed properly before closing the application.
         // The solution is to close the temporary Window.
         System.Windows.Window I = instance as System.Windows.Window;
         if (I != null)
         {
            I.Close(); // Let the Application close.
            return true;
         }
         return false;
      }

      static Parameters CheckParameters(Parameters parameters)
      {
         // Adds the internal modifiers to the list.
         if (parameters.ModifiersAssemblies == null)
            parameters.ModifiersAssemblies = new ModifiersAssembly[1] { UniversalSerializerWpf.InternalWpfModifiersAssembly };
         else
            if (!parameters.ModifiersAssemblies.Contains(UniversalSerializerWpf.InternalWpfModifiersAssembly))
         {
            var mas = new List<ModifiersAssembly>(parameters.ModifiersAssemblies);
            mas.Add(UniversalSerializerWpf.InternalWpfModifiersAssembly);
            parameters.ModifiersAssemblies = mas.ToArray();
         }

         return parameters;
      }

      internal static readonly ModifiersAssembly InternalWpfModifiersAssembly =
         ModifiersAssembly.GetModifiersAssembly(Assembly.GetExecutingAssembly());
   }

   // ####################################################################################
}

