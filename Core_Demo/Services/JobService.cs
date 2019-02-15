using Core_Demo.Entities;
using Core_Demo.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Demo.Services
{
    public class JobService : IJobService
    {
        private readonly IDapperHelper _dapperHelper;

        public JobService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<int> Create(Job job)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("JobTitle", job.JobTitle, DbType.String);
            dbPara.Add("JobImage", job.JobImage, DbType.String);
            dbPara.Add("CityId", job.CityId, DbType.Int32);
            dbPara.Add("IsActive", job.IsActive, DbType.String);
            dbPara.Add("CreatedBY", "1", DbType.String);
            dbPara.Add("CreatedDateTime", DateTime.Now, DbType.DateTime);
            dbPara.Add("UpdatedBY", "1", DbType.String);
            dbPara.Add("UpdatedDateTime", DateTime.Now, DbType.DateTime);

            #region using dapper  
            var data = await _dapperHelper.Insert<int>("[dbo].[SP_Add_Job]",
                            dbPara,
                            commandType: CommandType.StoredProcedure);
            return data;
            #endregion

        }

        public async Task<Job> GetByJobId(int JobId)
        {
            #region using dapper  
            var data = await _dapperHelper.Get<Job>($"select * from job  where JobId={JobId}", null,
                    commandType: CommandType.Text);
            return data;
            #endregion


        }

        public async Task<int> Delete(int JobId)
        {
            var data = await _dapperHelper.Execute($"Delete [Job] where JObId={JobId}", null,
                    commandType: CommandType.Text);
            return data;
        }

        public async Task<List<Job>> ListAll()
        {
            var data = await _dapperHelper.GetAll<Job>("[dbo].[SP_Job_List]", null, commandType: CommandType.StoredProcedure);
            return data.ToList();

        }

        public async Task<string> Update(Job job)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("JobTitle", job.JobTitle, DbType.String);
            dbPara.Add("JobId", job.JobID);
            dbPara.Add("JobImage", job.JobImage, DbType.String);
            dbPara.Add("CityId", job.CityId, DbType.Int32);
            dbPara.Add("IsActive", job.IsActive, DbType.String);
            dbPara.Add("UpdatedBY", "1", DbType.String);
            dbPara.Add("UpdatedDateTime", DateTime.Now, DbType.DateTime);


            var data = await _dapperHelper.Update<string>("[dbo].[SP_Update_Job]",
                            dbPara,
                            commandType: CommandType.StoredProcedure);
            return data;


        }
    }
}
