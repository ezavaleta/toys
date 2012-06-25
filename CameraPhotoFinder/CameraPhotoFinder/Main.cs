using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using com.drew.metadata;
using com.drew.imaging.jpg;

namespace CameraPhotoFinder
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var photos_path = args [1];
			var others_path = args [2];
			var files = Directory.GetFiles (args [0], "*.jpg");
			
			Console.WriteLine ("Files (*.jpg): {0}", files.Length);
			
			for (int i = 0; i < files.Length; i++) {
				var f = files [i];
				
				// Print Metadata
				//Console.WriteLine (ShowOneFileAllMetaDataAllTag (f));
				
				if (IsPhoto (f)) {
					Directory.Move (f, Path.Combine (photos_path, Path.GetFileName (f)));
				} else {
					Directory.Move (f, Path.Combine (others_path, Path.GetFileName (f)));
				}
				
				if (i % 100 == 0 || (i+1) == files.Length) {
					Console.WriteLine ("Progress: {0}/{1} ({2:p})", i + 1, files.Length, ((decimal)i+1) / files.Length);
				}
			}
		}
		
		public static bool IsPhoto (string fileName)
		{			
			try {
				var metadata = JpegMetadataReader.ReadMetadata (new FileInfo (fileName));
				
				foreach (AbstractDirectory directory in metadata) {
					foreach (Tag tag in directory) {
						switch (tag.GetTagName ()) {
						case "Make":
						case "Model":
						case "Flash":
						case "Focal Length":
							return true;
						}
						
						/*
						var desc = tag.GetDescription ().Trim ();
						
						if (desc.Contains("AppleMark")) {
							return true;
						}*/
					}
					
				}
			} catch (JpegProcessingException) {
			}
			
			return false;
		}
		
		public static String ShowOneFileAllMetaDataAllTag(string aFileName)
        {
            Metadata lcMetadata = null;
            try
            {
                FileInfo lcImgFile = new FileInfo(aFileName);
                // Loading all meta data
                lcMetadata = JpegMetadataReader.ReadMetadata(lcImgFile);
            }
            catch (JpegProcessingException e)
            {
                Console.Error.WriteLine(e.Message);
                return "Error";
            }

            // Now try to print them
            StringBuilder lcBuff = new StringBuilder(1024);
            lcBuff.Append("---> ").Append(aFileName).Append(" <---").AppendLine();
            // We want all directory, so we iterate on each
            foreach(AbstractDirectory lcDirectory in lcMetadata)
            {
                // We look for potential error
                if (lcDirectory.HasError)
                {
                    Console.Error.WriteLine("Some errors were found, activate trace using /d:TRACE option with the compiler");
                }
                lcBuff.Append("---+ ").Append(lcDirectory.GetName()).AppendLine();
                // Then we want all tags, so we iterate on the current directory
                foreach(Tag lcTag in lcDirectory) {
                    string lcTagDescription = null;
                    try
                    {
                        lcTagDescription = lcTag.GetDescription();
                    }
                    catch (MetadataException e)
                    {
                        Console.Error.WriteLine(e.Message);
                    }
                    string lcTagName = lcTag.GetTagName();
                    lcBuff.Append(lcTagName).Append('=').Append(lcTagDescription).AppendLine();

                    lcTagDescription = null;
                    lcTagName = null;
                }
            }
            lcMetadata = null;

            return lcBuff.ToString();
        }

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
	}
}
