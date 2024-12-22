using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Presentation.Repositories;
using Frontend.Models;

namespace Presentation.Controllers
{
    public class TransactionViewController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionViewController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // search

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                // If no search term is provided, redirect to Index or return all transactions.
                return RedirectToAction(nameof(Index));
            }

            var transactions = await _transactionRepository.SearchTransactionsAsync(searchTerm);
            return View("Index", transactions); // Reuse the same view for displaying the search results.
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            return View(transactions);
        }
          
    }
}
