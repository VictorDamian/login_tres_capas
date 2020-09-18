using System;
using System.Drawing;
using System.Windows.Forms;
using Dominio;

namespace Presentacion
{
    public partial class Trabajos : Form
    {
        CN_Trabajos objetoTra = new CN_Trabajos();
        private string idTrabajo=null;
        private bool editar = false;
        public Trabajos()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mostrarTra();
        }

        public void mostrarTra()
        {
            CN_Trabajos objetoTr = new CN_Trabajos();
            dataGridView1.DataSource = objetoTr.Mostrar();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtAnticipo.Text != "")
            {
                //INSERTAR
                if (editar == false)
                {
                    try
                    {

                        objetoTra.insertarTra(txtNombre.Text, txtNumero.Text, txtDireccion.Text, txtTrabajo.Text, txtAnticipo.Text, txtSaldo.Text, txtTotal.Text, txtFecha.Text);
                        MessageBox.Show("Se ha registrado correctamente");
                        mostrarTra();
                        limpiarCampos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo insertar por: " + ex);
                    }
                }
                //EDITAR
                if (editar == true)
                {
                    try
                    {
                        objetoTra.editarTra(txtNombre.Text, txtNumero.Text, txtDireccion.Text, txtTrabajo.Text, txtAnticipo.Text, txtSaldo.Text, txtTotal.Text, txtFecha.Text, idTrabajo);
                        MessageBox.Show("Se ha registrado correctamente");
                        mostrarTra();
                        editar = false;
                        limpiarCampos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo editar por: " + ex);
                    }
                }
            }
            else
            {
                MessageBox.Show("Faltan datos");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                editar = true;
                txtNombre.Text = dataGridView1.CurrentRow.Cells["nom"].Value.ToString();
                txtNumero.Text = dataGridView1.CurrentRow.Cells["num"].Value.ToString();
                txtDireccion.Text = dataGridView1.CurrentRow.Cells["dire"].Value.ToString();
                txtTrabajo.Text = dataGridView1.CurrentRow.Cells["tra"].Value.ToString();
                txtAnticipo.Text = dataGridView1.CurrentRow.Cells["anti"].Value.ToString();
                txtSaldo.Text = dataGridView1.CurrentRow.Cells["sal"].Value.ToString();
                txtTotal.Text = dataGridView1.CurrentRow.Cells["total"].Value.ToString();
                txtFecha.Text = dataGridView1.CurrentRow.Cells["fecha"].Value.ToString();
                idTrabajo = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Seleccione una fila");
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Clear();
            txtNumero.Clear();
            txtDireccion.Clear();
            txtTrabajo.Clear();
            txtAnticipo.Clear();
            txtSaldo.Clear();
            txtTotal.Clear();
            //txtFecha.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                idTrabajo = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                objetoTra.eliminarTra(idTrabajo);
                MessageBox.Show("Se ha eliminado");
                limpiarCampos();
                mostrarTra();
            }
            else
            {
                MessageBox.Show("seleccione el registro");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }
        #region METODO PARA COLOR EN LAS CELDAS
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "total")
            {
                if (Convert.ToInt32(e.Value) >= 200)
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(e.Value) <= 100) 
                {
                    e.CellStyle.ForeColor = Color.BlueViolet;
                    e.CellStyle.BackColor = Color.Gray;
                }
            }
        }
        #endregion
    }
}
