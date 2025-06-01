using CollegeFinderAPI.Data;
using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CollegeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepository _countryRepository;

        #region Configuration
        public CountryController(CountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        #endregion

        #region SelectAll
        [HttpGet]
        public IActionResult SelectAll()
        {
            try
            {
                var countries = _countryRepository.SelectAll();
                return Ok(countries);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region SelectById
        [HttpGet("{CountryID}")]
        public IActionResult SelectById(int CountryID)
        {
            try
            {
                var country = _countryRepository.SelectById(CountryID);
                if (country.CountryID == null)
                    return NotFound(new { status = false, message = "Country not found" });
                return Ok(country);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Insert
        [HttpPost]
        public IActionResult Insert(CountryModel country)
        {
            try
            {
                if (_countryRepository.Insert(country))
                    return Ok(new { status = true, message = "Country inserted successfully" });
                return BadRequest(new { status = false, message = "Insert operation failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Update
        [HttpPut("{countryID}")]
        public IActionResult Update(int countryID,CountryModel country)
        {
            try
            {
                if (_countryRepository.Update(countryID,country))
                    return Ok(new { status = true, message = "Country updated successfully" });
                return NotFound(new { status = false, message = "Country not found or update failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{countryID}")]
        public IActionResult Delete(int countryID)
        {
            try
            {
                if (_countryRepository.countryIDInState(countryID))
                {
                    return Ok(new { foreignKey = true, message = "Please delete all dependent rows in the State table first" });
                }
                if (_countryRepository.Delete(countryID))
                    return Ok(new { status = true, message = "Country deleted successfully" });
                return NotFound(new { status = false, message = "Country not found or deletion failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Dropdown
        [HttpGet("CountryDropDown")]
        public IActionResult CountryDropDown()
        {
            try
            {
                var countryDD = _countryRepository.CountryDropDown();
                return Ok(countryDD);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        
    }

}
