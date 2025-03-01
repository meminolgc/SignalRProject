using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.DiscountDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountService DiscountService, IMapper mapper)
        {
            _discountService = DiscountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult DiscountList()
        {
            var value = _mapper.Map<List<ResultDiscountDto>>(_discountService.TGetListAll());
            return Ok(value);
        }
        [HttpPost]
        public IActionResult CreateDiscount(CreateDiscountDto createDiscountDto)
        {
			var value = _mapper.Map<Discount>(createDiscountDto);
			_discountService.TAdd(value);
			return Ok("İndirim Eklendi");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDiscount(int id)
        {
            var value = _discountService.TGetByID(id);
            _discountService.TDelete(value);
            return Ok("İndirim Silindi");
        }
        [HttpGet("{id}")]
        public IActionResult GetDiscount(int id)
        {
            var value = _discountService.TGetByID(id);
            return Ok(_mapper.Map<GetDiscountDto>(value));
        }
        [HttpPut]
        public IActionResult UpdateDiscount(UpdateDiscountDto updateDiscountDto)
        {
			var value = _mapper.Map<Discount>(updateDiscountDto);
			_discountService.TUpdate(value);
			return Ok("İndirim Güncellendi");
        }

		[HttpGet("ChangeStatusToTrue/{id}")]
        public IActionResult ChangeStatusToTrue(int id)
        {
            _discountService.TChangeStatusToTrue(id);
            return Ok("Ürün indirimi aktifleştirildi");
        }

		[HttpGet("ChangeStatusToFalse/{id}")]
		public IActionResult ChangeStatusToFalse(int id)
		{
			_discountService.TChangeStatusToFalse(id);
			return Ok("Ürün indirimi pasifleştirildi");
		}

		[HttpGet("GetListByStatusTrue")]
		public IActionResult GetListByStatusTrue()
		{
			return Ok(_discountService.TGetListByStatusTrue());
		}
	}
}
