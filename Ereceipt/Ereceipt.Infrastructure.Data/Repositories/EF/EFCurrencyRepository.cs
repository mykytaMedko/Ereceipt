using Ereceipt.Domain.Interfaces;
using Ereceipt.Domain.Models;
using Ereceipt.Infrastructure.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Ereceipt.Infrastructure.Data.Repositories.EF
{
    public class EFCurrencyRepository : EFRepository<Currency>, ICurrencyRepository
    {
        public EFCurrencyRepository(EreceiptContext db) : base(db) { }

        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            return await db.Currencies
                .OrderBy(d => d.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Currency>> GetByCodeAsync(string code)
        {
            return await db.Currencies
                .Where(d => d.Code.Contains(code))
                .OrderBy(d => d.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}