using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UploadFile.Models
{
    public class MeuContexto : DbContext
    {
        public DbSet<UploadFileResult> uploadFileResults { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}