using MovieTickets.CostAnalyzer.Controllers;
using MovieTickets.CostAnalyzer.Models;
using MovieTickets.CostAnalyzer.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace MovieTickets.CostAnalyzer.Tests.Unit
{
    public class TransactionsTest
    {
        private TransactionsService transactionsServices;
        [SetUp]
        public void Setup()
        {
            transactionsServices = new TransactionsService();
        }
        [Test]
        public void VerifyTicketTypePerCustomer()
        {
            TransactionsController controller = new TransactionsController();

            string jsonString = File.ReadAllText(@"Data\MockData.json");
            controller.SetTransaction(transactionsServices.GetTransactions(jsonString));

            controller.SetTransactionsTicketTypes();

            List<Transaction> transactions = controller.GetTransactions();
            string expectedData = File.ReadAllText(@"Data\ExpectedData.json");
            List<Transaction> expectedTransactions = transactionsServices.GetTransactions(expectedData);

            for (int i = 0; i < expectedTransactions.Count; i++)
            {
                Assert.AreEqual(expectedTransactions[i].TransactionId, transactions[i].TransactionId);

                for (int j = 0; j < expectedTransactions[i].Customers.Count; j++)
                {
                    Assert.AreEqual(expectedTransactions[i].Customers[j].TicketType.TicketId, transactions[i].Customers[j].TicketType.TicketId);
                    Assert.AreEqual(expectedTransactions[i].Customers[j].TicketType.TicketPrice, transactions[i].Customers[j].TicketType.TicketPrice);
                    Assert.AreEqual(expectedTransactions[i].Customers[j].TicketType.TicketName, transactions[i].Customers[j].TicketType.TicketName);
                    Assert.AreEqual(expectedTransactions[i].Customers[j].TicketType.TicketStartingAge, transactions[i].Customers[j].TicketType.TicketStartingAge);
                    Assert.AreEqual(expectedTransactions[i].Customers[j].TicketType.TicketFinishingAge, transactions[i].Customers[j].TicketType.TicketFinishingAge);
                }
            }
        }
        [Test]
        public void Verify30PercentDiscount()
        {
            TransactionsController controller = new TransactionsController();

            string jsonString = File.ReadAllText(@"Data\MockData30PercentDiscount.json");
            controller.SetTransaction(transactionsServices.GetTransactions(jsonString));

            controller.SetTransactionsTicketTypes();
            controller.SetTicketDiscount();

            List<Transaction> transactions = controller.GetTransactions();

            string expectedData = File.ReadAllText(@"Data\ExpectedData30PercentDiscount.json");
            List<Transaction> expectedTransactions = transactionsServices.GetTransactions(expectedData);

            for (int i = 0; i < expectedTransactions.Count; i++)
            {
                Assert.AreEqual(expectedTransactions[i].TransactionId, transactions[i].TransactionId);

                for (int j = 0; j < expectedTransactions[i].Customers.Count; j++)
                {
                    Assert.AreEqual(expectedTransactions[i].Customers[j].TicketValue, transactions[i].Customers[j].TicketValue);
                }
            }
        }
        [Test]
        public void Verify25PercentDiscount()
        {
            TransactionsController controller = new TransactionsController();

            string jsonString = File.ReadAllText(@"Data\MockData25PercentDiscount.json");
            controller.SetTransaction(transactionsServices.GetTransactions(jsonString));

            controller.SetTransactionsTicketTypes();
            controller.SetTicketDiscount();

            List<Transaction> transactions = controller.GetTransactions();

            string expectedData = File.ReadAllText(@"Data\ExpectedData25PercentDiscount.json");
            List<Transaction> expectedTransactions = transactionsServices.GetTransactions(expectedData);

            for (int i = 0; i < expectedTransactions.Count; i++)
            {
                Assert.AreEqual(expectedTransactions[i].TransactionId, transactions[i].TransactionId);

                for (int j = 0; j < expectedTransactions[i].Customers.Count; j++)
                {
                    Assert.AreEqual(expectedTransactions[i].Customers[j].TicketValue, transactions[i].Customers[j].TicketValue);
                }
            }
        }
        [Test]
        public void VerifyTicketsQuantity()
        {
            TransactionsController controller = new TransactionsController();

            string jsonString = File.ReadAllText(@"Data\MockData.json");
            controller.SetTransaction(transactionsServices.GetTransactions(jsonString));

            controller.SetTransactionsTicketTypes();

            List<Transaction> transactions = controller.GetTransactions();
            string expectedData = File.ReadAllText(@"Data\ExpectedData.json");
            List<Transaction> expectedTransactions = transactionsServices.GetTransactions(expectedData);
            for (int i = 0; i < expectedTransactions.Count; i++)
            {
                int qtdExpectedSenior = expectedTransactions[i].Customers.FindAll(x => x.TicketType.TicketId == 1).Count;
                int qtdExpectedAdult = expectedTransactions[i].Customers.FindAll(x => x.TicketType.TicketId == 2).Count;
                int qtdExpectedTeen = expectedTransactions[i].Customers.FindAll(x => x.TicketType.TicketId == 3).Count;
                int qtdExpectedChildren = expectedTransactions[i].Customers.FindAll(x => x.TicketType.TicketId == 4).Count;

                int qtdActualSenior = transactions[i].Customers.FindAll(x => x.TicketType.TicketId == 1).Count;
                int qtdActualAdult = transactions[i].Customers.FindAll(x => x.TicketType.TicketId == 2).Count;
                int qtdActualTeen = transactions[i].Customers.FindAll(x => x.TicketType.TicketId == 3).Count;
                int qtdActualChildren = transactions[i].Customers.FindAll(x => x.TicketType.TicketId == 4).Count;

                Assert.AreEqual(qtdExpectedSenior, qtdActualSenior);
                Assert.AreEqual(qtdExpectedAdult, qtdActualAdult);
                Assert.AreEqual(qtdExpectedTeen, qtdActualTeen);
                Assert.AreEqual(qtdExpectedChildren, qtdActualChildren);
            }
        }
        [Test]
        public void VerifyTotalTransactionPrice()
        {
            TransactionsController controller = new TransactionsController();

            string jsonString = File.ReadAllText(@"Data\MockDataTotalValue.json");
            controller.SetTransaction(transactionsServices.GetTransactions(jsonString));

            controller.SetTransactionsTicketTypes();
            controller.SetTicketDiscount();

            List<Transaction> transactions = controller.GetTransactions();

            string expectedData = File.ReadAllText(@"Data\ExpectedDataTotalValue.json");
            List<Transaction> expectedTransactions = transactionsServices.GetTransactions(expectedData);

            for (int i = 0; i < expectedTransactions.Count; i++)
            {
                double totalExpectedValue = 0;
                double totalActualValue = controller.GetTransactionTotalValue(transactions[i]);
                for (int j = 0; j < expectedTransactions[i].Customers.Count; j++)
                {
                    totalExpectedValue += expectedTransactions[i].Customers[j].TicketValue ?? 00.00;
                }
                Assert.AreEqual(totalExpectedValue, totalActualValue);
            }
        }
        [Test]
        public void VerifyTotalValuePerTicketType()
        {
            TransactionsController controller = new TransactionsController();

            string jsonString = File.ReadAllText(@"Data\MockDataTotalValuePerTicketType.json");
            controller.SetTransaction(transactionsServices.GetTransactions(jsonString));

            controller.SetTransactionsTicketTypes();
            controller.SetTicketDiscount();

            List<Transaction> transactions = controller.GetTransactions();

            string expectedData = File.ReadAllText(@"Data\ExpectedDataTotalValuePerTicketType.json");
            List<Transaction> expectedTransactions = transactionsServices.GetTransactions(expectedData);

            TicketTypeService ticketTypeService = new TicketTypeService();
            List<TicketType> ticketTypes = ticketTypeService.GetTicketTypes();
            for (int i = 0; i < expectedTransactions.Count; i++)
            {
                foreach (var item in ticketTypes)
                {
                    double totalExpectedValue = 0, totalActualValue = 0;
                    var teste = expectedTransactions[i].Customers.FindAll(x => x.TicketType.TicketId == item.TicketId);
                    foreach (var customerExpected in teste )
                    {
                        totalExpectedValue += customerExpected.TicketValue ?? 00.00;
                    }

                    totalActualValue = controller.GetTotalValueByTicketType(transactions[i], item.TicketId);
                    Assert.AreEqual(totalExpectedValue, totalActualValue);
                }

            }
        }
    }
}