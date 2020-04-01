using ResumeSys.Data.Repository;
using ResumeSys.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeSys.Service
{
    public interface IResumeRepository : IRepositoryBase<ResumeEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(ResumeEntity resumeEntity, string keyValue);
    }
}
