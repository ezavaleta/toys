using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace MailingTool
{
	class MainClass
	{
		static string conn_string = "Server=localhost;" +
									"Database=web_db;" +
									"User ID=developer;" +
									"Password=123456;" +
									"Pooling=false";

		public static void Main (string[] args)
		{
			if (args.Length != 4) {
				Console.WriteLine ("Usage: SendMail <sender> <subject> <txt file> <html file>");
				return;
			}
			
			if (!File.Exists (args [2])) {
				Console.Error.WriteLine ("Invalid text filename, it doesn't exist.");
				return;
			}
			
			if (!File.Exists (args [3])) {
				Console.Error.WriteLine ("Invalid html filename, it doesn't exist.");
				return;
			}
			
			var addr_from = args [0];
			var subject = args [1];
			var text_body = File.ReadAllText (args [2]);
			var html_body = File.ReadAllText (args [3]);

			using (var db_update = new MySqlConnection (conn_string)) {
				db_update.Open ();
				using(var update_cmd = db_update.CreateCommand ()) {
					update_cmd.CommandText = "UPDATE subscriber SET sent = 1 WHERE subscriber_id =  @id";
					update_cmd.Prepare();
					update_cmd.Parameters.AddWithValue("@id", 0);
					using (var db_select = new MySqlConnection (conn_string)) {
						db_select.Open ();
						using(var dbcmd = db_select.CreateCommand ()) {
							dbcmd.CommandText = @"SELECT subscriber_id id, email FROM subscriber WHERE active = 1 AND sent = 0";
							using(var reader = dbcmd.ExecuteReader ()) {
								while (reader.Read()) {
									bool ret = false;
									string addr_to = reader.GetString ("email");

									ret = SendEmail (addr_from, addr_to, subject, text_body.Replace ("[[email]]", addr_to), html_body.Replace ("[[email]]", addr_to));
									Console.WriteLine ("{0}: {1}", addr_to, ret ? "Ok" : "Fail");
									if (!ret) continue;

									update_cmd.Parameters ["@id"].Value = reader.GetInt32 ("id");
									update_cmd.ExecuteNonQuery ();
								}
							}
						}
					}
				}
			}
		
			Console.WriteLine ("Finish!");
		}
		
		public static bool SendEmail(string addrFrom, string addrTo, string subject, string textBody, string htmlBody)
		{
			var smtp = new SmtpClient("localhost");
			
			var addr_from = new MailAddress(addrFrom);
			var addr_to = new MailAddress(addrTo);
			
			try {
				using (var message = new MailMessage(addr_from, addr_to)) {
					var mime_type = new System.Net.Mime.ContentType ("text/html");
					var html_view = AlternateView.CreateAlternateViewFromString (htmlBody, mime_type);

					message.Subject = subject;
					message.BodyEncoding =  System.Text.Encoding.UTF8;
					message.SubjectEncoding =  System.Text.Encoding.UTF8;
					message.IsBodyHtml = false;
					message.Body = textBody;
					message.AlternateViews.Add (html_view);
					
					smtp.Send (message);
				}
				
				return true;
			} catch(Exception e) {
				Console.Error.WriteLine (e);
			}
			
			return false;
		}
	}
}
