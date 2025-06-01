//using CollegeFinderAPI.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace CollegeFinderAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IConfiguration _config;
//        private readonly string _connectionString;

//        public AuthController(IConfiguration config)
//        {
//            _config = config;
//            _connectionString = _config.GetConnectionString("MyConnectionString"); // Corrected key name
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginModel login)
//        {
//            var user = ValidateUser(login.Email, login.Password);

//            if (user != null)
//            {
//                Dictionary<string, object> result = new Dictionary<string, object>();
//                var token = GenerateJwtToken(user);
//                result.Add("token", token);
//                result.Add("user", user);
//                return Ok(result);
//            }

//            return Unauthorized(new { message = "Invalid email or password" });
//        }

//        [HttpPost("register")]
//        public IActionResult Register([FromBody] UserModel model)
//        {
//            try
//            {
//                using (SqlConnection conn = new SqlConnection(_connectionString))
//                {
//                    using (SqlCommand cmd = new SqlCommand("API_USERS_INSERT", conn))
//                    {
//                        cmd.CommandType = CommandType.StoredProcedure;
//                        cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
//                        cmd.Parameters.AddWithValue("@LastName", model.LastName);
//                        cmd.Parameters.AddWithValue("@Email", model.Email);
//                        cmd.Parameters.AddWithValue("@PasswordHash", model.PasswordHash); // Store password as hash

//                        // Provide a default role if not specified
//                        string role = string.IsNullOrEmpty(model.Role) ? "User" : model.Role;
//                        cmd.Parameters.AddWithValue("@Role", role);

//                        conn.Open();
//                        cmd.ExecuteNonQuery();
//                    }
//                }
//                return Ok(new { message = "User registered successfully!" });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = $"Error: {ex.Message}" });
//            }
//        }


//        private UserModel ValidateUser(string email, string password)
//        {
//            UserModel user = null;

//            try
//            {
//                using (SqlConnection conn = new SqlConnection(_connectionString))
//                {
//                    using (SqlCommand cmd = new SqlCommand("sp_ValidateUser", conn))
//                    {
//                        cmd.CommandType = CommandType.StoredProcedure;
//                        cmd.Parameters.AddWithValue("@Email", email);
//                        cmd.Parameters.AddWithValue("@Password", password); // Use plain-text password

//                        conn.Open();
//                        SqlDataReader reader = cmd.ExecuteReader();
//                        if (reader.Read())
//                        {
//                            user = new UserModel
//                            {
//                                UserID = Convert.ToInt32(reader["UserID"]),
//                                Email = reader["Email"].ToString(),
//                                FirstName = reader["FirstName"].ToString(),
//                                LastName = reader["LastName"].ToString(),
//                                Role = reader["Role"].ToString()
//                            };
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception
//                Console.WriteLine($"Error validating user: {ex.Message}");
//            }

//            return user;
//        }
//        private string GenerateJwtToken(UserModel user)
//        {
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
//                new Claim(JwtRegisteredClaimNames.Email, user.Email),
//                new Claim("FirstName", user.FirstName),
//                new Claim("LastName", user.LastName),
//                new Claim(ClaimTypes.Role, user.Role),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };

//            var token = new JwtSecurityToken(
//                issuer: _config["Jwt:Issuer"],
//                audience: _config["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.UtcNow.AddHours(1),
//                signingCredentials: credentials
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CollegeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public AuthController(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("MyConnectionString"); // Corrected key name
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user = ValidateUser(login.Email, login.Password);

            if (user != null)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                var token = GenerateJwtToken(user);
                result.Add("token", token);
                result.Add("user", user);
                return Ok(result);
            }

            return Unauthorized(new { message = "Invalid email or password" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("API_USERS_INSERT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", model.LastName);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@PasswordHash", model.PasswordHash); // Store password as hash

                        // Provide a default role if not specified
                        string role = string.IsNullOrEmpty(model.Role) ? "User" : model.Role;
                        cmd.Parameters.AddWithValue("@Role", role);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Ok(new { message = "User registered successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error: {ex.Message}" });
            }
        }

        private UserModel ValidateUser(string email, string password)
        {
            UserModel user = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ValidateUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password); // Use plain-text password

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            user = new UserModel
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Email = reader["Email"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Role = reader["Role"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error validating user: {ex.Message}");
            }

            return user;
        }

        private string GenerateJwtToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("FirstName", user.FirstName),
        new Claim("LastName", user.LastName),
        new Claim(ClaimTypes.Role, user.Role), // Include the user's role
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}