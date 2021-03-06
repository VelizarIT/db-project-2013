﻿using System;
using System.IO;
using System.Linq;
using Supermarkets.Data;

namespace Supermarkets
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.SetIn(new StringReader("N\nN\nN\nN\nN\nY\nY\n"));
            if (Ask("Run Task 1?"))
            {
                using (var sqlserver = new SupermarketsEntities(true))
                {
                    // Database.SetInitializer<SupermarketsEntities>(new DropCreateDatabaseAlways<SupermarketsEntities>());
                    // sqlserver.Database.Initialize(true);
                    if (Ask("Transfer from MySQL?"))
                    {
                        MySqlTransfer.Transfer(sqlserver);
                        Console.WriteLine("Transfer complete");
                    }

                    if (Ask("Transfer from Excel?"))
                    {
                        ExcelTransfer.Transfer(sqlserver);
                        Console.WriteLine("Transfer complete");
                    }
                }
            }

            using (var sqlserver = new SupermarketsEntities())
            {
                if (Ask("Run Task 2?"))
                {
                    var file = @"output\report-aggregate-sales.pdf";

                    Directory.CreateDirectory("output");
                    Supermarkets.Task2.PDF.PdfSalesReport.GeneratePdfReport(sqlserver, file);

                    Console.WriteLine(string.Format("PDF aggregate sales report in {0}", file));
                }

                if (Ask("Run Task 3?"))
                {
                    var file = @"output\report-vendor-sales.xml";

                    Directory.CreateDirectory("output");
                    Supermarkets.Task3.XML.GenerateXMLFile.GenerateAggregateReport(sqlserver, file);

                    Console.WriteLine(string.Format("XML vendor sales report in {0}", file));
                }

                if (Ask("Run task 4?"))
                {
                    Supermarkets.Task4.MongoDB.InsertIntoMongoDB.GenerateMongoDBProductReport();

                    Console.WriteLine("Data uploaded to MongoDb");
                }

                if (Ask("Run task 5?"))
                {
                    var file = @"files\VendorExpenses.xml";

                    Supermarkets.Task5.VendorExpencesXML.GenerateVendorExpenses.WriteVendorExpensesReport(sqlserver, file);

                    Console.WriteLine("Task 5: expenses added to MongoDb and SQLServer");
                }

                if (Ask("Run Task 6?"))
                {
                    var db = @"output\taxes.sqlite";

                    File.Delete(db);
                    File.Copy(@"files\\taxes_and_vendor_financials.sqlite", db);
                    var file = @"output\final-report.xlsx";

                    using (var sqlite = new Supermarkets.SQLite.EntityFramework.SQLiteTaxesEntities())
                    {
                        Supermarkets.Task6.MongoToSQLite.LoadMongoIntoSQLite.Load(sqlite);

                        Supermarkets.Task6.TotalReport.ExcelWriter.GenerateExcel(sqlite.VendorFinancialResults, file);
                    }

                    Console.WriteLine(string.Format("Loaded SQLite in {0}", db));
                    Console.WriteLine(string.Format("Final report in {0}", file));
                }
            }
        }

        static bool Ask(string what)
        {
            Console.WriteLine(what);
            Console.WriteLine("(Y for yes, any other string for no)");
            var response = Console.ReadLine();
            if (response == null)
            {
                return false;
            }
            if (response.ToUpper().Trim() == "Y")
            {
                return true;
            }
            return false;
        }
    }
}