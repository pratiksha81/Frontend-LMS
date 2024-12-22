using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Presentation.Repositories;
using Frontend.Models;
using Frontend.Repositories;

namespace Presentation.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IBookRepository _bookRepository;

        public TransactionController(ITransactionRepository transactionRepository, IStudentRepository studentRepository, IBookRepository bookRepository)
        {
            _transactionRepository = transactionRepository;
            _studentRepository = studentRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            var students = await _studentRepository.GetAllStudentsAsync();
            var books = await _bookRepository.GetAllBooksAsync();

            var studentData = students.Select(s => new { s.StudentId, s.Name }).ToList();
            var bookData = books.Select(b => new { b.BookId, b.Title }).ToList();

            HttpContext.Session.SetObjectAsJson("StudentData", studentData);
            HttpContext.Session.SetObjectAsJson("BookData", bookData);

            ///Retrieve username from session
            //ViewBag.Username = HttpContext.Session.GetString("Username");

            return View(transactions);
        }





        // Add a transaction (submit form)
        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionDTO transaction)
        {
            if (ModelState.IsValid)
            {
                var success = await _transactionRepository.AddTransactionAsync(transaction);
                if (success)
                    return RedirectToAction("Index", "TransactionView");
            }

            return View(transaction);
        }

        // Edit a transaction (renders edit view)
        [HttpGet]
        public async Task<IActionResult> EditTransaction(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();

             return RedirectToAction("Index", "TransactionView"); ;
        }

        // Edit a transaction (submit changes)
        [HttpPost]
        public async Task<IActionResult> EditTransaction(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var success = await _transactionRepository.UpdateTransactionAsync(transaction);
                if (success)
                    return RedirectToAction("Index");
            }

            return View(transaction);
        }
        [HttpGet]
     
        // Delete a transaction
        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var success = await _transactionRepository.DeleteTransactionAsync(id);
            return RedirectToAction("Index");
        }
    }
}
