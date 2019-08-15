using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class SampleCity
    {
        public int CityID { get; set; }
        public int CountyID { get; set; }
        public string CityName { get; set; }

        public SampleCountry SampleCountry { get; set; }
    }

    
}
