﻿
using SnowmanLabsChallenge.Infra.CrossCutting.Identity.Models;
using SnowmanLabsChallenge.WebApi.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SnowmanLabsChallenge.Application.Interfaces;

namespace SnowmanLabsChallenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        private readonly ITouristSpotAppService touristSpotAppService;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings,
            ITouristSpotAppService touristSpotAppService)
            : base()
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._appSettings = appSettings.Value;
            this.touristSpotAppService = touristSpotAppService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistration userRegistration)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new SnowmanLabsChallengeException("Invalid Model.");
                }

                var user = new IdentityUser
                {
                    UserName = userRegistration.Email,
                    Email = userRegistration.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, userRegistration.Password);

                if (!result.Succeeded)
                {
                    var error = result.Errors.First();
                    throw new SnowmanLabsChallengeException(error.Description);
                }

                await _signInManager.SignInAsync(user, false);
                var token = await GenerateJwt(userRegistration.Email);

                return Response(token);
            }
            catch (SnowmanLabsChallengeException slcex)
            {
                return this.Response(slcex);
            }
            catch (Exception ex)
            {
                return this.Response(ex);
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new SnowmanLabsChallengeException("Invalid Model.");
                }

                var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

                if (!result.Succeeded)
                {
                    throw new SnowmanLabsChallengeException("Email or password invalid.");
                }

                var token = await GenerateJwt(userLogin.Email);
                return Response(token);
            }
            catch (SnowmanLabsChallengeException slcex)
            {
                return this.Response(slcex);
            }
            catch (Exception ex)
            {
                return this.Response(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            try
            {
                await _signInManager.SignOutAsync();

                var userId = this.UserId.Value;
                var identityUser = await _userManager.FindByIdAsync(userId.ToString());

                if (identityUser != null)
                {
                    await _userManager.DeleteAsync(identityUser);
                }

                return Response(null);
            }
            catch (SnowmanLabsChallengeException slcex)
            {
                return this.Response(slcex);
            }
            catch (Exception ex)
            {
                return this.Response(ex);
            }
        }

        private async Task<string> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            claims.Add(new Claim("userId", user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAt,
                Expires = DateTime.UtcNow.AddHours(_appSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
