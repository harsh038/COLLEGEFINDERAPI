using CollegeFinderAPI.Data;
using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly CollegeRepository _collegeRepository;

        #region Configuration
        public CollegeController(CollegeRepository collegeRepository)
        {
            _collegeRepository = collegeRepository;
        }
        #endregion

        #region SelectAll
        [HttpGet]
        public IActionResult SelectAll()
        {
            try
            {
                var colleges = _collegeRepository.SelectAll();
                return Ok(colleges);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region SelectById
        [HttpGet("{CollegeID}")]
        public IActionResult SelectById(int CollegeID)
        {
            try
            {
                var college = _collegeRepository.SelectById(CollegeID);
                if (college.CollegeID == null)
                    return NotFound(new { status = false, message = "College not found" });
                return Ok(college);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Insert
        [HttpPost]
        public IActionResult Insert(CollegeModel college)
        {
            try
            {
                if (_collegeRepository.Insert(college))
                    return Ok(new { status = true, message = "College inserted successfully" });
                return BadRequest(new { status = false, message = "Insert operation failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Update
        [HttpPut("{CollegeID}")]
        public IActionResult Update(int CollegeID, CollegeModel college)
        {
            try
            {
                if (_collegeRepository.Update(CollegeID, college))
                    return Ok(new { status = true, message = "College updated successfully" });
                return NotFound(new { status = false, message = "College not found or update failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{CollegeID}")]
        public IActionResult Delete(int CollegeID)
        {
            try
            {
                // Check if the college is being used in CollegeCourses before deletion
                if (_collegeRepository.CollegeIDInCollageCourses(CollegeID))
                {
                    return Ok(new { foreignKey = true, message = "Please delete all dependent rows in the CollegeCourse table first." });
                }

                if (_collegeRepository.Delete(CollegeID))
                    return Ok(new { status = true, message = "College deleted successfully" });

                return NotFound(new { status = false, message = "College not found or deletion failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region DropdownForCity
        [HttpGet("CollegeDropDown/{CityID}")]
        public IActionResult CollegeDropDown(int CityID)
        {
            try
            {
                var collegeDD = _collegeRepository.CollegeDropDown(CityID);
                if (collegeDD == null)
                    return NotFound(new { status = false, message = "No colleges found for the given city" });
                return Ok(collegeDD);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Dropdown
        [HttpGet("OnlyCollegeDropDown")]
        public IActionResult OnlyCollegeDropDown()
        {
            try
            {
                var collegeDD = _collegeRepository.OnlyCollegeDropDown();
                return Ok(collegeDD);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion
    }
}
