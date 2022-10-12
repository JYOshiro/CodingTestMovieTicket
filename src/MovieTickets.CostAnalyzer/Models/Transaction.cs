using System.Collections.Generic;

namespace MovieTickets.CostAnalyzer.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public List<Customer> Customers { get; set; }
        
    }
}
