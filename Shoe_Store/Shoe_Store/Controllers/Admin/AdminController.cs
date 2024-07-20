using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoe_Store.Models;

namespace Shoe_Store.Controllers
{
    public class AdminController : Controller
    {
        private readonly Shoe_Store_DbContext _db;

        public AdminController(Shoe_Store_DbContext db)
        {
            _db = db;
        }

        public IActionResult ListSanPham()
        {
            List<SanPham> sanPhams = _db.SanPhams.ToList();
            return View(sanPhams);
        }


        // thêm sản phẩm
        public IActionResult AddSanPham()
        {
            ViewBag.NhaCungCap = new SelectList(_db.NhaCungCaps, "Id", "TenNhaCungCap");
            ViewBag.Loai = new SelectList(_db.loais, "Id", "Name");
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> AddSanPham(SanPham sanPham, List<IFormFile> HinhAnh, List<IFormFile> HinhAnhPhu)
        {
            // Xử lý ảnh chính
            if (HinhAnh != null && HinhAnh.Count > 0)
            {
                var mainImage = HinhAnh.FirstOrDefault();
                if (mainImage != null && mainImage.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await mainImage.CopyToAsync(stream);
                        sanPham.HinhAnh = stream.ToArray();
                    }
                }
            }

            // Thêm sản phẩm vào cơ sở dữ liệu và lưu thay đổi để có SanPhamId
            _db.SanPhams.Add(sanPham);
            await _db.SaveChangesAsync();

            // Xử lý ảnh phụ
            if (HinhAnhPhu != null && HinhAnhPhu.Count > 0)
            {
                List<SanPhamChitiet> sanPhamChiTietList = new List<SanPhamChitiet>();
                foreach (var item in HinhAnhPhu)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            var sanPhamChiTiet = new SanPhamChitiet
                            {
                                HinhAnh = stream.ToArray(),
                                SanPhamId = sanPham.SanPhamId // Liên kết với sản phẩm vừa được thêm vào
                            };

                            sanPhamChiTietList.Add(sanPhamChiTiet);
                        }
                    }
                }

                _db.sanPhamChitiets.AddRange(sanPhamChiTietList);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ListSanPham));
        }

        // thêm nhà cung cấp 
        public IActionResult AddNhaCungcap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNhaCungcap(NhaCungcap nhaCungcap)
        {
            _db.NhaCungCaps.Add(nhaCungcap);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        //cập nhập sản phẩm 
        public IActionResult UpdateSanPham(int id)
        {
            var sanPham = _db.SanPhams.FirstOrDefault(x => x.SanPhamId == id);
            ViewBag.NhaCungCap = new SelectList(_db.NhaCungCaps, "Id", "TenNhaCungCap", sanPham.idnhacungcap);
            return View(sanPham);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSanPham(SanPham sp, List<IFormFile> HinhAnh)
        {
            var update = await _db.SanPhams.FindAsync(sp.SanPhamId);
            update.Ten = sp.Ten;
            update.Gia = sp.Gia;
            update.MoTa = sp.MoTa;
            update.SoLuong = sp.SoLuong;
            update.MauSac = sp.MauSac;
            update.NhaSanXuat = sp.NhaSanXuat;
            update.NhaCungcap = sp.NhaCungcap;

            if (HinhAnh != null && HinhAnh.Count > 0)
            {
                foreach (var item in HinhAnh)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            update.HinhAnh = stream.ToArray();
                        }
                    }
                }
            }
            _db.SanPhams.Update(update);
            await _db.SaveChangesAsync();
            return RedirectToAction("ListSanPham");
        }


        // xóa sản phẩm
        public IActionResult RemoveSanPham(int id)
        {
            var remove = _db.SanPhams.FirstOrDefault(x => x.SanPhamId == id);
            _db.SanPhams.Remove(remove);
            _db.SaveChanges();
            return RedirectToAction("ListSanPham");
        }

        // thêm loại Sản phẩm 

        public IActionResult AddLoai()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLoai(Loai loai)
        {
            _db.loais.Add(loai);
            _db.SaveChanges();
            return RedirectToAction("ListSanPham");
        }
    }
}
