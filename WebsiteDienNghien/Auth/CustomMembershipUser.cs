using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebsiteDienNghien.Models;

namespace WebsiteDienNghien.Auth
{
    public class CustomMembershipUser : MembershipUser
    {
        QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        #region User Properties

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<role> Roles { get; set; }

        #endregion

        public CustomMembershipUser(account user) : base("CustomMembership", user.username, user.id, user.email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.id;
            FirstName = user.firstname;
            LastName = user.lassname;
            Roles = user.roles;
        }
    }
}