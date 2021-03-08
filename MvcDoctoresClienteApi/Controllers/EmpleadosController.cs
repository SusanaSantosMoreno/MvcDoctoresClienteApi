using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcDoctoresClienteApi.Filters;
using MvcDoctoresClienteApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Controllers {
    public class EmpleadosController : Controller {

        ServiceEmpleados service;

        public EmpleadosController(ServiceEmpleados serv) {
            this.service = serv;
        }
        [EmpleadoAuthorize]
        public async Task<IActionResult> PerfilEmpleado () {
            string token = HttpContext.Session.GetString("TOKEN");
            return View(await this.service.GetPerfil(token));
        }

        public async Task<IActionResult> Subordinados () {
            String token = HttpContext.Session.GetString("TOKEN");
            return View(await this.service.GetSubordinados(token));
        }
    }
}
