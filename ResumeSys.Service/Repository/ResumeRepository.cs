using ResumeSys.Data.Repository;
using ResumeSys.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeSys.Service
{
    public class ResumeRepository : RepositoryBase<ResumeEntity>, IResumeRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<ResumeEntity>(t => t.Id == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(ResumeEntity roleEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(roleEntity);
                }
                else
                {
                    db.Insert(roleEntity);
                }
                db.Commit();
            }
        }
    }
}
