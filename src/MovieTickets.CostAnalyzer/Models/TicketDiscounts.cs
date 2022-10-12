namespace MovieTickets.CostAnalyzer.Models
{
    public class TicketDiscounts
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Rule { get; set; }
        public int DiscountPercentage { get; set; }
        public int TicketA { get; set; }
        public int TicketB { get; set; }
        public int Qtd { get; set; }
    }
}
