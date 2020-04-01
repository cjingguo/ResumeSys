using ResumeSys.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeSysDal.Interface
{
   public interface IResumeDal
    {
        /// <summary>
        /// 根据用户id获取简历信息
        /// </summary>
        DataTable GetResumeByUserId(int id);

        /// <summary>
        /// 根据条件获取所有简历信息
        /// </summary>
        DataTable GetAllResume(string where);

        /// <summary>
        /// 根据姓名获取简历信息
        /// </summary>
        ResumeEntity GetInfoByRoleName(string roleName);

        /// <summary>
        /// 添加简历
        /// </summary>
        int AddResume(ResumeEntity role, List<ResumeExpEntity> reEntityList);

        /// <summary>
        /// 删除简历
        /// </summary>
        bool DeleteResume(string id);

        DataTable GetResumeExp(int ResumeId);

        /// <summary>
        /// 修改简历
        /// </summary>
        bool EditResumeInfo(ResumeEntity entity, List<ResumeExpEntity> reEntityList);
    }
}
