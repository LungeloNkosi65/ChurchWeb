using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ChurchWeb.Models
{
    public class EmailSender
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        //public static void SendBookingEmail(Booking booking)
        //{
        //    var mailTo = new List<MailAddress>();
        //    mailTo.Add(new MailAddress(booking.CustomereEmail, GetCustomerName(booking.CustomereEmail)));
        //    var body = $"Good Day {GetCustomerName(booking.CustomereEmail)}," +
        //        $" Your booking status has changed to {booking.BookingStatus}." +
        //        $"<br/> This email confrims your cruise booking process, if you have anny further enquiries feel free to contact us.";

        //    EmailService emailService = new EmailService();
        //    emailService.SendEmail(new EmailContent()
        //    {
        //        mailTo = mailTo,
        //        mailCc = new List<MailAddress>(),
        //        mailSubject = $"{booking.BookingStatus}!!  | Ref No.:" + booking.BookingId,
        //        mailBody = body,
        //        mailFooter = $"<br/> Kind Regards, <br/> <b>Beyond Tech Solutions </b>",
        //        mailPriority = MailPriority.High,
        //        mailAttachments = new List<Attachment>()

        //    });
        //}

        public static void AnnouncementEmail(Anouncement anouncement)
        {
            var users = db.Members.ToList();
            foreach (var item in users)
            {
                var mailTo = new List<MailAddress>();
                mailTo.Add(new MailAddress(item.MemberEmail, item.FirstName));
                var body = $"Good Day {item.FirstName}, \n" +
                    $" {anouncement.Description}." +

                    $"<br/> Your received this email beacause your a member on the Churh Web.";

                EmailService emailService = new EmailService();
                emailService.SendEmail(new EmailContent()
                {
                    mailTo = mailTo,
                    mailCc = new List<MailAddress>(),
                    mailSubject = $"{anouncement.Title}!!  | Anouncement Date :" + DateTime.Now ,
                    mailBody = body,
                    mailFooter = $"<br/> Kind Regards, <br/> <b>Church Web </b>",
                    mailPriority = MailPriority.High,
                    mailAttachments = new List<Attachment>()

                });
            }

            
        }

        public static string GetCustomerName(string customerEmail)
        {
            var name = (from customer in db.Members
                        where customer.MemberEmail == customerEmail
                        select customer.FirstName).FirstOrDefault();
            return name;
        }
    }
}