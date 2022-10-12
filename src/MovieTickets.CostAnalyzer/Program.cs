using MovieTickets.CostAnalyzer.Controllers;
using MovieTickets.CostAnalyzer.Models;
using System;
using System.Collections.Generic;

namespace MovieTickets.CostAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TransactionsController transactions= new TransactionsController();
                transactions.SetTransaction();
                transactions.SetTransactionsTicketTypes();
                transactions.SetTicketDiscount();
                ShowTransactions(transactions.GetTransactions());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static void ShowTransactions(List<Transaction> transactions)
        {
            TransactionsController transactionsController= new TransactionsController();
            foreach (Transaction transa in transactions)
            {
                Console.WriteLine();
                Console.WriteLine($"## Transaction {transa.TransactionId} ##");
                foreach (var t in transactionsController.GetQtdTicketsTypeByTransaction(transa))
                {
                    Console.WriteLine($"{transactionsController.GetTicketTypeName(t.TicketTypeId)} ticket x {t.Quantity}: ${transactionsController.GetTotalValueByTicketType(transa, t.TicketTypeId)}");
                }
                Console.WriteLine();
                Console.WriteLine($"Projected total cost: ${transactionsController.GetTransactionTotalValue(transa)}");
                Console.WriteLine();
            }
        }
    }
}