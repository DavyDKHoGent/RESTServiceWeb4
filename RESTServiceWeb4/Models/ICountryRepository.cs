using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTServiceWeb4.Models
{
    public interface ICountryRepository
    {
        void AddCountry(Country country);
        Country GetCountry(int id);
        IEnumerable<Country> GetAll();
        IEnumerable<Country> GetAll(string continent);
        IEnumerable<Country> GetAll(string continent, string capital);
        void RemoveCountry(Country country);
        void UpdateCountry(Country country);
        bool ExistsCountry(int id);
    }
}
