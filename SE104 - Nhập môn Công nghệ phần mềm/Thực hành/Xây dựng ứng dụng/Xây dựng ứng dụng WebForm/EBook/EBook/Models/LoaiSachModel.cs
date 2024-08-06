using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EBook.Models
{
    [Table("LoaiSach")]
    public class LoaiSachModel
    {
        [Key]
        public int LoaiID { get; set; }
        public String TenTheLoai { get; set; }
    }
}