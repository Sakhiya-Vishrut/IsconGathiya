using IsconGathiya.Common.DependencyInjection;
using IsconGathiya.Domain;
using IsconGathiya.Domain.DataContext;
using IsconGathiya.Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Service.Branchs
{
    [TransientDependency(ServiceType = typeof(IBranchRepository))]
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;
        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<BranchDTO>> GetBranchDataWithFilter()
        {
            try
            {
                var brbranch = _context.Branches.Where(b => b.DeletedAt == null);

                var branchList = brbranch.Select(b => new BranchDTO
                {
                    branch = b,
                }).ToList();

                return branchList;
            } catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> AddEditBranch(Branch model, int userId)
        {
            try
            {
                DateTime CurrentDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                if (model == null) return false;
                else
                {
                    if (model.BranchId == default)
                    {
                        model.CreatedAt = CurrentDate;
                        model.ModifiedAt = CurrentDate;
                        model.CreatedBy = userId;
                        model.ModifiedBy = userId;

                        _context.Branches.Add(model);
                    }
                    else
                    {
                        var existingBranches = await _context.Branches.FirstOrDefaultAsync(c => c.BranchId == model.BranchId);

                        if (existingBranches != null)
                        {
                            existingBranches.BranchName = model.BranchName;
                            existingBranches.StateId = model.StateId;
                            existingBranches.CityId = model.CityId;
                            existingBranches.ModifiedBy = userId;
                            existingBranches.Address = model.Address;

                            _context.Branches.Update(existingBranches);
                        }
                    }
                    _context.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }


        #region DetailBranch
        public BranchDTO DetailBranch(int? branchId)
        {
            var branch = _context.Branches
                .Where(c => c.BranchId == branchId && c.DeletedAt == null)
                .Select(c => new BranchDTO
                {
                    branch = c
                }).FirstOrDefault();

            return branch;
        }

        #endregion

        #region DeleteBranch
        public async Task<bool> DeleteBranch(int? BranchId)
        {
            try
            {
                DateTime CurrentDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                var existingBranch = await _context.Branches.FirstOrDefaultAsync(branch => branch.BranchId == BranchId && branch.DeletedAt == null);
                if (existingBranch == null)
                    return false;
                else
                {
                    existingBranch.DeletedAt = CurrentDate;
                    _context.Branches.Update(existingBranch);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

    }
}
