using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FirstProject
{
    public partial class DBConnection : DbContext
    {
        public DBConnection()
            : base("name=DBConnection")
        {
        }

        public virtual DbSet<Text> Texts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Text>()
                .Property(e => e.Content)
                .IsUnicode(false);
        }
    }
}
