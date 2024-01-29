using System.Xml.Linq;

var project = XElement.Parse("""

                             <Project Sdk="Microsoft.NET.Sdk">
                             
                             	<PropertyGroup>
                             		<OutputType>Exe</OutputType>
                             		<TargetFramework>netcoreapp3.0</TargetFramework>
                             		<Authors>Joe Bloggs</Authors>
                             		<Version>1.1.42</Version>
                             	</PropertyGroup>
                             
                             	<ItemGroup>
                             		<PackageReference Include="Microsoft.EntityFrameworkCore"
                             						Version="3.0.0" />
                             		<PackageReference Include="Microsoft.EntityFrameworkCore.Proxies"
                             						Version="3.0.0" />
                             		<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer"
                             						Version="3.0.0" />
                             		<PackageReference Include="Newtonsoft.Json"
                             						Version="12.0.2" />
                             	</ItemGroup>

                             </Project>

                             """);

var query =
   new XElement("DependencyReport",
      from compileItem in
         project.Elements("ItemGroup").Elements("PackageReference")
      let include = compileItem.Attribute("Include")
      where include != null
      select new XElement("Dependency", include.Value)
   );

Console.WriteLine(query);