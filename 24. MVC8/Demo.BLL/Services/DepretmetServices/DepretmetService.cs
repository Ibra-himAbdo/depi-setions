using Demo.BLL.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Demo.BLL.Services;

public class DepretmetService : IDepretmetService
{
    private readonly IValidator<Department> _validator;
    private readonly IDepartmentRepository _departmentRepository;

    public DepretmetService(IValidator<Department> validator, IDepartmentRepository departmentRepository)
    {
        _validator = validator;
        _departmentRepository = departmentRepository;
    }
    public async Task<Result> CreateDepartmentAsync(Department department)
    {

        ValidationResult result = await _validator.ValidateAsync(department);

        if (!result.IsValid)
        {
            return result;
        }

        try
        {
            await _departmentRepository.AddAsync(department);
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }

        return Result.Success();
    }

    public async Task<Result> EditDepartmentAsync(Department department)
    {
        ValidationResult result = await _validator.ValidateAsync(department);

        if (!result.IsValid)
        {
            return result;
        }

        try
        {
            await _departmentRepository.UpdateAsync(department);
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }

        return Result.Success();
    }
}