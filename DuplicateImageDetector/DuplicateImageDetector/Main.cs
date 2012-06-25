using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;

public class Program {

	public static void Main()
	{
   		//new Program().TestImage ();
		new Program().TestFiles ();
	}
	
	public List<string> GetFiles(string dir, List<string> exts)
	{
		List<string> files = new List<string> ();
		
		try
		{
			foreach (string d in Directory.GetDirectories (dir)) 
			{
				foreach (string f in Directory.GetFiles (d)) 
				{
					FileInfo fi = new FileInfo (f);
					
					if(exts == null || exts.Count == 0 || exts.Contains (fi.Extension.ToLower ()))
						files.Add (fi.FullName);
				}
				
				files.AddRange (GetFiles (d, exts));
			}
		}
		catch (System.Exception ex) 
		{
			Console.WriteLine (ex.Message);
		}
		
		return files;
	}
	
	public void TestFiles ()
	{
		List<string> exts = new List<string> (new string[] {".jpg", ".png", ".gif"});
		List<string> files = GetFiles (@"/Volumes/Data/pictures/iPhoto Library/Masters", exts);
		Dictionary<string, List<string>> duplicates = new Dictionary<string, List<string>> ();
		//MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider ();
		//MemoryStream ms = new MemoryStream (10 * 1024 * 1024);
		List<string> dups;
		string hash;
		//byte[] data;
		int i = 0;
		
		foreach (string f in files) {
			using (Image img = Image.FromFile(f)) {
				/*
				ms.SetLength (0);
				img.Save (ms, img.RawFormat);
				
				hash = "";
				data = md5.ComputeHash (ms.GetBuffer (), 0, (int)ms.Length);
				
				for (int j = 0; j < data.Length; j++)
					hash += data [j].ToString ("x2");
				*/
				
				hash = HashFromImage (img);
				
				if (!duplicates.ContainsKey (hash))
					duplicates.Add (hash, new List<string> ());
				
				dups = duplicates [hash];
				dups.Add (f);
			}
			
			if ((i++ % 100) == 0) {
				System.GC.Collect ();
				Console.WriteLine ("{0:p}", i / (float)files.Count);
			}
		}
		
		Console.WriteLine ("{0:p}", i / (float)files.Count);
		
		foreach (List<string> values in duplicates.Values) {
			bool isFirst = true;
			
			if (values.Count == 1)
				continue;
			
			foreach (string f in values) {
				Console.WriteLine ("{0}{1}", isFirst ? "[ ]" : "[D]", f);
				
				if (!isFirst) {
					//string path = @"/Users/eddy/Desktop/dups/" + new FileInfo (f).Name;
					
					//if (File.Exists (path))
					//	File.Delete (path);
					
					//Directory.Move (f, path);
				} else {
					isFirst = false;
				}
			}
		}
	}
	
	string HashFromImage (Image img)
	{
		string hash;
		byte[] bytes = null;
		
		using (MemoryStream ms = new MemoryStream()) {
			img.Save (ms, img.RawFormat);
			bytes = ms.ToArray ();
		}
		
		using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider()) {
			bytes = sha1.ComputeHash (bytes);
			hash = BitConverter.ToString (bytes).Replace ("-", "").ToLower ();
		}
		
		return hash;
	}
	
	/*
	public void TestImage()
	{
		String img1 = HashString(imageToByteArray(Image.FromFile(@"/Users/eddy/Desktop/image-a.png")));
		String img2 = HashString(imageToByteArray(Image.FromFile(@"/Users/eddy/Desktop/image-b.png")));
		
		Console.WriteLine("Size: {0} {2} {1}", img1, img2, img1.Equals(img2) ? "==" : "!=");
	}
	
	private string HashString(byte[] data)
	{
		MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
		string ret = "";
		
		data = x.ComputeHash (data);
		
		for (int i=0; i < data.Length; i++)
		    ret += data[i].ToString("x2");
		
		return ret;
	}
	
	public byte[] imageToByteArray(Image img)
	{
		MemoryStream ms = new MemoryStream();
		
		img.Save(ms, img.RawFormat);
		
		return ms.ToArray();
	}
	*/
}
