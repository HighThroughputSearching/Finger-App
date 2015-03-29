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

namespace FingerprintAppForAdd
{
	class Program
	{
		public static string xmlPath = Path.Combine ("/Users/prisadumrongsiri/Projects/FingerprintApp/FingerprintApp/", "xml");

		public static MyPersons myPersons = new MyPersons ();
		public static MyPersons personsMatch = new  MyPersons ();
		public static List<List<string>> personList = new List<List<string>> ();

		public static List<List<string>> candidate = new List<List<string>> ();
		static List <List<string>> allPath;
		static List <List<string> > allFileName;

		// Shared AfisEngine instance (cannot be shared between different threads though)
		static readonly AfisEngine Afis = new AfisEngine ();

		static string id;
		static string email;
		static string organisation;

		static void Main (string[] args)
		{
			Stopwatch stopwatchAddperson = new Stopwatch ();
			stopwatchAddperson.Start ();
			string[] extensions = { "tif", "bmp", "jpg" };
			string ImagePath_FingerPosition;
	
			loadCandidateDB ();
			string xml = Path.Combine (xmlPath, "candidateDB.xml");
			// Create a new file stream for reading the XML file
			/*FileStream ReadFileStream = new FileStream (xml, FileMode.Open, FileAccess.Read, FileShare.Read);

			// Load the object saved above by using the Deserialize function
			XmlSerializer SerializerObj = new XmlSerializer (typeof(MyPersons));
			myPersons = (MyPersons)SerializerObj.Deserialize (ReadFileStream);


			// Cleanup
			ReadFileStream.Close ();*/
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
			stopwatchAddperson.Stop ();
			Console.WriteLine ("Total Elapsed Time {0}", stopwatchAddperson.Elapsed);


		}

		public static void loadCandidateDB(){
			Console.WriteLine ("/** Load Condidate Database **/");
			DBConnection db = new DBConnection ();
			candidate = db.getdb ();
			Console.WriteLine ("/** Load Condidate Database **/");
			for (int i = 0; i < candidate.Count(); i++) {
				List<string> Aperson = candidate[i];
				 allPath = new  List <List<string> >();
				allFileName = new  List <List<string> >();
				 id = Aperson[0];
				 email = Aperson[1];
				 organisation = Aperson[2];
				ImagePathManagement ipm = new ImagePathManagement();
				ipm.setNewPersonFingerPrints(id+"",organisation);
				ipm.addFileToList();

				//List<string> list1 = ipm.getFileNameList ();
				//List<string> list2 = ipm.getFilePathList ();
				//logger.Debug ("Check File: " + list1.Count());
				//logger.Debug ("Check File: " + list2.Count());
			
				if (ipm.getFilePath_right_thumb_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_right_thumb_List ());
					allFileName.Add (ipm.getFileName_right_thumb_List ());

				}
				if (ipm.getFileName_right_fore_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_right_fore_List ());
					allFileName.Add (ipm.getFileName_right_fore_List ());

				}

				if (ipm.getFileName_right_middle_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_right_middle_List ());
					allFileName.Add (ipm.getFileName_right_middle_List ());

				}

				if (ipm.getFileName_right_ring_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_right_ring_List ());
					allFileName.Add (ipm.getFileName_right_ring_List ());

				}

				if (ipm.getFileName_right_little_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_right_little_List ());
					allFileName.Add (ipm.getFileName_right_little_List ());

				}

				if (ipm.getFilePath_left_thumb_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_left_thumb_List ());
					allFileName.Add (ipm.getFileName_left_thumb_List ());
				}

				if (ipm.getFilePath_left_fore_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_left_fore_List ());
					allFileName.Add (ipm.getFileName_left_fore_List ());
				}

				if (ipm.getFilePath_left_middle_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_left_middle_List ());
					allFileName.Add (ipm.getFileName_left_middle_List ());
				}

				if (ipm.getFilePath_left_ring_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_left_ring_List ());
					allFileName.Add (ipm.getFileName_left_ring_List ());
				}

				if (ipm.getFilePath_left_little_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_left_little_List ());
					allFileName.Add (ipm.getFileName_left_little_List ());
				}
				//MyPerson person = Enroll (ipm.getFilePathList (),ipm.getFileNameList (),id,criminal_name+" "+criminal_surname);

				MyPerson person = Enroll(allPath,allFileName,id,id,organisation);
				if(person != null)  myPersons.Mypersons.Add(person);
				//myPersons.Mypersons.Add(person);

			}
			SetXMLFromObject(myPersons);

		}


		public  static void SetXMLFromObject (MyPersons mp)
		{


			XmlAttributeOverrides overrides = new XmlAttributeOverrides ();
			XmlAttributes attribs = new XmlAttributes ();
			attribs.XmlIgnore = true;
			attribs.XmlElements.Add (new XmlElementAttribute ("AsImageData"));
			overrides.Add (typeof(MyFingerprint), "AsImageData", attribs);

			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer (typeof(MyPersons), overrides);
			System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings ();
			settings.Encoding = new UnicodeEncoding (false, false); // no BOM in a .NET string
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;

			string xml = Path.Combine (xmlPath, "candidateDB.xml");
			using (TextWriter textWriter = File.CreateText (xml)) {
				using (System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create (textWriter, settings)) {
					serializer.Serialize (xmlWriter, mp);
				}
				// textWriter.ToString(); //This is the output as a string

			}
		}


		public static byte[,] GetPixels (Bitmap bmp)
		{
			int width = bmp.Width;
			int height = bmp.Height;
			BitmapData data = bmp.LockBits (new Rectangle (0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

			byte[] bytes = new byte[height * data.Stride];
			try {
				Marshal.Copy (data.Scan0, bytes, 0, bytes.Length);
			} finally {
				bmp.UnlockBits (data);
			}

			byte[,] result = new byte[height, width];
			for (int y = 0; y < height; ++y)
				for (int x = 0; x < width; ++x) {
					int offset = y * data.Stride + x * 3;
					result [y, x] = (byte)((bytes [offset + 0] + bytes [offset + 1] + bytes [offset + 2]) / 3);
				}
			return result;
		}

		// Take fingerprint image file and create Person object from the image
		public static MyPerson Enroll (string filename, string name)
		{

			Console.WriteLine ("Enrolling {0}...", name);

			// Initialize empty fingerprint object and set properties
			Fingerprint fp = new Fingerprint ();

			/* using (var bitmap = new Bitmap(filename))
			{
				fp.AsBitmap = bitmap;
			}*/
			//		fp.AsBitmap = new Bitmap(Bitmap.FromFile(filename));


			using (FileStream fs = new FileStream (filename, FileMode.Open, FileAccess.Read)) {
				try {
					using (Image fromFile = Image.FromStream (fs)) {
						using (Bitmap bmp = new Bitmap (fromFile)) {
							fp.Image = GetPixels (bmp);
							fromFile.Dispose ();
						}
					}
				} catch (Exception e) {
					return null; 
				}
			}

			// Above update of fp.AsBitmapSource initialized also raw image in fp.Image
			// Check raw i
			// Image dimensions, Y axis is first, X axis is second
			Console.WriteLine (" Image size = {0} x {1} (width x height)", fp.Image.GetLength (1), fp.Image.GetLength (0));

			// Initialize empty person object and set its properties
			MyPerson person = new MyPerson ();
			person.name = name;
			person.Id = Convert.ToInt32 (name);
			// Add fingerprint to the person

			fp.Finger = Finger.RightThumb;
			person.Fingerprints.Add (fp);

			// Execute extraction in order to initialize fp.Template
			Console.WriteLine (" Extracting template... {0} : {1}", person.name, fp.AsBitmap);



			Stopwatch sEx = new Stopwatch ();

			sEx.Start ();
			Afis.Extract (person);

			sEx.Stop ();
			Console.WriteLine ("Extract Elapsed Time {0}", sEx.Elapsed);
			// Check template size
			Console.WriteLine (" Template size = {0} bytes", fp.Template.Length);

			return person;
		}


		// Take fingerprint image file and create Person object from the image
		public static MyPerson Enroll (List<List<string>> allPath,List<List<string>> allFileName,string id, string name,string organisation)
		{
			//Console.WriteLine("Enrolling {0}...", name);
			Console.WriteLine ("Enrolling name: {0} id : {1}",name,id);
			MyPerson person = new MyPerson ();
			person.name = name;
			person.Id = Convert.ToInt32(id);
			person.organisation = organisation;
			Console.WriteLine  ("No. of Folder: {0}" ,allPath.Count ());

			// Initialize empty fingerprint object and set properties
			for (int i = 0; i < allPath.Count (); i++) {
				//		logger.Debug ("folderName : " + allFileName[i][j]);
				//List<Fingerprint> FingerprintList = new List<Fingerprint>(Fingerprint)
				for (int j = 0; j < allPath [i].Count (); j++) {
					MyFingerprint fp = new MyFingerprint ();
					Console.WriteLine  ("folderName : {0}" ,allFileName[i][j]);
					string[] fileSpilt = allFileName[i][j].Split('_');
					if (fileSpilt[2].Contains("L"))
					{
						if (fileSpilt[2].Equals("L0")) fp.Finger = Finger.LeftThumb;
						else if (fileSpilt[2].Equals("L1")) fp.Finger = Finger.LeftIndex;
						else if (fileSpilt[2].Equals("L2")) fp.Finger = Finger.LeftMiddle;
						else if (fileSpilt[2].Equals("L3")) fp.Finger = Finger.LeftRing;
						else if (fileSpilt[2].Equals("L4")) fp.Finger = Finger.LeftLittle;
					}
					else if (fileSpilt[2].Contains("R"))
					{
						if (fileSpilt[2].Equals("R0")) fp.Finger = Finger.RightThumb;
						else if (fileSpilt[2].Equals("R1")) fp.Finger = Finger.RightIndex;
						else if (fileSpilt[2].Equals("R2")) fp.Finger = Finger.RightMiddle;
						else if (fileSpilt[2].Equals("R3")) fp.Finger = Finger.RightRing;
						else if (fileSpilt[2].Equals("R4")) fp.Finger = Finger.RightLittle;
					}
					Console.WriteLine ("finger position : {0}", fp.Finger );

					String filePath = allPath [i][j];
					using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
					{
						try
						{
							using (Image fromFile = Image.FromStream(fs))
							{
								using (Bitmap bmp = new Bitmap(fromFile))
								{
									fp.filename = allFileName[i][j];
									fp.Image = GetPixels(bmp);
									Console.WriteLine ("========== Get Bitmap Image ==========");
									fromFile.Dispose();
								}
							}
						}
						catch (Exception e) {
							Console.WriteLine (e);
							return null; 
						}
					}

					person.Fingerprints.Add (fp);

				}


				//person.Fingerprints.Add (myFingerprints);

			}

			// Execute extraction in order to initialize fp.Template
			Console.WriteLine("Extraction template.. : {0}",person.name);

			//Console.WriteLine(" Extracting template... {0} : {1}",person.name,fp.AsBitmap);

			Afis.Extract (person);
			Console.WriteLine ("Template size {0}" , person.Fingerprints);
			// Check template size
			//Console.WriteLine(" Template size = {0} bytes", fp.Template.Length);

			return person;
		}





	}
}


