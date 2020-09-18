using System;
using System.Windows.Forms;
using Dominio;
namespace Presentacion
{
    public partial class FormRecoverPassword : Form
    {
        public FormRecoverPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = new UserModel();
            var result = user.recoverPassword(txtRecoverPas.Text);
            label2.Text = result;
        }
    }
}
