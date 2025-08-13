using System.ComponentModel.DataAnnotations;

namespace Market.Application.DTOs.Supliers
{
    public class CreateSuplierDto
    {
        public string SupplierName { get; set; } = default!;
        public string? Address { get; set; }
    }
}
