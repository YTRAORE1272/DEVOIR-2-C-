using GesBanqueAspNet.DTO;
using GesBanqueAspNet.Models;

namespace GesBanqueAspNet.ViewModels
{
    public class CompteDetailViewModel
    {
        public CompteSearchFormDto Search { get; set; } = new();
        public Compte? Compte { get; set; }

        public decimal TotalDepots { get; set; }
        public decimal TotalRetraits { get; set; }
        public int NombreTransactions { get; set; }
        public DateTime? DerniereTransaction { get; set; }

        public List<TransactionCompte> Transactions { get; set; } = new();
        public TransactionFilterDto Filter { get; set; } = new();
        public PaginationViewModel Pagination { get; set; } = new();
    }
}
