using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Dominio;

namespace Presentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        //libreria para mover
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void Login_Enter(object sender, EventArgs e)
        {
            if (txtUsr.Text == "Usuario")
            {
                txtUsr.Text = "";
                txtUsr.ForeColor = Color.LightGray;
            }
        }

        private void Login_Leave(object sender, EventArgs e)
        {
            if (txtUsr.Text == "")
            {
                txtUsr.Text = "Usuario";
                txtUsr.ForeColor = Color.DimGray;
            }
        }

        private void txtContra_Enter(object sender, EventArgs e)
        {
            if (txtContra.Text == "Contraseña")
            {
                txtContra.Text = "";
                txtContra.ForeColor = Color.LightGray;
                txtContra.UseSystemPasswordChar = true;
            }
        }

        private void txtContra_Leave(object sender, EventArgs e)
        {
            if (txtContra.Text == "")
            {
                txtContra.Text = "Contraseña";
                txtContra.ForeColor = Color.DimGray;
                txtContra.UseSystemPasswordChar = false;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //evento mover interfaz
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //evento cuando el cursor esta dentro de la caja de texto
        private void txtUsr_Enter(object sender, EventArgs e)
        {
            if (txtUsr.Text == "Usuario")
            {
                txtUsr.Text = "";
                txtUsr.ForeColor = Color.LightGray;
            }
        }
        //evento cuando el cursor sale del cuadro de texto
        private void txtUsr_Leave(object sender, EventArgs e)
        {
            if (txtUsr.Text == "")
            {
                txtUsr.Text = "Usuario";
                txtUsr.ForeColor = Color.DimGray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsr.Text != "Usuario")
            {
                if (txtContra.Text != "Contraseña")
                {
                    UserModel user = new UserModel();
                    var validarLogin = user.LoginUsers(txtUsr.Text, txtContra.Text);
                    if (validarLogin==true)
                    {
                        this.Hide();
                        FormWelcome welcome = new FormWelcome();
                        welcome.ShowDialog();
                        Menu menup = new Menu();
                        menup.Show();
                        menup.FormClosed += Salir;
                    }
                    else
                    {
                        msError("Usuario y contraseña incorrecta, por favor intenta de nuevo");
                        this.txtContra.Text = "Contraseña";
                        this.txtUsr.Focus();
                    }
                }
                else msError("Porfavor ingrese la contraseña");
            }
            else msError("Porfavor ingreso el usuario");
        }
        private void msError(string ms)
        {
            //lblMensageError.Text = "    " + ms;
            lblMensageError.Text = ms;
            lblMensageError.Visible = true;
        }

        private void Salir(object sender, FormClosedEventArgs e)
        {
            txtContra.Text = "Contraseña";
            txtContra.ForeColor = Color.DimGray;
            txtContra.UseSystemPasswordChar = false;
            txtUsr.Text = "Usuario";
            txtUsr.ForeColor = Color.DimGray;
            lblMensageError.Visible = false;
            this.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var recoverPass = new FormRecoverPassword();
            recoverPass.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //var recoverPass = new FormRecoverPassword();
            //recoverPass.ShowDialog();
        }
    }
}
