using Shoe_Store.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_Store.Models
{
    public class Loai
    {
        public Loai()
        {
            this.SanPhams = new HashSet<SanPham>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Size { get; set; }

        public ICollection<SanPham> SanPhams { get; set; }
    }
}