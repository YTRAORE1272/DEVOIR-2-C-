namespace GesBanqueAspNet.Models
{
    public class Compte
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string Titulaire { get; set; } = string.Empty;
        public TypeCompte Type { get; set; }
        public decimal SoldeActuel { get; set; }
        public DateTime DateCreation { get; set; }

        public StatutCompte Statut { get; set; }
        public int? DureeBlocageJours { get; set; }
        public DateTime? DateDeblocage { get; set; }

        public List<TransactionCompte> Transactions { get; set; } = new();
    }
}
