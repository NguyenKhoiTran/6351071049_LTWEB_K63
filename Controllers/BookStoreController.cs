using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

// Ensure this matches your models namespace

namespace TH1.Controllers
{
    public class BookStoreController : Controller
    {
        QLBANSACHEntities db = new QLBANSACHEntities();

        // GET: BookStore
        public ActionResult Index()
        {
            var latestBooks = db.SACHes
                                .OrderByDescending(s => s.Ngaycapnhat)
                                .Take(5)
                                .ToList();

            return View(latestBooks);
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
