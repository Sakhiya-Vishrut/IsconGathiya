using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_extensions.Contains(extension))
            {
                return new ValidationResult($"This file extension is not allowed!");
            }
        }

        return ValidationResult.Success;
    }
}

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;
    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"Maximum allowed file size is {_maxFileSize / 1024 / 1024} MB.");
            }
        }

        return ValidationResult.Success;
    }
}
