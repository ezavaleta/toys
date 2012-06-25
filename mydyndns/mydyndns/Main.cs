using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Mictlanix.DynDNS
{
	class UpdateClient
	{
		public static void Main (string[] args)
		{
			string url = System.Configuration.ConfigurationManager.AppSettings ["URL"];
			string domains = System.Configuration.ConfigurationManager.AppSettings ["Domain"];
			string key = System.Configuration.ConfigurationManager.AppSettings ["ValidationKey"];
			
			ServicePointManager.ServerCertificateValidationCallback = Validator;
			
			foreach (var domain in domains.Split(',')) {
				UpdateDNS (url, domain.Trim (), key);
			}
		}
		
		static void UpdateDNS (string url, string domain, string key)
		{
			var request = WebRequest.Create (url);
			byte[] data = Encoding.UTF8.GetBytes (
				string.Format ("domain={0}&key={1}", domain, key));
			
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = data.Length;
			
			var stream = request.GetRequestStream ();
			stream.Write (data, 0, data.Length);
			stream.Close ();
			
			var response = request.GetResponse ();
			var reader = new StreamReader (response.GetResponseStream ());
			
			Console.WriteLine (((HttpWebResponse)response).StatusDescription);
			Console.WriteLine (reader.ReadToEnd ());
			
			reader.Close ();
			response.Close ();
		}
		
		public static bool Validator (object sender,
		                              X509Certificate certificate,
		                              X509Chain chain, 
                                      SslPolicyErrors sslPolicyErrors)
		{
			//Console.WriteLine ("== certificate ==");
			//Console.WriteLine (certificate);
			//Console.WriteLine ("== Subject ==");
			//Console.WriteLine (certificate.Subject);
			
			return certificate.Subject.Contains ("CN=dns.mictlanix.net");
		}
	}
}
