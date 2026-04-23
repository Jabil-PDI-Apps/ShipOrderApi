using ShipOrderBack.Model;

namespace ShipOrderBack.Service.Interface
{
    public interface ILabelService
    {
        Task<IEnumerable<ValidationLabelHistoryArcon>> GetValidationLabelCustomer(long customerId, DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<ValidationLabelHistoryArcon>> GetValidationLabelAll(int rangeQty);
    }
}
