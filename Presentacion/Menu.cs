using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CapaTransversal.Cache;
namespace Presentacion
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        #region funciones del formulario
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;


        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.panelContenedor.Region = region;
            this.Invalidate();
        }

        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            //activa rayas para minimizar
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        //METODO PARA ARRASTRAR EL FORMULARIO---------------------------------------------------------------------
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //METODO PARA ABRIR FORM DENTRO DE PANEL-----------------------------------------------------
        private void AbrirFormEnPanel<Forms>() where Forms : Form, new()
        {
            Form formulario;
            formulario = panelContenedor.Controls.OfType<Forms>().FirstOrDefault();

            //si el formulario/instancia no existe, creamos nueva instancia y mostramos
            if (formulario == null)
            {
                formulario = new Forms();
                formulario.TopLevel = false;
                //formulario.FormBorderStyle = FormBorderStyle.None;
                //formulario.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(formulario);
                panelContenedor.Tag = formulario;
                formulario.Show();

                formulario.BringToFront();
                // formulario.FormClosed += new FormClosedEventHandler(CloseForms);               
            }
            else
            {

                //si la Formulario/instancia existe, lo traemos a frente
                formulario.BringToFront();

                //Si la instancia esta minimizada mostramos
                if (formulario.WindowState == FormWindowState.Minimized)
                {
                    formulario.WindowState = FormWindowState.Normal;
                }
            }
        }

        //METODO PARA ABRIR FORMULARIOS DENTRO DEL PANEL
        //generico
        private void abrirFormulario<miForm>() where miForm : Form, new()
        {
            Form formulari;
            formulari = panelFormularios.Controls.OfType<miForm>().FirstOrDefault();//busca en la conexion el formulario
            //si el formulario/instaccia no existe
            if (formulari == null)
            {
                formulari = new miForm();
                formulari.TopLevel = false;

                //aqui para mostrar o no los bordes de los formularios dentro del panel
                formulari.FormBorderStyle = FormBorderStyle.None;
                formulari.Dock = DockStyle.Fill;

                panelFormularios.Controls.Add(formulari);
                panelFormularios.Tag = formulari;
                formulari.Show();
                formulari.BringToFront();
                formulari.FormClosed += new FormClosedEventHandler(CloseForms);
            }//si existe
            else
            {
                formulari.BringToFront();
            }
        }
        private void CloseForms(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form2"] == null)
                btnAdministracion.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["Form3"] == null)
                btnContador.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["Form4"] == null)
                btnProgramacion.BackColor = Color.FromArgb(4, 41, 68);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            abrirFormulario<Trabajos>();
            btnAdministracion.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            abrirFormulario<Clientes>();
            btnContador.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abrirFormulario<Proveedores>();
            btnProgramacion.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.btnMaximizar.Visible = true;
            this.btnRestaurar.Visible = false;

            this.Size = new Size(sw, sh);
            this.Location = new Point(ly, lx);
        }

        int lx, ly, sw, sh;

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estas seguro que quieres cerrar la aplicación?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }
   

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;

            this.btnMaximizar.Visible = false;
            this.btnRestaurar.Visible = true;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            loadUserData();
            manejarPermisos();
        }

        private void manejarPermisos()
        {
            //permisos
            if (UserLoginCache.position == Positions.Administrador)
            {
                btnProgramacion.Enabled = false;
            }
            if (UserLoginCache.position == Positions.Ingeniero)
            {
                btnAdministracion.Enabled = false;
                btnContador.Enabled = false;
            }
            if (UserLoginCache.position == Positions.Contador)
            {
                btnProgramacion.Enabled = false;
            }
        }

        private void loadUserData()
        {
            lblNombre.Text = UserLoginCache.firstName + ", " + UserLoginCache.lastName;
            lblPosicion.Text = UserLoginCache.position;
            lblEmail.Text = UserLoginCache.email;
        }
    }
}
