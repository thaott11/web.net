using System.ComponentModel.DataAnnotations;

namespace Demo_buoi4.Models
{
    public class SanPham
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Ten { get; set; }    
        public int SoLuong { get; set; }
        public string ImgURL { get; set; }
    }


}
