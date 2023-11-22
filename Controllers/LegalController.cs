using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClockInTimeWeb.Controllers
{
    public class LegalController : Controller
    {
        // GET: LegalController/Details/5
        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        // POST: LegalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
