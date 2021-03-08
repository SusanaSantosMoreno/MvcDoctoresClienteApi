using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcDoctoresClienteApi.Models;
using MvcDoctoresClienteApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Controllers {
    public class IdentityController : Controller {

        ServiceEmpleados service;

        public IdentityController (ServiceEmpleados serv) {
            this.service = serv;
        }

        public IActionResult Login () {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(String userName, String password) {
            String token = await this.service.getToken(userName, password);
            if(token == null) {
                ViewData["MENSAJE"] = "Usuario/Contreseña incorrectos";
                return View();
            } else {
                Empleado empleado = await this.service.GetPerfil(token);
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, empleado.IdEmpleado.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, empleado.Apellido.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, empleado.Oficio.ToString()));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.Now.AddMinutes(15)
                        });

                //para poder trabajar necesitamos almacenar el token para las peticiones.
                HttpContext.Session.SetString("TOKEN", token);
                return RedirectToAction("PerfilEmpleado", "Empleados");
            }
        }

        public async Task<IActionResult> Logout () {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if(HttpContext.Session.GetString("TOKEN") != null) {
                HttpContext.Session.Remove("TOKEN");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
