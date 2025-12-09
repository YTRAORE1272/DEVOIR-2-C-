using GesBanqueAspNet.Models;

namespace GesBanqueAspNet.Services
{
    public interface ICompteService
    {
        Task<Compte?> GetByNumeroAsync(string numero);

        Task<(decimal totalDepots,
              decimal totalRetraits,
              int nombreTransactions,
              DateTime? derniereTransaction)>
            GetStatistiquesAsync(int compteId);
    }
}
