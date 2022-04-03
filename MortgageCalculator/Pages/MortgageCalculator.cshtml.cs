using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MortgageCalculator.Helpers;
using MortgageCalculator.Models;

namespace MortgageCalculator.Pages
{
    public class MortgageModel : PageModel
    {
        private readonly MortgageHelper _mortgageHelper;

        public MortgageModel(MortgageHelper loanhelper)
        {
            _mortgageHelper = loanhelper;
        }

        [BindProperty(SupportsGet = true)]
        public Loan Loan { get; set; }
       

        public void OnGet()
        {
            Loan.LoanTerm = 60;
            Loan.LoanInterest = 3.5m;
            Loan.LoanAmount = 1000m;
            Loan.PayDown = 0m;
            Loan.Payment = 0m;
            Loan.TotalInterest = 0m;
            Loan.TotalCost = 0m;
            Loan.PMIRate = 0m;
        }

        

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            _mortgageHelper.GetPayments(Loan);

            return Page();
        }
    }
}
