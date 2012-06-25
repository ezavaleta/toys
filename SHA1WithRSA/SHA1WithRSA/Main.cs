using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.OpenSsl;

namespace SHA1WithRSA
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string seal = SHA1WithRSA ("../../cert/aaa010101aaa_CSD_01.key",
									   "a0123456789",
									   "||2.0|A|1|2009-08-16T16:30:00|1|2009|ingreso|Una sola exhibición|350.00|5.25|397.25|ISP900909Q88|Industrias del Sur Poniente, S.A. de C.V.|Alvaro Obregón|37|3|Col. Roma Norte|México|Cuauhtémoc|Distrito Federal|México|06700|Pino Suarez|23|Centro|Monterrey|Monterrey|Nuevo Léon|México|95460|CAUR390312S87|Rosa María Calderón Uriegas|Topochico|52|Jardines del Valle|Monterrey|Monterrey|Nuevo León|México|95465|10|Caja|Vasos decorados|20.00|200|1|pieza|Charola metálica|150.00|150|IVA|15.00|52.50||");

			Console.WriteLine (seal);
			Console.WriteLine ();
		}
		
		public static string LoadPrivateKey (string fileName, string password)
		{
			AsymmetricKeyParameter akp;
			StringWriter str_writer;
			PemWriter pem_writer;
			byte[] dataKey;
			 
	        str_writer = new StringWriter ();
			
			dataKey = File.ReadAllBytes (fileName);
	        akp = PrivateKeyFactory.DecryptKey (password.ToCharArray (), dataKey);
			
	        pem_writer = new PemWriter (str_writer);
			pem_writer.WriteObject (akp);
			
			str_writer.Close ();
			
	        return str_writer.ToString ();
		}
		
		public static string SHA1WithRSA (string keyfile, string password, string message)
		{
			ISigner signer;
			byte[] data;
			byte[] signature;
			AsymmetricKeyParameter key;
			
			data = File.ReadAllBytes (keyfile);
	        key = PrivateKeyFactory.DecryptKey (password.ToCharArray (), data);
			signer = SignerUtilities.GetSigner ("SHA1WithRSA");
			signer.Init (true, key);
			
			data = System.Text.Encoding.UTF8.GetBytes (message);
			signer.BlockUpdate (data, 0, data.Length);
			signature = signer.GenerateSignature ();
			
			return Convert.ToBase64String (signature);
		}
	}
}
