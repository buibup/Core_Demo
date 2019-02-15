using Core_Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Demo.Interface
{
    public interface IJobService
    {
        Task<int> Delete(int JobId);
        Task<Job> GetByJobId(int JobId);
        Task<string> Update(Job job);
        Task<int> Create(Job JobDetails);
        Task<List<Job>> ListAll();
    }
}
