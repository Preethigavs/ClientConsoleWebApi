using WebApiClientConsole;

Console.WriteLine("Api client");
EmployeeAPIClient.DeleteEmployee(21).Wait();
Console.ReadLine();