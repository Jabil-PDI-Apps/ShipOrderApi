using ShipOrderBack.Exceptions.Base;

namespace ShipOrderBack.Exceptions
{
    public class InvalidRangeQty: DomainException
    {
        public InvalidRangeQty(): base("Quantidade precisar ser maior que zero.", 400) { }
    }
}
