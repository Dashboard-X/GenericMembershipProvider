using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Configuration;
using System.Reflection;
using System.Runtime.Remoting;


namespace Sample.Utility
{
    // generic membership provider that use IMembershipManager
    public sealed class GenericMembershipProvider : MembershipProvider
    {

        private string m_ApplicationName;
        private string m_ConnectionStringName;
        private string m_ConnectionString;
        private IMembershipManager m_MembershipManager;

        public override string ApplicationName
        {
            get { return m_ApplicationName; }
            set { m_ApplicationName = value; }
        }

        public IMembershipManager MembershipManager
        {
            get { return m_MembershipManager; }
        }

	// provider initialization 
        public override void Initialize( string name, System.Collections.Specialized.NameValueCollection config )
        {
            if( config == null )
                throw new ArgumentNullException( "config" );

            if( String.IsNullOrEmpty( name ) )
                name = "GenericMembershipProvider";

            m_ApplicationName = config["applicationName"];
            m_ConnectionStringName = config["connectionStringName"];

            if( String.IsNullOrEmpty( m_ConnectionStringName ) )
            {
                throw new ArgumentException( "Attribute: connectionStringName, not set!" );
            }

            m_ConnectionString = ConfigurationManager.ConnectionStrings[m_ConnectionStringName].ConnectionString;

	    // get concrete MembershipManager nama from web.config
            string membershipManagerTypeName = config["membershipManagerType"];

            if( String.IsNullOrEmpty( membershipManagerTypeName ) )
            {
                throw new ArgumentException( "Attribute: membershipManagerType, not set!" );
            }

	    // create of memmebrship manager
            string[] tokens = membershipManagerTypeName.Split( ',' );
            ObjectHandle oHandle = Activator.CreateInstance( tokens[1].Trim(), tokens[0].Trim() );
            m_MembershipManager = oHandle.Unwrap() as IMembershipManager;
            m_MembershipManager.ApplicationName = m_ApplicationName;
            m_MembershipManager.ConnectionString = m_ConnectionString;

            m_MembershipManager.Init();

            base.Initialize( name, config );
        }

        public override MembershipUser CreateUser( string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status )
        {            
            throw new Exception( "The method or operation is not implemented." );
        }

        public override bool DeleteUser( string username, bool deleteAllRelatedData )
        {
            return m_MembershipManager.DeleteUser( username  );
        }

        public override MembershipUser GetUser( string username, bool userIsOnline )
        {
            MembershipUser user = m_MembershipManager.GetUser( username );

            if( user != null )
            {
                return user;
            }
            else
            {
                throw new Exception( String.Format( "MembershipUser error, username: {0} not found!", username ) );
            }
        }

        public override MembershipUser GetUser( object providerUserKey, bool userIsOnline )
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override bool ChangePassword( string username, string oldPassword, string newPassword )
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override bool ChangePasswordQuestionAndAnswer( string username, string password, string newPasswordQuestion, string newPasswordAnswer )
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override bool EnablePasswordReset
        {
            get { throw new Exception( "The method or operation is not implemented." ); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new Exception( "The method or operation is not implemented." ); }
        }

        public override MembershipUserCollection FindUsersByEmail( string emailToMatch, int pageIndex, int pageSize, out int totalRecords )
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override MembershipUserCollection FindUsersByName( string usernameToMatch, int pageIndex, int pageSize, out int totalRecords )
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override MembershipUserCollection GetAllUsers( int pageIndex, int pageSize, out int totalRecords )
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override string GetPassword( string username, string answer )
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override string GetUserNameByEmail( string email )
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new Exception( "The method or operation is not implemented." ); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new Exception( "The method or operation is not implemented." ); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new Exception( "The method or operation is not implemented." ); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new Exception( "The method or operation is not implemented." ); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new Exception( "The method or operation is not implemented." ); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new Exception( "The method or operation is not implemented." ); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override string ResetPassword( string username, string answer )
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override bool UnlockUser( string userName )
        {
            throw new Exception( "The method or operation is not implemented." );
        }

        public override void UpdateUser( MembershipUser user )
        {
            return m_MembershipManager.UpdateUser( user  );
        }

        public override bool ValidateUser( string username, string password )
        {
            return m_MembershipManager.ValidateUser( username, password );
        }
    }
}
