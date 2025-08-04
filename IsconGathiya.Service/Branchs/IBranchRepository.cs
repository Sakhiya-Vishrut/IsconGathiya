using IsconGathiya.Domain;
using IsconGathiya.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Service.Branchs
{
    public interface IBranchRepository
    {
        Task<List<BranchDTO>> GetBranchDataWithFilter();

        Task<bool> AddEditBranch(Branch model);

        BranchDTO DetailBranch(int? BranchId);
    }
}
