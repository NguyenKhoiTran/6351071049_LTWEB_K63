using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

using PagedList;
using PagedList.Mvc;
using System.Collections.Generic;
// Ensure this matches your models namespace

namespace TH1.Controllers
{
    public class BookStoreController : Controller
    {
        QLBANSACHEntities db = new QLBANSACHEntities();

        // GET: BookStore
        private List<SACH> Laysachmoi(int count)
        {
            // Sắp xếp sách theo ngày cập nhật, sau đó lấy top @count
            return db.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        public ActionResult Index(int? page, int? categoryId, int? publisherId)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);

            // Store the selected category and publisher in the ViewBag
            ViewBag.CurrentCategoryId = categoryId;
            ViewBag.CurrentPublisherId = publisherId;

            var books = db.SACHes.AsQueryable();

            // Apply category filter if selected
            if (categoryId.HasValue)
            {
                books = books.Where(b => b.MaCD == categoryId.Value);
            }

            // Apply publisher filter if selected
            if (publisherId.HasValue)
            {
                books = books.Where(b => b.MaNXB == publisherId.Value);
            }

            return View(books.OrderByDescending(b => b.Ngaycapnhat).ToPagedList(pageNum, pageSize));
        }



        public ActionResult Chude()
        {
            var categories = db.CHUDEs.ToList(); // Fetch categories from the database
            return PartialView("~/Views/BookStore/_Categories.cshtml", categories); // Return the partial view
        }

        public ActionResult Publishers()
        {
            var publishers = db.NHAXUATBANs.ToList(); // Fetch publishers from the database
            return PartialView("~/Views/BookStore/_Publishers.cshtml", publishers); // Return the partial view
        }

        public ActionResult Details(int id)
        {
            var book = db.SACHes.Find(id); // Find the book by its ID
            if (book == null)
            {
                return HttpNotFound(); // Return 404 if not found
            }
            return View(book); // Pass the book to the view
        }

        public ActionResult BooksByCategory(int categoryId)
        {
            var books = db.SACHes
                          .Where(b => b.MaCD == categoryId)
                          .ToList();
            return View("Index", books); // Assuming 'Index' displays a list of books
        }

        // Action to display books by publisher
        public ActionResult BooksByPublisher(int publisherId)
        {
            var books = db.SACHes
                          .Where(b => b.MaNXB == publisherId)
                          .ToList();
            return View("Index", books);
        }



    }
}
