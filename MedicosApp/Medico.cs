using System;
using System.ComponentModel;

namespace MedicosApp
{
    public class Medico
    {
        [DisplayName("DNI")]
        public string Dni { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Apellido")]
        public string Apellido { get; set; }

        public override string ToString()
        {
            return $"{Dni} - {Apellido}, {Nombre}";
        }
    }
}
