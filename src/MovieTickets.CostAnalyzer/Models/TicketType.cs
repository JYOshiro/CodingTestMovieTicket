namespace MovieTickets.CostAnalyzer.Models
{
    public class TicketType
    {
        public int TicketId { get; set; }
        public string TicketName{ get; set; }
        public double? TicketPrice { get; set; }
        public int TicketStartingAge { get; set; }
        public int TicketFinishingAge { get; set; }
    }
}
