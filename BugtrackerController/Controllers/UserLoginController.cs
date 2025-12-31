using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace BugtrackerController.Controllers
{
    public class UserLoginController : ApiController
    {
        private LoggedInUserDTO ObjLoggedInUserDTO = new LoggedInUserDTO();

        #region GET

        //Get User Details
        //Return user record
        public LoggedInUserDTO LoggedInUser()
        {

            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            foreach (Claim claim in claimsIdentity.Claims)
            {
                if (claim.Type == "OrgUserId")
                {
                    ObjLoggedInUserDTO._OrgUserId = Convert.ToInt32(claim.Value);
                }
                else if (claim.Type == "FirstName")
                {
                    ObjLoggedInUserDTO._FirstName = claim.Value;
                }
                else if (claim.Type == "LastName")
                {
                    ObjLoggedInUserDTO._LastName = claim.Value;
                }
                else if (claim.Type == "OrgUserStatusId")
                {
                    ObjLoggedInUserDTO._OrgUserStatusId = Convert.ToInt32(claim.Value);
                }
                else if (claim.Type == "PhoneNo")
                {
                    ObjLoggedInUserDTO._PhoneNo = claim.Value;
                }
                else if (claim.Type == "OrgId")
                {
                    ObjLoggedInUserDTO._OrgId = Convert.ToInt32(claim.Value);
                }
                else if (claim.Type == "OrgName")
                {
                    ObjLoggedInUserDTO._OrgName = claim.Value;
                }
                else if (claim.Type == "OrgStatusId")
                {
                    ObjLoggedInUserDTO._OrgStatusId = Convert.ToInt32(claim.Value);
                }
                else if (claim.Type == "OrgUserTypeId")
                {
                    ObjLoggedInUserDTO._OrgUserTypeId = Convert.ToInt32(claim.Value);
                }
                else if (claim.Type == "EmailId")
                {
                    ObjLoggedInUserDTO._EmailId = claim.Value;
                }
                else if (claim.Type == "ProfileImage")
                {
                    ObjLoggedInUserDTO._ProfileImage = claim.Value;
                }
                else if (claim.Type == "BioData")
                {
                    ObjLoggedInUserDTO._BioData = claim.Value;
                }
            }
            return ObjLoggedInUserDTO;
        }

        #endregion
    }
}
