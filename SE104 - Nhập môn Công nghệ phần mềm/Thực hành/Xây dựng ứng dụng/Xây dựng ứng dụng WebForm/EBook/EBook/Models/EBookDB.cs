using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EBook.Models
{
    public class EBookDB : DbContext
    {
        public DbSet<LoaiSachModel> LoaiSach { get; set; }
        public DbSet<SachModel> Sach { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LoaiSachModel>().ToTable("LoaiSach");
            modelBuilder.Entity<SachModel>().ToTable("Sach");
        }
    }
}