using Microsoft.AspNetCore.Mvc;
using MvcDoctoresClienteApi.Models;
using MvcDoctoresClienteApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Controllers {
    public class DoctoresController : Controller {

        ServiceApiDoctores service;

        public DoctoresController () {
            service = new ServiceApiDoctores();
        }

        public IActionResult Index () {
            return View();
        }

        public IActionResult DoctoresCliente () {
            return View();
        }

        public async Task<IActionResult> DoctoresServidor () {
            List<Doctor> doctores = await this.service.GetDoctoresAsync();
            List<String> especialidades = await this.service.GetEspecialidadesAsync();
            ViewData["Especialidades"] = especialidades;
            return View(doctores);
        }

        [HttpPost]
        public async Task<IActionResult> DoctoresServidor (String especialidad) {
            List<Doctor> doctores = await this.service.GetDoctoresEspecialidadAsync(especialidad);
            List<String> especialidades = await this.service.GetEspecialidadesAsync();
            ViewData["Especialidades"] = especialidades;
            return View(doctores);
        }

        public async Task<IActionResult> DoctoresDetalle (int idDoctor) {
            Doctor doctor = await this.service.GetDoctorAsync(idDoctor);
            return View(doctor);
        }
    }
}
