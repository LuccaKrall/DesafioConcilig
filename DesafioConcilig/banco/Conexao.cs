using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace DesafioConcilig.banco
{
    internal class Conexao
    {
        private string conexao = "Server=localhost;Database=concilig;User ID=root;Password=123;";

        public MySqlConnection AbrirConexao()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(conexao);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao abrir a conexão: {ex.Message}");
                return null;
            }
        }

    }
}
