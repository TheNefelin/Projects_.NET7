// See https://aka.ms/new-console-template for more information
using Core.Data;
using Dapper;
using Utils;

Console.WriteLine("Hello, World!");

var cnn = "Data Source=localhost; Initial Catalog=db_testing; User ID=testing; Password=testing; TrustServerCertificate=True;";

var dapper = new DapperContext(cnn);
var encryption = new EncryptionUtil();

var commandDefinition = new CommandDefinition(
    commandText: "SELECT Data_Id, Data01, Data02, Data03, User_Id FROM PM_CoreData WHERE User_Id = @User_Id ORDER BY Data01",
    parameters: new
    {
        User_Id = "3FA85F64-5717-4562-B3FC-2C963F66AFA6"
    }
);

using var connection = dapper.CreateConnection();
var res = await connection.QueryAsync<dynamic>(commandDefinition);

var password = Console.ReadLine();

foreach (var item in res)
{
    var data01 = encryption.Decrypt(item.Data01, password, "05lvmZ36EoSIBVF4NjQYsA==");
    var data02 = encryption.Decrypt(item.Data02, password, "05lvmZ36EoSIBVF4NjQYsA==");
    var data03 = encryption.Decrypt(item.Data03, password, "05lvmZ36EoSIBVF4NjQYsA==");

    Console.WriteLine("=======================================================");
    Console.WriteLine($"Data01: {data01}");
    Console.WriteLine($"Data02: {data02}");
    Console.WriteLine($"Data03: {data03}");
}

Console.ReadKey();
