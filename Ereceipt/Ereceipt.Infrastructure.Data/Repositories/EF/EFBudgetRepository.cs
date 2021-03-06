using Ereceipt.Domain.Interfaces;
using Ereceipt.Domain.Models;
using Ereceipt.Infrastructure.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ereceipt.Infrastructure.Data.Repositories.EF
{
    public class EFBudgetRepository : EFRepository<Budget>, IBudgetRepository
    {
        public EFBudgetRepository(EreceiptContext db) : base(db) { }

        public async Task<List<Budget>> GetActiveBudgetsAsync(Guid id)
        {
            var todayDate = DateTime.UtcNow;
            return await db.Budgets
                .AsNoTracking()
                .Where(x => x.GroupId == id && (x.StartPeriod < todayDate && x.EndPeriod > todayDate))
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Budget>> GetUnactiveBudgetsAsync(Guid id)
        {
            return await db.Budgets
                .AsNoTracking()
                .Where(x => x.GroupId == id && x.EndPeriod < DateTime.UtcNow)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Receipt>> GetReceiptsByBudgetIdAsync(long budgetId, int skip)
        {
            return await db.Receipts
                .AsNoTracking()
                .Where(x => x.BudgetId == budgetId)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skip).Take(20)
                .ToListAsync();
        }
    }
}