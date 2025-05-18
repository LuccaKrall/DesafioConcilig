using DesafioConcilig.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesafioConcilig.views
{
    public partial class Login : Form
    {
   
        public Login()
        {
            InitializeComponent();
        }
        public static class Session
        {
            public static string UsuarioLogado { get; set; }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Funcao para validacao de Login do usuario, ele vai utilizar as informacoes passadas nos campos do form Login, cria um novo objeto da classse Controlador Login
            //que armazena a funcao que ira buscar no banco de dados e validar ou nao o Login
            Controller.Login login = new Controller.Login();
            if (login.ValidarLogin(textBox1.Text, textBox2.Text))
            {
                Session.UsuarioLogado = textBox1.Text;
                Form2 novoForm = new Form2(Session.UsuarioLogado);//este codigo e para poder levar os dados colocados de login no proximo formulario, para assim conseguir saber quem esta inserindo dados no banco
                novoForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login ou senha inválidos!");
            }
        }
    }
}
