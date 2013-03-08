using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CertTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string sn = string.Empty;
			X509Certificate2 cert = new X509Certificate2();

			cert.Import("/Users/eddy/Google Drive/private/sat/certificates/ZARE860422FD7-seal.cer");

			foreach (var b in cert.GetSerialNumber()) {
				sn = (char)b + sn;
			}

			Console.WriteLine (cert.Subject);
			Console.WriteLine (cert.Version);
			Console.WriteLine (cert.SignatureAlgorithm.Value.Replace("1.2.840.113549.1.1.5", "RSA_SHA1RSA"));
			Console.WriteLine (cert.NotBefore);
			Console.WriteLine (cert.NotAfter);
			Console.WriteLine (cert.SerialNumber);
			Console.WriteLine (sn);
		}
	}
}
