using System;
using System.Windows.Forms;
using Dominio;
using CapaTransversal.Cache;
namespace Presentacion
{
    public partial class usuarioEdit : Form
    {
        public usuarioEdit()
        {
            InitializeComponent();
        }

        private void usuarioEdit_Load(object sender, EventArgs e)
        {
            loadUserData();
            initializePassEditControls();
        }
        
        private void loadUserData()
        {
            //View
            lblUser.Text = UserLoginCache.LoginName;
            lblName.Text = UserLoginCache.firstName;
            lblLastName.Text = UserLoginCache.lastName;
            lblPosition.Text = UserLoginCache.position;
            //Edit Panel
            txtUsername.Text = UserLoginCache.LoginName;
            txtFirstName.Text = UserLoginCache.firstName;
            txtLastName.Text = UserLoginCache.lastName;
            txtEmail.Text = UserLoginCache.email;
            txtPassword.Text = UserLoginCache.Password;
            txtConfirmPass.Text = UserLoginCache.Password;
            txtCurrentPassword.Text = "";
        }
        private void initializePassEditControls()
        {
            LinkEditPass.Text = "Edit";
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.Enabled = false;
            txtConfirmPass.UseSystemPasswordChar = true;
            txtConfirmPass.Enabled = false;
        }
        private void reset()
        {
            loadUserData();
            initializePassEditControls();
        }
        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Panel1.Visible = true;
            loadUserData();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length >= 5)
            {
                if (txtPassword.Text == txtConfirmPass.Text)
                {
                    if (txtCurrentPassword.Text == UserLoginCache.Password)
                    {
                        var userModel = new UserModel(
                            idUser: UserLoginCache.idUser,
                            loginName: txtUsername.Text,
                            password: txtPassword.Text,
                            firstName: txtFirstName.Text,
                            lastName: txtLastName.Text,
                            position: null,
                            email: txtEmail.Text);
                        var result = userModel.editUserProfile();
                        MessageBox.Show(result);
                        reset();
                        Panel1.Visible = false;
                    }
                    else
                        MessageBox.Show("Icorrect current password, try again");
                }
                else
                    MessageBox.Show("The password do not match, try again");
            }
            else
                MessageBox.Show("The password must have a minimum of 5 characters");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel1.Text == "Edit")
            {
                LinkEditPass.Text = "Cancel";
                txtPassword.Enabled = true;
                txtPassword.Text = "";
                txtConfirmPass.Enabled = true;
                txtConfirmPass.Text = "";
            }
            else if (LinkEditPass.Text == "Cancel")
            {
                initializePassEditControls();
                txtPassword.Text = UserLoginCache.Password;
                txtConfirmPass.Text = UserLoginCache.Password;
            }
        }
    }
}
