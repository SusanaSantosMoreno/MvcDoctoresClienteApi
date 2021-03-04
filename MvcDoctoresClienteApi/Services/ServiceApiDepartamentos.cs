using MvcDoctoresClienteApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Services {
    public class ServiceApiDepartamentos {

        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiDepartamentos(String url) {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApi<T>(String request) {
            using(HttpClient client = new HttpClient()) {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode) {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                } else {
                    //DEVUELVE NULL EN CASO DE OBJETOS O 0 EN CASO DE PRIMITIVOS
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync () {
            String request = "api/Departamentos";
            List<Departamento> departamentos = await this.CallApi<List<Departamento>>(request);
            return departamentos;
        }

        public async Task<Departamento> BuscarDepartamentoAsync (int id) {
            String request = "api/Departamentos/" + id;
            Departamento departamento = await this.CallApi<Departamento>(request);
            return departamento;
        }
    
        public async Task DeleteDepartamentoAsync(int id) {
            using(HttpClient client = new HttpClient()) {
                String request = "api/Departamentos/" + id;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                await client.DeleteAsync(request);
            }
        }

        public async Task InsertDepartamentoAsync(int id, String nombre, String localidad) {
            using(HttpClient client = new HttpClient()) {
                String request = "api/Departamentos";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Departamento dept = new Departamento(id, nombre, localidad);
                String json = JsonConvert.SerializeObject(dept);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }
    
        public async Task EditDepartamentoAsync(int id, String nombre, String localidad) {
            using (HttpClient client = new HttpClient()) {
                String request = "api/Departamentos";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Departamento dept = new Departamento(id, nombre, localidad);
                String json = JsonConvert.SerializeObject(dept);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }
    }
}
