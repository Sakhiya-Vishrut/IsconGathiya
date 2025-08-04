using IsconGathiya.Common.DependencyInjection;
using IsconGathiya.Service.Branchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Service.Employee
{
    [TransientDependency(ServiceType = typeof(IEmployeeRepository))]
    public class EmployeeRepository : IEmployeeRepository
    {
    }
}
