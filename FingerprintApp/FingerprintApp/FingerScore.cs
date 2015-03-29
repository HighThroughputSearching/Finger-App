using System;
using SourceAFIS.Simple;
using System.IO;
using log4net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FingerprintApp
{
	public class FingerScore
	{
		static AfisEngine Afis = new AfisEngine();
		string ImagePathProbe = Path.Combine("/Applications/XAMPP/xamppfiles/htdocs/fingerprintWebsite/assets/images/temporary/");
		string ImagePathPersonFinger = Path.Combine("/Applications/XAMPP/xamppfiles/htdocs/fingerprintWebsite/assets/images/form/");
		private static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");
		private  string pathPersonFinger;
		private  string pathProbe;
		float score = 0;

		public void verfiyAFingerScore (string FingerDBArray, string probeArray){
			logger.Debug("================================== Enroll candidate's finger =======================================");
			logger.Debug(FingerDBArray);
			string[] spilt = FingerDBArray.Split('/');

			string folder = spilt[3];
			string fingerPosition_id = spilt [4];
			string fileName = spilt[5];
			logger.Debug("Folder : " + folder);
			logger.Debug("FileName : " + fileName);
			string privateFolder = Path.Combine(folder+"/"+fingerPosition_id, fileName);
			this.pathPersonFinger = Path.Combine(ImagePathPersonFinger, privateFolder);
			logger.Debug("Path: " + pathPersonFinger);
			MyPerson person = enroll(pathPersonFinger);
			logger.Debug("================================== =======================================");
			logger.Debug("==================================PROBE=======================================");
			string[] spiltProbe = probeArray.Split('/');
			string folderProbe = spiltProbe[3];
			string fileNameProbe = spiltProbe[4];
			string privateFolderProbe = Path.Combine(folderProbe, fileNameProbe);
			this.pathProbe = Path.Combine(ImagePathProbe, privateFolderProbe);

			logger.Debug("Probe PAth : " + pathProbe);
			MyPerson personProbe = enroll(pathProbe);
			logger.Debug("=============================================================================");

			logger.Debug("=======================================Score ================================");
			score = Afis.Verify(person, personProbe);
			logger.Debug("=============================================================================");
			logger.Debug("score : " + score);
		}

		public float getScores() {
			return score;
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

