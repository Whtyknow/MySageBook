using EFSageBookModel;
using EFSageBookModel.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySageBook.Areas.Admin.Controllers
{
    public class SageController : Controller
    {
        UnitOfWork uow;

        public SageController()
        {
            uow = new UnitOfWork(new SageBookContext("LibraryDb"));
        }

        public ActionResult Sages()
        {
            return View(uow.Sages.GetAll());
        }

        public ActionResult GetBookSages(int? Id)
        {
            Book book = uow.Books.GetAll().Where(x => x.Id == Id).Single();
            IEnumerable<Sage> Sages = book.Sages;
            ViewBag.Book = book;
            return View("BookSages", Sages);
        }

        public ActionResult DeleteSageBook(int? BookId , int? SageId )
        {
            Book book = uow.Books.GetAll().Where(x => x.Id == SageId).Single();

            book.Sages.Remove(book.Sages.Where(x => x.Id == BookId).SingleOrDefault());
            uow.Save();

            ViewBag.Book = book;
            IEnumerable<Sage> Sages = book.Sages;
            return View("BookSages", Sages);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sage sage)
        {
            try
            {
                uow.Sages.Create(sage);
                ViewBag.Message = "Created";
            }
            catch (Exception)
            {
                ViewBag.Message = "Error";
            }

            return View();
        }

        public ActionResult Edit(int? Id)
        {
            Sage s = uow.Sages.GetAll().Where(x => x.Id == Id).Single();

            Book [] books = uow.Books.GetAll().Except(s.Books).ToArray();

            ViewBag.Books = books;

            return View(s);
        }

        [HttpPost]
        public ActionResult Edit(Sage s, List<string> Books)
        {
            uow.Sages.Update(s);
            s = uow.Sages.GetAll().Where(x => x.Id == s.Id).Single();

            if (Books != null)
            {
                int[] ids = Books.Select(x => Convert.ToInt32(x)).ToArray();

                IEnumerable<Book> books = uow.Books.GetAll().Where(x => (ids.Contains(x.Id)));

                s.Books.AddRange(books);
                uow.Save();
            }

            return View("Sages", uow.Sages.GetAll());
        }

        public ActionResult Delete(int? Id)
        {
            uow.Sages.Delete(uow.Sages.GetAll().Where(x => x.Id == Id).Single());
            return View("Sages", uow.Sages.GetAll());
        }

    }
}