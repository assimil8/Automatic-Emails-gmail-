//this program automates the distribution of emails, including the ability to CC as many others as you want

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace gmail_automation {
	class Program {
		static void Main(string[]args) {
			//declare vars for user input down the line
			var a = 0;
			var b = 0;

			//initialize our ccEntry that will hold each new address that
			//is to be appended within our toCC array

			//initialize our ccName that will hold each new name; to be
			//appended within toNames array

			string ccEntry = "";
			string ccName = "";
			string[] toCC = new string[]{""};
			string[] toNames = new string[]{""};

			//getting necessary info from user
			Console.WriteLine("Please enter your email address: ");
			var senderAdd = Console.ReadLine():

			Console.WriteLine("Please enter your name: ");
			var name = Console.ReadLine();

			var fromAdd = new MailAddress(senderAdd, name);
			Console.WriteLine("Hello, " + name + ", please enter the recipient's email address: ");

			//NOTE: 'm' holds the recip emails as of right here
			var m = Console.ReadLine();

			Console.WriteLine("Please enter recipient's name: ");
			var theirName = Console.ReadLine();

			//this following line needs to be implemented in our foreach loop later on
			var toAdd = new MailAddress(m, theirName);

			//WIP, can we 'cc' easily?
			Console.WriteLine("Do you wish to include any more addresses that will recieve a CC? (1=yes/2=no)");
			var userAnswer = Console.ReadLine();

			try {a = Convert.ToInt32(userAnswer);}
			catch (FormatException e){
				Console.WriteLine("Input failed to parse!");
			}

			//while user decision isn't 'no', allow input loop, storing email address in our toCC string array
			while (a != 0) {
				Console.WriteLine("Please enter an address that you wish to include via CC: ");
				ccEntry = Console.ReadLine();

				//resize our array and plug CC address in
				Array.Resize (ref toCC, toCC.Length + 1); //we put an 'empty' slot in our array, manually changing array length
				toCC[toCC.Length - 1] = ccEntry;		  //we then plug something into this slot, and it sits at the end of our array
				var ccAdd = ccEntry; //note this variable for later

				//resize our names array and plug CC names in
				Console.WriteLine("Please enter recipient's name: ");
				ccName = Console.ReadLine();
				Array.Resize(ref toNames, toNames.Length + 1);
				toNames[toNames.Length - 1] = ccName; //same technique for resizing our array as above

				//need to be able to exit while loop; conditions follow as:
				Console.WriteLine("Another?(1=yes/0=no");
				var choice = Console.ReadLine();

				try {a = Convert.ToInt32(choice);}
				catch (FormatException e) {
					Console.WriteLine("Input failed to parse!");
				}

				if (a == 0) {
					continue;
				} else {
					a = 1;
				}
			}

			//grab passwords for user's account verification
			Console.WriteLine("Enter account password for verification purposes: ");
			string fromPass = Console.ReadLine();

			//construct the content of our emails
			//change this to suit your business/personal needs
			const string subj = "WE ARE LIVE AND TESTING";
			const string body = "Hello! This is an automated email sent between two custom" +
				"accounts created for the sole purpose of proving that automation of the email sending process" +
				"can be easily accomplished on a platform other than Office365 (not excluding such, of course)!";

			//necessary smtp client info
			//here we tell our program "Don't use default creds, use these instead"
			var smtp = new SmtpClient {
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliverMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAdd.Address, fromPass)
			};

			//take OUR email address as sender
			string fromAddress = fromAdd.ToString();

			//our email (from our email address/name) with subject and body that were defined above as constants
			using (var message = new MailMessage(fromAddress, name) {
				Subject = subj,
				Body = body
			})

			//WIP implementing foreach that grabs the CC'd addresses and names, if any
			//if our toCC array isn't empty then begin iteration
			if (toCC != null) {
				foreach (var x in toCC) {
					message.To.Add(y);
				}
				message.To.Add(x);
				try {
					{
						smtp.Send(message);
						Console.WriteLine("Message Successfully Sent To: " + x);
					}
				} catch(Exception ex) {
					Console.WriteLine(ex.Message.ToString());
				}
			}
		}
	}
}
