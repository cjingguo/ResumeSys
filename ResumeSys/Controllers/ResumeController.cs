//using ResumeSys.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using ResumeSys.Common;
using ResumeSys.Bll;
using ResumeSys.Entity;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace ResumeSys.Controllers
{
    public class ResumeController : Controller
    {
        //
        // GET: /Resume/
        private ResumeBll bll = new ResumeBll();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ResumeAdd()
        {
            return View();
        }

        public ActionResult ResumeEdit()
        {
            return View();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllResumeInfo()
        {
            string strWhere = "1=1";
            string sort = Request["sort"] == null ? "Id" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            if (!string.IsNullOrEmpty(Request["RealName"]) && !SqlInjection.GetString(Request["RealName"]))
            {
                strWhere += " and RealName like '%" + Request["RealName"] + "%'";
            }

            if (!string.IsNullOrEmpty(Request["IdCard"]) && !SqlInjection.GetString(Request["IdCard"]))
            {
                strWhere += " and IdCard like '%" + Request["IdCard"] + "%'";
            }

            int totalCount;   //输出参数
            string strJson = bll.GetPager("Resume_Base", "Id,RealName,Sex,Nation,IdCard,PoliticalAffiliation,Education,Major,IsMarry,IsBred,Residence,ResidenceType,Address,FillingDate ", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
            var jsonResult = new { total = totalCount.ToString(), rows = strJson };
            return Content("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");
        }

        /// <summary>
        /// 删除简历信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteResume()
        {
            try
            {
                string Ids = Request["IDs"] == null ? "" : Request["IDs"];
                if (!string.IsNullOrEmpty(Ids))
                {
                    if (bll.DeleteResume(Ids))
                    {
                        return Content("{\"msg\":\"删除成功！\",\"success\":true}");
                    }
                    else
                    {
                        return Content("{\"msg\":\"删除失败！\",\"success\":false}");
                    }
                }
                else
                {
                    return Content("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            catch (Exception ex)
            {
                return Content("{\"msg\":\"删除失败," + ex.Message + "\",\"success\":false}");
            }
        }

        /// <summary>
        /// 添加简历
        /// </summary>
        /// <returns></returns>
        public ActionResult AddResume()
        {
            try
            {
                #region 实体信息          
                ResumeEntity rEntity = new ResumeEntity();
                List<ResumeExpEntity> reEntityList = new List<ResumeExpEntity>();

                //rEntity.Id = System.Guid.NewGuid().ToString();
                rEntity.RealName = Request["txtRealName"];
                rEntity.Sex = Request["txtSex"];
                rEntity.Nation = Request["txtNation"];
                rEntity.IdCard = Request["txtIdCard"];
                rEntity.PoliticalAffiliation = Request["txtPoliticalAffiliation"];
                rEntity.Education = Request["txtEducation"];
                rEntity.Major = Request["txtMajor"];
                rEntity.IsMarry = Request["txtIsMarry"];
                rEntity.IsBred = Request["txtIsBred"];
                rEntity.Residence = Request["txtResidence"];
                rEntity.ResidenceType = Request["txtResidenceType"];
                rEntity.Address = Request["txtAddress"];

                if (Request["txtEducationalServices1"] != "")
                {
                    ResumeExpEntity reEntity1 = new ResumeExpEntity();
                    reEntity1.Id = System.Guid.NewGuid().ToString();
                    //reEntity1.ResumeId = rEntity.Id;
                    reEntity1.EducationalServices = Request["txtEducationalServices1"];
                    reEntity1.Major = Request["txtMajor1"];
                    reEntity1.StartTime = Request["txtStartTime1"];
                    reEntity1.Mode = Request["txtMode1"];
                    reEntity1.Diploma = Request["txtDiploma1"];
                    reEntityList.Add(reEntity1);
                }

                if (Request["txtEducationalServices2"] != "")
                {
                    ResumeExpEntity reEntity2 = new ResumeExpEntity();
                    reEntity2.Id = System.Guid.NewGuid().ToString();
                    //reEntity2.ResumeId = rEntity.Id;
                    reEntity2.EducationalServices = Request["txtEducationalServices2"];
                    reEntity2.Major = Request["txtMajor2"];
                    reEntity2.StartTime = Request["txtStartTime2"];
                    reEntity2.Mode = Request["txtMode2"];
                    reEntity2.Diploma = Request["txtDiploma2"];
                    reEntityList.Add(reEntity2);
                }

                if (Request["txtEducationalServices2"] != "")
                {
                    ResumeExpEntity reEntity3 = new ResumeExpEntity();
                    reEntity3.Id = System.Guid.NewGuid().ToString();
                    //reEntity3.ResumeId = rEntity.Id;
                    reEntity3.EducationalServices = Request["txtEducationalServices3"];
                    reEntity3.Major = Request["txtMajor3"];
                    reEntity3.StartTime = Request["txtStartTime3"];
                    reEntity3.Mode = Request["txtMode3"];
                    reEntity3.Diploma = Request["txtDiploma3"];
                    reEntityList.Add(reEntity3);
                }

                #endregion

                int ResumeId = bll.AddResume(rEntity, reEntityList);
                return Content("{\"msg\":\"添加成功！\",\"success\":true}");
                //if (ResumeId > 0)
                //{
                //    return Content("{\"msg\":\"添加成功！\",\"success\":true}");
                //}
                //else
                //{
                //    return Content("{\"msg\":\"添加失败！\",\"success\":false}");
                //}
            }
            catch (Exception ex)
            {
                return Content("{\"msg\":\"添加失败," + ex.Message + "\",\"success\":false}");
            }
        }

        public ActionResult GetResumeExp()
        {
            int ResumeId = Convert.ToInt32(Request["Id"]);
            string strJson = bll.GetResumeExp(ResumeId);

            return Content("{\"ResumeExpList\":" + strJson + "}");
        }

        public ActionResult EditResumeInfo()
        {
            try
            {
                #region 实体信息          
                ResumeEntity rEntity = new ResumeEntity();
                List<ResumeExpEntity> reEntityList = new List<ResumeExpEntity>();

                rEntity.Id = Request["rId"]; 
                rEntity.RealName = Request["txtRealName"];
                rEntity.Sex = Request["txtSex"];
                rEntity.Nation = Request["txtNation"];
                rEntity.IdCard = Request["txtIdCard"];
                rEntity.PoliticalAffiliation = Request["txtPoliticalAffiliation"];
                rEntity.Education = Request["txtEducation"];
                rEntity.Major = Request["txtMajor"];
                rEntity.IsMarry = Request["txtIsMarry"];
                rEntity.IsBred = Request["txtIsBred"];
                rEntity.Residence = Request["txtResidence"];
                rEntity.ResidenceType = Request["txtResidenceType"];
                rEntity.Address = Request["txtAddress"];

                if (Request["txtEducationalServices1"] != "")
                {
                    ResumeExpEntity reEntity1 = new ResumeExpEntity();
                    reEntity1.Id = Request["rExpId1"];
                    reEntity1.EducationalServices = Request["txtEducationalServices1"];
                    reEntity1.Major = Request["txtMajor1"];
                    reEntity1.StartTime = Request["txtStartTime1"];
                    reEntity1.Mode = Request["txtMode1"];
                    reEntity1.Diploma = Request["txtDiploma1"];
                    reEntityList.Add(reEntity1);
                }

                if (Request["txtEducationalServices2"] != "")
                {
                    ResumeExpEntity reEntity2 = new ResumeExpEntity();
                    reEntity2.Id = Request["rExpId2"];
                    //reEntity2.ResumeId = rEntity.Id;
                    reEntity2.EducationalServices = Request["txtEducationalServices2"];
                    reEntity2.Major = Request["txtMajor2"];
                    reEntity2.StartTime = Request["txtStartTime2"];
                    reEntity2.Mode = Request["txtMode2"];
                    reEntity2.Diploma = Request["txtDiploma2"];
                    reEntityList.Add(reEntity2);
                }

                if (Request["txtEducationalServices2"] != "")
                {
                    ResumeExpEntity reEntity3 = new ResumeExpEntity();
                    reEntity3.Id = Request["rExpId3"];
                    //reEntity3.ResumeId = rEntity.Id;
                    reEntity3.EducationalServices = Request["txtEducationalServices3"];
                    reEntity3.Major = Request["txtMajor3"];
                    reEntity3.StartTime = Request["txtStartTime3"];
                    reEntity3.Mode = Request["txtMode3"];
                    reEntity3.Diploma = Request["txtDiploma3"];
                    reEntityList.Add(reEntity3);
                }

                #endregion

                if (bll.EditResumeInfo(rEntity, reEntityList))
                {
                    return Content("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    return Content("{\"msg\":\"修改失败！\",\"success\":true}");
                }
            }
            catch (Exception ex)
            {
                return Content("{\"msg\":\"修改失败," + ex.Message + "\",\"success\":false}");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData == null || string.IsNullOrEmpty(fileData.FileName) || fileData.ContentLength == 0)
            {
                return Json(new { flag = false, message = "没有需要上传的文件" });
            }
            string file = Path.GetFileName(fileData.FileName);//获得文件名
            string extension = Path.GetExtension(fileData.FileName);//获得文件扩展名 
            string uploadDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string savedbname = "pic\\" + Path.GetFileNameWithoutExtension(fileData.FileName) + uploadDate + extension; //保存到数据库的文件名
            string fullsaveName = System.Web.HttpContext.Current.Request.MapPath("~\\") + savedbname;//完整路径
            fileData.SaveAs(fullsaveName);
            return Json(new { flag = true, path = savedbname });
        }

    }
}
