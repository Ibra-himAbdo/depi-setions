namespace Demo.BLL.Interfaces;

public interface IDepartmentRepository
{
    List<Department> GetAll();

    Department? GetById(int id);

    int Add(Department department);

    int Edit(Department department);

    int Delete(Department department);

    //int Delete(int id);
}