using System;
using System.Windows.Forms;
using CapaTransversal.Cache;

namespace Presentacion
{
    public partial class FormWelcome : Form
    {
        public FormWelcome()
        {
            InitializeComponent();
        }
        //el primer minutero se encarga de aparecer gradualmente
        //int contador = 0;//controla el tiempo 
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.05;
            circularProgressBar1.Value += 1;//cada ciclo aumentara en 1
            circularProgressBar1.Text = circularProgressBar1.Value.ToString();
            if (circularProgressBar1.Value == 100)
            {
                timer1.Stop();
                timer2.Start();//si el contador llega a 100 iniciamos el minutero 2
            }
        }
        //el segundo se encargara de desvanecer el formulario y cerrarlo
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
            if (this.Opacity==0)
            {
                timer2.Stop();
                this.Close();
            }
        }

        private void FormWelcome_Load(object sender, EventArgs e)
        {
            lblUsername.Text = UserLoginCache.firstName + ", " + UserLoginCache.lastName;
            this.Opacity = 0.0;//inicializamos la opacidad en 0
            
            circularProgressBar1.Value = 0;
            circularProgressBar1.Maximum = 100;
            circularProgressBar1.Minimum = 0;
            timer1.Start();//inicializamos el minutero 1
        }
        //menos o mas tiempo en el intervalo
    }
}
