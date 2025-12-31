using System;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Owin.Security.OAuth;
using BugTrackerBusiness.IBusiness;
using BugTrackerBusiness.Business;
using BugTrackerModels.DataModels;

namespace BugtrackerController.App_Start
{
    public class OAuthProvider: OAuthAuthorizationServerProvider
    {
        #region[GrantResourceOwnerCredentials]
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var userName = context.UserName;
                var password = context.Password;

                try
                {
                    MiscellaneousInterface miscellaneousInterface = new MiscellaneousBusiness();

                    LoggedInUserDTO loggedInUserDTO = miscellaneousInterface.UserLogin(userName, password);

                    if (loggedInUserDTO != null)
                    {
                        if(loggedInUserDTO._OrgErrMsg != null)
                        {
                            context.SetError("invalid_grant", loggedInUserDTO._OrgErrMsg);
                            return;
                        }

                        var claims = new List<Claim>()
                    {
                        new Claim("OrgUserId", Convert.ToString(loggedInUserDTO._OrgUserId)),
                        new Claim("FirstName", Convert.ToString(loggedInUserDTO._FirstName)),
                        new Claim("LastName", Convert.ToString(loggedInUserDTO._LastName)),
                        new Claim("OrgUserStatusId", Convert.ToString(loggedInUserDTO._OrgUserStatusId)),
                        new Claim("PhoneNo", Convert.ToString(loggedInUserDTO._PhoneNo)),
                        new Claim("OrgId", Convert.ToString(loggedInUserDTO._OrgId)),
                        new Claim("OrgName", Convert.ToString(loggedInUserDTO._OrgName)),
                        new Claim("OrgStatusId", Convert.ToString(loggedInUserDTO._OrgStatusId)),
                        new Claim("OrgUserTypeId", Convert.ToString(loggedInUserDTO._OrgUserTypeId)),
                        new Claim("EmailId", Convert.ToString(loggedInUserDTO._EmailId)),
                        new Claim("ProfileImage", Convert.ToString(loggedInUserDTO._ProfileImage)),
                        new Claim("BioData", Convert.ToString(loggedInUserDTO._BioData)),
                        new Claim("OrgPlanId", Convert.ToString(loggedInUserDTO._OrgPlanId))
                    };
                        ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims,
                                    Startup.OAuthOptions.AuthenticationType);

                        var properties = CreateProperties(loggedInUserDTO);
                        var ticket = new AuthenticationTicket(oAuthIdentity, properties);
                        context.Validated(ticket);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect");
                    }
                }
                catch(Exception ex){
                    throw ex;
                }
              
            });
        }
        #endregion

        #region[ValidateClientAuthentication]
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
                context.Validated();

            return Task.FromResult<object>(null);
        }
        #endregion

        #region[TokenEndpoint]
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
        #endregion

        #region[CreateProperties]
        public static AuthenticationProperties CreateProperties(LoggedInUserDTO Model)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "OrgUserId", Model._OrgUserId.ToString() },
                { "FirstName", Model._FirstName },
                { "LastName", Model._LastName },
                { "EmailId", Model._EmailId },
                { "OrgUserStatusId", Model._OrgUserStatusId.ToString() },
                { "PhoneNo", Model._PhoneNo.ToString() },
                { "OrgId", Model._OrgId.ToString() },
                { "OrgStatusId", Model._OrgStatusId.ToString() },
                { "OrgUserTypeId", Model._OrgUserTypeId.ToString() },
                { "OrgName", Model._OrgName },
                { "ProfileImage", Model._ProfileImage },
                { "BioData", Model._BioData },
                { "OrgPlanId",Model._OrgPlanId.ToString()}
            };
            return new AuthenticationProperties(data);
        }
        #endregion
    }
}