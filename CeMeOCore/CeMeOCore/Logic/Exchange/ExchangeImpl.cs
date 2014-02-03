using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using CeMeOCore.Logic.Spots;
using log4net;
using System.Text;

namespace CeMeOCore.Logic.Exchange
{
    public class ExchangeImpl
    {
        private readonly ILog logger = log4net.LogManager.GetLogger(typeof(ExchangeImpl));
        private const string EXCHANGE_CEMEO_URL = "https://webmail.cemeo.be/EWS/Exchange.asmx";
        private ExchangeService _service;
       
        private string _username;
        private string _password;
        private string _domain;
    
        public ExchangeImpl(string username, string password, string domain)
        {
            try
            {
                this._username = username;
                this._password = password;
                this._domain = domain;

                ServicePointManager.ServerCertificateValidationCallback = Exchange.CertificateValidationCallBack;
                this._service = new ExchangeService(ExchangeVersion.Exchange2010);

                this._service.TraceEnabled = true;
                this._service.TraceFlags = TraceFlags.All;
                this._service.Url = new Uri(EXCHANGE_CEMEO_URL);
                this._service.Credentials = new WebCredentials(username, password, domain);
            }
            catch(Exception ex)
            {
                logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(ExchangeImpl) + "\t" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
            }

            /*
             * DirectoryEntry de = new DirectoryEntry("LDAP://cemeo.be/OU=aon,OU=cemeo,DC=cemeo,DC=be", username, password);
             * //de.Path = "LDAP://OU=aon,OU=cemeo,DC=cemeo,DC=be";
             * de.AuthenticationType = AuthenticationTypes.Secure;
             * 
             * DirectorySearcher ds = new DirectorySearcher(de);
             * var y = de.Children.Find("");
             * var x = ds.FindOne();
             */
        }



        public void SendMail()
        {
            EmailMessage email = new EmailMessage(this._service);
            email.ToRecipients.Add( this._username + "@" + this._domain);
            email.Subject = "Email from C#";
            email.Body = new MessageBody("Hellooooooooo it's me C#");
            email.Send();
        }

        public void GenerateBlackSpots(string organiserID, CeMeOCore.DAL.Models.UserProfile user)
        {
            try
            {
                CalendarFolder calendarFolder = CalendarFolder.Bind(this._service, WellKnownFolderName.Calendar, new PropertySet());
                CalendarView calendarView = new CalendarView(DateTime.Today, DateTime.Today.AddDays(2));
                calendarView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

                FindItemsResults<Appointment> appointments = calendarFolder.FindAppointments(calendarView);

                StringBuilder sb = new StringBuilder();

                sb.AppendLine(DateTime.Now.ToString() + "\t" + "Class: " + typeof(ExchangeImpl));
                sb.AppendLine("\tOrganiserID:" + organiserID);
                foreach (Appointment a in appointments)
                {
                    if (a.Subject != null)
                    {
                        sb.AppendLine("\t\tSubject: " + a.Subject.ToString() + " ");
                    }
                    sb.AppendLine("\t\tStart: " + a.Start.ToString() + " ");
                    sb.AppendLine("\t\tEnd: " + a.End.ToString());
                    sb.AppendLine("");

                    Startup.SpotManagerFactory.AddSpot(new PersonBlackSpot(a.Start, a.End, user, organiserID));
                }

                logger.Debug(sb.ToString());

            }
            catch(Exception ex)
            {
                logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(ExchangeImpl) + "\t" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
            }
        }
    }
   // http://www.codeproject.com/Articles/18102/Howto-Almost-Everything-In-Active-Directory-via-C
}