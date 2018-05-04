using DreamCar.Models.Domain;

namespace DreamCar.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DreamCarDbContext : DbContext
    {
        public DreamCarDbContext()
            : base("name=DreamCarDbContext")
        {
        }

        public virtual IDbSet<Dealer> Dealers { get; set; }
        public virtual IDbSet<Car> Cars { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
