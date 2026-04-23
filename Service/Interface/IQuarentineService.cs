using ShipOrderBack.Model;

namespace ShipOrderBack.Service.Interface
{
    public interface IQuarentineService
    {
        Task<IEnumerable<Quarentine>> GetQuarentinesAll(int rangeQty);
        Task<IEnumerable<Quarentine>> GetQuarentineSerial(List<string> serials);
        Task<IEnumerable<Quarentine>> GetQuarentine(long customerId, DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<OrderModelSerial>> GetOrderModelSerial(long customerId, DateTime dateStart, DateTime dateEnd);
    }
}
