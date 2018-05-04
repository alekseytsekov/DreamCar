using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DreamCar.Models.Domain
{
    [Table("Dealers")]
    public class Dealer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
