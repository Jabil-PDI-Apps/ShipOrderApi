using ShipOrderBack.Model;

namespace ShipOrderBack.Service.Interface
{
    public interface IPackoutService
    {
        Task<IEnumerable<WpWip>> GetPackoutAll(int rangeQty);
        Task<IEnumerable<WpWip>> GetPackoutCustomer(long customerId, DateTime dateStart, DateTime dateEnd);
    }
}
