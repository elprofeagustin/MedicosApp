using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MedicosApp
{
    public static class JsonHelper
    {
        // Archivo en la carpeta de la aplicación
        private static readonly string Archivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "medicos.json");

        public static void GuardarLista(List<Medico> medicos)
        {
            try
            {
                string json = JsonConvert.SerializeObject(medicos, Formatting.Indented);
                File.WriteAllText(Archivo, json);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al guardar el archivo JSON:\n" + ex.Message);
            }
        }

        public static List<Medico> LeerLista()
        {
            try
            {
                if (!File.Exists(Archivo))
                    return new List<Medico>();

                string json = File.ReadAllText(Archivo);
                var lista = JsonConvert.DeserializeObject<List<Medico>>(json);
                return lista ?? new List<Medico>();
            }
            catch
            {
                return new List<Medico>();
            }
        }

        public static string ObtenerRutaArchivo()
        {
            return Archivo;
        }
    }
}
