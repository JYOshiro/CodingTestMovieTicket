using MovieTickets.CostAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace MovieTickets.CostAnalyzer.Services
{
    public class TicketDiscountsService
    {
        public TicketDiscountsService()
        {

        }
        public double? DiscontPercentageInTicket(TicketType a, TicketDiscounts ticketDiscounts)
        {
            double percentage = ticketDiscounts.DiscountPercentage / 100.0;

            return a.TicketPrice - (percentage * a.TicketPrice);
        }

        public IEnumerable<TicketDiscounts> GetTicketDiscounts()
        {
            string jsonString = File.ReadAllText(@"Data\TicketDiscounts.json");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            List<TicketDiscounts> teste = JsonSerializer.Deserialize<List<TicketDiscounts>>(jsonString, options)!;
            return teste;
        }

        public void ShowRules()
        {
            try
            {
                string jsonString = File.ReadAllText("TicketRules.json");
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                var ticket_rules= JsonSerializer.Deserialize<List<TicketDiscounts>>(jsonString, options)!;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
