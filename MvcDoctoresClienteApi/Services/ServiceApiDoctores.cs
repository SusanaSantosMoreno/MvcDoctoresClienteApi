using MvcDoctoresClienteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Services {
    public class ServiceApiDoctores {
        private String url;
        MediaTypeWithQualityHeaderValue header;

        public ServiceApiDoctores () {
            this.url = "https://apihospitalssm.azurewebsites.net/";
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Doctor>> GetDoctoresAsync () {
            using(HttpClient client = new HttpClient()) {
                String request = "api/Doctores";
                client.BaseAddress = new Uri(this.url);
                //Elimina las cabeceras cada la petición
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode) {//peticion correcta
                    List<Doctor> doctores = await response.Content.ReadAsAsync<List<Doctor>>();
                    return doctores;
                } else {//petición incorrecta
                    return null;
                }
            }
        }

        public async Task<List<String>> GetEspecialidadesAsync () {
            using(HttpClient client = new HttpClient()) {
                String request = "api/Doctores/GetEspecialidades";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode) {
                    List<String> especialidades = await response.Content.ReadAsAsync<List<String>>();
                    return especialidades;
                } else {
                    return null;
                }
            }
        }

        public async Task<List<Doctor>> GetDoctoresEspecialidadAsync(String especialidad) {
            using(HttpClient client = new HttpClient()) {
                String request = "api/Doctores/GetDoctoresEspecialidad/"+ especialidad;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode) {
                    List<Doctor> doctores = await response.Content.ReadAsAsync<List<Doctor>>();
                    return doctores;
                } else {
                    return null;
                }
            }
        }

        public async Task<Doctor> GetDoctorAsync (int idDoctor) {
            using(HttpClient client = new HttpClient()) {
                String request = "api/Doctores/" + idDoctor;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode) {
                    Doctor doctor = await response.Content.ReadAsAsync<Doctor>();
                    return doctor;
                } else {
                    return null;
                }
            }
        }
    }
}
