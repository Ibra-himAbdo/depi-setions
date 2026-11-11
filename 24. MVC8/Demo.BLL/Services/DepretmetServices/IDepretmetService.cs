using Demo.BLL.Models;
using Demo.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.BLL.Services;

public interface IDepretmetService
{
    Task<Result> CreateDepartmentAsync(Department department);
    Task<Result> EditDepartmentAsync(Department department);
}
