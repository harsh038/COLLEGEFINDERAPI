using CollegeFinderAPI.Data;
using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepository _cityRepository;

        #region Configuration
        public CityController(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        #endregion

        #region SelectAll
        [HttpGet]
        public IActionResult SelectAll()
        {
            try
            {
                var cities = _cityRepository.SelectAll();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region SelectById
        [HttpGet("{CityID}")]
        public IActionResult SelectById(int CityID)
        {
            try
            {
                var city = _cityRepository.SelectById(CityID);
                if (city.CityID == null)
                    return NotFound(new { status = false, message = "City not found" });
                return Ok(city);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Insert
        [HttpPost]
        public IActionResult Insert(CityModel city)
        {
            try
            {
                if (_cityRepository.Insert(city))
                    return Ok(new { status = true, message = "City inserted successfully" });
                return BadRequest(new { status = false, message = "Insert operation failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Update
        [HttpPut("{CityID}")]
        public IActionResult Update(int CityID,CityModel city)
        {
            try
            {
                if (_cityRepository.Update(CityID,city))
                    return Ok(new { status = true, message = "City updated successfully" });
                return NotFound(new { status = false, message = "City not found or update failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{CityID}")]
        public IActionResult Delete(int CityID)
        {
            try
            {
                if (_cityRepository.CityIDInColleges(CityID))
                {
                    return Ok(new { foreignKey = true,message = "Please delete all dependent rows in the Colleges table first" });
                }
                if (_cityRepository.Delete(CityID))
                    return Ok(new { status = true, message = "City deleted successfully" });
                return NotFound(new { status = false, message = "City not found or deletion failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region DROPDOWN
        [HttpGet("CityDropDown/{stateId}")]
        public IActionResult CityDropDown(int stateId)
        {
            try
            {
                var city = _cityRepository.CityDropDown(stateId);
                if (city.Count <= 0)
                    return NotFound(new { status = false, message = "City not found" });
                return Ok(city);
            }
            catch (Exception e)
            {
                Console.WriteLine("---------"+e.Message);
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion
    }
}
