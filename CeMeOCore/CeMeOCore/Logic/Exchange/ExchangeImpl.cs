using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace CeMeOCore.Logic.Exchange
{
    public class ExchangeImpl
    {
        public ExchangeImpl()
        {
            /*

            DirectoryEntry de = new DirectoryEntry("LDAP://cemeo.be/OU=aon,OU=cemeo,DC=cemeo,DC=be", username, password);
            //de.Path = "LDAP://OU=aon,OU=cemeo,DC=cemeo,DC=be";
            de.AuthenticationType = AuthenticationTypes.Secure;
            
            DirectorySearcher ds = new DirectorySearcher(de);
            var y = de.Children.Find("");
            var x = ds.FindOne();
             */
        }



        public void SendMail(string username, string password, string domain)
        {
            ServicePointManager.ServerCertificateValidationCallback = Exchange.CertificateValidationCallBack;

            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010);

            //service.TraceEnabled = true;
            //service.TraceFlags = TraceFlags.All;

            service.Credentials = new WebCredentials(username, password, domain);
            string EWSUrl = "https://webmail.cemeo.be/EWS/Exchange.asmx";
            service.Url = new Uri(EWSUrl);
            //service.AutodiscoverUrl(username, Exchange.RedirectionUrlValidationCallback);

            EmailMessage email = new EmailMessage(service);
            email.ToRecipients.Add(username + "@" + domain);
            email.Subject = "Email from C#";
            email.Body = new MessageBody("Hellooooooooo it's me C#");
            email.Send();
        }

        public void GetAppointments(string username, string password, string domain)
        {
            ServicePointManager.ServerCertificateValidationCallback = Exchange.CertificateValidationCallBack;

            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010);
            service.Credentials = new WebCredentials(username, password, domain);
            string EWSUrl = "https://webmail.cemeo.be/EWS/Exchange.asmx";
            service.Url = new Uri(EWSUrl);

            CalendarFolder calendarFolder = CalendarFolder.Bind(service, WellKnownFolderName.Calendar, new PropertySet());

            CalendarView calendarView = new CalendarView(DateTime.Today, DateTime.Today.AddDays(1));

            calendarView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

            FindItemsResults<Appointment> appointments = calendarFolder.FindAppointments(calendarView);

            foreach (Appointment a in appointments)
            {
                System.Diagnostics.Debug.Write("Subject: " + a.Subject.ToString() + " ");
                System.Diagnostics.Debug.Write("Start: " + a.Start.ToString() + " ");
                System.Diagnostics.Debug.Write("End: " + a.End.ToString());
                System.Diagnostics.Debug.Write("\n");
            }
        }
    }
   // http://www.codeproject.com/Articles/18102/Howto-Almost-Everything-In-Active-Directory-via-C
}