using System.ComponentModel.DataAnnotations;

namespace Shoe_Store.Models
{
    public class SanPham
    {
        public SanPham()
        {
            this.Loais = new HashSet<Loai>();
            this.sanPhamChitiets = new HashSet<SanPhamChitiet>();
            this.DanhGias = new HashSet<DanhGia>();
        }
        [Key]
        public int SanPhamId { get; set; }
        [Required]
        public string Ten { get; set; }
        [Required]
        public decimal Gia { get; set; }
        [Required]
        public string MoTa { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public string MauSac { get; set; }
        [Required]
        public string NhaSanXuat { get; set; }
        [Required]
        public byte[] HinhAnh { get; set; }

        public ICollection<SanPhamChitiet> sanPhamChitiets { get; set; }
        public ICollection<Loai> Loais { get; set; }
        public ICollection<DanhGia> DanhGias { get; set; }
        public int idnhacungcap { get; set; }
        public NhaCungcap NhaCungcap { get; set; }
    }
}
