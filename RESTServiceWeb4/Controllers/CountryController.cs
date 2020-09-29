using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTServiceWeb4.Models;

namespace RESTServiceWeb4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryRepository repo;
        public CountryController(ICountryRepository repo)
        {
            this.repo = repo;
        }
        // GET: api/country
        [HttpGet]
        [HttpHead]
        public IEnumerable<Country> Getall([FromQuery]string continent, [FromQuery] string capital)
        {
            if (!string.IsNullOrWhiteSpace(continent))
            {
                if (!string.IsNullOrWhiteSpace(capital.Trim())
                    return repo.GetAll(continent, capital);
                else
                    return repo.GetAll(continent);
            }
            return repo.GetAll();
        }
        //GET: api/country/5
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public ActionResult<Country> Get(int id)
        {
            try
            {
                return repo.GetCountry(id);
            }
            catch (CountryException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
