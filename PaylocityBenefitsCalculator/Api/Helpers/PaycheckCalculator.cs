namespace Api.Helpers;

/// <summary>
/// Encapsulates paycheck calculation logic.
/// </summary>
public class PaycheckCalculator : IPaycheckCalculator
{
    // All constants needed for calculation are hardcoded as I do not expect changing of those values often.
    private const int PaychecksPerYear = 26;
    private const int BaseBenefitCostPerMonth = 1000;
    private const int DependentBenefitCostPerMonth = 600;
    private const int AdditionalBenefitCostForOlderDependents = 200;
    private const int SalaryYearThreshold = 80000;
    private const decimal AdditionalBenefitCostForHighSalary = 0.02m;
    private const int OldDependentAge = 50;
    private const int MonthInTheYear = 12;
    private const int NumberOfDigitsToRound = 2;
    public decimal CalculatePaycheck(Models.Employee employee) 
    {
        decimal yearlyBenefitCost = CalculateYearlyBenefitCost(employee);
        decimal totalSalaryMinusBenefits = employee.Salary - yearlyBenefitCost;
        var monthlyPaycheck = totalSalaryMinusBenefits / PaychecksPerYear;
        return Math.Round(monthlyPaycheck, NumberOfDigitsToRound);
    }

    private decimal CalculateYearlyBenefitCost(Models.Employee employee)
    {
        decimal totalBenefitCost = BaseBenefitCostPerMonth * MonthInTheYear;
        foreach (var dependent in employee.Dependents)
        {
            totalBenefitCost += DependentBenefitCostPerMonth * MonthInTheYear;
            if (IsDependentOld(dependent.DateOfBirth))
            {
                totalBenefitCost += AdditionalBenefitCostForOlderDependents * MonthInTheYear;
            }
        }
        if (employee.Salary > SalaryYearThreshold)
        {
            totalBenefitCost += employee.Salary * AdditionalBenefitCostForHighSalary;
        }
        return totalBenefitCost;
    }

    private bool IsDependentOld(DateTime dateOfBirth)
    {
        // Assuming we're considering the age at this year
        int age = DateTime.UtcNow.Year - dateOfBirth.Year;
        return age > OldDependentAge;
    }
}