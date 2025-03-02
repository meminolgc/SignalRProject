using FluentValidation;
using SignalR.DtoLayer.BookingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.ValidationRules.BookingValidation
{
	public class CreateBookingValidation : AbstractValidator<CreateBookingDto>
	{
		public CreateBookingValidation()
		{
			RuleFor(x=>x.Name).NotEmpty().WithMessage("İsim Alanı Boş Geçilemez!");
			RuleFor(x=>x.Phone).NotEmpty().WithMessage("Telefon Boş Geçilemez!");
			RuleFor(x=>x.Mail).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez!");
			RuleFor(x=>x.PersonCount).NotEmpty().WithMessage("Kişi Alanı Boş Geçilemez!");
			RuleFor(x=>x.Date).NotEmpty().WithMessage("Tarih Alanı Boş Geçilemez!");

			RuleFor(x=>x.Name).MinimumLength(5).WithMessage("İsim alanı 5 karakterden az olamaz!").MaximumLength(50).WithMessage("İsim alanı 50 karakterden fazla olamaz!");
			RuleFor(x=>x.Description).MinimumLength(30).WithMessage("Açıklama alanı 30 karakterden az olamaz!").MaximumLength(500).WithMessage("Açıklama alanı 500 karakterden fazla olamaz!");

			RuleFor(x => x.Mail).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz!");
		}
	}
}
