using CollegeFinderAPI.Data;
using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CollegeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterCollegesController : ControllerBase
    {
        private readonly FilterCollegesRepository _filterCollegesRepository;

        #region Constructor
        public FilterCollegesController(FilterCollegesRepository filterCollegesRepository)
        {
            _filterCollegesRepository = filterCollegesRepository;
        }
        #endregion

        #region Filter Colleges
        [HttpPost("filter")]
        public IActionResult FilterColleges([FromBody] FilterCollegesInputModel filter)
        {
            try
            {
                //Console.WriteLine("=== Incoming Payload ===");
                //Console.WriteLine(JsonConvert.SerializeObject(filter));

                var filteredColleges = _filterCollegesRepository.FilterColleges(filter);

                if (filteredColleges.Count == 0)
                {
                    // Return 200 OK with empty data and message
                    return Ok(new
                    {
                        status = true,
                        message = "No colleges found based on the provided filters.",
                        data = new List<FilterCollegesModel>()
                    });
                }

                return Ok(new { status = true, data = filteredColleges });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Internal Server Error", error = ex.Message });
            }
        }
        #endregion
    }
}
