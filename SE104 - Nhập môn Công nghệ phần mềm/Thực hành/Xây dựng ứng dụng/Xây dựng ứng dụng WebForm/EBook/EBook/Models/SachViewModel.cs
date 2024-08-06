using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EBook.Models
{

    public class SachViewModel
    {
        public int Id { get; set; }
        public String TuaSach { get; set; }
        public String HinhMinhHoa { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public String Noidung { get; set; }
        public int LoaiID { get; set; }
        public String TenTheLoai { get; set; }
        public List<LoaiSachModel> DanhMuc { get; set; }
    }
}