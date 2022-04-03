using MortgageCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalculator.Helpers
{
    public class MortgageHelper
    {
        //TODO PMI calc - optional?
        #region Get Payments
        public Loan GetPayments(Loan loan)
        {
            loan.LoanAmount = loan.TotalMortgageCost - loan.PayDown;


            decimal avoidPMI = loan.TotalMortgageCost * 0.2m;
            loan.LoanAmount -= loan.PayDown;

            if (loan.PayDown < avoidPMI)
            {
                decimal PMIAmount = loan.PMIRate * loan.LoanAmount;
                loan.PMIRequired = true;
                loan.LoanAmount += PMIAmount;
            }
            else
            {
               
                loan.PMIRequired = false;
            }


            // calc monthly payments
            loan.Payment = CalculateMonthlyPayment(loan.LoanAmount, loan.LoanInterest, loan.LoanTerm);


            var balance = loan.LoanAmount;
            var totalInterest = 0m;
            var monthlyInterest = 0m;
            var monthlyPrincipal = 0m;
            var monthlyRate = CalculateMonthlyRate(loan.LoanInterest);

            for (int month = 1; month <= loan.LoanTerm; month++)
            {


                monthlyInterest = CalculateMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new();

                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;

                loan.Payments.Add(loanPayment);

            }
            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.LoanAmount + totalInterest;
            return loan;
        }
        #endregion

     

        #region Monthly Payment Calculation
        private decimal CalculateMonthlyPayment(decimal loanAmount, decimal rate, int loanTerm)
        {
            var monthlyRate = CalculateMonthlyRate(rate);
            var rateD = Convert.ToDouble(monthlyRate);
            var amountD = Convert.ToDouble(loanAmount);

            var paymentD = (amountD * rateD) / (1 - Math.Pow(1 + rateD, -loanTerm));


            return Convert.ToDecimal(paymentD);
        } 
        #endregion

        #region rate
        private decimal CalculateMonthlyRate(decimal rate)
        {
            return rate / 1200;
        }
        #endregion

        #region monthly Interest
        private decimal CalculateMonthlyInterest(decimal balance, decimal monthlyRate)
        {
            return balance * monthlyRate;
        } 
        #endregion
    }
}
