namespace MovieTickets.CostAnalyzer.Models
{
    public class TicketsTypeQuantities
    {
        public int TicketTypeId { get; set; }
        public int Quantity { get; set; }
        public TicketsTypeQuantities(int ticketTypeId, int quantity)
        {
            TicketTypeId = ticketTypeId;
            Quantity = quantity;
        }   
    }
}
