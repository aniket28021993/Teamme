using BugTrackerModels.DataModels;
using BugTrackerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.Business
{
    public class MailBusiness
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

        public void SuccessfullRegistration(OrganizationDTO Model)
        {
            try
            {

                MailMessage mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress(Model._OrgEmail, Model._OrgName));

                // From
                mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                mailMsg.Subject = string.Format("Welcome {0} to Teamme", Model._OrgName);

                var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                                <h3>welcome!</h3>
                                <h5>Thank you for registering with TeamME!</h5>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>
                                    <i>Our customer support team will contact you in the next 24hrs to discuss your requirements.</i>
                                    <p>We built TeamMe to help teams manage performance effectively on a whole new level. In the meantime if you need more info please visit <a href='teamme.azurewebsites.net' target='_blank'>teamme.azurewebsites.net</a></p>
                                    <p>We look forward to working with you.</p>
                                    <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                        <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                    </p>
                                </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 
                                </p>
                            </div>
                        </div>
                    </body>
                </html>
", Model._OrgName);


                mailMsg.Body = htmlString;
                mailMsg.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMsg);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
            }
        }
        public void ContactUs(ContactUsDTO Model)
        {
            try
            {

                MailMessage mailMsg = new MailMessage();
                // To
                mailMsg.To.Add(new MailAddress(smtpSection.Network.UserName));

                // From
                mailMsg.From = new MailAddress(Model.ContactEmail, Model.ContactName);

                mailMsg.Subject = string.Format("Contact Us - {0}", Model.ContactEmail);

                var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi TeamMe,</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>
                                    <i>{0} has contact support team for his queries. please go through the queries and reply as soon as possible on <a href='mailto:{1}' target='_blank'>{1}</a></i>
                                <p>{2}</p>
                                <p>{0} is looking forward to have a chat or conversation on email with TeamMe.</p>
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>{0}</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>",Model.ContactName,Model.ContactEmail,Model.ContactMessage);


                mailMsg.Body = htmlString;
                mailMsg.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMsg);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
            }
        }
        public void SuccessfullAdmin(OrgUserDTO Model)
        {
            string webURL = ConfigurationManager.AppSettings["WebUrl"];
            string MainURL = ConfigurationManager.AppSettings["MainUrl"];
            try
            {
                MailMessage mailMsg = new MailMessage();

                string UserName = Model.FirstName + " " + Model.LastName;
                // To
                mailMsg.To.Add(new MailAddress(Model.EmailId, UserName));

                // From
                mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                mailMsg.Subject = string.Format("Successfully Admin created");

                var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
				<h3>Congralutions</h3>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i>A warm welcome to the TeamME platform,now you can manage and track your team performance at one place.</i>
                                <div>
                    <br/>
				  <div> you can start your journey using <a href='{3}'>{3}</a> for the below admin credentials </div>
				  <br/>
				  <div> UserName : {1} </div>
				  <br/>
				  <div> Password : {2} </div>	
				</div>
				<p><span style='font-family:cursive; color:#dd3d31;'>NOTE</span> : We highly suggest you to change your password after login into <a href='{4}'>{4} as this is a system generate password.</a></p>
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", UserName, Model.EmailId, Model.Password, webURL, MainURL);


                mailMsg.Body = htmlString;
                mailMsg.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMsg);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
            }
        }
        public void SuccessfullUser(OrgUserDTO Model)
        {
            string webURL = ConfigurationManager.AppSettings["WebUrl"];
            string MainURL = ConfigurationManager.AppSettings["MainUrl"];

            try
            {
                MailMessage mailMsg = new MailMessage();

                string UserName = Model.FirstName + " " + Model.LastName;
                // To
                mailMsg.To.Add(new MailAddress(Model.EmailId, UserName));

                // From
                mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                mailMsg.Subject = string.Format("Successfully User created");

                var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
				<h3>Congralutions</h3>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i>A warm welcome to the TeamME platform,now you can start managing your backlogs, tasks and many more at one place.</i>
                                <div>
                    <br/>
				  <div> you can start your journey using <a href='{3}'>{3}</a> for the below credentials </div>
				  <br/>
				  <div> UserName : {1} </div>
				  <br/>
				  <div> Password : {2} </div>	
				</div>
				<p><span style='font-family:cursive; color:#dd3d31;'>NOTE</span> : We highly suggest you to change your password after login into <a href='{4}'>{4}</a> as this is a system generated password.</p>
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", UserName, Model.EmailId, Model.Password, webURL, MainURL);


                mailMsg.Body = htmlString;
                mailMsg.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMsg);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
            }
        }
        public void SendOTP(string Email, int OTP)
        {
            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    var UserName = mailRepository.GetUserNameFromOrgUser(con, Email);
                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(Email));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("Successfully Send OTP");

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i>Oops..!! Forgot your password,Not to worry!!</i>
                                <div>
                    <br/>
				  <div> You need to use below one time password to reset your password.</div>
				  <br/>
				  <div> OTP : {1} </div>	
				</div>
				<p><span style='font-family:cursive; color:#dd3d31;'>NOTE</span> : Please don't share this one time password with anyone.</p>
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", UserName, OTP);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
                
        }

        public void UpgradeOrgPlan(LoggedInUserDTO Model, int OrgPlanId)
        {
            string OrgPlanName = string.Empty;
            string MainURL = ConfigurationManager.AppSettings["MainUrl"];

            try
            {
                if (OrgPlanId == 2)
                {
                    OrgPlanName = "PLUS";
                }
                else if (OrgPlanId == 3)
                {
                    OrgPlanName = "PRO";
                }


                MailMessage mailMsg = new MailMessage();
                // To
                mailMsg.To.Add(new MailAddress(smtpSection.Network.UserName));

                // From
                mailMsg.From = new MailAddress(Model._EmailId);

                mailMsg.Subject = string.Format("Upgarde plan - {0}", Model._EmailId);

                var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi TeamME,</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i>Hurray..!! you are one step ahead to reach your milestone.</i>
                                <div>
				<br/>
				  <div> You have received details of plan where {0} wants to upgrade their plan to {1}</div>
				  <br/>
				  <div>please contact {0} admin for the payment details and then approve their plan using <a href='{2}'>{2}</a></div>	
				</div>
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", Model._OrgName, OrgPlanName, MainURL);


                mailMsg.Body = htmlString;
                mailMsg.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMsg);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
            }
        }
        public void OrgApprovePlan(OrganizationDTO Model, int OrgPlanId)
        {
            string OrgPlanName = string.Empty;
            string AddUser = string.Empty;
            string MainURL = ConfigurationManager.AppSettings["MainUrl"];

            try
            {
                if (OrgPlanId == 2)
                {
                    OrgPlanName = "PLUS";
                    AddUser = "Upto 30";

                }
                else if (OrgPlanId == 3)
                {
                    OrgPlanName = "PRO";
                    AddUser = "Unlimited";
                }

                MailMessage mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress(Model._OrgEmail, Model._OrgName));

                // From
                mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                mailMsg.Subject = string.Format("{0} Aprrove Plan", Model._OrgName);

                var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
				<h3>Congralutions</h3>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i>Hurray..!! you are one step ahead to manage your team at one place.</i>
                                <div>
				<br/>
				  <div> TeamME has received details of your payment and approved your {1} plan.</div>
				  <br/>
				  <div>Now you can start using all TeamME functionality and can add {3} users using <a href='{2}'>{2}</a></div>	
				</div>
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", Model._OrgName, OrgPlanName, MainURL, AddUser);


                mailMsg.Body = htmlString;
                mailMsg.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMsg);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
            }
        }
        public void OrgStatusChange(int OrgId, int OrgStatusId)
        {
            string OrgStatus = string.Empty;

            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    var OrgData = mailRepository.GetOrgData(con, OrgId);

                    if (OrgStatusId == 2)
                    {
                        OrgStatus = "ACTIVE";
                    }
                    else if (OrgStatusId == 3)
                    {
                        OrgStatus = "PASUED";
                    }
                    else if (OrgStatusId == 4)
                    {
                        OrgStatus = "SUSPENDED";
                    }
                    else if (OrgStatusId == 5)
                    {
                        OrgStatus = "REJECTED";
                    }

                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(OrgData[0], OrgData[1]));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("{0} Status Change", OrgData[1]);

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i>TeamMe support team has changed your organization status to {1}.</i>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", OrgData[1], OrgStatus);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void OrgUserStatusChange(int OrgUserId, int OrgStatusId)
        {
            string OrgStatus = string.Empty;

            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    var OrgData = mailRepository.GetOrgUserData(con, OrgUserId);

                    if (OrgStatusId == 1)
                    {
                        OrgStatus = "CREATED";
                    }
                    else if (OrgStatusId == 2)
                    {
                        OrgStatus = "ACTIVE";
                    }
                    else if (OrgStatusId == 3)
                    {
                        OrgStatus = "PASUED";
                    }
                    else if (OrgStatusId == 4)
                    {
                        OrgStatus = "SUSPENDED";
                    }

                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(OrgData[0]));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("Status Change");

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i> Your organization admin has changed your status to {1}.</i>
				    <p>
				    	If you have any doubts or queries,please contact your admin.
				    </p>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", OrgData[1],OrgStatus);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void SendPayemntDetails(int OrgId, int OrgAmount)
        {
            DateTime currdate = DateTime.UtcNow;

            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    var OrgData = mailRepository.GetOrgData(con, OrgId);

                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(OrgData[0], OrgData[1]));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("{0} Aprrove Plan", OrgData[1]);

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i> We are glad to inform you that we have received your payment of Rs.{1} for the month {2}.</i>
				    <p>
				    	Thank you for the payment and we hope we are able to solve your problems through the TeamME portal.
				    </p>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", OrgData[1], OrgAmount, currdate);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }


        public void ProductBacklogCreate(ProductBacklogDTO Model)
        {
            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    var EmailId = mailRepository.GetOrgUserData(con, Model.AssignedTo);
                    var Developer = mailRepository.GetOrgUserData(con, Model.AssignedDeveloper);
                    var Designer = mailRepository.GetOrgUserData(con, Model.AssignedDesigner);
                    var Tester = mailRepository.GetOrgUserData(con, Model.AssignedTester);
                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(EmailId[0]));
                    mailMsg.Bcc.Add(new MailAddress(Developer[0]));
                    mailMsg.Bcc.Add(new MailAddress(Designer[0]));
                    mailMsg.Bcc.Add(new MailAddress(Tester[0]));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("Successfully Product Backlog created");

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi Team,</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i> We are glad to inform you that we have assigned you new backlog.</i>
				    <div>
					<p>Backlog Title: {0}</p>
					<p>Owner Name : {1} </p>
					<p>Developer Name : {2} </p>
					<p>Designer Name : {3} </p>
					<p>Tester Name : {4} </p>					
				    </div>
				<p>Now you can start creating task and testcase against your backlog and co-ordinate with every team member so you all make this backlog live with in the given estimation time.</p>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", Model.Title,EmailId[1],Developer[1],Designer[1],Tester[1]);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void ProductBacklogEdit(ProductBacklogDTO Model)
        {
            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    string EmailId = mailRepository.GetEmailFromOrgUser(con, Model.AssignedTo);
                    string Developer = mailRepository.GetEmailFromOrgUser(con, Model.AssignedDeveloper);
                    string Designer = mailRepository.GetEmailFromOrgUser(con, Model.AssignedDesigner);
                    string Tester = mailRepository.GetEmailFromOrgUser(con, Model.AssignedTester);
                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(EmailId));
                    mailMsg.Bcc.Add(new MailAddress(Developer));
                    mailMsg.Bcc.Add(new MailAddress(Designer));
                    mailMsg.Bcc.Add(new MailAddress(Tester));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("Successfully Product Backlog Edited");

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi Team,</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i> We want to inform you that there are few changes made to your assigned backlog - {0}.</i>
				<p>Please go through the backlog assigned to you and make changes in your task, enhancement, bug and testcase accordingly and co-ordinate with every team member so you all make this backlog live with in the given estimation time.</p>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", Model.Title);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void ProductBacklogBugCreate(ProductBacklogDataDTO Model)
        {
            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    string EmailId = mailRepository.GetEmailFromOrgUser(con, Model.AssignedTo);
                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(EmailId, Model.UserName));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("Successfully Bug created");

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i> We are glad to inform you that you have been assigned bug - {1}.</i>
				<p>Please go through the bug assigned to you and if you have any queries, please contact your assigned tester | team member.</p>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", Model.UserName, Model.Title);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void ProductBacklogBugEdit(ProductBacklogDataDTO Model)
        {
            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    string EmailId = mailRepository.GetEmailFromOrgUser(con, Model.AssignedTo);
                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(EmailId, Model.UserName));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("Successfully Bug Edited");

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i> We want to inform you that there are changes done in bug - {1}.</i>
				<p>Please go through the bug assigned to you and if you have any queries, please contact your assigned tester | team member.</p>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", Model.UserName, Model.Title);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void ProductBacklogTaskCreate(ProductBacklogDataDTO Model)
        {
            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    string EmailId = mailRepository.GetEmailFromOrgUser(con, Model.AssignedTo);
                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(EmailId, Model.UserName));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("Successfully Task created");

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i> We are glad to inform you that you have been assigned task - {1}.</i>
				<p>Please go through the task assigned to you and if you have any queries, please contact your assigned tester | team member.</p>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", Model.UserName, Model.Title);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void ProductBacklogTaskEdit(ProductBacklogDataDTO Model)
        {
            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    string EmailId = mailRepository.GetEmailFromOrgUser(con, Model.AssignedTo);
                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(EmailId, Model.UserName));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("Successfully Task Edited");

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i> We want to inform you that there are changes done in task - {1}.</i>
				<p>Please go through the task assigned to you and if you have any queries, please contact your assigned tester | team member.</p>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", Model.UserName, Model.Title);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void ProductBacklogEnhancementCreate(ProductBacklogDataDTO Model)
        {
            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    string EmailId = mailRepository.GetEmailFromOrgUser(con, Model.AssignedTo);
                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(EmailId, Model.UserName));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("Successfully Enhancement created");

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i> We are glad to inform you that you have been assigned Enhancement - {1}.</i>
				<p>Please go through the Enhancement assigned to you and if you have any queries, please contact your assigned tester | team member.</p>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", Model.UserName, Model.Title);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void ProductBacklogEnhancementEdit(ProductBacklogDataDTO Model)
        {
            MailRepository mailRepository = new MailRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    string EmailId = mailRepository.GetEmailFromOrgUser(con, Model.AssignedTo);
                    MailMessage mailMsg = new MailMessage();

                    // To
                    mailMsg.To.Add(new MailAddress(EmailId, Model.UserName));

                    // From
                    mailMsg.From = new MailAddress(smtpSection.From, "Teamme");

                    mailMsg.Subject = string.Format("Successfully Enhancement Edited");

                    var htmlString = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body style='font-family:Verdana;'>
                        <div style='width:70%; margin:0 auto;'>
                            <div style='font-family: cursive; font-size:25px; margin: 8px; color:#dd3d31; text-align:center;'>
                                <span>TeamME</span>
                            </div>
                            <div style='padding:15px; color:white; text-align:center; background-color:#dd3d31'>
                                <h1>Hi {0},</h1>
                            </div>
                            <div style='float: left; width: 100%; padding: 0 20px;'>
                                <div style='padding:50px;'>                                    
				    <i> We want to inform you that there are changes done in enhancement - {1}.</i>
				<p>Please go through the enhancement assigned to you and if you have any queries, please contact your assigned tester | team member.</p>
                                
                                <p style='margin-top:50px;'>Best Regards,
                                    <br />
                                    <span style='font-family:cursive; color:#dd3d31;'>TeamME</span>
                                </p>
                             </div>        
                            </div>    
                            <div style='text-align:center;padding:10px;font-size:12px; border:1px solid #dd3d31;'>
                                <p>If you have any questions, feel free to message us at <a href='mailto:teamme0228@gmail.com' target='_blank'>teamme0228@gmail.com</a> | call us at 8850389492 </p>
                            </div>
                        </div>
                    </body>
                 </html>", Model.UserName, Model.Title);


                    mailMsg.Body = htmlString;
                    mailMsg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient(smtpSection.Network.Host, Convert.ToInt32(smtpSection.Network.Port));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMsg);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
