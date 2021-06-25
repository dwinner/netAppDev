#region Using directives

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

#endregion

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle(@"")]
[assembly: AssemblyDescription(@"")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(@"AppDevUnited")]
[assembly: AssemblyProduct(@"TaskFlowDSLSample")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: System.Resources.NeutralResourcesLanguage("en")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion(@"1.0.0.0")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"AppDevUnited.TaskFlowDSLSample.DslPackage, PublicKey=002400000480000094000000060200000024000052534131000400000100010059854D49659C77FB376976C052C956073026152A0F655983582FE042B82D6881A4394F9B3AB40463236C15EE45D92C140F858FFB9D305C33ECF2BD427A020BD6FCBA5ED1AC74CD8DA2FF5B97850879F50E37B08AF35B7F8E23D9EAEF6B5FA24288C2211894AA53F9ABFD6243B5B6B6D755E7ED9D67A1812C70CC225BB02093B8")]