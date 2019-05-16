namespace TestNinja.Mocking
{
    public interface IEmployeeStorage
    {
        EmployeeContext Db { get; set; }

        void DeleteEmployee(int id);
    }
}