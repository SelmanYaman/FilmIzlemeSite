using FilmIzle.DTO.DTOs.CategoryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Business.ValidationRules.FluentValidation
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(I => I.Name).NotNull().WithMessage("Kategori Boş Geçilemez..");
        }
    }
}
