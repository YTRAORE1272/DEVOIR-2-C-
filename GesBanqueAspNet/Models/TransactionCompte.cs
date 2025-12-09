namespace GesBanqueAspNet.Models
{
    public class TransactionCompte
    {
        public int Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public DateTime DateOperation { get; set; }
        public TypeTransaction Type { get; set; }
        public decimal Montant { get; set; }
        public decimal SoldeApres { get; set; }
        public string Description { get; set; } = string.Empty;

        public int CompteId { get; set; }
        public Compte Compte { get; set; } = null!;
    }
}
