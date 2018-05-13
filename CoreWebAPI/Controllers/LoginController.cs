using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreWebAPI.EF;
using CoreWebAPI.Models;
using CoreWebAPI.Models.Request;
using CoreWebAPI.Models.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        // POST: api/Login
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthRequest auth)
        {
            Courier courier = await _context.Courier.FirstOrDefaultAsync(u => u.Login == auth.Login && u.Password == auth.Password);

            if (courier != null)
                return Ok(await Authenticate(courier)); // аутентификация
            else
            {
                ModelState.AddModelError("error", "Некорректные логин и(или) пароль");
                return BadRequest(ModelState);
            }
        }

        private async Task<AuthResponse> Authenticate(Courier courier)
        {
            CourierToken token = await _context.CourierToken.FirstOrDefaultAsync(tok => tok.CourierID == courier.ID);

            if (token == null)
            {
                token = new CourierToken { Value = Guid.NewGuid().ToString(), DateOfExpire = DateTime.Now.AddDays(3), Courier = courier, CourierID = courier.ID };
                await _context.CourierToken.AddAsync(token);

                await _context.SaveChangesAsync();
            }
            else if (token.DateOfExpire < DateTime.Now)
            {
                token.Value = Guid.NewGuid().ToString();
                token.DateOfExpire = DateTime.Now.AddDays(3);

                _context.CourierToken.Update(token);

                await _context.SaveChangesAsync();
            }

            return new AuthResponse { Token = token.Value, Courier = courier };
        }
    }
}
