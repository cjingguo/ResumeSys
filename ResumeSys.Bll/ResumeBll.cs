using ResumeSys.Common;
using ResumeSys.Entity;
using ResumeSysDal;
using ResumeSysDal.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeSys.Bll
{
    public class ResumeBll
    {
        IResumeDal dal = new ResumeDal();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">要取的列名（逗号分开）</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="where">查询条件</param>
        /// <param name="totalCount">总记录数</param>
        public string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            DataTable dt = SqlPagerHelper.GetPager(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return JsonHelper.ToJson(dt);
        }

        public string GetResumeExp(int ResumeId)
        {
            DataTable dt = dal.GetResumeExp(ResumeId);
            return JsonHelper.ToJson(dt);
        }

        public bool DeleteResume(string id)
        {
          return  dal.DeleteResume(id);
        }

        public int AddResume(ResumeEntity entity, List<ResumeExpEntity> reEntityList)
        {
            return dal.AddResume(entity, reEntityList);
        }

        public bool EditResumeInfo(ResumeEntity entity, List<ResumeExpEntity> reEntityList)
        {
            return dal.EditResumeInfo(entity, reEntityList);
        }
    }
}
