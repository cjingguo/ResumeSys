using ResumeSys.Service.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeSys.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        [HttpGet]
        public virtual ActionResult Index()
        {
            //var test = string.Format("{0:E2}",1);
            return View();
        }

        //获取验证码图片
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(),@"image/Gif");
        }

        public ActionResult CheckLogin(string username, string password, string code)
        {
            try
            {
                //if (Common.IsEmpty(Session["session_verifycode"]) || Md5.md5(code.ToLower(), 16) != Session["session_verifycode"].ToString())
                //{
                //    throw new Exception("验证码错误，请重新输入");
                //}

                UserEntity userEntity = new UserApp().CheckLogin(username, password);
                if (userEntity != null)
                {
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = userEntity.F_Id;
                    operatorModel.UserCode = userEntity.F_Account;
                    //operatorModel.UserName = userEntity.F_RealName;
                    //operatorModel.CompanyId = userEntity.F_OrganizeId;
                    //operatorModel.DepartmentId = userEntity.F_DepartmentId;
                    operatorModel.RoleId = userEntity.F_RoleId.ToString();
                    //operatorModel.LoginTime = DateTime.Now;
                    operatorModel.LoginToken = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    //if (userEntity.F_Account == "admin")
                    //{
                    //    operatorModel.IsSystem = true;
                    //}
                    //else
                    //{
                    //    operatorModel.IsSystem = false;
                    //}
                    OperatorProvider.Provider.AddCurrent(operatorModel);
                }
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }
    }
}
