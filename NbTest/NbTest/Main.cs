using System;
using System.Drawing;
using System.IO;

using org.pdfclown.documents;
using org.pdfclown.documents.contents;
using org.pdfclown.documents.contents.composition;
using org.pdfclown.documents.contents.colorSpaces;
using PDFFonts = org.pdfclown.documents.contents.fonts;
using org.pdfclown.documents.contents.layers;
using org.pdfclown.documents.contents.objects;
using org.pdfclown.documents.interchange.metadata;
using PDFFiles = org.pdfclown.files;
using org.pdfclown.objects;
using org.pdfclown.tools;

namespace NbTest
{
	class MainClass
	{
		//static string FONT_NAME = "Vollkorn-Italic.ttf";
		//static string FONT_NAME = "LibreBaskerville-Italic.otf";
		static string FONT_NAME = "Galliard Italic BT.ttf";

		public static void Main (string[] args)
		{
			if (args.Length != 3) {
				Console.WriteLine ("Usage: NbTest <background.pdf> <text> <output.pdf>");
				return;
			}

			if (!File.Exists (args [0])) {
				Console.Error.WriteLine ("Invalid text filename, it doesn't exist.");
				return;
			}
			
			string text = args [1];
			PDFFiles.File file = new PDFFiles.File (args [0]);
			Document document = file.Document;
			PageStamper stamper = new PageStamper (document.Pages [0]);
			PrimitiveComposer composer = stamper.Foreground;
			PDFFonts.Font font = PDFFonts.Font.Get (document, FONT_NAME);

			if (document.Layer == null) {
				document.Layer = new LayerDefinition (document);
			}

			document.PageMode = Document.PageModeEnum.Layers;
			
			Layer layer = new Layer(document, "Personalization");
			document.Layer.Layers.Add(layer);

			var margin = new RectangleF(MilimitersToPoints(2), MilimitersToPoints(1),
			                            MilimitersToPoints(6), MilimitersToPoints(1));
			//var frame = new RectangleF(MilimitersToPoints(0), MilimitersToPoints(179),
			//                           MilimitersToPoints(255), MilimitersToPoints(26));
			var frame = new RectangleF(MilimitersToPoints(105), MilimitersToPoints(155),
			                           MilimitersToPoints(100), MilimitersToPoints(26));
			var text_frame = new RectangleF(frame.X + margin.X,
			                                frame.Y + margin.Y,
			                                frame.Width - margin.Right,
			                                frame.Height - margin.Bottom);
			int font_size = GetFontSizeToFit(text, text_frame);

			composer.BeginLayer(layer);

			//composer.Rotate(90, new PointF(0, MilimitersToPoints(255)));

			composer.SetFillColor(new DeviceRGBColor(86f/255,175f/255,49f/255));
			composer.DrawRectangle(frame);
			composer.Fill();

			composer.SetFillColor(DeviceRGBColor.White);
			composer.SetFont(font, font_size);

			BlockComposer blockComposer = new BlockComposer(composer);
			blockComposer.Begin(text_frame, AlignmentXEnum.Center, AlignmentYEnum.Middle);
			blockComposer.ShowText(text);
			blockComposer.End();

			composer.End();
			stamper.Flush();

			file.Save(args [2], PDFFiles.SerializationModeEnum.Standard);

			file.Dispose();
		}

		static float MilimitersToPoints (float x)
		{
			return x * 72f / 25.4f;
		}

		static int GetFontSizeToFit(string text, RectangleF frame)
		{
			PDFFiles.File file = new PDFFiles.File();
			Page page = new Page(file.Document);
			PDFFonts.Font font = PDFFonts.Font.Get(file.Document, FONT_NAME);
			PrimitiveComposer composer;
			BlockComposer blockComposer;
			int font_size = (int)frame.Height;
			int chars = 0;

			file.Document.Pages.Add(page);
			composer = new PrimitiveComposer(page);
			blockComposer = new BlockComposer(composer);

			while (blockComposer.BoundBox.Height > frame.Height || text.Length != chars) {
				blockComposer.Begin(frame, AlignmentXEnum.Center, AlignmentYEnum.Middle);
				composer.SetFont(font, font_size);
				chars = blockComposer.ShowText(text);
				blockComposer.End();
				Console.WriteLine ("FontSize: {0}, Text Size: [w:{1} h:{2}], Frame Size: [w:{3} h:{4}], Chars: {5}", font_size,
				                   blockComposer.BoundBox.Width, blockComposer.BoundBox.Height,
				                   frame.Width, frame.Height, chars);
				font_size--;
			}

			file.Dispose();

			return font_size + 1;
		}
	}
}
