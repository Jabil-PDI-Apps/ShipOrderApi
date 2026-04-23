using Microsoft.EntityFrameworkCore;
using OneOf;
using ShipOrderBack.Context;
using ShipOrderBack.Exceptions;
using ShipOrderBack.Model;
using ShipOrderBack.Service.Interface;

namespace ShipOrderBack.Service
{
    public class QuarentineService(QuarentineDbContext quarentinesContext, OrderContext orderContext): IQuarentineService
    {
      
        private readonly QuarentineDbContext _quarentinesContext = quarentinesContext;
        private readonly OrderContext _orderContext = orderContext;

       
        public async Task<IEnumerable<Quarentine>> GetQuarentinesAll(int rangeQty)
        {
            DateTime dataLimite = new(DateTime.Now.Year, 1, 1);

            if (rangeQty < 0) throw new InvalidRangeQty();

            var result = await _quarentinesContext.SerialCheckeds
                .AsNoTracking()
                .Where(q => q.DatePackout >= dataLimite)
                .Take(rangeQty)
                .ToListAsync();

            return result;
        }
     

        public async Task<IEnumerable<OrderModelSerial>> GetOrderModelSerial(long customerId, DateTime dateStart, DateTime dateEnd)
        {
            return await _orderContext.OrderModelSerials
            .AsNoTracking()
            .Where(oms => oms.OrderModel.Model.CustomerId == customerId &&
                  oms.DateCreation >= dateStart &&
                  oms.DateCreation < dateEnd)
            .ToListAsync();
        }

        public async Task<IEnumerable<Quarentine>> GetQuarentineSerial(List<string> serials)
        {
            var result = await _quarentinesContext.SerialCheckeds
                .AsNoTracking()
                .Where(q => serials.Contains(q.SerialNumber))
                .ToListAsync();
 
            return result;

        }
        public async Task<IEnumerable<Quarentine>> GetQuarentine(long customerId, DateTime dateStart, DateTime dateEnd)
        {
            if (dateEnd < dateStart) throw new InvalidDate(dateStart, dateEnd);

            dateStart = dateStart.Date;
            dateEnd = dateEnd.Date.AddDays(1);

            var result = await GetOrderModelSerial(customerId,dateStart, dateEnd);

            var serialStrings = result.Select(x => x.Serial).Distinct().ToList();

            if (serialStrings.Count == 0) return [];

            var serialQuarentine = await GetQuarentineSerial(serialStrings!);

            return serialQuarentine;
        }

    }
}
