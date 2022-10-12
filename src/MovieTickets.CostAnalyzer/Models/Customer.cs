namespace MovieTickets.CostAnalyzer.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public TicketType TicketType { get; set; }
        public double? TicketValue { get; set; }

    }
}
