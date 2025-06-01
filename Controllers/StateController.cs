using CollegeFinderAPI.Data;
using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepository _stateRepository;

        #region Configuration
        public StateController(StateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }
        #endregion

        #region SelectAll
        [HttpGet]
        public IActionResult SelectAll()
        {
            try
            {
                var states = _stateRepository.SelectAll();
                return Ok(states);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region SelectById
        [HttpGet("{StateID}")]
        public IActionResult SelectById(int StateID)
        {
            try
            {
                var state = _stateRepository.SelectById(StateID);
                if (state.StateID == null)
                    return NotFound(new { status = false, message = "State not found" });
                return Ok(state);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Insert
        [HttpPost]
        public IActionResult Insert(StateModel state)
        {
            try
            {
                if (_stateRepository.Insert(state))
                    return Ok(new { status = true, message = "State inserted successfully" });
                return BadRequest(new { status = false, message = "Insert operation failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Update
        [HttpPut("{StateID}")]
        public IActionResult Update(int StateID, StateModel state)
        {
            try
            {
                if (_stateRepository.Update(StateID,state))
                    return Ok(new { status = true, message = "State updated successfully" });
                return NotFound(new { status = false, message = "State not found or update failed" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{stateID}")]
        public IActionResult Delete(int stateID)
        {
            try
            {
                if(_stateRepository.StateIDInCity(stateID))
                {
                    return Ok(new
                    {
                        foreignKey = true,
                        message = "Please delete all dependent rows in the City table first"
                    });
                }
                if (_stateRepository.Delete(stateID))
                    return Ok(new { status = true, message = "State deleted successfully" });
                return NotFound(new { status = false, message = "State not found or deletion failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion

        #region DROPDOWN
        [HttpGet("StateDropDown/{countryID}")]
        public IActionResult StateDropDown(int countryID)
        {
            try
            {
                var state = _stateRepository.StateDropDown(countryID);
                if (state.Count <= 0)
                    return NotFound(new { status = false, message = "State not found" });
                return Ok(state);
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = false, message = "An error occurred" });
            }
        }
        #endregion


    }
}
