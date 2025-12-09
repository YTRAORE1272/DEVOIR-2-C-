using GesBanqueAspNet.Data;
using GesBanqueAspNet.Models;
using Microsoft.EntityFrameworkCore;

namespace GesBanqueAspNet.Services.Impl
{
    public class TransactionService : ITransactionService
    {
        private readonly BanqueDbContext _db;

        public TransactionService(BanqueDbContext db)
        {
            _db = db;
        }

        public async Task<(List<TransactionCompte> Items, int TotalCount)>
            GetByComptePagedAsync(int compteId, string type, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize <= 0) pageSize = 5;

            var query = _db.Transactions
                .Where(t => t.CompteId == compteId);

            type = (type ?? "all").ToLowerInvariant();

            switch (type)
            {
                case "depot":
                    query = query.Where(t => t.Type == TypeTransaction.Depot);
                    break;
                case "retrait":
                    query = query.Where(t => t.Type == TypeTransaction.Retrait);
                    break;
                case "all":
                default:
                    break;
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(t => t.DateOperation)
                .ThenByDescending(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
