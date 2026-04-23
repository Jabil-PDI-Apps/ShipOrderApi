using Microsoft.EntityFrameworkCore;
using ShipOrderBack.Context;
using ShipOrderBack.Exceptions;
using ShipOrderBack.Model;
using ShipOrderBack.Service.Interface;

namespace ShipOrderBack.Service
{
    public class LabelService(EpsDbContext epsDbContext): ILabelService
    {
        private readonly EpsDbContext _epsContext = epsDbContext;

        public async Task<IEnumerable<ValidationLabelHistoryArcon>> GetValidationLabelAll(int rangeQty)
        {

            DateTime dataLimite = new(DateTime.Now.Year, 1, 1);

            if (rangeQty < 0) throw new InvalidRangeQty();

            var result = await _epsContext.LabelHistories
                .AsNoTracking()
                .Where(x => x.Status == "Fail" && x.PrintDate >= dataLimite)
                .OrderByDescending(x => x.PrintDate)
                .Take(rangeQty)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ValidationLabelHistoryArcon>> GetValidationLabelCustomer(long customerId, DateTime dateStart, DateTime dateEnd)
        {
            if (dateEnd < dateStart) throw new InvalidDate(dateStart, dateEnd);

            dateStart = dateStart.Date;
            dateEnd = dateEnd.Date.AddDays(1);

            var result = await _epsContext.LabelHistories
               .AsNoTracking()
               .Where(x =>
                   x.Status == "Fail" &&
                   x.PrintDate >= dateStart &&
                   x.PrintDate < dateEnd &&
                   x.CustomerID == customerId
               )
               .ToListAsync();

            return result;

        }
    }
}
