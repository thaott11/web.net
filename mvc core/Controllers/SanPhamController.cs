using Demo_buoi4.Migrations;
using Demo_buoi4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Demo_buoi4.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly SanPhamDbContext _db;

        public SanPhamController(SanPhamDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var sanPhamList = await _db.SanPham.ToListAsync();
            return View(sanPhamList);
        }

        public IActionResult AddSanPham()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSanPham(SanPham sanPham, Guid id, IFormFile ImgURL)
        {
            if (ImgURL != null)
            {
                // Xây dựng 1 đường dẫn
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img", ImgURL.FileName);
                // Tạo 1 đối tượng FileStream để ghi dữ liệu vào file
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    // Sao chép hình ảnh vào thư mục root
                    await ImgURL.CopyToAsync(stream);
                }
                // Gán tên file cho thuộc tính
                sanPham.ImgURL = ImgURL.FileName;
            }
            else
            {
                ModelState.AddModelError("ImgURL", "Vui lòng thêm ảnh.");
                return View(sanPham); // Trả về view với model để hiển thị lỗi
            }

            _db.SanPham.Add(sanPham);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveSanPham(Guid id, SanPham sp)
        {
             var remote = _db.SanPham.FirstOrDefault(x => x.Id == id);
             _db.SanPham.Remove(remote);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
            
        public IActionResult UpdateSanPham(Guid? id)
        {
            var update = _db.SanPham.FirstOrDefault(x => x.Id == id);
            return View(update);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSanPham(SanPham sp, Guid? id, IFormFile ImgURL)
        {
            var update = _db.SanPham.FirstOrDefault(x => x.Id == id);
            if (update == null)
            {
                return NotFound();
            }

            update.Ten = sp.Ten;
            update.SoLuong = sp.SoLuong;

            if (ImgURL != null && ImgURL.Length > 0)
            {
                // Xây dựng đường dẫn
                string fileName = Path.GetFileName(ImgURL.FileName);
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img");
                string path = Path.Combine(directoryPath, fileName);

                // Kiểm tra và tạo thư mục nếu cần
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Tạo đối tượng FileStream để ghi dữ liệu vào file
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    // Sao chép hình ảnh vào thư mục root
                    await ImgURL.CopyToAsync(stream);
                }

                // Gán tên file cho thuộc tính
                update.ImgURL = fileName;
            }

            _db.SanPham.Update(update);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult DelTaiSanPham(Guid id)
        {
            var sp = _db.SanPham.FirstOrDefault(x => x.Id == id);
            return View(sp);
        }
    }
}
