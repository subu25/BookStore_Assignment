using BookStore.Models;
using BookStore.Models.Domain;
    using BookStore.Repositories.Abstract;
using BookStore.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using X.PagedList;

using System.Drawing.Printing;



namespace BookStore.Controllers
    {
        public class BookController : Controller
        {
            private readonly IBookService bookService;
            private readonly IAuthorService authorService;
            private readonly IGenreService genreService;
            private readonly IPublisherService publisherService;
        private dynamic term;

        public BookController(IBookService bookService, IGenreService genreService, IPublisherService publisherService, IAuthorService authorService)
            {
                this.bookService = bookService;
                this.genreService = genreService;
                this.publisherService = publisherService;
                this.authorService = authorService;
            }
            public IActionResult Add()
            {
                var model = new Book();
                model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString() }).ToList();
                model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() }).ToList();
                model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
                return View(model);
            }

            [HttpPost]
            public IActionResult Add(Book model)
            {
                model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
                model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PubhlisherId }).ToList();
                model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var result = bookService.Add(model);
                if (result)
                {
                    TempData["msg"] = "Added Successfully";
                    return RedirectToAction(nameof(Add));
                }
                TempData["msg"] = "Error has occured on server side";
                return View(model);
            }



        public IActionResult Update(int id)
            {
                var model = bookService.FindById(id);
                model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
                model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PubhlisherId }).ToList();
                model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
                return View(model);
            }

            [HttpPost]
            public IActionResult Update(Book model)
            {
                model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
                model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PubhlisherId }).ToList();
                model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var result = bookService.Update(model);
                if (result)
                {
                    return RedirectToAction("GetAll");
                }
                TempData["msg"] = "Error has occured on server side";
                return View(model);
            }


            public IActionResult Delete(int id)
            {

                var result = bookService.Delete(id);
                return RedirectToAction("GetAll");
            }

            public IActionResult GetAll()
            {

                var data = bookService.GetAll();
                return View(data);
            }

        [HttpGet("All")]
        public IActionResult GetAll(string sortOrder, string searchString, int? page)
        {
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.GenreSortParam = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewBag.IsbnSortParam = sortOrder == "Isbn" ? "isbn_desc" : "Isbn";
            ViewBag.PagesSortParam = sortOrder == "Pages" ? "pages_desc" : "Pages";
            ViewBag.AuthorSortParam = sortOrder == "Author" ? "author_desc" : "Author";
            ViewBag.PublisherSortParam = sortOrder == "Publisher" ? "publisher_desc" : "Publisher";
            
            var books = bookService.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)||
                b.AuthorName.Contains(searchString, StringComparison.OrdinalIgnoreCase)||
                b.PublisherName.Contains(searchString, StringComparison.OrdinalIgnoreCase)||
                b.GenreName.Contains(searchString, StringComparison.OrdinalIgnoreCase)||
                b.Isbn==searchString).ToList();
            }

            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title).ToList();
                    break;
                case "Genre":
                    books = books.OrderBy(b => b.GenreName).ToList();
                    break;
                case "genre_desc":
                    books = books.OrderByDescending(b => b.GenreName).ToList();
                    break;
                case "Isbn":
                    books = books.OrderBy(b => b.Isbn).ToList();
                    break;
                case "isbn_desc":
                    books = books.OrderByDescending(b => b.Isbn).ToList();
                    break;
                case "Pages":
                    books = books.OrderBy(b => b.TotalPages).ToList();
                    break;
                case "pages_desc":
                    books = books.OrderByDescending(b => b.TotalPages).ToList();
                    break;
                case "Author":
                    books = books.OrderBy(b => b.AuthorName).ToList();
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.AuthorName).ToList();
                    break;
                case "Publisher":
                    books = books.OrderBy(b => b.PublisherName).ToList();
                    break;
                case "publisher_desc":
                    books = books.OrderByDescending(b => b.PublisherName).ToList();
                    break;
                default:
                    books = books.OrderBy(b => b.Title).ToList();
                    break;
            }

           

             return View(books);



        }


    }
}