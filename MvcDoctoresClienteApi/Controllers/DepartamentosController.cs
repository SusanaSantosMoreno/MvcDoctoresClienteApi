using Microsoft.AspNetCore.Mvc;
using MvcDoctoresClienteApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Controllers {
    public class DepartamentosController : Controller {

        ServiceApiDepartamentos serviceApi; 

        public DepartamentosController(ServiceApiDepartamentos service) {
            this.serviceApi = service;
        }

        public IActionResult Index () {
            return View();
        }

        public IActionResult ApiCRUDClienteAjax () {
            return View();
        }

        public async Task<IActionResult> ListaDepartamentos () {
            return View(await this.serviceApi.GetDepartamentosAsync());
        }

        public async Task<IActionResult> DetailsDepartamentos (int id) {
            return View(await this.serviceApi.BuscarDepartamentoAsync(id));
        }

        public async Task<IActionResult> EditDepartamentos (int id) {
            return View(await this.serviceApi.BuscarDepartamentoAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditDepartamentos (int IdDepartamento, String nombre, String localidad) {
            await this.serviceApi.EditDepartamentoAsync(IdDepartamento, nombre, localidad);
            return RedirectToAction("ListaDepartamentos");
        }

        public IActionResult CreateDepartamentos () {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartamentos (int IdDepartamento, String nombre, String localidad) {
            await this.serviceApi.InsertDepartamentoAsync(IdDepartamento, nombre, localidad);
            return RedirectToAction("ListaDepartamentos");
        }

        public async Task<IActionResult> DeleteDepartamentos(int id) {
            await this.serviceApi.DeleteDepartamentoAsync(id);
            return RedirectToAction("ListaDepartamentos");
        }
    }
}
