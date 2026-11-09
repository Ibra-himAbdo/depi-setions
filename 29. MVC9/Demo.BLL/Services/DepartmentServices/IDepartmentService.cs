using Demo.BLL.Models;
using FluentValidation.Results;

namespace Demo.BLL.Services;

public interface IDepartmentService
{
    Task<Result> CreateDepartmentAsync(Department department);
    Task<Result> UpdateDepartmentAsync(Department department);
}

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IValidator<Department> _validator;

    public DepartmentService(IDepartmentRepository departmentRepository, IValidator<Department> validator)
    {
        _departmentRepository = departmentRepository;
        _validator = validator;
    }

    public async Task<Result> CreateDepartmentAsync(Department department)
    {
        ValidationResult valdaiton = await _validator.ValidateAsync(department);

        if (!valdaiton.IsValid)
        {
            // 3. 
            return valdaiton;
            //List<Error> errors = new List<Error>();
            //foreach (ValidationFailure? error in valdaiton.Errors)
            //{
            //    errors.Add(Error.Validation(error.PropertyName, error.ErrorMessage));
            //}

            //// 1. return Result.Failure(errors);
            //2 . return errors;
        }

        try
        {
            await _departmentRepository.AddAsync(department);
        }
        catch (Exception ex)
        {
            return Error.Failure(descreiption: ex.Message);
        }

        return Result.Success();
    }

    public async Task<Result> UpdateDepartmentAsync(Department department)
    {
        ValidationResult valdaiton = await _validator.ValidateAsync(department);
        if (!valdaiton.IsValid) return valdaiton;

        try
        {
            await _departmentRepository.EditAsync(department);
        }
        catch (Exception ex)
        {
            return Error.Failure(descreiption: ex.Message);
        }

        return Result.Success();
    }
}