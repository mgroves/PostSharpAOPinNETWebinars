using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace CallThisInstead
{
    public class InvoiceRepository
    {
        const string connectionString = @"Data Source=C:\Dropbox\talks\postsharp webinars\2_CallThisInstead\sample.s3db";

        public void AddItem(long invoiceId, Item item)
        {
            var sql = "INSERT INTO Item (InvoiceId, ItemDesc, ItemPrice, ItemQuantity) VALUES (@InvoiceId, @ItemDesc, @ItemPrice, @ItemQuantity);";
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var sqlCommand = new SQLiteCommand(sql, conn);
                sqlCommand.Parameters.Add(new SQLiteParameter("InvoiceId", invoiceId));
                sqlCommand.Parameters.Add(new SQLiteParameter("ItemDesc", item.ItemDesc));
                sqlCommand.Parameters.Add(new SQLiteParameter("ItemPrice", item.ItemPrice));
                sqlCommand.Parameters.Add(new SQLiteParameter("ItemQuantity", item.ItemQuantity));
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public long AddInvoice(Invoice invoice)
        {
            var sql = "INSERT INTO Invoice (BillingAddress, InvoiceDate) VALUES (@BillingAddress, @InvoiceDate); SELECT last_insert_rowid() AS newId;";
            long invoiceId;
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var sqlCommand = new SQLiteCommand(sql, conn);
                sqlCommand.Parameters.Add(new SQLiteParameter("BillingAddress", invoice.BillingAddress));
                sqlCommand.Parameters.Add(new SQLiteParameter("InvoiceDate", invoice.InvoiceDate));
                invoiceId = (long)sqlCommand.ExecuteScalar();
                conn.Close();
            }
            return invoiceId;
        }

        public List<Invoice> GetAllInvoices()
        {
            var invoices = new List<Invoice>();
            var sql = "SELECT * FROM Invoice";
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var sqlCommand = new SQLiteCommand(sql, conn);
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var invoice = new Invoice();
                    invoice.Id = (long)reader["Id"];
                    invoice.BillingAddress = (string)reader["BillingAddress"];
                    invoice.InvoiceDate = (DateTime)reader["InvoiceDate"];
                    invoice.Items = GetItems(invoice.Id);
                    invoices.Add(invoice);
                }
                conn.Close();
            }

            return invoices;
        }
    
        private List<Item> GetItems(long id)
        {
            var items = new List<Item>();
            var sql = "SELECT * FROM Item WHERE InvoiceId = @InvoiceId";
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var sqlCommand = new SQLiteCommand(sql, conn);
                sqlCommand.Parameters.Add(new SQLiteParameter("InvoiceId", id));
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var item = new Item
                    {
                        Id = (long)reader["Id"],
                        ItemDesc = (string)reader["ItemDesc"],
                        ItemPrice = (decimal)reader["ItemPrice"],
                        ItemQuantity = (long)reader["ItemQuantity"]
                    };
                    items.Add(item);
                }
                conn.Close();
            }
            return items;
        }
    }
}