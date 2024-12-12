﻿using Frontend.Models;
using Frontend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> Indexx(int BookId, string Title, int AuthorId, string Genre, string ISBN, int Quantity)
        {
            var book = new Book { BookId = BookId, Title = Title, AuthorId = AuthorId, Genre = Genre, ISBN = ISBN, Quantity = Quantity };
            ViewBag.IsEdit = true;
            ViewBag.Book = book;

            var books = await _bookRepository.GetAllBooksAsync();
            return View("Index", books);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                var success = await _bookRepository.AddBookAsync(book);
                if (success)
                    return RedirectToAction("Index");
            }

            var books = await _bookRepository.GetAllBooksAsync();
            return View("Index", books);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            if (ModelState.IsValid)
            {
                var success = await _bookRepository.UpdateBookAsync(book);
                if (success)
                    return RedirectToAction("Index");
            }

            var books = await _bookRepository.GetAllBooksAsync();
            return View("Index", books);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int BookId)
        {
            var success = await _bookRepository.DeleteBookAsync(BookId);
            return RedirectToAction("Index");
        }
    }
}
