using BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IPraCIF:ICrud<PraCIF>
    {
        IEnumerable<PraCIF> GetByName(string name);
        IEnumerable<ViewCountryCity> GetAllCityWithCountry();
        IEnumerable<SampleCity> GetCityWithCountry();
    }
}
