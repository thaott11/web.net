using Microsoft.AspNetCore.Mvc;
using Shoe_Store.Models;

namespace Shoe_Store.Controllers.NguoiDung
{
    public class UseController : Controller
    {
        private readonly Shoe_Store_DbContext _db;

        public UseController(Shoe_Store_DbContext db)
        {
            this._db = db;
        }
        public IActionResult UseIndex()
        {
            List<SanPham> sanphams = _db.SanPhams.ToList();
            return View(sanphams);
        }
    }
}
