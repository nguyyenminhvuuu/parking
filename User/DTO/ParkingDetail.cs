using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.DTO

{
    public class ParkingDetail
    {
        public ParkingDetail() { }
        public Guid Id { get;set; }
        public Guid IdParking { get;set; }
        public Guid Type { get;set; }
        public double Price { get; set; }
        public DateTime lastUpdate { get; set; }
        public string Status { get; set; }

    }
}
