using MovieTickets.CostAnalyzer.Models;
using MovieTickets.CostAnalyzer.Services;
using System;
using System.Collections.Generic;

namespace MovieTickets.CostAnalyzer.Controllers
{
    public class TransactionsController
    {
        List<Transaction> _transactions;
        public TransactionsController(List<Transaction> transaction)
        {
            _transactions = transaction;
        }
        public TransactionsController()
        {
            _transactions = new List<Transaction>();
        }
        public void SetTransaction()
        {
            TransactionsService service = new TransactionsService();

            _transactions = service.GetTransactions();
        }
        public void SetTransaction(List<Transaction> transaction)
        {
            _transactions = transaction;
        }
        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }
        public List<TicketsTypeQuantities> GetQtdTicketsTypeByTransaction(Transaction transaction)
        {
            var listqtd = new List<TicketsTypeQuantities>();
            foreach (var t in transaction.Customers)
            {
                var qtdtick = listqtd.Find(x => x.TicketTypeId == t.TicketType.TicketId);
                if (qtdtick == null)
                {
                    listqtd.Add(new TicketsTypeQuantities(t.TicketType.TicketId, 1));
                }
                else
                {
                    qtdtick.Quantity++;
                }
            }
            return listqtd;
        }
        public void SetTransactionsTicketTypes()
        {
            var ticketType = new TicketTypeService();

            foreach (var transaction in _transactions)
            {
                foreach (var customer in transaction.Customers)
                {
                    customer.TicketType = ticketType.GetTicketTypeByAge(customer.Age);
                    customer.TicketValue = customer.TicketType.TicketPrice;
                }
            }
        }
        public void SetTicketDiscount()
        {
            TransactionsService transactionService = new TransactionsService();
            TicketTypeService ticketTypeService = new TicketTypeService();
            TicketDiscountsService discounts = new TicketDiscountsService();

            List<TicketDiscounts> teste = (List<TicketDiscounts>)discounts.GetTicketDiscounts();

            foreach (var transaction in _transactions)
            {
                foreach (var discount in teste)
                {
                    var listqtdticket = GetQtdTicketsTypeByTransaction(transaction).Find(x => x.TicketTypeId == discount.TicketA);
                    foreach (var customer in transaction.Customers)
                    {
                        if (listqtdticket != null && customer.TicketType.TicketId == discount.TicketA && listqtdticket.Quantity >= discount.Qtd)
                        {
                            customer.TicketValue = discounts.DiscontPercentageInTicket(ticketTypeService.GetTicketTypeById(discount.TicketB), discount);
                        }
                    }
                }
            }
        }
        public Double GetTransactionTotalValue(Transaction transaction)
        {
            double total = 0;
            foreach (var customer in transaction.Customers)
            {
                total += customer.TicketValue ?? 0.0;
            }
            return total;
        }
        public double GetTotalValueByTicketType(Transaction transaction, int id)
        {
            double total = 0;
            foreach (var customer in transaction.Customers.FindAll(x => id == x.TicketType.TicketId))
            {
                total += customer.TicketValue ?? 0.0;
            }
            return total;
        }
        public string GetTicketTypeName(int TicketTypeId)
        {
            switch (TicketTypeId)
            {
                case 1:
                    return "SENIOR";
                case 2:
                    return "ADULT";
                case 3:
                    return "TEEN";
                case 4:
                    return "CHILDREN";
                default:
                    return String.Empty;
            }
        }
    }
}
