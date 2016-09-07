using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using BLL.Interface.Entities;
using System.Web.Helpers;
using System.Drawing;
using System.IO;

namespace MvcPL.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {

        public IUserService UserService 
            => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService 
            => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public MembershipUser CreateUser(string email, string password, byte[] avatarImg)
        {
            MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser != null)
            {
                return null;
            }

            /*Image img = Image.FromFile(avatarPath);
            byte[] byteImage = imgToByteArray(img);*/

            var user = new UserEntity
            {
                Email = email,
                Password = Crypto.HashPassword(password),
                CreatedOn = DateTime.Now,
                Photo = avatarImg
            };

            var role = RoleService.GetAllRoles().FirstOrDefault(r => r.Name == "User");

            if (role != null)
            {
                user.Roles.Add(role);
            }
            
            UserService.CreateUser(user);
            membershipUser = GetUser(email, false);
            return membershipUser;
        }

        public override bool ValidateUser(string email, string password)
        {
            var user = UserService.GetUserByEmail(email);

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                return true;

            return false;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = UserService.GetUserByEmail(email);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Email,
                null, null, null, null,
                false, false, user.CreatedOn,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        private byte[] imgToByteArray(Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, img.RawFormat);
                return mStream.ToArray();
            }
        }

        #region Stabs
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}