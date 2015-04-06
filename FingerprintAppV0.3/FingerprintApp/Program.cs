using System;
using SourceAFIS.Simple;
using System.IO;
using System.Collections.Generic;

using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using log4net;
namespace FingerprintApp{

public class Program
{
	private static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");  
	// Initialize path to images
	public  string xmlPath = Path.Combine("xml");

	public  MyPersons myPersons = new MyPersons ();
	public MyPersons personsMatch = new MyPersons ();
	public MyPersons personsMatchXML = new MyPersons ();
	public List<Person> personsMatchXML2 = new List<Person> ();
	public List<List<string>> candidate = new List<List<string>> ();
		

	// Shared AfisEngine instance (cannot be shared between different threads though)
	AfisEngine Afis = new AfisEngine ();

	

	public void loadCandidateDB(){
			logger.Debug ("/** Load Condidate Database **/");
			DBConnection db = new DBConnection ();
			candidate = db.getdb ();

			for (int i = 0; i < candidate.Count(); i++) {
				List<string> Aperson = candidate[i];
				string id = Aperson[0];
				string email = Aperson[1];
				string organisation = Aperson[2];
				ImagePathManagement ipm = new ImagePathManagement();
				ipm.setNewPersonFingerPrints(id+"");
				ipm.addFileToList();

				//List<string> list1 = ipm.getFileNameList ();
				//List<string> list2 = ipm.getFilePathList ();
				//logger.Debug ("Check File: " + list1.Count());
				//logger.Debug ("Check File: " + list2.Count());
				List <List<string> > allPath = new  List <List<string> >();
				List <List<string> > allFileName = new  List <List<string> >();
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

				if (ipm.getFilePath_unknown_1_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_unknown_1_List ());
					allFileName.Add (ipm.getFileName_unknown_1_List ());
				}

				if (ipm.getFilePath_unknown_2_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_unknown_2_List ());
					allFileName.Add (ipm.getFileName_unknown_2_List ());
				}

				if (ipm.getFilePath_unknown_3_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_unknown_3_List ());
					allFileName.Add (ipm.getFileName_unknown_3_List ());
				}

				if (ipm.getFilePath_unknown_4_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_unknown_4_List ());
					allFileName.Add (ipm.getFileName_unknown_4_List ());
				}

				if (ipm.getFilePath_unknown_5_List ().Count () > 0) {
					allPath.Add (ipm.getFilePath_unknown_5_List ());
					allFileName.Add (ipm.getFileName_unknown_5_List ());
				}
				//MyPerson person = Enroll (ipm.getFilePathList (),ipm.getFileNameList (),id,criminal_name+" "+criminal_surname);
				MyPerson person = Enroll(allPath,allFileName,id,id,organisation);
				if(person != null)  myPersons.Mypersons.Add(person);
				//myPersons.Mypersons.Add(person);

			}
			SetXMLFromObject(myPersons);

	}

	public void identifyPersonMatch (string emailUser)
	   {
		
			string[] extensions = { "tif", "bmp", "jpg" };
		//	string[] fileArray = Directory.GetFiles(ImagePath, "*.*", SearchOption.AllDirectories).Where(f => extensions.Contains(f.Split('.').Last().ToLower())).ToArray();

/*			for (int i = 0; i < fileArray.Count();i++) {
				Console.WriteLine(fileArray[i]);
				myPersons.Mypersons.Add(Enroll(fileArray[i], i + ""));

		}
			SetXMLFromObject(myPersons); */
		
		string xml = Path.Combine (xmlPath, "candidateDB.xml");
		// Create a new file stream for reading the XML file
		FileStream ReadFileStream = new FileStream (xml, FileMode.Open, FileAccess.Read, FileShare.Read);

		// Load the object saved above by using the Deserialize function
		XmlSerializer SerializerObj = new XmlSerializer (typeof(MyPersons));
		myPersons = (MyPersons)SerializerObj.Deserialize (ReadFileStream);
		
			logger.Debug("Persons : "+myPersons.Mypersons.Count());

		// Cleanup
		ReadFileStream.Close ();
		//Stopwatch stopwatch = new Stopwatch();
		//stopwatch.Start();
		//	Console.WriteLine("=========================Identify PROBE ======================");
		logger.Debug("=========================Identify PROBE ======================");
		// Enroll visitor with unknown identity
		ImagePathManagement ipm_probe = new ImagePathManagement();
		ipm_probe.setProbeFingerPrints(emailUser);
		ipm_probe.addFileToListProbe();


			MyPerson probe = EnrollOld(  ipm_probe.getFilePathList(),ipm_probe.getFileNameList(), "0","Visitor #1234");

			if (probe != null) {
				//stopwatch.Stop();

				// Look up the probe using Threshold = 10
//				Afis.MinMatches = ipm_probe.getFilePathList().Count();
				Afis.Threshold = 50;

//			Console.WriteLine("Identifying {0} in database of {1} persons...", probe.name, myPersons.Mypersons.Count);
				logger.Debug ("Identifying " + probe.name + " in database of " + myPersons.Mypersons.Count + " persons...");
				personsMatch.Mypersons = Afis.Identify (probe, myPersons.Mypersons).OfType<MyPerson> ().ToList ();
				logger.Debug ("=========================== ===== ======================");
				//	Console.WriteLine("=========================== ===== ======================");



//			Console.WriteLine( personsMatch.Mypersons.Count());
				//   Console.WriteLine(personsMatch.Mypersons.Count());
				for (int i = 0; i < personsMatch.Mypersons.Count (); i++) {
					float scoreM = Afis.Verify ( probe,personsMatch.Mypersons [i]);

					//Console.WriteLine("Persons: {0} ,Score : {1}", personsMatch.Mypersons[i].name, scoreM);
					logger.Debug ("Person : "+personsMatch.Mypersons[i].name+ " Score : "+scoreM);
					// Set A personMatch infp
			
					MyPerson aPerson = new MyPerson ();
					aPerson.Id = personsMatch.Mypersons [i].Id;
					aPerson.name = personsMatch.Mypersons [i].name;
					aPerson.score = scoreM;
					aPerson.organisation = personsMatch.Mypersons [i].organisation;
//					aPerson.Fingerprints = personsMatch.Mypersons [i].Fingerprints;
					//aPerson.fp = personsMatch.Mypersons [i].fp;
//					List<MyFingerprint> fp = personsMatch.Mypersons [i].Fingerprints.OfType<MyFingerprint>;
//					for (int m = 0; m < personsMatch.Mypersons [i].Fingerprints.Count (); m++) {
						
//						aPerson.fp.Add(fp[i].filename);
//					}
					personsMatchXML2.Add (aPerson);
				}
			}

		//stopwatch.Stop ();

		//Console.WriteLine ("Elapsed Time {0}", stopwatch.Elapsed);
	
	}

		public List<Person> getPersonMatch ()
	{
			return personsMatchXML2;	
	}

		public  void SetXMLFromObject (MyPersons mp)
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

	public  byte[,] GetPixels (Bitmap bmp)
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
		public MyPerson Enroll (List<List<string>> allPath,List<List<string>> allFileName,string id, string name,string organisation)
		{
			//Console.WriteLine("Enrolling {0}...", name);
			logger.Debug ("Enrolling : "+name+" id : "+id);
			MyPerson person = new MyPerson ();
			person.name = name;
			person.Id = Convert.ToInt32(id);
			person.organisation = organisation;
			logger.Debug ("No. of Folder: " + allPath.Count ());
		
			// Initialize empty fingerprint object and set properties
			for (int i = 0; i < allPath.Count (); i++) {
		//		logger.Debug ("folderName : " + allFileName[i][j]);
				//List<Fingerprint> FingerprintList = new List<Fingerprint>(Fingerprint)
				for (int j = 0; j < allPath [i].Count (); j++) {
					MyFingerprint fp = new MyFingerprint ();
					logger.Debug ("folderName : " + allFileName[i][j]);
					string[] fileSpilt = allFileName[i][j].Split('_');
					if (fileSpilt [2].Contains ("L")) {
						if (fileSpilt [2].Equals ("L0"))
							fp.Finger = Finger.LeftThumb;
						else if (fileSpilt [2].Equals ("L1"))
							fp.Finger = Finger.LeftIndex;
						else if (fileSpilt [2].Equals ("L2"))
							fp.Finger = Finger.LeftMiddle;
						else if (fileSpilt [2].Equals ("L3"))
							fp.Finger = Finger.LeftRing;
						else if (fileSpilt [2].Equals ("L4"))
							fp.Finger = Finger.LeftLittle;
					} else if (fileSpilt [2].Contains ("R")) {
						if (fileSpilt [2].Equals ("R0"))
							fp.Finger = Finger.RightThumb;
						else if (fileSpilt [2].Equals ("R1"))
							fp.Finger = Finger.RightIndex;
						else if (fileSpilt [2].Equals ("R2"))
							fp.Finger = Finger.RightMiddle;
						else if (fileSpilt [2].Equals ("R3"))
							fp.Finger = Finger.RightRing;
						else if (fileSpilt [2].Equals ("R4"))
							fp.Finger = Finger.RightLittle;
					} else fp.Finger = Finger.Any;
					logger.Debug ("finger position : " + fp.Finger );

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
									logger.Debug ("========== Get Bitmap Image ==========");
									fromFile.Dispose();
								}
							}
						}
						catch (Exception e) {
							logger.Debug (e);
							return null; 
						}
					}

					person.Fingerprints.Add (fp);

				}


				//person.Fingerprints.Add (myFingerprints);
			
			}

			// Execute extraction in order to initialize fp.Template
			logger.Debug ("Extraction template.. : "+person.name);

			//Console.WriteLine(" Extracting template... {0} : {1}",person.name,fp.AsBitmap);

			Afis.Extract (person);
			logger.Debug ("Template size " + person.Fingerprints);
			// Check template size
			//Console.WriteLine(" Template size = {0} bytes", fp.Template.Length);

			return person;
		}

	// Take fingerprint image file and create Person object from the image
		public MyPerson EnrollOld (List<string> pathList,List<string> fileNameList,string id, string name)
	{

		//Console.WriteLine("Enrolling {0}...", name);
			logger.Debug ("Enrolling : "+name+" id : "+id);
			MyPerson person = new MyPerson ();
			person.name = name;
			person.Id = Convert.ToInt32(id);
			logger.Debug ("file size : " + pathList.Count ());
		// Initialize empty fingerprint object and set properties
			for (int i = 0; i < pathList.Count (); i++) {
				Fingerprint fp = new Fingerprint ();

				// using (var bitmap = new Bitmap(filename))
				//{
				//	fp.AsBitmap = bitmap;
				//} 
				//fp.AsBitmap = new Bitmap(Bitmap.FromFile(filename));
				
				logger.Debug ("fileName : " + fileNameList[i]);
//				string[] fileSpilt = fileNameList[i].Split('_'); 
//				if (fileSpilt [1].Contains ("L")) {
//					if (fileSpilt [1].Contains ("0")) fp.Finger = Finger.LeftThumb;
//					else if (fileSpilt [1].Contains ("1")) fp.Finger = Finger.LeftIndex;
//					else if (fileSpilt [1].Contains ("2")) fp.Finger = Finger.LeftMiddle;
//					else if (fileSpilt [1].Contains ("3")) fp.Finger = Finger.LeftRing;
//					else if (fileSpilt [1].Contains ("4")) fp.Finger = Finger.LeftLittle;
//				} else if (fileSpilt [1].Contains ("R")) {
//					if (fileSpilt [1].Contains ("0")) fp.Finger = Finger.RightThumb;
//					else if (fileSpilt [1].Contains ("1")) fp.Finger = Finger.RightIndex;
//					else if (fileSpilt [1].Contains ("2")) fp.Finger = Finger.RightMiddle;
//					else if (fileSpilt [1].Contains ("3")) fp.Finger = Finger.RightRing;
//					else if (fileSpilt [1].Contains ("4")) fp.Finger = Finger.RightLittle;
//				} else fp.Finger = Finger.Any;
//				logger.Debug ("finger position : " + fp.Finger );
				logger.Debug ("finger position : " + fp.Finger );
				String filePath = pathList [i];
				using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
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
						logger.Debug (e);
						return null; 
					}
				}
				// Above update of fp.AsBitmapSource initialized also raw image in fp.Image
				// Check raw i
				// Image dimensions, Y axis is first, X axis is second
				//	Console.WriteLine(" Image size = {0} x {1} (width x height)", fp.Image.GetLength(1), fp.Image.GetLength(0));
				// Initialize empty person object and set its properties

				// Add fingerprint to the person
				person.Fingerprints.Add (fp);
			}

		// Execute extraction in order to initialize fp.Template
		logger.Debug ("Extraction template.. : "+person.name);
		//Console.WriteLine(" Extracting template... {0} : {1}",person.name,fp.AsBitmap);

		Afis.Extract (person);
		logger.Debug ("Template size " + person.Fingerprints);
		// Check template size
		//Console.WriteLine(" Template size = {0} bytes", fp.Template.Length);

		return person;
	} 

		public MyPerson Enroll (string filePath, string id)
		{

			//Console.WriteLine("Enrolling {0}...", name);
			logger.Debug ("Enrolling : Person_"+id+" id : "+id);
			MyPerson person = new MyPerson ();
			person.name = id;
			person.Id = Convert.ToInt32(id);

			Fingerprint fp = new Fingerprint ();
			logger.Debug ("filePath : "+filePath);
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
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
					logger.Debug (e);
					return null; 
				}
			}

				// Above update of fp.AsBitmapSource initialized also raw image in fp.Image
				// Check raw i
				// Image dimensions, Y axis is first, X axis is second
				//	Console.WriteLine(" Image size = {0} x {1} (width x height)", fp.Image.GetLength(1), fp.Image.GetLength(0));
				// Initialize empty person object and set its properties

				// Add fingerprint to the person
				person.Fingerprints.Add (fp);


			// Execute extraction in order to initialize fp.Template
			logger.Debug ("Extraction template.. : "+person.name);
			//Console.WriteLine(" Extracting template... {0} : {1}",person.name,fp.AsBitmap);

			Afis.Extract (person);
			logger.Debug ("Template size " + person.Fingerprints);
			// Check template size
			//Console.WriteLine(" Template size = {0} bytes", fp.Template.Length);

			return person;
		}


		public void addPersonDB(int id,string organisation )
		{
			ImagePathManagement fingersForm = new ImagePathManagement();
			fingersForm.setNewPersonFingerPrints(id+"");
			fingersForm.addFileToList();
			logger.Debug(fingersForm.getFileNameList());

			string xml = Path.Combine (xmlPath, "candidateDB.xml");
			// Create a new file stream for reading the XML file
			FileStream ReadFileStream = new FileStream (xml, FileMode.Open, FileAccess.Read, FileShare.Read);

			// Load the object saved above by using the Deserialize function
			XmlSerializer SerializerObj = new XmlSerializer (typeof(MyPersons));
			MyPersons myPersonList = (MyPersons)SerializerObj.Deserialize (ReadFileStream);
			logger.Debug("db myPersonList "+ myPersonList.Mypersons.Count());
			// Cleanup
			ReadFileStream.Close ();
			//int index = list_name_form.Count();
			List <List<string> > allPath = new  List <List<string> >();
			List <List<string> > allFileName = new  List <List<string> >();
			if (fingersForm.getFilePath_right_thumb_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_right_thumb_List ());
				allFileName.Add (fingersForm.getFileName_right_thumb_List ());

			}
			if (fingersForm.getFileName_right_fore_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_right_fore_List ());
				allFileName.Add (fingersForm.getFileName_right_fore_List ());

			}

			if (fingersForm.getFileName_right_middle_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_right_middle_List ());
				allFileName.Add (fingersForm.getFileName_right_middle_List ());

			}

			if (fingersForm.getFileName_right_ring_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_right_ring_List ());
				allFileName.Add (fingersForm.getFileName_right_ring_List ());

			}

			if (fingersForm.getFileName_right_little_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_right_little_List ());
				allFileName.Add (fingersForm.getFileName_right_little_List ());

			}

			if (fingersForm.getFilePath_left_thumb_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_left_thumb_List ());
				allFileName.Add (fingersForm.getFileName_left_thumb_List ());
			}

			if (fingersForm.getFilePath_left_fore_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_left_fore_List ());
				allFileName.Add (fingersForm.getFileName_left_fore_List ());
			}

			if (fingersForm.getFilePath_left_middle_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_left_middle_List ());
				allFileName.Add (fingersForm.getFileName_left_middle_List ());
			}

			if (fingersForm.getFilePath_left_ring_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_left_ring_List ());
				allFileName.Add (fingersForm.getFileName_left_ring_List ());
			}

			if (fingersForm.getFilePath_left_little_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_left_little_List ());
				allFileName.Add (fingersForm.getFileName_left_little_List ());
			}

			if (fingersForm.getFilePath_unknown_1_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_unknown_1_List ());
				allFileName.Add (fingersForm.getFileName_unknown_1_List ());
			}

			if (fingersForm.getFilePath_unknown_2_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_unknown_2_List ());
				allFileName.Add (fingersForm.getFileName_unknown_2_List ());
			}

			if (fingersForm.getFilePath_unknown_3_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_unknown_3_List ());
				allFileName.Add (fingersForm.getFileName_unknown_3_List ());
			}

			if (fingersForm.getFilePath_unknown_4_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_unknown_4_List ());
				allFileName.Add (fingersForm.getFileName_unknown_4_List ());
			}

			if (fingersForm.getFilePath_unknown_5_List ().Count () > 0) {
				allPath.Add (fingersForm.getFilePath_unknown_5_List ());
				allFileName.Add (fingersForm.getFileName_unknown_5_List ());
			}

			MyPerson newCandidate = Enroll(allPath,allFileName,id+"",id+"" ,organisation);
			if (newCandidate != null) {
				myPersonList.Mypersons.Add (newCandidate);
				SetXMLFromObject (myPersonList);
			}else logger.Debug("Add person is fail");
			logger.Debug("Add person is success");


		}



}
}


