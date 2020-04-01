using ResumeSys.Code;
using ResumeSys.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeSys.Service
{
    public class ResumeApp
    {
        private IResumeRepository service = new ResumeRepository();

        public List<ResumeEntity> GetList(string RealName = "", string IdCard = "")
        {
            var expression = ExtLinq.True<ResumeEntity>();

            if (!string.IsNullOrEmpty("RealName"))
            {
                expression = expression.And(t => t.RealName.Contains(RealName));
            }

            if (!string.IsNullOrEmpty("IdCard"))
            {
                expression = expression.And(t => t.IdCard.Contains(IdCard));
            }

            return service.IQueryable(expression).OrderBy(t => t.FillingDate).ToList();
        }
    }
}
