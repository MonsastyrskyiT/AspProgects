using Homvork_asp.DTO;

namespace Homvork_asp.Service
{
    
    public interface IInsuranceService
    {
        InsuranceViewModel Calculate(InsuranceViewModel model);
    }
}
