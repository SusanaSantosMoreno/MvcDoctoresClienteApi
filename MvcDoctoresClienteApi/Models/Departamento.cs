using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Models {
    public class Departamento {

        public int IdDepartamento { get; set; }
        public String Nombre { get; set; }
        public String Localidad { get; set; }

        public Departamento () {}

        public Departamento (int idDepartamento, string nombre, string localidad) {
            IdDepartamento = idDepartamento;
            Nombre = nombre;
            Localidad = localidad;
        }
    }
}
