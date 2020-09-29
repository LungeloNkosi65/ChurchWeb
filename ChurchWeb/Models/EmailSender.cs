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

        //public static void ConfirmBookingEmail(Booking booking)
        //{
        //    var mailTo = new List<MailAddress>();
        //    mailTo.Add(new MailAddress(booking.CustomereEmail, GetCustomerName(booking.CustomereEmail)));
        //    var body = $"Good Day {GetCustomerName(booking.CustomereEmail)}," +
        //        $" Your booking has been successfuly completed please wait for further instructions then all will be set and ready." +
        //        $" Please ensure to pay your booking fee of {booking.BookingPrice}." +

        //        $"<br/> This email confrims your  booking , if you have anny further enquiries feel free to contact us.";

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

        public static string GetCustomerName(string customerEmail)
        {
            var name = (from customer in db.Members
                        where customer.MemberEmail == customerEmail
                        select customer.FirstName).FirstOrDefault();
            return name;
        }
    }
}