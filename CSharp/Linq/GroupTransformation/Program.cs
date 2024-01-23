var files = Directory.GetFiles(Path.GetTempPath()).Take(100).ToArray().AsQueryable();

// In Fluent Syntax
var fluentGrp = files.GroupBy(
   file => Path.GetExtension(file),
   file => file.ToUpper());
Console.WriteLine(fluentGrp);

// In query syntax
var qGrp =
   from file in files
   group file.ToUpper() by Path.GetExtension(file);
Console.WriteLine(qGrp);