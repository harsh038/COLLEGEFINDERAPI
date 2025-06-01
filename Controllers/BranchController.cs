using CollegeFinderAPI.Data;
using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CollegeFinderAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly BranchRepository _branchRepository;

        #region Configuration
        public BranchController(BranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }
        #endregion

        #region SelectAll
        [HttpGet]
        public IActionResult SelectAll()
        {
            try
            {
                var branches = _branchRepository.SelectAll();
                return Ok(branches);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region SelectById
        [HttpGet("{BranchID}")]
        public IActionResult SelectById(int BranchID)
        {
            try
            {
                var branch = _branchRepository.SelectById(BranchID);
                if (branch.BranchID == null)
                    return NotFound(new { status = false, message = "Branch not found" });
                return Ok(branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

      #region Insert
        [HttpPost]
        public IActionResult Insert(BranchModel branch)
        {
            try
            {
                if (_branchRepository.Insert(branch))
                    return Ok(new { status = true, message = "Branch inserted successfully" });
                return BadRequest(new { status = false, message = "Insert operation failed" });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("already exists"))
                {
                    return Conflict(new
                    {
                        Duplication = true,
                        message = "Branch name already exists for the given course."
                    });
                }

                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion


        #region Update
        [HttpPut("{BranchID}")]
        public IActionResult Update(int BranchID, BranchModel branch)
        {
            try
            {
                if (_branchRepository.Update(BranchID, branch))
                    return Ok(new { status = true, message = "Branch updated successfully" });
                return NotFound(new { status = false, message = "Branch not found or update failed" });
            }
            catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation
            {
                return Conflict(new { Duplication = true, message = "Branch name already exists for the given course."});
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{BranchID}")]
        public IActionResult Delete(int BranchID)
        {
            try
            {
                if (_branchRepository.BranchIDInCollegeCourses(BranchID))
                {
                    return Ok(new { foreignKey = true, message = "Please delete all dependent rows in either CollegeCourses table" });
                }
                if (_branchRepository.Delete(BranchID))
                    return Ok(new { status = true, message = "Branch deleted successfully" });
                return NotFound(new { status = false, message = "Branch not found or deletion failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Dropdown
        [HttpGet("branchDropDown/{courseID}")]
        public IActionResult branchDropDown(int courseID)
        {
            try
            {
                var branch = _branchRepository.BranchDropDown(courseID);
                if (branch.Count <= 0)
                    return NotFound(new { status = false, message = "State not found" });
                return Ok(branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion
    }
}
