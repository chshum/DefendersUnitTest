// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using BankManagement.Transactions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.BDDfy;

namespace BankManagementTests
{
    [TestClass]
    public class TransactionTests

    {
        private Context _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new Context
            {
            };
        }

        [TestMethod]
        public void Insert_transaction_into_account()
        {
            double toInsert = 100;
            this.Given(_ => An_insert_transaction())
                .And(_ => A_bank_account())
                .And(_ => A_balance_of(1000))
                .When(_ => Transacting(toInsert))
                .Then(_ => The_balance_should_increase_by(toInsert))
                .BDDfy();
        }

        [TestMethod]
        public void Withdraw_transaction_from_account()
        {
            double toWithdraw = 100;
            this.Given(_ => A_withdraw_transaction())
                .And(_ => A_bank_account())
                .And(_ => A_balance_of(200))
                .When(_ => Transacting(toWithdraw))
                .Then(_ => The_balance_should_decrease_by(toWithdraw))
                .BDDfy();
        }

        [Given]
        private void An_insert_transaction()
        {
            _context.Transaction = new InsertBankTransaction();
        }


        [Given]
        private void A_withdraw_transaction()
        {
            _context.Transaction = new WithdrawBankTransaction();
        }

        [Given]
        private void A_balance_of(double balance)
        {
            _context.BankAccount.Balance = balance;
            _context.originalBalance = balance;
        }

        [Given]
        private void A_bank_account()
        {
            _context.BankAccount = new BankAccount
            {
                Balance = 0,
                Currency = Currency.NIS
            };
        }

        [When]

        private void Transacting(double toInsert)
        {
            _context.Transaction.Transact(_context.BankAccount, toInsert);
        }

        [Then]
        private void The_balance_should_increase_by(double balance)
        {
            _context.BankAccount.Balance.Should().Be(_context.originalBalance + balance);
        }

        [Then]
        private void The_balance_should_decrease_by(double balance)
        {
            _context.BankAccount.Balance.Should().Be(_context.originalBalance - balance);
        }

        private class Context
        {
            public IBankTransaction Transaction { get; set; }

            public double originalBalance { get; set; }

            public BankAccount BankAccount { get; set; }
    }
}
}
