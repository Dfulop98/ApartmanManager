
using ServiceLayer.ServiceInterfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using ServiceLayer.Factories.Model;
using DataModelLayer.Models;

namespace ServiceLayer.Services
{
    public class EmailService : IEmailService
    {
        public Result<Reservation> SendRequestEmail(Reservation incomingReservation)
        {
            try
            {
                MimeMessage message = new ();
                message.From.Add(new MailboxAddress("RentalMasterApp", "RentalMaster@proton.me"));
                message.To.Add(new MailboxAddress("Dobó Fülöp", "dobo.fulop97@gmail.com"));
                
                message.Subject = $"{incomingReservation.Name} Reservation Request";

                BodyBuilder bodyBuilder= new();
                bodyBuilder.TextBody = _toEmailMessage(incomingReservation);
                message.Body= bodyBuilder.ToMessageBody();

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.sendgrid.net", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("apikey", "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");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return Result<Reservation>.Success(incomingReservation);
            }
            catch (Exception ex)
            {
                return Result<Reservation>.Failure($"Failed to send email: {ex.Message}");
            }
        }

        private string _toEmailMessage(Reservation reservation)
        {
            return 
                (
                $"{reservation.Name} would make a reservation, from {reservation.CheckInDate} till {reservation.CheckOutDate} \n" +
                $"His/Her telefon number is {reservation.Phone}, and His/Her Email: {reservation.Email} \n" +
                $"for {reservation.NumberOfGuests} person to the {reservation.RoomId} room"

                );
        }
    }
}
