using System;
using SourceAFIS.Simple;
using System.IO;
using log4net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;

namespace FingerprintApp
{
	public class FingerScore
	{
		static AfisEngine Afis = new AfisEngine();
		string ImagePathProbe = Path.Combine("/Applications/XAMPP/xamppfiles/htdocs/fingerprintWebsite/");
		private static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");
		public  string xmlPath = Path.Combine("xml");
		private  string pathProbe;
		float score = 0;

		FingerInfo fingerInfo = new FingerInfo();

//		public void verfiyAFingerScore (string FingerDBArray, string probeArray){
//			logger.Debug("================================== Enroll candidate's finger =======================================");
//			logger.Debug(FingerDBArray);
//			string[] spilt = FingerDBArray.Split('/');
//
//			string folder = spilt[3];
//			string fingerPosition_id = spilt [4];
//			string fileName = spilt[5];
//			logger.Debug("Folder : " + folder);
//			logger.Debug("FileName : " + fileName);
//			string privateFolder = Path.Combine(folder+"/"+fingerPosition_id, fileName);
//			this.pathPersonFinger = Path.Combine(ImagePathPersonFinger, privateFolder);
//			logger.Debug("Path: " + pathPersonFinger);
//			MyPerson person = enroll(pathPersonFinger);
//			logger.Debug("================================== =======================================");
//			logger.Debug("==================================PROBE=======================================");
//			string[] spiltProbe = probeArray.Split('/');
//			string folderProbe = spiltProbe[3];
//			string fileNameProbe = spiltProbe[4];
//			string privateFolderProbe = Path.Combine(folderProbe, fileNameProbe);
//			this.pathProbe = Path.Combine(ImagePathProbe, privateFolderProbe);
//
//			logger.Debug("Probe PAth : " + pathProbe);
//			MyPerson personProbe = enroll(pathProbe);
//			logger.Debug("=============================================================================");
//
//			logger.Debug("=======================================Score ================================");
//			score = Afis.Verify(person, personProbe);
//			logger.Debug("=============================================================================");
//			logger.Debug("score : " + score);
//		}
//
		public void verfiyAFingerScore (string id,string probeArray){
			
			logger.Debug("================================== Enroll PROBE=======================================");
			string[] spiltProbe = probeArray.Split('/');
			string folderProbe = spiltProbe[3];
			string fileNameProbe = spiltProbe[4];
			string privateFolderProbe = Path.Combine(folderProbe, fileNameProbe);
			this.pathProbe = Path.Combine(ImagePathProbe, probeArray);

			logger.Debug("Probe PAth : " + pathProbe);
			MyPerson personProbe = enroll(pathProbe);
			logger.Debug("=============================================================================");

			logger.Debug("================================== Enroll candidate's finger =======================================");
			ImagePathManagement ipm = new ImagePathManagement();
			ipm.setNewPersonFingerPrints (id+"");
			ipm.addFileToList ();
			List <List<string> > allPath = new  List <List<string> >();
			List <List<string> > allFileName = new  List <List<string> >();

//			if (ipm.getFilePath_right_thumb_List ().Count > 0) {
//				allPath.Add (ipm.getFilePath_right_thumb_List ());
//				allFileName.Add (ipm.getFileName_right_thumb_List ());
//			}
//			if (ipm.getFileName_right_fore_List ().Count > 0) {
//				allPath.Add (ipm.getFilePath_right_fore_List ());
//				allFileName.Add (ipm.getFileName_right_fore_List ());
//			}
//			if (ipm.getFileName_right_middle_List ().Count> 0) {
//				allPath.Add (ipm.getFilePath_right_middle_List ());
//				allFileName.Add (ipm.getFileName_right_middle_List ());
//			}
//
//			if (ipm.getFileName_right_ring_List ().Count > 0) {
//				allPath.Add (ipm.getFilePath_right_ring_List ());
//				allFileName.Add (ipm.getFileName_right_ring_List ());
//			}
//
//			if (ipm.getFileName_right_little_List ().Count> 0) {
//				allPath.Add (ipm.getFilePath_right_little_List ());
//				allFileName.Add (ipm.getFileName_right_little_List ());
//			}
//
//			if (ipm.getFilePath_left_thumb_List ().Count > 0) {
//				allPath.Add (ipm.getFilePath_left_thumb_List ());
//				allFileName.Add (ipm.getFileName_left_thumb_List ());
//			}
//
//			if (ipm.getFilePath_left_fore_List ().Count> 0) {
//				allPath.Add (ipm.getFilePath_left_fore_List ());
//				allFileName.Add (ipm.getFileName_left_fore_List ());
//			}
//
//			if (ipm.getFilePath_left_middle_List ().Count > 0) {
//				allPath.Add (ipm.getFilePath_left_middle_List ());
//				allFileName.Add (ipm.getFileName_left_middle_List ());
//			}
//
//			if (ipm.getFilePath_left_ring_List ().Count> 0) {
//				allPath.Add (ipm.getFilePath_left_ring_List ());
//				allFileName.Add (ipm.getFileName_left_ring_List ());
//			}
//
//			if (ipm.getFilePath_left_little_List ().Count > 0) {
//				allPath.Add (ipm.getFilePath_left_little_List ());
//				allFileName.Add (ipm.getFileName_left_little_List ());
//			}
//
//			if (ipm.getFilePath_unknown_1_List ().Count> 0) {
//				allPath.Add (ipm.getFilePath_unknown_1_List ());
//				allFileName.Add (ipm.getFileName_unknown_1_List ());
//			}
//
//			if (ipm.getFilePath_unknown_2_List ().Count> 0) {
//				allPath.Add (ipm.getFilePath_unknown_2_List ());
//				allFileName.Add (ipm.getFileName_unknown_2_List ());
//			}
//
//			if (ipm.getFilePath_unknown_3_List ().Count> 0) {
//				allPath.Add (ipm.getFilePath_unknown_3_List ());
//				allFileName.Add (ipm.getFileName_unknown_3_List ());
//			}
//
//			if (ipm.getFilePath_unknown_4_List ().Count > 0) {
//				allPath.Add (ipm.getFilePath_unknown_4_List ());
//				allFileName.Add (ipm.getFileName_unknown_4_List ());
//			}
//
//			if (ipm.getFilePath_unknown_5_List ().Count > 0) {
//				allPath.Add (ipm.getFilePath_unknown_5_List ());
//				allFileName.Add (ipm.getFileName_unknown_5_List ());
//			}
//
			string xml = Path.Combine (xmlPath, "candidateDB.xml");
			// Create a new file stream for reading the XML file
			FileStream ReadFileStream = new FileStream (xml, FileMode.Open, FileAccess.Read, FileShare.Read);

			// Load the object saved above by using the Deserialize function
			XmlSerializer SerializerObj = new XmlSerializer (typeof(MyPersons));
			MyPersons myPersons = (MyPersons)SerializerObj.Deserialize (ReadFileStream);

			logger.Debug("=======================================Score ================================");

			float maxScore = 0;
			string maxScore_path = "";
			MyPerson person;
			int index = Int32.Parse (id)-1;
			do{
				person = myPersons.Mypersons[index];
				if(index < person.Id) index--;
				else if (index > person.Id) index++;
				logger.Debug ("id: "+ person.Id);
			}while(Int32.Parse(id) != person.Id);



			for (int i = 0; i < person.Fingerprints.Count; i++) {
				MyFingerprint fp = person.Fingerprints [i] as MyFingerprint;
				MyPerson aFinger = new MyPerson ();
				aFinger.Fingerprints.Add (fp);
				score = Afis.Verify(aFinger, personProbe);
				string path = fp.filename;
				if (maxScore < score) {
					maxScore = score;
					maxScore_path = path;
				}
			}

//			}
//			for(int i=0; i<allPath.Count; i++){
//				for(int j=0; j<allPath[i].Count; j++){
//					MyPerson aFinger = enroll (allPath[i][j]);
//					logger.Debug ("p: " + aFinger);
//					score = Afis.Verify(aFinger, personProbe);
////					hashtable.Add (allPath [i] [j], score);
//					string[] spilt = allPath [i] [j].Split('/');
//					string path = spilt[8]+'/'+spilt[9]+'/'+spilt[10]+'/'+spilt[11];
//
//					if (maxScore < score) {
//						maxScore = score;
//						maxScore_path = path;
//					}
//
//
////					logger.Debug("Hash Table :  ===="+ path +" ==== > " + score);
//					if(score <= 0 ) break;
//				}
//
//				logger.Debug("=============================================================================");
//			}

			fingerInfo.score = maxScore;
			fingerInfo.path = maxScore_path;


			logger.Debug("score : " + maxScore);
		}

		public float getScores() {
			return score;
		}


	
		public FingerInfo getFingerInfo(){
			return fingerInfo;
		}

		static public MyPerson enroll(string filePath)
		{



			// Initialize empty person object and set its properties
			MyPerson person = new MyPerson();
			Fingerprint fp = new Fingerprint();
		
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				try
				{  logger.Debug("Enrolling ");
					using (Image fromFile = Image.FromStream(fs))
					{
						using (Bitmap bmp = new Bitmap(fromFile))
						{
							
							fp.Image = GetPixels(bmp);
							fromFile.Dispose();
							logger.Debug("Image ");
						}
					}
				}
				catch (Exception e) {
					logger.Debug("Erorr "+e);
					return null; 
				}
			}
			person.Fingerprints.Add(fp);
			Afis.Extract(person);
			logger.Debug("Extractted ");
			return person;
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

	}
}

