using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Models {
    [Table("EMP")]
    public class Empleado {

        [Key]
        [Column("EMP_NO")]
        public int IdEmpleado { get; set; }
        [Column("APELLIDO")]
        public String Apellido { get; set; }
        [Column("OFICIO")]
        public String Oficio { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
        [Column("DIR")]
        public int Director { get; set; }

        public Empleado () { }

        public Empleado (int idEmpleado, string apellido,
            string oficio, int salario, int director) {
            IdEmpleado = idEmpleado;
            Apellido = apellido;
            Oficio = oficio;
            Salario = salario;
            Director = director;
        }
    }
}
