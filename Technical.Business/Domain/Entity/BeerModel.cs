using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical.Business.Domain.Entity
{
    public class BeerModel
    {
        public Guid BeerId { get; set; }
        public string Name { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
    }
}
