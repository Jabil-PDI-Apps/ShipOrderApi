using Microsoft.AspNetCore.Mvc;
using ShipOrderBack.Model;
using ShipOrderBack.Service.Interface;

namespace ShipOrderBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProdutoController : ControllerBase
    {
        private readonly IValidateService _validateLabel;

        public ProdutoController(IValidateService validateLabel)
        {
            _validateLabel = validateLabel;
        }

        [HttpGet("LabelFailAll/{rangeQty:int?}")]
        public async Task<ActionResult<IEnumerable<ValidationLabelHistoryArcon>>> Get(int rangeQty = 100) {
            try
            {
                var produtos = await _validateLabel.GetValidationLabelAll(rangeQty);
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return BadRequest();
            }
        }

        [HttpGet("LabelFailCustomer/{customerId:long}/{dateStart}/{dateEnd}")]
        public async Task<ActionResult<ValidationLabelHistoryArcon>> GetLabelFail(long customerId, string dateStart, string dateEnd)
        {
            try
            {
                    if (!DateTime.TryParse(dateStart, out DateTime startDate))
                    {
                        return BadRequest("Invalid date format for dateStart. Please use a valid date format.");
                    }
                    if (!DateTime.TryParse(dateEnd, out DateTime endDate))
                    {
                        return BadRequest("Invalid date format for dateEnd. Please use a valid date format.");
                    }
                var produtos = await _validateLabel.GetValidationLabelCustomer(customerId, startDate, endDate);
                return Ok(produtos);

            } catch (Exception ex) {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpGet("PackoutAll/{rangeQty:int?}")]
        public async Task<ActionResult<IEnumerable<EpContainerWipLink>>> GetPackout(int rangeQty = 100)
        {
            try
            {
                var packout = await _validateLabel.GetPackoutAll(rangeQty);
               
                return Ok(packout);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return BadRequest();
            }
        }

        [HttpGet("PackoutCustomer/{customerId:long}/{dateStart}/{dateEnd}")]
        public async Task<ActionResult<IEnumerable<EpContainerWipLink>>> GetPackoutCustomer(long customerId, string dateStart, string dateEnd)
        {
            try
            {
                if (!DateTime.TryParse(dateStart, out DateTime startDate))
                {
                    return BadRequest("Invalid date format for dateStart. Please use a valid date format.");
                }
                if (!DateTime.TryParse(dateEnd, out DateTime endDate))
                {
                    return BadRequest("Invalid date format for dateEnd. Please use a valid date format.");
                }
                var packout = await _validateLabel.GetPackoutCustomer(customerId, startDate, endDate);

                return Ok(packout);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpGet("QuarentinesAll/{rangeQty:int?}")]
        public async Task<ActionResult<IEnumerable<Quarentine>>> GetQuarentine(int rangeQty = 100)
        {
            try
            {
                var prodquarentine = await _validateLabel.GetQuarentinesAll(rangeQty);
                return Ok(prodquarentine);

            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return BadRequest();
            }
        }

        [HttpGet("GetQuarentineSerial/{customerId:long}/{dateStart}/{dateEnd}")]
        public async Task<ActionResult<IEnumerable<OrderModelSerial>>> GetQuarentineSerial(long customerId, string dateStart, string dateEnd)
        {
            try
            {
                if (!DateTime.TryParse(dateStart, out DateTime startDate))
                {
                    return BadRequest("Invalid date format for dateStart. Please use a valid date format.");
                }
                if (!DateTime.TryParse(dateEnd, out DateTime endDate))
                {
                    return BadRequest("Invalid date format for dateEnd. Please use a valid date format.");
                }

                var serials = await _validateLabel.GetQuarentine(customerId, startDate, endDate);

                return Ok(serials);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

    }
}
