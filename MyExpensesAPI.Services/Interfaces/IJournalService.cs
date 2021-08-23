using MyExpensesAPI.Models.Models.Journal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpensesAPI.Services.Interfaces
{
    public interface IJournalService
    {
        public Task<IEnumerable<JournalRecordApiModel>> GetRefillRecords(Guid userId, DateTime dateFrom, DateTime dateTo);
        public Task<IEnumerable<JournalRecordApiModel>> GetWithdrawalRecords(Guid userId, DateTime dateFrom, DateTime dateTo);
    }
}
