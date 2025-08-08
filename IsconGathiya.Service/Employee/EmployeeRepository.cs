using IsconGathiya.Common.DependencyInjection;

namespace IsconGathiya.Service.Employee
{
    [TransientDependency(ServiceType = typeof(IEmployeeRepository))]
    public class EmployeeRepository : IEmployeeRepository
    {
    }
}
