using DesafioConcilig.banco;
using MySql.Data.MySqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DesafioConcilig.Controller
{
    internal class ManipuladorArquivos
    {
        public string SelecionarArquivo()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "Arquivos CSV e Excel (*.csv, *.xls, *.xlsx)|*.csv;*.xls;*.xlsx";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return string.Empty;
        }
        /// funcao  para salvar o arquivo selecionado em um diretório específico
        public string SalvarArquivoSelecionado(string caminhoOrigem, string loginUsuario)
        {
            

            string diretorioDestino = "C:/Users/pc/source/repos/DesafioConcilig/DesafioConcilig/ArquivosSalvos";

            if (string.IsNullOrWhiteSpace(caminhoOrigem))
                return "Erro: O caminho de origem não pode ser vazio.";

            if (!File.Exists(caminhoOrigem))
                return "Erro: Arquivo de origem não encontrado.";

            try
            {
                // Verifica se o diretório existe, se não, cria
                if (!Directory.Exists(diretorioDestino))
                {
                    Directory.CreateDirectory(diretorioDestino);
                }

                // Obtém o nome do arquivo do caminho de origem
                string nomeArquivo = Path.GetFileName(caminhoOrigem);
                string caminhoCompletoDestino = Path.Combine(diretorioDestino, nomeArquivo);

                File.Copy(caminhoOrigem, caminhoCompletoDestino, true);

                return $"Arquivo salvo com sucesso em: {caminhoCompletoDestino}";
            }
            catch (Exception ex)
            {
                return $"Erro ao salvar arquivo: {ex.Message}";
            }
        }
        /// funcao para registrar o caminho do arquivo no banco de dados
        public void RegistrarArquivoNoBanco(string caminhoArquivo, string loginUsuario)
        {
            var conexao = new Conexao();
            using (var conexaoBanco = conexao.AbrirConexao())
            {
                if (conexaoBanco == null || conexaoBanco.State != ConnectionState.Open)
                    throw new InvalidOperationException("Conexão não está válida ou aberta.");

                using (var cmd = new MySqlCommand(
                    @"INSERT INTO arquivos (caminho_arquivo, LOGIN_Usuario)
                          VALUES (@caminhoArquivo, @loginUsuario)
                          ON DUPLICATE KEY UPDATE 
                          LOGIN_Usuario = VALUES(LOGIN_Usuario);",
                    conexaoBanco))
                {
                    cmd.Parameters.AddWithValue("@caminhoArquivo", caminhoArquivo);
                    cmd.Parameters.AddWithValue("@loginUsuario", loginUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// funcao para ler o arquivo e salvar no banco de dados
        public void LerESalvarNoBanco(string caminhoOrigem, string loginUsuario)
        {
            var dados = new List<List<string>>();
            string diretorioDestino = "C:/Users/pc/source/repos/DesafioConcilig/DesafioConcilig/ArquivosSalvos";
            string nomeArquivo = Path.GetFileName(caminhoOrigem);
            string caminhoDestino = Path.Combine(diretorioDestino, nomeArquivo); // Caminho onde o arquivo FOI SALVO

            try
            {
                if (caminhoOrigem.EndsWith(".csv"))
                {
                    LerArquivoCsv(caminhoOrigem, dados);
                }
                else if (caminhoOrigem.EndsWith(".xls") || caminhoOrigem.EndsWith(".xlsx"))
                {
                    LerArquivoExcel(caminhoOrigem, dados);
                }
                else
                {
                    MessageBox.Show("Formato de arquivo não suportado.");
                    return;
                }

                if (dados.Count > 0)
                {
                    InserirDadosNoBanco(dados);
                    RegistrarArquivoNoBanco(caminhoDestino, loginUsuario);
                }
                else
                {
                    MessageBox.Show("Nenhum dado válido encontrado no arquivo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar arquivo: {ex.Message}");
            }
        }
        /// funcao para ler o arquivo excel e salvar os dados em uma lista
        public void LerArquivoExcel(string caminhoArquivo, List<List<string>> dados)
        {
            using (var fluxo = new FileStream(caminhoArquivo, FileMode.Open, FileAccess.Read))
            {
                IWorkbook planilha = caminhoArquivo.EndsWith(".xlsx") ?
                    new XSSFWorkbook(fluxo) :
                    new HSSFWorkbook(fluxo);

                ISheet aba = planilha.GetSheetAt(0);

                for (int linha = 1; linha <= aba.LastRowNum; linha++)
                {
                    IRow linhaAtual = aba.GetRow(linha);
                    if (linhaAtual == null) continue;

                    var dadosLinha = new List<string>();
                    for (int coluna = 0; coluna < linhaAtual.LastCellNum; coluna++)
                    {
                        ICell celula = linhaAtual.GetCell(coluna);
                        dadosLinha.Add(celula?.ToString() ?? string.Empty);
                    }

                    if (dadosLinha.Count >= 6)
                    {
                        dados.Add(dadosLinha);
                    }
                }
            }
        }
        /// funcao para ler o arquivo csv e salvar os dados em uma lista
        public void LerArquivoCsv(string caminhoArquivo, List<List<string>> dados)
        {
            using (var leitor = new StreamReader(caminhoArquivo, Encoding.UTF8))
            {
                string linha;
                bool primeiraLinha = true;

                while ((linha = leitor.ReadLine()) != null)
                {
                    if (primeiraLinha)
                    {
                        primeiraLinha = false;
                        continue;
                    }

                    var dadosLinha = linha.Split(';').Select(x => x.Trim()).ToList();
                    if (dadosLinha.Count >= 6)
                    {
                        dados.Add(dadosLinha);
                    }
                }
            }
        }
        /// funcao para inserir os dados no banco de dados
        public void InserirDadosNoBanco(List<List<string>> dados)
        {
            var conexao = new Conexao();
            using (var conexaoBanco = conexao.AbrirConexao())
            {
                if (conexaoBanco == null || conexaoBanco.State != ConnectionState.Open)
                    throw new InvalidOperationException("Conexão não está válida ou aberta.");

                using (var transacao = conexaoBanco.BeginTransaction())
                {
                    try
                    {
                        // Dicionário para armazenar CPFs e seus respectivos IDs de cliente
                        var clientesProcessados = new Dictionary<string, int>();

                        foreach (var linha in dados)
                        {
                            if (linha.Count < 6) continue;

                            string nome = linha[0].Trim();
                            string cpf = linha[1].Trim().Replace(".", "").Replace("-", "");
                            string idContrato = linha[2].Trim();
                            string nomeProduto = linha[3].Trim();

                            if (!DateTime.TryParseExact(linha[4].Trim(), "dd/MM/yyyy",
                                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime vencimento))
                            {
                                continue;
                            }

                            if (!decimal.TryParse(linha[5].Trim(), NumberStyles.Currency,
                                CultureInfo.GetCultureInfo("pt-BR"), out decimal valor))
                            {
                                continue;
                            }

                            if (!clientesProcessados.ContainsKey(cpf))
                            {
                                // Insere ou atualiza o cliente
                                using (var cmdCliente = new MySqlCommand(
                                    @"INSERT INTO clientes (cpf, nome)
                                  VALUES (@cpf, @nome)
                                  ON DUPLICATE KEY UPDATE nome = VALUES(nome);",
                                    conexaoBanco, transacao))
                                {
                                    cmdCliente.Parameters.AddWithValue("@cpf", cpf);
                                    cmdCliente.Parameters.AddWithValue("@nome", nome);
                                    cmdCliente.ExecuteNonQuery();
                                }

                                // Obtém o ID do cliente
                                using (var cmdObterId = new MySqlCommand(
                                    "SELECT id FROM clientes WHERE cpf = @cpf", conexaoBanco, transacao))
                                {
                                    cmdObterId.Parameters.AddWithValue("@cpf", cpf);
                                    clientesProcessados[cpf] = Convert.ToInt32(cmdObterId.ExecuteScalar());
                                }
                            }

                            int clienteId = clientesProcessados[cpf];

                            // Insere o contrato (mesmo que seja do mesmo cliente)
                            using (var cmdContrato = new MySqlCommand(
                                @"INSERT INTO contratos 
                              (id, cliente_id, nome_produto, vencimento, valor)
                              VALUES
                              (@id, @clienteId, @nomeProduto, @vencimento, @valor)
                              ON DUPLICATE KEY UPDATE 
                              cliente_id = VALUES(cliente_id),
                              nome_produto = VALUES(nome_produto),
                              vencimento = VALUES(vencimento),
                              valor = VALUES(valor);",
                                conexaoBanco, transacao))
                            {
                                cmdContrato.Parameters.AddWithValue("@id", idContrato);
                                cmdContrato.Parameters.AddWithValue("@clienteId", clienteId);
                                cmdContrato.Parameters.AddWithValue("@nomeProduto", nomeProduto);
                                cmdContrato.Parameters.AddWithValue("@vencimento", vencimento);
                                cmdContrato.Parameters.AddWithValue("@valor", valor);
                                cmdContrato.ExecuteNonQuery();
                            }
                        }

                        transacao.Commit();
                        MessageBox.Show("Dados importados com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        MessageBox.Show($"Erro ao inserir dados: {ex.Message}");
                        throw;
                    }
                }
            }
        }
    }
}