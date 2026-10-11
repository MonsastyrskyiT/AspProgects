using Homvork_asp.DTO;

namespace Homvork_asp.Service
{
    public class InsuranceService : IInsuranceService
    {
        private const decimal BaseRate = 1000m;

        public InsuranceViewModel Calculate(InsuranceViewModel model)
        {
            if (model.EngineCapacity <= 0 || model.DriverExperienceYears < 0)
            {
                model.ErrorMessage = "Перевірте введені дані: об'єм двигуна має бути більше 0, а стаж водіння не може бути від'ємним.";
                model.CalculatedPrice = null;
                return model;
            }

            decimal engineCoefficient = model.EngineCapacity switch
            {
                <= 1600 => 1.0m,
                <= 2000 => 1.3m,
                _ => 1.6m
            };

            decimal cityCoefficient = model.CityType switch
            {
                "Kyiv" => 1.8m,
                "LargeCity" => 1.3m,
                _ => 1.0m
            };

            decimal price = BaseRate * engineCoefficient * cityCoefficient;

            if (model.DriverExperienceYears > 3)
            {
                price *= 0.9m; // -10%
            }

            if (model.HasDiscountCategory)
            {
                price *= 0.8m; // -20%
            }

            model.CalculatedPrice = Math.Round(price, 2);
            model.ErrorMessage = null;

            return model;
        }
    }
}
