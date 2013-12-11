using System;
using System.Data.SQLite;
using System.Linq;

namespace CallThisInstead
{
    class Program
    {
        static void Main(string[] args)
        {
            var svc = new InvoiceService();
            //svc.AddInvoice();

            //try
            //{
            //    svc.AddInvoiceWithProblem();
            //}
            //catch
            //{
            //    Console.WriteLine("Error creating invoice");
            //}

            svc.AddInvoiceWithProblem();

            svc.ListAllInvoices();
        }
    }

    public class InvoiceService
    {
        [TransactionAspect]
        public void AddInvoiceWithProblem()
        {
            var repo = new InvoiceRepository();
            repo.AddInvoice(new Invoice { BillingAddress = "555 Elm St.", InvoiceDate = DateTime.Now });
            throw new SQLiteException("SQL Timeout, constraint violation, etc");
        }

        public void AddInvoice()
        {
            var repo = new InvoiceRepository();
            var invoiceId = repo.AddInvoice(new Invoice { BillingAddress = "555 Elm St.", InvoiceDate = DateTime.Now});
            repo.AddItem(invoiceId, new Item { ItemDesc = "Lettuce", ItemQuantity = 5, ItemPrice = 0.49M });
            repo.AddItem(invoiceId, new Item { ItemDesc = "Tomato", ItemQuantity = 10, ItemPrice = 0.19M });
            repo.AddItem(invoiceId, new Item { ItemDesc = "Pickle", ItemQuantity = 3, ItemPrice = 0.09M });
        }

        public void ListAllInvoices()
        {
            var repo = new InvoiceRepository();
            var invoices = repo.GetAllInvoices();
            if (!invoices.Any())
            {
                Console.WriteLine("No invoices entered yet.");
                return;
            }
            foreach (var invoice in invoices)
            {
                Console.WriteLine("Invoice #: {0}", invoice.Id);
                Console.WriteLine("Invoice Date: {0}", invoice.InvoiceDate);
                Console.WriteLine("Billing Address: {0}", invoice.BillingAddress);
                int i = 0;
                foreach (var item in invoice.Items)
                {
                    Console.WriteLine("{0}: {1}\t\t{2}\t{3}", i, item.ItemDesc, item.ItemPrice, item.ItemQuantity);
                    i++;
                }
                Console.WriteLine("---------------------------");
            }
        }
    }
}
