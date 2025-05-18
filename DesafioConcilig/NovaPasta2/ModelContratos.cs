namespace DesafioConcilig.Modelos
{
    public class Contrato
    {
        public int Id { get; set; }
        public int Cliente_id { get; set; }
        public string nome_produto { get; set; }
        public DateTime vencimento { get; set; }
        public decimal valor { get; set; }

    }
    public class EstatisticasContratos
    {
        public int QuantidadeContratos { get; set; }
        public decimal ValorTotal { get; set; }
        public int DiasAtraso { get; set; }
        public string ContratoMaisAtrasado { get; set; }
        public decimal ValorContratoAtrasado { get; set; }
    }
}
