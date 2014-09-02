namespace CurrencyService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CurEntityModel : DbContext
    {
        public CurEntityModel()
            : base("name=CurEntityModel")
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                .Property(e => e.Code)
                .IsUnicode(false);
        }
    }
}
