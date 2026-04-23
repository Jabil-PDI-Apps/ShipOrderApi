using Microsoft.EntityFrameworkCore;
using ShipOrderBack.Context;
using ShipOrderBack.Exceptions;
using ShipOrderBack.Model;
using ShipOrderBack.Service.Interface;

namespace ShipOrderBack.Service
{
    public class PackoutService(JemsDbContext jemsDbContext): IPackoutService
    {
        private readonly JemsDbContext _jemsContext = jemsDbContext;

        public async Task<IEnumerable<WpWip>> GetPackoutAll(int rangeQty)
        {
            DateTime dataLimite = new(DateTime.Now.Year, 1, 1);

            if (rangeQty < 0) throw new InvalidRangeQty();

            var result = await _jemsContext.WpWips
                .AsNoTracking()
                .Where(w => !w.ContainerLinks.Any() && w.LastUpdated >= dataLimite)
                .Take(rangeQty)
                .ToListAsync();

            return result;
        }
        public async Task<IEnumerable<WpWip>> GetPackoutCustomer(long customerId, DateTime dateStart, DateTime dateEnd)
        {
            if (dateEnd < dateStart) throw new InvalidDate(dateStart, dateEnd);

            dateStart = dateStart.Date;
            dateEnd = dateEnd.Date.AddDays(1);

            var packout = await _jemsContext.WpWips
                .AsNoTracking()
                .Where(w =>
                    w.Customer_ID == customerId &&
                    w.ContainerLinks.Any(c =>
                        c.Wip_ID == w.Wip_ID &&
                        c.LastUpdated >= dateStart &&
                        c.LastUpdated < dateEnd
                    )
                )
                .ToListAsync();

            return packout;
        }
        //public async Task<IEnumerable<Order>> GetPackoutFailCustomer(long customerId, DateTime dateStart, DateTime dateEnd)
        //{
        //    if (dateEnd < dateStart) throw new InvalidDate(dateStart, dateEnd);

        //    dateStart = dateStart.Date;
        //    dateEnd = dateEnd.Date.AddDays(1);

        //    var OrderSerial = await GetOrderModelSerial(customerId, dateStart, dateEnd);
        //    if (!OrderSerial.Any()) return Enumerable.Empty<Order>();
        //    var packouts = await GetPackoutCustomer(customerId, dateStart, dateEnd);
        //    if (!packouts.Any()) return Enumerable.Empty<Order>();

        //    var serials = OrderSerial.Select(os => os.Serial).ToList();
        //    var packout = packouts.Where(p => serials.Contains(p.SerialNumber)).ToList();
        //    var packoutSerials = packout.Select(p => p.SerialNumber).ToList();  

        //    var orderPackout = GetOrderSerial(packoutSerials).Result;

        //    return orderPackout;
        //}
    }
}
