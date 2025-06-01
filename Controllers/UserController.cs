using CollegeFinderAPI.Data;
using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        #region Configuration
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region SelectAll
        [HttpGet]
        public IActionResult selectAll()
        {
            try
            {
                var users = this._userRepository.selectAll();
                return Ok(users);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { status = false, message = "Some Error Has Been occured" });
            }
        }
        #endregion

        #region SelectById
        [HttpGet("{UserID}")]
        public IActionResult SelectById(int UserID)
        {
            try
            {
                UserModel user = this._userRepository.SelectById(UserID);
                if (user.UserID == 0)
                {
                    return NotFound(new { status = false, message = "user not found" });
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Some Error Has Been occured" });
            }
        }
        #endregion

        #region Insert
        [HttpPost]
        public IActionResult POST(UserModel user)
        {
            try
            {
                if (this._userRepository.Insert(user))
                    return Ok(new { status = true, Message = "User Inserted Successfully" });
                else
                    return Ok(new { status = false, message = "Some Error Has Been occured" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insert Fail" + ex.Message);
                return StatusCode(500, new { status = false, message = "Some Error Has Been occured" });
            }
        }
        #endregion

        #region Update
        [HttpPut("{UserId}")]
        public IActionResult PUT(int UserId,UserModel user)
        {
            Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.WriteLine(User);
            try
            {
                if (this._userRepository.Update(UserId,user))
                    return Ok(new { status = true, Message = "User Updated Successfully" });
                else
                    return NotFound(new { status = false, Message = "User not found or could not be Updated." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { status = false, message = "Some Error Has Been occured" });
            }
        }
        #endregion

        #region Delete
        //[HttpDelete("{UserID}")]
        [HttpDelete]
        public IActionResult Delete(int UserID)
        {
            try
            {
                if (_userRepository.userIDInReview(UserID))
                {
                    return Ok(new { foreignKey = true, message = "Please delete all dependent rows in the Review table" });
                }
                if (this._userRepository.Delete(UserID))
                    return Ok(new { status = true, Message = "User Deleted Successfully" });
                else
                    return NotFound(new { status = false, Message = "User not found or could not be deleted." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("::::::::::::::::::::  " + ex.ToString());
                return StatusCode(500, new { status = false, message = "Some Error Has Been occured" });
            }
        }
        #endregion
    }
}
