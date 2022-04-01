using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalculator.Models
{
    public class Loan
    {
        public decimal LoanAmount { get; set; }
        public int LoanTerm { get; set; }

        public bool PMIRequired { get; set; }
        public decimal PMIRate { get; set; }
        public decimal PayDown { get; set; }
        public decimal LoanInterest { get; set; }
        public decimal Payment { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalCost { get; set; }
        public List<LoanPayment> Payments { get; set; } = new List<LoanPayment>();


    }
}
