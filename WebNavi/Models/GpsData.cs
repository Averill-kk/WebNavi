using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNavi.Models
{
    public class GpsData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public int Number { get; set; }
        public double Latitude { get; set; }

        public double Lontitude { get; set; }

        public int Satellite { get; set; }

        public double Battery { get; set; }

        public double RSSI { get; set; }

        public string Status { get; set; }

        public DateTime DateStatus { get; set; }

    }
}
