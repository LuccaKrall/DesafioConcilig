using DesafioConcilig.Controller;
using DesafioConcilig.NovaPasta2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
namespace DesafioConcilig.views
{
    public partial class Form2 : Form
    {
        private string _loginUsuario; // Armazena o login do usuário logado

        public Form2(string loginUsuario)
        {
            InitializeComponent();
            _loginUsuario = loginUsuario;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Botão para selecionar arquivo
            ManipuladorArquivos manipulador = new ManipuladorArquivos();
            string resultado = manipulador.SelecionarArquivo();
            label3.Text = resultado; // Exibe o caminho do arquivo selecionado
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Botão para salvar arquivo (sem registrar no banco de dados , salvamento local do arquivo)
            ManipuladorArquivos manipulador = new ManipuladorArquivos();
            string caminhoArquivo = label3.Text;

            if (!string.IsNullOrEmpty(caminhoArquivo))
            {
                string resultado = manipulador.SalvarArquivoSelecionado(caminhoArquivo, _loginUsuario);
                MessageBox.Show(resultado);
            }
            else
            {
                MessageBox.Show("Nenhum arquivo selecionado.");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label3.Text))
            {
                MessageBox.Show("Por favor, selecione um arquivo primeiro.");
                return;
            }

            try
            {
                ManipuladorArquivos manipulador = new ManipuladorArquivos();
                string caminhoOrigem = label3.Text;
                string diretorioDestino = "C:/Users/pc/source/repos/DesafioConcilig/DesafioConcilig/ArquivosSalvos";
                string nomeArquivo = Path.GetFileName(caminhoOrigem);
                string caminhoDestino = Path.Combine(diretorioDestino, nomeArquivo); // Caminho de destino

                // Chama o método com o caminho de DESTINO passando os paramentros necessarios da funcao
                manipulador.LerESalvarNoBanco(caminhoOrigem, _loginUsuario); 
                MessageBox.Show("Dados importados e caminho registrado no banco com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar no banco: {ex.Message}");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Evento do label (opcional)
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        // ...

        private void button2_Click(object sender, EventArgs e)
        {
            NovaPasta2.Cruds crud = new NovaPasta2.Cruds();
            List<DesafioConcilig.Modelos.Contrato> contratos = crud.ObterTodosContratos();

            StringBuilder mensagem = new StringBuilder();

            foreach (var contrato in contratos)
            {
                //forma a lista vinda do banco de dados, ira percorrer ate o final da lista e mostrar os dados de cada contrato
                mensagem.AppendLine($"ID: {contrato.Id}");
                mensagem.AppendLine($"ID do Cliente: {contrato.Cliente_id}");
                mensagem.AppendLine($"Tipo de Contrato: {contrato.nome_produto}");
                mensagem.AppendLine($"Vencimento: {contrato.vencimento:dd/MM/yyyy}");
                mensagem.AppendLine($"Valor: {contrato.valor:C}");
                mensagem.AppendLine(new string('-', 40)); // Separador entre contratos
            }

            MessageBox.Show(mensagem.ToString(), "Lista de Contratos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Botão para buscar contratos por nome do cliente
            string nomeCliente = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(nomeCliente))
            {
                MessageBox.Show("Por favor, informe o nome do cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NovaPasta2.Cruds crud = new NovaPasta2.Cruds();

            var (quantidadeContratos, somaValores, contratoAtrasado, diasAtraso, valorAtrasado) = crud.ObterEstatisticasComAtrasoPorNomeCliente(nomeCliente);

            if (quantidadeContratos > 0)
            {
                string mensagem = $"Cliente: {nomeCliente}\n" +
                                 $"Total de Contratos: {quantidadeContratos}\n" +
                                 $"Soma dos Valores: {somaValores.ToString("C", CultureInfo.GetCultureInfo("pt-BR"))}";

                if (!string.IsNullOrEmpty(contratoAtrasado))
                {
                    mensagem += $"\n\nContrato mais atrasado: {contratoAtrasado}\n" +
                                $"Valor: {valorAtrasado.ToString("C", CultureInfo.GetCultureInfo("pt-BR"))}\n" +
                                $"Dias em atraso: {diasAtraso}";
                }
                else
                {
                    mensagem += "\n\nNenhum contrato em atraso";
                }

                MessageBox.Show(mensagem, "Resumo dos Contratos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cliente não encontrado ou sem contratos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Obter a lista de arquivos do banco de dados
                Cruds crud = new Cruds(); 
                var arquivos = crud.ObterArquivosImportados();

                // Verificar se há arquivos
                if (arquivos.Count == 0)
                {
                    MessageBox.Show("Nenhum arquivo importado encontrado.", "Informação",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Criar uma string com os dados formatados
                StringBuilder sb = new StringBuilder();
                foreach (var arquivo in arquivos)
                {
                    // Extrair apenas o nome do arquivo do caminho completo
                    string nomeArquivo = Path.GetFileName(arquivo.CaminhoArquivo);

                    sb.AppendLine($"Usuário: {arquivo.LoginUsuario}");
                    sb.AppendLine($"Arquivo: {nomeArquivo}");
                    sb.AppendLine("-----------------------");
                }

                MessageBox.Show(sb.ToString(), "Arquivos Importados",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter arquivos: {ex.Message}", "Erro",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}