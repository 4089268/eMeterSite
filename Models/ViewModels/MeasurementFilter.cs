using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMeterSite.Models.ViewModels
{
    public class MeasurementFilter
    {

        public MeasurementFilter()
        {
            var now = DateTime.Now;
            Desde = new DateTime( now.Year, now.Month, 1);
            Hasta = new DateTime( now.Year, now.Month, DateTime.DaysInMonth( now.Year, now.Month));
        }

        [DataType(DataType.Date)]
        public DateTime Desde {get;set;}
        
        [DataType(DataType.Date)]
        public DateTime Hasta {get;set;}
    }
}