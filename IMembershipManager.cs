using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace Sample.Utility
{
    // the user membership manager interface
    public interface IMembershipManager
    {
        void Init();
        MembershipUser GetUser( string username );
        bool ValidateUser( string username, string password );
        MembershipUser CreateUser( string username, string password, string email, bool isApproved, object providerUserKey, out MembershipCreateStatus status );
        void UpdateUser( MembershipUser user );
        bool DeleteUser( string username );

        string ApplicationName { get; set; }
        string ConnectionString { get; set; }
    }
}
