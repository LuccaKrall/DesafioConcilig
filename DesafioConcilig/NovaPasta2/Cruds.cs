using DesafioConcilig.banco;
using DesafioConcilig.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DesafioConcilig.NovaPasta2
{
    internal class Cruds
    {
        public List<Contrato> ObterTodosContratos()
        {
            List<Contrato> contratos = new List<Contrato>();

            Conexao conexao = new Conexao();
            using (var conn = conexao.AbrirConexao())
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM contratos", conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contrato contrato = new Contrato
                            {
                                Id = reader.GetInt32("id"),
                                Cliente_id = reader.GetInt32("Cliente_id"),
                                nome_produto = reader.GetString("nome_produto"),
                                vencimento = reader.GetDateTime("vencimento"),
                                valor = reader.GetDecimal("valor")
                            };

                            contratos.Add(contrato);
                        }
                    }
                }
            }

            return contratos;
        }

        public (int Quantidade, decimal ValorTotal, string ContratoAtrasado, int DiasAtraso, decimal ValorAtrasado)
    ObterEstatisticasComAtrasoPorNomeCliente(string nomeCliente)
        {
            int quantidade = 0;
            decimal valorTotal = 0;
            string contratoAtrasado = null;
            int diasAtraso = 0;
            decimal valorAtrasado = 0;
            var dataAtual = DateTime.Now;

            Conexao conexao = new Conexao();
            using (var conn = conexao.AbrirConexao())
            {
                // Query para estatísticas 
                string query = @"
            SELECT 
                COUNT(c.id) AS Quantidade,
                SUM(c.valor) AS ValorTotal
            FROM contratos c
            INNER JOIN clientes cli ON c.cliente_id = cli.id
            WHERE cli.nome = @nomeCliente";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nomeCliente", nomeCliente);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            quantidade = reader.GetInt32("Quantidade");
                            valorTotal = reader.GetDecimal("ValorTotal");
                        }
                    }
                }

                // Query para achar o contrato mais atrasado
                string queryAtraso = @"
            SELECT 
                c.id AS Contrato,
                c.valor AS Valor,
                DATEDIFF(@dataAtual, c.vencimento) AS DiasAtraso
            FROM contratos c
            INNER JOIN clientes cli ON c.cliente_id = cli.id
            WHERE cli.nome = @nomeCliente
            AND c.vencimento < @dataAtual
            ORDER BY DiasAtraso DESC
            LIMIT 1";

                using (MySqlCommand cmdAtraso = new MySqlCommand(queryAtraso, conn))
                {
                    cmdAtraso.Parameters.AddWithValue("@nomeCliente", nomeCliente);
                    cmdAtraso.Parameters.AddWithValue("@dataAtual", dataAtual);

                    using (var reader = cmdAtraso.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            contratoAtrasado = reader["Contrato"].ToString();
                            valorAtrasado = reader.GetDecimal("Valor");
                            diasAtraso = reader.GetInt32("DiasAtraso");
                        }
                    }
                }
            }

            return (quantidade, valorTotal, contratoAtrasado, diasAtraso, valorAtrasado);
        }

        public List<ArquivoImportado> ObterArquivosImportados()
        {
            var arquivos = new List<ArquivoImportado>();

            var conexao = new Conexao();
            using (var conn = conexao.AbrirConexao())
            {
                string query = @"SELECT LOGIN_Usuario, caminho_arquivo 
                        FROM arquivos 
                        ORDER BY LOGIN_Usuario, caminho_arquivo";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            arquivos.Add(new ArquivoImportado
                            {
                                LoginUsuario = reader["LOGIN_Usuario"].ToString(),
                                CaminhoArquivo = reader["caminho_arquivo"].ToString()
                            });
                        }
                    }
                }
            }
            return arquivos;
        }
    }
}
