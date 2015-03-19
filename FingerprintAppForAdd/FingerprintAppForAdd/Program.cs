using System;
using SourceAFIS.Simple;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Net;

namespace FingerprintAppForAdd{
	class Program
	{	

		// Initialize path to images
		public  static string ImagePath = Path.Combine ("/Applications/XAMPP/xamppfiles/htdocs/fingerprint3/assets/images/form/", "right_thumb/");
		public static string xmlPath = Path.Combine ("/Users/prisadumrongsiri/Projects/WebFinger1/WebFinger1/", "xml");

		public static MyPersons myPersons = new MyPersons();
		public static MyPersons personsMatch = new  MyPersons();
		public static List<List<string>> personList = new List<List<string>>();


		// Shared AfisEngine instance (cannot be shared between different threads though)
		static readonly AfisEngine Afis = new AfisEngine();


		static void Main(string[] args)
		{
			Stopwatch stopwatchAddperson = new Stopwatch();
			stopwatchAddperson.Start();
			string[] extensions = { "tif", "bmp", "jpg" };
			// Enroll some people
			string[] fileArray = Directory.GetFiles(ImagePath, "*.*", SearchOption.AllDirectories).Where(f => extensions.Contains(f.Split('.').Last().ToLower())).ToArray();
			Array.Sort(fileArray);
			for (int i = 0; i <fileArray.Count();i++) {
				Console.WriteLine(fileArray[i]);
				string[] spit = fileArray [i].Split ('/');
				//	Console.WriteLine ("spit :"+spit);
				string filename = spit [10];
				string[] spitId = fileArray [i].Split ('_');
				string id = spitId [3];
				string[] spitwithoutdot = id.Split ('.');
				string idWithoutdot = spitwithoutdot [0];
				Console.WriteLine ("ID : " + idWithoutdot);
				MyPerson person = Enroll (fileArray [i], idWithoutdot + "");
				if( person!= null){
					myPersons.Mypersons.Add ( person);
				}

			}
			SetXMLFromObject(myPersons);
			string xml = Path.Combine (xmlPath, "candidateDB.xml");
			// Create a new file stream for reading the XML file
			FileStream ReadFileStream = new FileStream(xml, FileMode.Open, FileAccess.Read, FileShare.Read);

			// Load the object saved above by using the Deserialize function
			XmlSerializer SerializerObj = new XmlSerializer(typeof(MyPersons));
			myPersons = (MyPersons)SerializerObj.Deserialize(ReadFileStream);


			// Cleanup
			ReadFileStream.Close();
			/*Stopwatch sIdentify = new Stopwatch();
			sIdentify.Start();
			Console.WriteLine("=========================Identify PROBE ======================");
			// Enroll visitor with unknown identity
			MyPerson probe = Enroll(Path.Combine(ImagePath, "probe.tif"), "Visitor #12345");
			sIdentify.Stop ();
			//stopwatch.Stop();
			Console.WriteLine ("Identify Elapsed Time {0}", sIdentify.Elapsed);
			// Look up the probe using Threshold = 10
			Afis.Threshold = 50;
			Console.WriteLine("Identifying {0} in database of {1} persons...", probe.name, myPersons.Mypersons.Count);
			personsMatch.Mypersons = Afis.Identify(probe, myPersons.Mypersons).OfType<MyPerson>().ToList();

			Console.WriteLine("=========================== ===== ======================");



			Console.WriteLine( personsMatch.Mypersons.Count());
			  Console.WriteLine(personsMatch.Mypersons.Count());
			for (int i = 0; i < personsMatch.Mypersons.Count(); i++) {
				float scoreM = Afis.Verify(probe, personsMatch.Mypersons[i]);
				Console.WriteLine("Persons: {0} ,Score : {1}", personsMatch.Mypersons[i].name, scoreM);

				// Set A personMatch infp
				List<string> aPerson = new List<string> ();
			//	aPerson.Add (personsMatch.Mypersons[i].Id);
				aPerson.Add (personsMatch.Mypersons [i].name);
				aPerson.Add (scoreM+"");	
				personList.Add (aPerson);
			}*/
			stopwatchAddperson.Stop();
			Console.WriteLine ("Total Elapsed Time {0}", stopwatchAddperson.Elapsed);


		}


		public List<List<string>> getPersonMatch(){
			return personList;	
		}

		public  static void SetXMLFromObject(object o)
		{


			XmlAttributeOverrides overrides = new XmlAttributeOverrides();
			XmlAttributes attribs = new XmlAttributes();
			attribs.XmlIgnore = true;
			attribs.XmlElements.Add(new XmlElementAttribute("AsImageData"));
			overrides.Add(typeof(Fingerprint), "AsImageData", attribs);

			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(MyPersons),overrides);
			System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
			settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;

			string xml = Path.Combine (xmlPath, "candidateDB.xml");
			using (TextWriter textWriter = File.CreateText(xml))
			{
				using (System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(textWriter, settings))
				{
					serializer.Serialize(xmlWriter, myPersons);
				}
				// textWriter.ToString(); //This is the output as a string

			}

		}

		public static byte[,] GetPixels(Bitmap bmp)
		{
			int width = bmp.Width;
			int height = bmp.Height;
			BitmapData data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

			byte[] bytes = new byte[height * data.Stride];
			try
			{
				Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
			}
			finally
			{
				bmp.UnlockBits(data);
			}

			byte[,] result = new byte[height, width];
			for (int y = 0; y < height; ++y)
				for (int x = 0; x < width; ++x)
				{
					int offset = y * data.Stride + x * 3;
					result[y, x] = (byte)((bytes[offset + 0] + bytes[offset + 1] + bytes[offset + 2]) / 3);
				}
			return result;
		}

		// Take fingerprint image file and create Person object from the image
		public static MyPerson Enroll(string filename, string name)
		{

			Console.WriteLine("Enrolling {0}...", name);

			// Initialize empty fingerprint object and set properties
			Fingerprint fp = new Fingerprint();

			/* using (var bitmap = new Bitmap(filename))
			{
				fp.AsBitmap = bitmap;
			}*/
			//		fp.AsBitmap = new Bitmap(Bitmap.FromFile(filename));


			using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
			{
				try
				{
					using (Image fromFile = Image.FromStream(fs))
					{
						using (Bitmap bmp = new Bitmap(fromFile))
						{
							fp.Image = GetPixels(bmp);
							fromFile.Dispose();
						}
					}
				}
				catch (Exception e) {
					return null; 
				}
			}

			// Above update of fp.AsBitmapSource initialized also raw image in fp.Image
			// Check raw i
			// Image dimensions, Y axis is first, X axis is second
			Console.WriteLine(" Image size = {0} x {1} (width x height)", fp.Image.GetLength(1), fp.Image.GetLength(0));

			// Initialize empty person object and set its properties
			MyPerson person = new MyPerson();
			person.name = name;
			person.Id = Convert.ToInt32(name);
			// Add fingerprint to the person

			fp.Finger = Finger.RightThumb;
			person.Fingerprints.Add(fp);

			// Execute extraction in order to initialize fp.Template
			Console.WriteLine(" Extracting template... {0} : {1}",person.name,fp.AsBitmap);



			Stopwatch sEx = new Stopwatch ();

			sEx.Start ();
			Afis.Extract(person);

			sEx.Stop ();
			Console.WriteLine ("Extract Elapsed Time {0}", sEx.Elapsed);
			// Check template size
			Console.WriteLine(" Template size = {0} bytes", fp.Template.Length);

			return person;
		}





	}
}


