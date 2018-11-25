using Mints.BLayer.Helpers;
using Mints.BLayer.Models.Common;
using Mints.BLayer.Models.Identity;
using Mints.BLayer.Repositories;
using Mints.Helpers;
using Mints.Models.ApiModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mints.BLayer.Models.Farmer;
using ExamCardPin.Models.ApiModels;

namespace ExamCardPin.Controllers
{

    [Route("api/v1/auth/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IFarmerRepository _farmerRepository;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,  IUserRepository userRepository, IFarmerRepository farmerRepository, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _farmerRepository = farmerRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(200, Type=typeof(TokenRequestModel))]
        [ProducesResponseType(404, Type = typeof(ErrorModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Token([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, $"{model.Password + user.HashSalt}", false);
                    if (result.Succeeded)
                    {
                        var token = await GetToken(user);
                        var response = new TokenRequestModel
                        {
                            Status = "success",
                            Message = "successfully authenticated",
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            Expires = token.ValidTo,
                        };
                        return Ok(response);
                    }

                    return BadRequest(new ErrorModel { Status = "error", Message = $"invalid credentitals" });
                }
                else
                {
                    return NotFound(new ErrorModel { Status = "error", Message = $"user {model.UserName} does not exist" });
                }
            }
            catch(Exception ex)
            {
                var response = new ErrorModel
                {
                    Status = "error",
                    Message = $"{ex.Message}",
                };
                return BadRequest(response);
            }           
        }

        private async Task<JwtSecurityToken> GetToken(User user)
        {
            IList<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };


            var providerOptions = _configuration.GetSection("TokenAuthentication");
            var tokenProviderOptions = providerOptions.Get<TokenProviderOptions>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenProviderOptions.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var claim in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, claim));
            }

            var token = new JwtSecurityToken(issuer: tokenProviderOptions.Issuer,
             audience: tokenProviderOptions.Audience,
              claims: claims,
              notBefore: DateTime.Now,
              expires: tokenProviderOptions.Expiration,
              signingCredentials: creds);            
            return token;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TokenRequestModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model) 
        {
            try
            {
                var salt = Guid.NewGuid().ToString();
                var user = new User { UserName = model.Email, Email = model.Email, PhoneNumber = model.Phone, HashSalt = salt};

                var password = $"{model.Password + salt}";
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    var token = await GetToken(user);
                    var response = new TokenRequestModel
                    {
                        Status = "success",
                        Message = "acount successfully created",
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expires = token.ValidTo,
                    };
                    return Created("", response);
                }
                return BadRequest(new ErrorModel { Status = "error", Message = $"cannot register user {model.Email}" });
            }
            catch(Exception ex)
            {
                var response = new ErrorModel
                {
                    Status = "error",
                    Message = $"{ex.Message}",
                };
                return BadRequest(response);
            }    
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(201, Type = typeof(RegisterFarmerRequestModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> RegisterFarmerData([FromBody] RegisterFarmerDataViewModel model)
        {
            try
            {
                var username = User.Identity.Name;
                var farmer = new Farmer { FirstName = model.FirstName, LastName = model.LastName,  Address = model.Address, Email = username, Phone = model.Phone};
                var result = await _farmerRepository.RegisterData(username, farmer);
                switch (result.Status)
                {
                    case Status.Success:
                        var response = new RegisterFarmerRequestModel
                        {
                            Status = "success",
                            Message = "farmer registration successful",
                            Email = username,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            DateRegistered = DateTime.Now,                          
                        };
                        return CreatedAtAction("profile", "farmer", null, response);
                    default:
                        return BadRequest(new ErrorModel { Status = "error", Message = $"cannot create record for user {username}", Data = result.Data});
                }
            }
            catch (Exception ex)
            {
                var response = new ErrorModel
                {
                    Status = "error",
                    Message = $"{ex.Message}",
                };
                return BadRequest(response);
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TokenRequestModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> RegisterFarmer([FromBody] RegisterFarmerViewModel model)
        {
            try
            {
                var salt = Guid.NewGuid().ToString();
                var password = $"{model.Password + salt}";
                var passwordHash = _userManager.PasswordHasher.HashPassword(new User(), password);
                var farmer = new Farmer { FirstName = model.FirstName, LastName = model.LastName, Address = model.Address, Email = model.Email, Phone = model.Phone};
                var user = new User { UserName = model.Email, Email = model.Email, HashSalt = salt, PasswordHash = passwordHash, PhoneNumber = model.Phone };
                var result = await _farmerRepository.RegisterFarmer(user, farmer);
                switch (result.Status)
                {
                    case Status.Success:
                        var token = await GetToken(user);
                        var response = new TokenRequestModel
                        {
                            Status = "success",
                            Message = "account registration successful",
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            Expires = token.ValidTo,
                            Data = result.Data
                        };
                        return CreatedAtAction("profile", "farmer", null, response);
                    default:
                        return BadRequest(new ErrorModel { Status = "error", Message = $"cannot register user {model.Email}", Data = result.Data });
                }
            }
            catch(Exception ex)
            {
                var response = new ErrorModel
                {
                    Status = "error",
                    Message  = $"{ex.Message}",
                };
                return BadRequest(response);
            }
        }       
    }
}
