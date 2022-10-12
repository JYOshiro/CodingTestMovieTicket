using MovieTickets.CostAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MovieTickets.CostAnalyzer.Services
{
    public class TicketTypeService
    {
        List<TicketType> _ticketTypes;
        public TicketTypeService()
        {
            if (_ticketTypes == null)
            {
                SetTicketTypes();
            }
        }
        public TicketTypeService(string jsonString)
        {
            SetTicketTypes(jsonString);
        }
        public TicketType GetTicketTypeById(int id)
        {
            return _ticketTypes.Find(x => x.TicketId == id);
        }
        public List<TicketType> GetTicketTypes()
        {
            return _ticketTypes;
        }
        private void SetTicketTypes()
        {
            try
            {
                string jsonString = File.ReadAllText(@"Data\TicketType.json");
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                _ticketTypes = JsonSerializer.Deserialize<List<TicketType>>(jsonString, options)!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        private void SetTicketTypes(string jsonString)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                _ticketTypes = JsonSerializer.Deserialize<List<TicketType>>(jsonString, options)!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public TicketType GetTicketTypeByAge(int age)
        {
            return _ticketTypes.Find(x => age >= x.TicketStartingAge && age < x.TicketFinishingAge);

        }
    }
}
