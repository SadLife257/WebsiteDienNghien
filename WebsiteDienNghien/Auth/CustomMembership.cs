using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebsiteDienNghien.Models;

namespace WebsiteDienNghien.Auth
{
    public class CustomMembership : MembershipProvider
    {
        QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        public override bool EnablePasswordRetrieval => throw new NotImplementedException();

        public override bool EnablePasswordReset => throw new NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

        public override int PasswordAttemptWindow => throw new NotImplementedException();

        public override bool RequiresUniqueEmail => throw new NotImplementedException();

        public override MembershipPasswordFormat PasswordFormat => throw new NotImplementedException();

        public override int MinRequiredPasswordLength => throw new NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

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
            //Missing
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

        //MODIFIED
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = (from us in db.accounts
                        where string.Compare(username, us.username, StringComparison.OrdinalIgnoreCase) == 0
                        select us).FirstOrDefault();

            if (user == null)
            {
                return null;
            }
            var selectedUser = new CustomMembershipUser(user);

            return selectedUser;
        }

        //MODIFIED
        public override string GetUserNameByEmail(string email)
        {
            string username = (from u in db.accounts
                               where String.Compare(email, u.email) == 0
                               select u.username).FirstOrDefault();

            return !string.IsNullOrEmpty(username) ? username : string.Empty;
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

        //MODIFIED
        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var user = (from us in db.accounts
                       where string.Compare(username, us.username, StringComparison.OrdinalIgnoreCase) == 0
                       && string.Compare(password, us.password, StringComparison.OrdinalIgnoreCase) == 0
                       select us).FirstOrDefault();

            return (user != null) ? true : false;
        }
    }
}