using FilmIzle.DTO.DTOs.FilmDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Business.ValidationRules.FluentValidation
{
    public class FilmAddValidator : AbstractValidator<FilmAddDto>
    {
        public FilmAddValidator()
        {
            RuleFor(I => I.Name).NotNull().WithMessage("Film İsmi Giriniz");
            RuleFor(I => I.TRDubbingUrl).NotNull().When(I => I.TRDubbing.Equals(true)).WithMessage("Türkçe Dublaj linki ekleyiniz");
            RuleFor(I => I.TRSubtitleUrl).NotNull().When(I => I.TRSubtitle.Equals(true)).WithMessage("Türkçe Altyazı linkki ekleyiniz");
            RuleFor(I => I.Description).NotNull().WithMessage("Açıklama Giriniz");
            RuleFor(I => I.Director).NotNull().WithMessage("Yönetmen İsminiz Giriniz");
            RuleFor(I => I.ReleaseDate).NotEmpty().WithMessage("Gösterim Tarihini Giriniz");
            RuleFor(I => I.IMDBPoint).NotEmpty().WithMessage("IMDB Puanı Giriniz");
        }
    }
}
