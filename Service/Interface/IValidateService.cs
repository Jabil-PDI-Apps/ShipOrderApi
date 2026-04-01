using ShipOrderBack.Model;

namespace ShipOrderBack.Service.Interface
{
    public interface IValidateService
    {
        Task<IEnumerable<ValidationLabelHistoryArcon>> GetValidationLabelAll(int rangeQty);
        Task<IEnumerable<ValidationLabelHistoryArcon>> GetValidationLabelCustomer(long customerId, DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<WpWip>> GetPackoutAll(int rangeQty);
        Task<IEnumerable<Quarentine>> GetQuarentinesAll(int rangeQty);
        Task<IEnumerable<Quarentine>> GetQuarentineSerial(List<string> serials);
        Task<IEnumerable<Quarentine>> GetQuarentine(long customerId, DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<WpWip>> GetPackoutCustomer(long customerId, DateTime dateStart, DateTime dateEnd);
        //Task<IEnumerable<Order>> GetPackoutFailCustomer(long customerId, DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<OrderModelSerial>> GetOrderModelSerial(long customerId, DateTime dateStart, DateTime dateEnd);
    }
}
