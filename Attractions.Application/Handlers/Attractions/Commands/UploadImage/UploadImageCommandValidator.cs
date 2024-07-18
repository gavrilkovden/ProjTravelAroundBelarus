using Attractions.Application.Handlers.Attractions.Commands.CreateAttraction;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Commands.UploadImage
{
    public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
    {
        public UploadImageCommandValidator()
        {
            RuleFor(x => x.Image)
                .Must(BeAValidImage).WithMessage("Invalid image file.")
                .When(x => x.Image != null);
        }

        private bool BeAValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false; // Проверка на существование файла и его непустоту
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(extension);
        }
    }
}
