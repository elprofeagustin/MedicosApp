using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MedicosApp
{
    public partial class FormListado : Form
    {
        private List<Medico> _medicos;

        public FormListado()
        {
            InitializeComponent();
            CargarYMostrar();
        }

        private void CargarYMostrar()
        {
            _medicos = JsonHelper.LeerLista();
            dgvListado.DataSource = null;
            dgvListado.DataSource = _medicos;
            dgvListado.AutoResizeColumns();
            lblCantidad.Text = $"Registros: {_medicos.Count}";
            lblRuta.Text = $"Archivo: {JsonHelper.ObtenerRutaArchivo()}";
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarYMostrar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
