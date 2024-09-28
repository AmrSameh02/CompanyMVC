using Company.Route.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.Route.PL.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{

			var client = new SmtpClient("smtp.gmail.com", 587);

			client.EnableSsl = true;

			client.Credentials = new NetworkCredential("amrsameh903@gmail.com", "chpjnnavvmguqxey");

			client.Send("amrsameh903@gmail.com",email.To,email.Subject,email.Body);



		}
	}
}
