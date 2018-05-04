using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamCar.Models.Domain
{
    [Table("Cars")]
    public class Car
    {
        public  int Id { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public short YearBuilt { get; set; }
        public short HorsePower { get; set; }
        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
    }
}
