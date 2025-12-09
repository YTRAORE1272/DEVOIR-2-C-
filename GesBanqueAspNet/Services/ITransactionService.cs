using GesBanqueAspNet.Models;

namespace GesBanqueAspNet.Services
{
    public interface ITransactionService
    {
        Task<(List<TransactionCompte> Items, int TotalCount)>
            GetByComptePagedAsync(int compteId, string type, int page, int pageSize);
    }
}
