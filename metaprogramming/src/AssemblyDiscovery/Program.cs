﻿using System.Reflection;

var assembly = Assembly.GetEntryAssembly();
Console.WriteLine(assembly!.FullName);
var assemblies = assembly.GetReferencedAssemblies();
var assemblyNames = string.Join(", ", assemblies.Select(assemblyName => assemblyName.Name));
Console.WriteLine(assemblyNames);