// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
		public void Linq1()
		{
			int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

			var lowNums =
				from num in numbers
				where num < 5
				select num;

			Console.WriteLine("Numbers < 5:");
			foreach (var x in lowNums)
			{
				Console.WriteLine(x);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("This sample returns all presented in market products")]

		public void Linq2()
		{
			var products =
				from p in dataSource.Products
				where p.UnitsInStock > 0
				select p;

			foreach (var p in products)
			{
				ObjectDumper.Write(p);
			}
		}

        [Category("Homework tasks")]
        [Title("Task 1")]
        [Description("This sample returns all clients with summary turnover greater than some 'x'")]
        public void Linq3()
        {
            var clients = (from c in dataSource.Customers
                           from o in c.Orders
                           where o.Total > 10000 
                           select new {Client = c.CompanyName, o.Total})
                .OrderByDescending(arg => arg.Total).AsParallel();

            foreach (var client in clients)
            {
                ObjectDumper.Write(string.Format($"{client.Client}\t{client.Total}"));

            }

        }

        [Category("Homework tasks")]
        [Title("Task 2")]
        [Description("This sample returns all clients with same cities and countries")]
        public void Linq4()
        {
            var data = (from c in dataSource.Customers
                      from s in dataSource.Suppliers
                      where c.City == s.City && c.Country == s.Country
                      select new{c.CompanyName, s.SupplierName, c.City, c.Country}).OrderBy(arg => arg.SupplierName);

            foreach (var r in data)
            {
                ObjectDumper.Write($"{r.CompanyName,25} {r.SupplierName,30} {r.City,11}/{r.Country}");
            }

        }

        [Category("Homework tasks")]
        [Title("Task 3")]
        [Description("This sample returns clients whose order count grater than some 'X' value")]
        public void Linq5()
        {
            var data = 
                from c in dataSource.Customers
                orderby c.CompanyName, c.Orders.Skip(c.Orders.Length - 1)
                where c.Orders.Length > 10
                        select new { c.CompanyName, Count = c.Orders.Count()};
            
            foreach (var r in data)
            {
                ObjectDumper.Write($"{r.CompanyName,35} {r.Count,15}");
            }
        }

        [Category("Homework tasks")]
        [Title("Task 4")]
        [Description("This sample returns all clients with same cities and countries")]
        public void Linq6()
        {
            var data = 
                from c in dataSource.Customers
                from d in c.Orders.Skip(c.Orders.Length - 1)
                select new { c.CompanyName, d.OrderDate };

            foreach (var r in data)
            {
                ObjectDumper.Write($"{r.CompanyName,35} {r.OrderDate.Month,15}/{r.OrderDate.Year}");
                ObjectDumper.Write($"\n{new string('=', 56)}\n");
            }
        }

        [Category("Homework tasks")]
        [Title("Task 5")]
        [Description("This sample returns all clients with same cities and countries sorted by" +
                     "year, month, turnover and clients name.")]
        public void Linq7()
        {
            var data =
                from c in dataSource.Customers
                from d in c.Orders.Skip(c.Orders.Length - 1)
                orderby d.OrderDate.Year, d.OrderDate.Month, d.Total, c.CompanyName
                select new { c.CompanyName, d.OrderDate, d.Total };

            foreach (var r in data)
            {
                ObjectDumper.Write($"{r.CompanyName,35} |{r.Total, 10}| {r.OrderDate.Month,7}/{r.OrderDate.Year}|");
                ObjectDumper.Write($"\n{new string('=', 76)}\n");
            }
        }

        [Category("Homework tasks")]
        [Title("Task 6")]
        [Description("This sample returns all clients with incorrect postal code and phone number")]
        public void Linq8()
        {
            var data =
                (from c in dataSource.Customers
            where c.Region is null 
                  && !c.Phone.Contains("(") 
                  && Regex.Matches(c.PostalCode, @"[a-zA-Z]").Count > 0
                 select new { c.CompanyName, c.Phone, c.PostalCode, c.Region}).ToArray();

            foreach (var r in data)
            {
                ObjectDumper.Write($"{r.CompanyName,35} {r.Phone,18} {r.PostalCode,10} {r.Region,15}");
                ObjectDumper.Write($"\n{new string('-', 86)}\n");
            }
        }

        [Category("Homework tasks")]
        [Title("Task 7")]
        [Description("This sample returns grouped products by categories")]
        public void Linq9()
        {
            var data =
                from c in dataSource.Products
                    where c.UnitsInStock > 0
                          orderby c.UnitPrice
                    select new { c.ProductName, c.Category, c.UnitsInStock, c.UnitPrice};

            foreach (var r in data)
            {
                ObjectDumper.Write($"{r.ProductName,35} {r.Category, 20} {r.UnitsInStock,5}{r.UnitPrice,10}");
                ObjectDumper.Write($"\n{new string('-', 70)}\n");
            }
        }

        [Category("Homework tasks")]
        [Title("Task 7.1")]
        [Description("This sample returns grouped products by categories")]
        public void Linq10()
        {
            var data =
                from c in dataSource.Products
                    where c.UnitsInStock > 0
                    orderby c.UnitPrice
                    group c by c.ProductName into g
                select new
                    {
                        ItemPrice = from n in g select n.UnitPrice,
                        ProductName = g
                    };

            foreach (var r in data)
            {
                ObjectDumper.Write($"{r.ProductName.Key,35}      InStock {string.Join("", r.ItemPrice),10}");
                ObjectDumper.Write($"\n{new string('-', 70)}\n");
            }
        }

        [Category("Homework tasks")]
        [Title("Task 8")]
        [Description("This sample returns grouped products by cheap, middle and expensive prices")]
        public void Linq11()
        {
            var data =
                from c in dataSource.Products
                let cheap = c.UnitPrice < 20
                let middle = c.UnitPrice > 20 && c.UnitPrice <=80

                select new{c.ProductName, c.UnitPrice, cheap, middle};

            foreach (var r in data)
            {
                if (r.cheap is true)
                    ObjectDumper.Write($"{r.ProductName,35}{r.UnitPrice,10}     Cheap");

                else if (r.middle is true)
                    ObjectDumper.Write($"{r.ProductName,35}{r.UnitPrice,10}     Middle");
                else
                    ObjectDumper.Write($"{r.ProductName,35}{r.UnitPrice,10}     Expensive");

                ObjectDumper.Write($"\n{new string('-', 70)}\n");
            }
        }
        
        [Category("Homework tasks")]
        [Title("Task 9")]
        [Description("This sample returns average city's turnover etc., as in DQL tasks.docx")]
        public void Linq12()
        {
            var data =
                  (from d in dataSource.Customers
                   from o in d.Orders.Skip(d.Orders.Length - 1)
                   let count = d.Orders.Length
                   let zeroOrder = o.Total == 0
                   let tAvg = d.Orders.Select(order => order.Total).Average()
                   let oAvg = d.Orders.Select(_ => count).Average()
                   orderby oAvg descending , tAvg descending 
                select new
                {
                    d.City,
                    d.CompanyName,
                    TotalAvg = tAvg,
                    OrdersAvg = oAvg,
                    zeroOrder
                }).ToArray();
            

            foreach (var item in data)
            {
                if (!item.zeroOrder)
                ObjectDumper.Write(
                    $"{item.City,15}{item.CompanyName,40}" +
                    $"{Math.Round(item.TotalAvg, 4),10}" +
                    $"{item.OrdersAvg,6}");

                ObjectDumper.Write($"\n{new string('-', 75)}\n");
            }
        }
    }
}
