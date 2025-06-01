using CollegeFinderAPI.Data;
using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CollegeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeCourseController : ControllerBase
    {
        private readonly CollegeCourseRepository _collegeCourseRepository;

        #region Configuration
        public CollegeCourseController(CollegeCourseRepository collegeCourseRepository)
        {
            _collegeCourseRepository = collegeCourseRepository;
        }
        #endregion

        #region SelectAll
        [HttpGet]
        public IActionResult SelectAll()
        {
            try
            {
                var collegeCourses = _collegeCourseRepository.SelectAll();
                return Ok(collegeCourses);
            }
            catch (Exception ee)
            {
                Console.WriteLine("............................" + ee);
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region SelectById
        [HttpGet("{CollegeCourseID}")]
        public IActionResult SelectById(int CollegeCourseID)
        {
            try
            {
                var collegeCourse = _collegeCourseRepository.SelectById(CollegeCourseID);
                if (collegeCourse.CollegeCourseID == null)
                    return NotFound(new { status = false, message = "CollegeCourse not found" });
                return Ok(collegeCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region SelectByCollegeID
        [HttpGet("colleges/{CollegeID}")]
        public IActionResult SelectByCollegeID(int CollegeID)
        {
            try
            {
                var collegeCourses = _collegeCourseRepository.SelectByCollegeID(CollegeID);

                if (collegeCourses == null || collegeCourses.Count == 0)
                    return NotFound(new { status = false, message = "No courses found for the specified college." });

                return Ok(collegeCourses);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching data: " + ex);
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion


        #region Insert
        [HttpPost]
        public IActionResult Insert(CollegeCourseModel collegeCourse)
        {
            try
            {
                if (_collegeCourseRepository.Insert(collegeCourse))
                    return Ok(new { status = true, message = "CollegeCourse inserted successfully" });

                return BadRequest(new { status = false, message = "Insert operation failed" });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation
            {
                return Conflict(new { Duplication = true, message = "College course already exists." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString()); // Log error
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion


        #region Update
        [HttpPut("{collegeCourseID}")]
        public IActionResult Update(int collegeCourseID, CollegeCourseModel collegeCourse)
        {
            try
            {
                if (_collegeCourseRepository.Update(collegeCourseID, collegeCourse))
                    return Ok(new { status = true, message = "CollegeCourse updated successfully" });

                return NotFound(new { status = false, message = "CollegeCourse not found or update failed" });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation
            {
                return Conflict(new { Duplication = true, message = "College course name already exists." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString()); // Log error
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion


        #region Delete
        [HttpDelete("{CollegeCourseID}")]
        public IActionResult Delete(int CollegeCourseID)
        {
            try
            {
                if (_collegeCourseRepository.Delete(CollegeCourseID))
                    return Ok(new { status = true, message = "CollegeCourse deleted successfully" });
                return NotFound(new { status = false, message = "CollegeCourse not found or deletion failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

    

    }
}
