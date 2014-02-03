using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.ActiveDirectory
{
    public class CemeoAD
    {
        public static bool Authenticate(string username, string password, string domain)
        {
            bool authentic = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain,
                    username, password);
                object nativeObject = entry.NativeObject;

                authentic = true;
            }
            catch (DirectoryServicesCOMException) { }
            return authentic;
        }

        public static string GetUserIdFromDisplayName(string displayName, string password)
        {
            // set up domain context
            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "cemeo.be" ,"DC=cemeo,DC=be", displayName, password))
            {
                // find user by display name
                UserPrincipal user = UserPrincipal.FindByIdentity(ctx, displayName);

                // 
                if (user != null)
                {
                    return user.GivenName;
                    // or maybe you need user.UserPrincipalName;
                }
                else
                {
                    return string.Empty;
                }
        
            }
        }

        public static RegisterBindingModel GetRegisterBindingModelFromAD(string username, string password)
        {
            try
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "cemeo.be", "DC=cemeo,DC=be", username,password))
                {
                    // find user by display name
                    UserPrincipal user = UserPrincipal.FindByIdentity(ctx, username);

                    RegisterBindingModel rbm = new RegisterBindingModel();
                    rbm.FirstName = user.GivenName;
                    rbm.LastName = user.Surname;
                    rbm.Password = password;
                    rbm.ConfirmPassword = password;
                    rbm.EMail = user.EmailAddress;
                    rbm.UserName = username;
                    return rbm;
                }
            }
            catch(Exception)
            {
                
            }
            return null;
        }
    }
}