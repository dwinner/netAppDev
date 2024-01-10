using DynamicFileReader;

var helper = new DynamicFileHelper();
var employeeList = helper.ParseCsvFile("EmployeeList.txt");
foreach (var employee in employeeList)
{
   Console.WriteLine(string.Format("{0} {1} lives in {2} {3}.",
      employee.FirstName,
      employee.LastName,
      employee.City,
      employee.State));
}

Console.ReadLine();