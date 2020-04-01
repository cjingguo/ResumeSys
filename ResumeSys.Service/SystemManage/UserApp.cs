using ResumeSys.Code;
using ResumeSys.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeSys.Service.SystemManage
{
    public class UserApp
    {
        private IUserRepository service = new UserRepository();

        public UserEntity CheckLogin(string username, string password)
        {
            UserEntity userEntity = service.FindEntity(t => t.F_Account == username);
            if (userEntity != null)
            {
                string dbPassword = password;
                //string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                if (dbPassword == userEntity.F_UserPassword)
                {
                    return userEntity;
                }
                else
                {
                    throw new Exception("密码不正确，请重新输入");
                }
            }
            else
            {
                throw new Exception("账户不存在，请重新输入");
            }
        }
    }
}
