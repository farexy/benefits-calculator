namespace Api.Helpers;

public interface IPaycheckCalculator
{
    decimal CalculatePaycheck(Models.Employee employee);
}