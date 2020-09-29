using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
                if (!string.IsNullOrWhiteSpace(capital.Trim()))
                    return repo.GetAll(continent, capital);
                else
                    return repo.GetAll(continent);
            }
            return repo.GetAll();
        }
        //GET: api/country/5
        [HttpGet("{id}", Name = "Get")]
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
        [HttpPost]
        public ActionResult<Country> Post([FromBody] Country country)
        {
            repo.AddCountry(country);
            return CreatedAtAction(nameof(Get), new { id = country.Id}, country);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!repo.ExistsCountry(id))
                return NotFound();
            repo.RemoveCountry(repo.GetCountry(id));
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Country country)
        {
            if (country == null || country.Id != id)
                return BadRequest();
            if (!repo.ExistsCountry(id))
            {
                repo.AddCountry(country);
                return CreatedAtAction(nameof(Get), new { id = country.Id }, country);
            }
            var countryDB = repo.GetCountry(id);
            repo.UpdateCountry(country);
            return new NoContentResult();
        }
        [HttpPatch("{id}")]
        public ActionResult<Country> Patch(int id, [FromBody]JsonPatchDocument<Country> countryPatch)
        {
            Country countryDB = repo.GetCountry(id);
            if (countryDB == null)
                return NotFound();
            try
            {
                countryPatch.ApplyTo(countryDB);
                return countryDB;
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
