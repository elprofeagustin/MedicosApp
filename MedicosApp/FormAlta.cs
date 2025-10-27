using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MedicosApp
{
    public partial class FormAlta : Form
    {
        private List<Medico> _medicos;

        public FormAlta()
        {
            InitializeComponent();
            _medicos = JsonHelper.LeerLista();
            RefrescarGrilla();
            lblRutaArchivo.Text = $"Archivo: {JsonHelper.ObtenerRutaArchivo()}";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones simples
            string dni = txtDni.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();

            if (string.IsNullOrWhiteSpace(dni) ||
                string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("Complete DNI, Nombre y Apellido.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Evitar duplicados por DNI (opcional)
            if (_medicos.Exists(m => m.Dni == dni))
            {
                var resp = MessageBox.Show("Ya existe un médico con ese DNI. ¿Desea agregarlo igual?", "Duplicado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.No) return;
            }

            var medico = new Medico
            {
                Dni = dni,
                Nombre = nombre,
                Apellido = apellido
            };

            _medicos.Add(medico);
            JsonHelper.GuardarLista(_medicos);
            RefrescarGrilla();
            LimpiarEntradas();
        }

        private void RefrescarGrilla()
        {
            dgvMedicos.DataSource = null;
            dgvMedicos.DataSource = _medicos;
            dgvMedicos.AutoResizeColumns();
        }

        private void LimpiarEntradas()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDni.Focus();
        }

        private void btnAbrirListado_Click(object sender, EventArgs e)
        {
            var listado = new FormListado();
            listado.ShowDialog(this);
            // Al volver, recargo por si se modificó el archivo desde allí
            _medicos = JsonHelper.LeerLista();
            RefrescarGrilla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection c = new SqlConnection("server=DESKTOP-EIOE9O2 ; database=db_medina ; integrated security = true");
            c.Open();
            string nombre = textBox1.Text;
            string autor = textBox2.Text;
            int año = (int)numericUpDown1.Value;
            float precio = (float)numericUpDown2.Value;

            string cadena = "INSERT INTO Libros (nombre, autor, año, precio) VALUES ('"
                 + nombre + "', '"
                 + autor + "', "
                 + año + ", "
                 + precio + ")";

            SqlCommand mi_instruccion = new SqlCommand(cadena, c);
            mi_instruccion.ExecuteNonQuery();
            MessageBox.Show("Los datos se guardaron correctamente");
            c.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection("server=DESKTOP-EIOE9O2 ; database=db_medina ; integrated security = true");
                c.Open();

                string mi_instruccion = "select nombre,año,precio from Libros";

                SqlDataAdapter adaptador = new SqlDataAdapter(mi_instruccion, c);

                DataTable tabla = new DataTable();

                adaptador.Fill(tabla);

                dataGridView1.DataSource = tabla;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message + "Error desde el servidor");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            Stack<int> mis_datos = new Stack<int>();
            mis_datos.Push(20);
            mis_datos.Push(41);

            int total = mis_datos.Pop();
            MessageBox.Show($"{total}");
            
            Queue<string> mis_datos= new Queue<string>();
            mis_datos.Enqueue("juan");
            mis_datos.Enqueue("pedro");
            */

            List<Medico> mi_lista = new List<Medico>();
            Medico m1 = new Medico();
            Medico m2 = new Medico();
            Medico m3 = new Medico();
         
            mi_lista.Add(m1);
            mi_lista.Add(m2);
            mi_lista.Add(m3);

            //mi_lista.Sort();

            foreach (Medico item in mi_lista)
            {
                MessageBox.Show($"{item}");
            }



        }
    }
}
