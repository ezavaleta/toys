using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace BatchImageEncoder
{
	class MainClass
	{
		static string FILENAME_FMT = @"/Users/eddy/Desktop/Photos/{0}.png";
		
		public static List<string> GetFiles(string dir, List<string> exts)
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
		
		public static void Main (string[] args)
		{
			List<string> files = GetFiles (@"/Volumes/Data/documents/projects/ramos/ferreteria/public/files", null);
			int i = 0;
			
			foreach (string f in files)
			{
				try {
					using (Image img = Image.FromFile(f))
					{
						img.Save(string.Format(FILENAME_FMT, Path.GetFileNameWithoutExtension(f).Trim()),
							     ImageFormat.Png);
					}
					
					if ((i++ % 100) == 0)
					{
						System.GC.Collect ();
						Console.WriteLine ("{0:p}", i / (float)files.Count);
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
		}
	}
}
