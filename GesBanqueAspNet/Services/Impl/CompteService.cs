using GesBanqueAspNet.Data;
using GesBanqueAspNet.Models;
using Microsoft.EntityFrameworkCore;

namespace GesBanqueAspNet.Services.Impl
{
    public class CompteService : ICompteService
    {
        private readonly BanqueDbContext _db;

        public CompteService(BanqueDbContext db)
        {
            _db = db;
        }

        public async Task<Compte?> GetByNumeroAsync(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                return null;

            numero = numero.Trim();

            return await _db.Comptes
                .Include(c => c.Transactions)
                .FirstOrDefaultAsync(c => c.Numero == numero);
        }

        public async Task<(decimal totalDepots,
                          decimal totalRetraits,
                          int nombreTransactions,
                          DateTime? derniereTransaction)>
            GetStatistiquesAsync(int compteId)
        {
            var query = _db.Transactions
                .Where(t => t.CompteId == compteId);

            var totalDepots = await query
                .Where(t => t.Type == TypeTransaction.Depot)
                .SumAsync(t => (decimal?)t.Montant) ?? 0m;

            var totalRetraits = await query
                .Where(t => t.Type == TypeTransaction.Retrait)
                .SumAsync(t => (decimal?)t.Montant) ?? 0m;

            var nombreTransactions = await query.CountAsync();

            var derniereDate = await query
                .OrderByDescending(t => t.DateOperation)
                .Select(t => (DateTime?)t.DateOperation)
                .FirstOrDefaultAsync();

            return (totalDepots, totalRetraits, nombreTransactions, derniereDate);
        }
    }
}
