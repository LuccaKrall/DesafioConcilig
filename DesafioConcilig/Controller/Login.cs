using DesafioConcilig.banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioConcilig.Controller
{
    internal class Login
    {
        public Boolean ValidarLogin(string usuario, string senha)
        {
            Conexao conexao = new Conexao();
            var conn = conexao.AbrirConexao(); 
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT COUNT(*) FROM usuario WHERE LOGIN=@LOGIN and SENHA=@SENHA";
                cmd.Parameters.AddWithValue("@LOGIN", usuario);
                cmd.Parameters.AddWithValue("@SENHA", senha);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return count > 0;
            }
        }


    }
}
