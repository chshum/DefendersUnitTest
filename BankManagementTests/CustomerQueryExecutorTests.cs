// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.BankQueries;
using BankManagement.Models;
using BankManagement.Storage;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestStack.BDDfy;

namespace BankManagementTests
{
    [TestClass]
    public class CustomerQueryExecutorTests
    {
        private Context _context;

        [TestInitialize]
        public void Setup()
        {
            var currencyConverterMock = new Mock<ICurrencyConverter>();
            currencyConverterMock
                .Setup(x => x.GetCurrencyValue(It.IsAny<Currency>(), It.IsAny<Currency>()))
                .Returns<Currency, Currency>((from, to) => Task.FromResult((double) 1));

            var customers = new List<BankCustomer>();
            var bank = new Bank
                {
                    BankName = "TestBank",
                    InternationalBankCode = 101010,
                    LocalBankCode = 10
                };
            
            var bankDb = new BankDb
            {
                BankCustomers = customers,
                Bank = bank
            };

            _context = new Context
            {
                QueryExecutor = new CustomerQueryExecutor(currencyConverterMock.Object, bankDb),
                BankDb = bankDb
            };
        }

        [TestMethod]
        [Ignore]
        public void Bad_test_example()
        {
            this.Given(_ => A_premium_customer_in_real_db()) //bad! we dont want IO dependenceis in our code, it should run isolated and in any envirnoment. 
                .And(_ => A_customer_in_debt())
                .When(_ => Getting_premium_customers())
                .And(_ => Converting_their_balance_to_NIS_from_real_data()) /// bad! we dont want to have A. external dependenceis and B. long running tests with REST calls
                .Then(_ => The_number_of_customers_should_be(1))
                .And(_ => The_balance_should_be(200)) // Bad! wWe are testing 2 different things here, nubmer of customers and balance (this is not a hard rule, should be based on dev judgement)
                .BDDfy();
        }


        [TestMethod]
        public void Get_premium_customers_should_Ignore_customers_in_debt()
        {
            this.Given(_ => A_premium_customer())
                .And(_ => A_customer_in_debt())
                .When(_ => Getting_premium_customers())
                .Then(_ => The_number_of_customers_should_be(1))
                .BDDfy();
        }

        [TestMethod]
        public void Get_premium_customers_should_Ignore_ex_premium_customers()
        {
            this.Given(_ => A_premium_customer())
                .When(_ => Getting_premium_customers())
                .Then(_ => The_number_of_customers_should_be(1))
                .When(_ => Account_is_emptied())
                .And(_ => Getting_premium_customers())
                .Then(_ => The_number_of_customers_should_be(0))
                .BDDfy();
        }

        [Given]
        private void A_premium_customer()
        {
            CreateCustomer(20001);
        }

        [Given]
        private void A_customer_in_debt()
        {
            CreateCustomer(-400);
        }

        [When]
        private async Task Getting_premium_customers()
        {
            _context.PriorityCustomers = await _context.QueryExecutor.GetPriorityCustomers();
        }

        [When]
        private void Account_is_emptied()
        {
            foreach (var account in _context.Customers.SelectMany(x => x.BankAccounts))
            {
                account.Balance = 0;
            }
        }

        [Then]
        private void The_number_of_customers_should_be(int numberOfCustomers)
        {
            _context.PriorityCustomers.Count().Should().Be(numberOfCustomers);
        }

        private void CreateCustomer(double balance)
        {
            var customer = new BankCustomer
            {
                CustomerId = Guid.NewGuid().ToString(),
                BankAccounts = new List<BankAccount>
                {
                    new BankAccount
                    {
                        Balance = balance,
                        Currency = Currency.NIS
                    }
                }
            };

            _context.Customers.Add(customer);
        }


        private void The_balance_should_be(int v)
        {
            throw new NotImplementedException();
        }

        private void Converting_their_balance_to_NIS_from_real_data()
        {
            throw new NotImplementedException();
        }

        private void A_premium_customer_in_real_db()
        {
            throw new NotImplementedException();
        }

        private class Context
        {
            public Bank Bank => BankDb.Bank;

            public List<BankCustomer> Customers => BankDb.BankCustomers;
            public ICustomerQueryExecutor QueryExecutor { get; internal set; }
            public BankDb BankDb { get; internal set; }
            public IEnumerable<BankCustomer> PriorityCustomers { get; internal set; }
        }
    }
}
