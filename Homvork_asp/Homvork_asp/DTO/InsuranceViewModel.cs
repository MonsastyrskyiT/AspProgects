using System.ComponentModel.DataAnnotations;

namespace Homvork_asp.DTO
{
    public class InsuranceViewModel
    {
        [Display(Name = "Об'єм двигуна (см³)")]
        public int EngineCapacity { get; set; }

        [Display(Name = "Місто реєстрації")]
        public string CityType { get; set; } = "SmallTown";

        [Display(Name = "Стаж водіння (років)")]
        public int DriverExperienceYears { get; set; }

        [Display(Name = "Наявність пільг")]
        public bool HasDiscountCategory { get; set; }

        public decimal? CalculatedPrice { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
