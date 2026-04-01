using Microsoft.EntityFrameworkCore;
using OneOf;
using ShipOrderBack.Context;
using ShipOrderBack.Exceptions;
using ShipOrderBack.Model;
using ShipOrderBack.Service.Interface;

namespace ShipOrderBack.Service
{
    public class ValidateService: IValidateService
    {
        private readonly ILogger<ValidateService> _logger;
        private readonly EpsDbContext _epsContext;
        private readonly JemsDbContext _jemsContext;
        private readonly QuarentineDbContext _quarentinesContext;
        private readonly OrderContext _orderContext;

        public ValidateService(OrderContext order, QuarentineDbContext quarentines, EpsDbContext epsContext, JemsDbContext jemsDbContext, ILogger<ValidateService> logger)
        {
            _epsContext = epsContext;
            _jemsContext = jemsDbContext;
            _quarentinesContext = quarentines;
            _orderContext = order;
            _logger = logger;
        }

        public async Task<IEnumerable<ValidationLabelHistoryArcon>> GetValidationLabelAll(int rangeQty)
        {
            DateTime dataLimite = new DateTime(DateTime.Now.Year, 1, 1);

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
            if (dateEnd < dateStart) throw new InvalidDate(dateStart,dateEnd);

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


        public async Task<IEnumerable<WpWip>> GetPackoutAll(int rangeQty)
        {
            DateTime dataLimite = new DateTime(DateTime.Now.Year, 1, 1);

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

        public async Task<IEnumerable<Quarentine>> GetQuarentinesAll(int rangeQty)
        {
            DateTime dataLimite = new DateTime(DateTime.Now.Year, 1, 1);

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

            var serialQuarentine = await GetQuarentineSerial(serialStrings);

            return serialQuarentine;
        }

    }
}
