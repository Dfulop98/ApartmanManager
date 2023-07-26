
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
        public Result<Reservation> SendRequestEmail
            (
            Reservation incomingReservation,
            string sender,
            string receipt,
            string server,
            int port,
            string username,
            string password
            )
        {
            try
            {
                MimeMessage message = new ();
                message.From.Add(new MailboxAddress("RentalMasterApp", sender));
                message.To.Add(new MailboxAddress("Dobó Fülöp", receipt));
                
                message.Subject = $"{incomingReservation.Name} Reservation Request";

                BodyBuilder bodyBuilder = new()
                {
                    TextBody = _toEmailMessage(incomingReservation)
                };
                message.Body= bodyBuilder.ToMessageBody();

                using (SmtpClient client = new())
                {
                    client.Connect(server, port, SecureSocketOptions.StartTls);
                    client.Authenticate(username, password);
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
