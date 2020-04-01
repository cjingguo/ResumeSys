using ResumeSys.Common;
using ResumeSys.Entity;
using ResumeSysDal.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeSysDal
{
    public class ResumeDal:IResumeDal
    {
         /// <summary>
        /// 根据用户id获取简历信息
        /// </summary>
        public DataTable GetResumeByUserId(int id)
        {
            return null;
        }

        /// <summary>
        /// 根据条件获取所有简历信息
        /// </summary>
        public DataTable GetAllResume(string where)
        {
            return null;
            
        }

        /// <summary>
        /// 根据姓名获取简历信息
        /// </summary>
        public ResumeEntity GetInfoByRoleName(string roleName)
        {
            return null;
        }

        /// <summary>
        /// 添加简历
        /// </summary>
        public int AddResume(ResumeEntity entity, List<ResumeExpEntity> reEntityList)
        {
            int iResut = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Resume_Base(RealName,Sex,Nation,IdCard,PoliticalAffiliation,Education,Major,IsMarry,IsBred,Residence,ResidenceType,Address,FillingDate)");
            strSql.Append(" values ");
            strSql.Append("(@RealName,@Sex,@Nation,@IdCard,@PoliticalAffiliation,@Education,@Major,@IsMarry,@IsBred,@Residence,@ResidenceType,@Address,@FillingDate)");
            strSql.Append(";SELECT @@IDENTITY");   //返回插入用户的主键
            SqlParameter[] paras = { 
                                   //new SqlParameter("@Id",entity.Id),
                                   new SqlParameter("@RealName",entity.RealName),
                                   new SqlParameter("@Sex",entity.Sex),
                                   new SqlParameter("@Nation",entity.Nation),
                                   new SqlParameter("@IdCard",entity.IdCard),
                                   new SqlParameter("@PoliticalAffiliation",entity.PoliticalAffiliation),
                                   new SqlParameter("@Education",entity.Education),
                                   new SqlParameter("@Major",entity.Major),
                                   new SqlParameter("@IsMarry",entity.IsMarry),
                                   new SqlParameter("@IsBred",entity.IsBred),
                                   new SqlParameter("@Residence",entity.Residence),
                                   new SqlParameter("@ResidenceType",entity.ResidenceType),
                                   new SqlParameter("@Address",entity.Address),
                                   new SqlParameter("@FillingDate",DateTime.Now.ToString("yyyy-MM-dd")),
                                   };

            iResut = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into Resume_EducationExp(Id,ResumeId,EducationalServices,Major,StartTime,Mode,Diploma)");
            strSql2.Append(" values ");
            strSql2.Append("(@Id,@ResumeId,@EducationalServices,@Major,@StartTime,@Mode,@Diploma)");

            foreach (ResumeExpEntity reEntity in reEntityList)
            {
                SqlParameter[] paras2 = { 
                                   new SqlParameter("@Id",reEntity.Id),
                                   new SqlParameter("@ResumeId",iResut),
                                   new SqlParameter("@EducationalServices",reEntity.EducationalServices),
                                   new SqlParameter("@Major",reEntity.Major),
                                   new SqlParameter("@StartTime",reEntity.StartTime),
                                   new SqlParameter("@Mode",reEntity.Mode),
                                   new SqlParameter("@Diploma",reEntity.Diploma),
                                   };
                Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql2.ToString(), paras2));
            }

            return iResut;
        }

        /// <summary>
        /// 删除简历
        /// </summary>
        public bool DeleteResume(string id)
        {
            bool res = false;
            try
            {
                string sql = "delete from Resume_Base where Id = " + id;
                string sql2 = "delete from Resume_EducationExp where ResumeId = " + id;
                SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, sql.Replace(",",""), null);
                SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, sql2.Replace(",", ""), null);

                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }

        public DataTable GetResumeExp(int ResumeId)
        {
            string sql = "select Id,ResumeId,EducationalServices,Major,StartTime,Mode,Diploma from Resume_EducationExp where ResumeId=@ResumeId";
            return SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sql, new SqlParameter("@ResumeId", ResumeId));
        }

        /// <summary>
        /// 修改简历
        /// </summary>
        public bool EditResumeInfo(ResumeEntity entity, List<ResumeExpEntity> reEntityList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Resume_Base set ");
            strSql.Append("RealName=@RealName,Sex=@Sex,Nation=@Nation,IdCard=@IdCard,PoliticalAffiliation=@PoliticalAffiliation,");
            strSql.Append("Education=@Education,Major=@Major,IsMarry=@IsMarry,IsBred=@IsBred,Residence=@Residence,ResidenceType=@ResidenceType,Address=@Address,FillingDate=@FillingDate");
            strSql.Append(" where Id=@Id");

            SqlParameter[] paras = {
                                   new SqlParameter("@Id",entity.Id),
                                   new SqlParameter("@RealName",entity.RealName),
                                   new SqlParameter("@Sex",entity.Sex),
                                   new SqlParameter("@Nation",entity.Nation),
                                   new SqlParameter("@IdCard",entity.IdCard),
                                   new SqlParameter("@PoliticalAffiliation",entity.PoliticalAffiliation),
                                   new SqlParameter("@Education",entity.Education),
                                   new SqlParameter("@Major",entity.Major),
                                   new SqlParameter("@IsMarry",entity.IsMarry),
                                   new SqlParameter("@IsBred",entity.IsBred),
                                   new SqlParameter("@Residence",entity.Residence),
                                   new SqlParameter("@ResidenceType",entity.ResidenceType),
                                   new SqlParameter("@Address",entity.Address),
                                   new SqlParameter("@FillingDate",DateTime.Now.ToString("yyyy-MM-dd")),
                                   };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update Resume_EducationExp set ");
            strSql2.Append("EducationalServices=@EducationalServices,Major=@Major,StartTime=@StartTime,Mode=@Mode,Diploma=@Diploma");
            strSql2.Append(" where Id=@Id ");

            foreach (ResumeExpEntity reEntity in reEntityList)
            {
                SqlParameter[] paras2 = {
                                   new SqlParameter("@Id",reEntity.Id),
                                   new SqlParameter("@EducationalServices",reEntity.EducationalServices),
                                   new SqlParameter("@Major",reEntity.Major),
                                   new SqlParameter("@StartTime",reEntity.StartTime),
                                   new SqlParameter("@Mode",reEntity.Mode),
                                   new SqlParameter("@Diploma",reEntity.Diploma),
                                   };
                object obj2 = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql2.ToString(), paras2);
            }

            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }
    }
}
