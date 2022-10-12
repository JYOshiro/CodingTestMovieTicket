using MovieTickets.CostAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MovieTickets.CostAnalyzer.Services
{
    public class TransactionsService
    {
        public TransactionsService()
        {
        }
        public List<Transaction> GetTransactions()
        {
            try
            {
                string jsonString = File.ReadAllText("transactions.json");
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                return JsonSerializer.Deserialize<List<Transaction>>(jsonString, options)!;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Transaction> GetTransactions(string jsonString)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                return JsonSerializer.Deserialize<List<Transaction>>(jsonString, options)!;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
