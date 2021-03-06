﻿using System;
using System.Linq;
using System.Text;
using System.Xml;
using Supermarkets.Data;

namespace Supermarkets.Task3.XML
{
    public class GenerateXMLFile
    {
        public static void GenerateAggregateReport(SupermarketsEntities sqlserver, string fileName)
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            using (XmlTextWriter writer = new XmlTextWriter(fileName, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                WriteVendorSales(writer, sqlserver);
            }
        }

        static void Main()
        {
            using (var context = new SupermarketsEntities())
            {
                GenerateAggregateReport(context, "../../aggregated-sales-report.xml");
            }
        }

        private static void WriteVendorSales(XmlTextWriter writer, SupermarketsEntities sqlserver)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("sales");

            var query = from sale in sqlserver.Sales
                        group sale by new { VendorName = sale.Product.Vendor.Name, DateSold = sale.DateSold }
                        into g select new
                        {
                            VendorName = g.Key.VendorName,
                            DateSold = g.Key.DateSold,
                            TotalSum = g.Sum(y => y.Quantity * y.UnitPrice)
                        };

            string currVendor = string.Empty;

            foreach (var item in query)
            {
                if (item.VendorName != currVendor)
                {
                    if (currVendor != String.Empty)
                    {
                        writer.WriteEndElement();
                    }

                    writer.WriteStartElement("sale");
                    writer.WriteAttributeString("vendor", item.VendorName);
                    currVendor = item.VendorName;
                }

                writer.WriteStartElement("summary");
                writer.WriteAttributeString("date", item.DateSold.ToString());
                writer.WriteAttributeString("total-sum", item.TotalSum.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}