using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EBook.Models
{
    [Table("Sach")]
    public class SachModel
    {
        [Key]
        public int Id { get; set; }
        public String TuaSach { get; set; }
        public String HinhMinhHoa { get; set; }
        public String Noidung { get; set; }
        public int LoaiID { get; set; }        
    }
}