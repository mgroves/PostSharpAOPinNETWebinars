using System;
using System.Collections.Generic;

namespace CallThisInstead
{
    public class Invoice
    {
        public List<Item> Items { get; set; }

        public long Id { get; set; }

        public string BillingAddress { get; set; }

        public DateTime InvoiceDate { get; set; }
    }
}