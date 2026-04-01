using ShipOrderBack.Exceptions.Base;

namespace ShipOrderBack.Exceptions
{
    public class InvalidDate: DomainException
    {
        public InvalidDate(DateTime start, DateTime end): base($"Data {end} menor que {start}.", 400) { }
    }
}
