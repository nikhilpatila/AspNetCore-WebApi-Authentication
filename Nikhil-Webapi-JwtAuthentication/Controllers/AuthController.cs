using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Nikhil_Webapi_JwtAuthentication.Controllers
{
    [Route("api/{controller}")]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("token")]
        public IActionResult GetToken()
        {
            //security key
            var securityKey =
                "this_is_our_super_long_security_key_for_token_validation_project_2020_02_14_nikhilpatila@gmail.com";

            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //sign-In credentials
            var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            claims.Add(new Claim("Our_Custom_Claim", "our custom value"));
            claims.Add(new Claim("Our_Custom_Claim_2", "another custom value"));

            //create token
            var token = new JwtSecurityToken(
                issuer: "nikhil.myFirm.in",
                audience:"readers",
                expires:DateTime.Now.AddHours(1),
                signingCredentials:signInCredentials,
                claims: claims
                );

            //return token
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}