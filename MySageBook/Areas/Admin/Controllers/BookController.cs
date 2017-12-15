using EFSageBookModel;
using EFSageBookModel.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySageBook.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        UnitOfWork uow;

        public BookController()
        {
            uow = new UnitOfWork(new SageBookContext("LibraryDb"));
        }

        public ActionResult Books()
        {

            return View(uow.Books.GetAll());
        }

        public ActionResult GetSageBooks(int? Id)
        {
            Sage sage = uow.Sages.GetAll().Where(x => x.Id == Id).Single();
            IEnumerable<Book> Books = sage.Books;
            ViewBag.Sage = sage;
            return View("SageBooks", Books);
        }

        public ActionResult DeleteSageBook(int? SageId, int? BookId)
        {

            Sage sage = uow.Sages.GetAll().Where(x => x.Id == SageId).Single();

            sage.Books.Remove(sage.Books.Where(x => x.Id == BookId).SingleOrDefault());
            uow.Save();

            ViewBag.Sage = sage;
            IEnumerable<Book> Books = sage.Books;
            return View("SageBooks", Books);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            try
            {
                uow.Books.Create(book);
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
            Book b = uow.Books.GetAll().Where(x => x.Id == Id).Single();

            Sage[] sages = uow.Sages.GetAll().Except(b.Sages).ToArray();


            ViewBag.Sages = sages;

            return View(b);
        }

        [HttpPost]
        public ActionResult Edit(Book b, List<string> Sages)
        {
            uow.Books.Update(b);
            b = uow.Books.GetAll().Where(x => x.Id == b.Id).Single();

            if (Sages != null)
            {
                int[] ids = Sages.Select(x => Convert.ToInt32(x)).ToArray();

                IEnumerable<Sage> sages = uow.Sages.GetAll().Where(x => (ids.Contains(x.Id)));     
                               
                b.Sages.AddRange(sages);
                uow.Save();
            }          

            return View("Books", uow.Books.GetAll());
        }

        public ActionResult Delete(int? Id)
        {
            uow.Books.Delete(uow.Books.GetAll().Where(x => x.Id == Id).Single());
            return View("Books", uow.Books.GetAll());
        }
    }
}