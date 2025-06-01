using CollegeFinderAPI.Data;
using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseRepository _courseRepository;

        #region Configuration
        public CourseController(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        #endregion

        #region SelectAll
        [HttpGet]
        public IActionResult SelectAll()
        {
            try
            {
                var courses = _courseRepository.SelectAll();
                return Ok(courses);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region SelectById
        [HttpGet("{CourseID}")]
        public IActionResult SelectById(int CourseID)
        {
            try
            {
                var course = _courseRepository.SelectById(CourseID);
                if (course.CourseID == null)
                    return NotFound(new { status = false, message = "Course not found" });
                return Ok(course);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Insert
        [HttpPost]
        public IActionResult Insert(CourseModel course)
        {
            try
            {
                if (_courseRepository.Insert(course))
                    return Ok(new { status = true, message = "Course inserted successfully" });
                return BadRequest(new { status = false, message = "Insert operation failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Update
        [HttpPut("{CourseID}")]
        public IActionResult Update(int CourseID,CourseModel course)
        {
            try
            {
                if (_courseRepository.Update(CourseID ,course))
                    return Ok(new { status = true, message = "Course updated successfully" });
                return NotFound(new { status = false, message = "Course not found or update failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{CourseID}")]
        public IActionResult Delete(int CourseID)
        {
            try
            {
                if (_courseRepository.CourseIDInCollegeCourses(CourseID))
                {
                    return Ok(new { foreignKey = true, message = "Please delete all dependent rows in either CollegeCourses table" });
                }

                if (_courseRepository.Delete(CourseID))
                    return Ok(new { status = true, message = "Course deleted successfully" });
                return NotFound(new { status = false, message = "Course not found or deletion failed" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete method: {ex.Message}");
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Dropdown
        [HttpGet("CourseDropDown")]
        public IActionResult courseDropDown()
        {
            try
            {
                var coursesDD = _courseRepository.CourseDropDown();
                return Ok(coursesDD);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion
    }
}
