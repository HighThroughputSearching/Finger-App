using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using log4net;


namespace WebFinger1
{
	[System.Xml.Serialization.XmlRoot("ImagePathManagement")]
	public class ImagePathManagement
	{
		string ImagePathProbe;
		string ImagePath;
		string filename_left_fore;
		string filename_left_little;
		string filename_left_middle;
		string filename_left_ring;
		string filename_left_thumb;
		string filename_left_thumb_hand;
		string filename_lefthands;
		string filename_lefthand;

		string path_left_fore;
		string path_left_little;
		string path_left_middle;
		string path_left_ring;
		string path_left_thumb;
		string path_left_thumb_hand;
		string path_lefthand;

		string filename_right_fore;
		string filename_right_little;
		string filename_right_middle;
		string filename_right_ring;
		string filename_right_thumb;
		string filename_right_thumb_hand;
		string filename_righthands;
		string filename_righthand;

		string path_right_fore;
		string path_right_little;
		string path_right_middle;
		string path_right_ring;
		string path_right_thumb;
		string path_right_thumb_hand;
		string path_righthand;

		string id;
		string email;
		List<string> fileNameList = new List<string>();
		List<string> filePathList = new List<string>();
		List<MyPerson> database = new List<MyPerson>();
		private static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");   

		public void setProbeFingerPrints(string emailUser)
		{
			setEmail (emailUser);
			this.ImagePathProbe = Path.Combine("/Applications/XAMPP/xamppfiles/htdocs/fingerprint3/assets/images/temporary/",emailUser+"/");
			//Check finferprints files

			logger.Debug(ImagePathProbe);
			// Left fileName
			this.filename_left_fore = "left_fore_" + emailUser + getExtension (ImagePathProbe, "left_fore");
			this.filename_left_little = "left_little_" + emailUser + getExtension (ImagePathProbe, "left_little");
			this.filename_left_middle = "left_middle_" + emailUser + getExtension (ImagePathProbe, "left_middle");
			this.filename_left_ring = "left_ring_" + emailUser+ getExtension (ImagePathProbe, "left_ring");
			this.filename_left_thumb = "left_thumb_" + emailUser + getExtension (ImagePathProbe, "left_thumb");
			this.filename_left_thumb_hand = "left_thumb_hand_" + emailUser +getExtension (ImagePathProbe, "left_thumb_hand");
			this.filename_lefthand = "lefthand_" + emailUser + getExtension (ImagePathProbe, "lefthand");
			// this.filename_left_thumb_hand = "finger_6_" + emailUser + ".bmp";
			// this.filename_lefthand = "finger_7_" + emailUser + ".bmp";

			// Left Path
			this.path_left_fore = Path.Combine(ImagePathProbe, filename_left_fore);
			this.path_left_little = Path.Combine(ImagePathProbe, filename_left_little);
			this.path_left_middle = Path.Combine(ImagePathProbe, filename_left_middle);
			this.path_left_ring = Path.Combine(ImagePathProbe, filename_left_ring);
			this.path_left_thumb = Path.Combine(ImagePathProbe, filename_left_thumb);
			//this.path_left_thumb_hand = Path.Combine(ImagePathProbe, filename_left_thumb_hand);
			//this.path_lefthand = Path.Combine(ImagePathProbe, filename_lefthand);

			// right fileName
			this.filename_right_fore = "right_fore_" + emailUser + getExtension (ImagePathProbe, "right_fore");
			this.filename_right_little = "right_little_" + emailUser + getExtension (ImagePathProbe, "right_little");
			this.filename_right_middle = "right_middle_" + emailUser + getExtension (ImagePathProbe, "right_middle");
			this.filename_right_ring = "right_ring_" + emailUser+ getExtension (ImagePathProbe, "right_ring");
			this.filename_right_thumb = "right_thumb_" + emailUser + getExtension (ImagePathProbe, "right_thumb");
			this.filename_right_thumb_hand = "right_thumb_hand_" + emailUser +getExtension (ImagePathProbe, "right_thumb_hand");
			this.filename_righthand = "righthand_" + emailUser + getExtension (ImagePathProbe, "righthand");
			//  this.filename_right_thumb_hand = "finger_" + emailUser + ".bmp";
			// this.filename_righthand = "finger_" + emailUser + ".bmp";

			// right Path
			this.path_right_fore = Path.Combine(ImagePathProbe, filename_right_fore);
			this.path_right_little = Path.Combine(ImagePathProbe, filename_right_little);
			this.path_right_middle = Path.Combine(ImagePathProbe, filename_right_middle);
			this.path_right_ring = Path.Combine(ImagePathProbe, filename_right_ring);
			this.path_right_thumb = Path.Combine(ImagePathProbe, filename_right_thumb);
			//this.path_right_thumb_hand = Path.Combine(ImagePathProbe, filename_right_thumb_hand);
			//this.path_righthand = Path.Combine(ImagePathProbe, filename_righthand);

			logger.Debug(path_right_thumb);

		}

		public void setNewPersonFingerPrints(string id)
		{
			this.ImagePathProbe = Path.Combine("/Applications/XAMPP/xamppfiles/htdocs/fingerprint3/assets/images/form/");
		//	this.ImagePathProbe = Path.Combine("/Applications/XAMPP/xamppfiles/htdocs/fingerprint3/assets/images/temporary/",emailUser+"/");
			setId (id);
			logger.Debug("===== setNewPersonFingerPrints ======");
			//Check finferprints files
			// Left fileName
			this.filename_left_fore = "left_fore_" + id + getExtensionForm (ImagePathProbe, "left_fore");
			this.filename_left_little = "left_little_" + id + getExtensionForm (ImagePathProbe, "left_little");
			this.filename_left_middle = "left_middle_" + id + getExtensionForm (ImagePathProbe, "left_middle");
			this.filename_left_ring = "left_ring_" + id + getExtensionForm (ImagePathProbe, "left_ring");
			this.filename_left_thumb = "left_thumb_" + id + getExtensionForm (ImagePathProbe, "left_thumb");
			this.filename_left_thumb_hand = "left_thumb_hand_" + id + getExtensionForm (ImagePathProbe, "left_thumb_hand");
			this.filename_lefthand = "lefthand_" + id + getExtensionForm (ImagePathProbe, "lefthand");


			// Left Path
			this.path_left_fore = Path.Combine(ImagePathProbe + "left_fore/", filename_left_fore);
			this.path_left_little = Path.Combine(ImagePathProbe + "left_little/", filename_left_little);
			this.path_left_middle = Path.Combine(ImagePathProbe + "left_middle/", filename_left_middle);
			this.path_left_ring = Path.Combine(ImagePathProbe + "left_ring/", filename_left_ring);
			this.path_left_thumb = Path.Combine(ImagePathProbe + "left_thumb/", filename_left_thumb);
			this.path_left_thumb_hand = Path.Combine(ImagePathProbe + "left_thumb_hand/", filename_left_thumb_hand);
			this.path_lefthand = Path.Combine(ImagePathProbe + "lefthand/", filename_lefthand);

		
			// right fileName
			this.filename_right_fore = "right_fore_" + id + getExtensionForm (ImagePathProbe, "right_fore");
			this.filename_right_little = "right_little_" + id + getExtensionForm (ImagePathProbe, "right_little");
			this.filename_right_middle = "right_middle_" + id + getExtensionForm (ImagePathProbe, "right_middle");
			this.filename_right_ring = "right_ring_" + id + getExtensionForm (ImagePathProbe, "right_ring");
			this.filename_right_thumb = "right_thumb_" + id + getExtensionForm (ImagePathProbe, "right_thumb");
			this.filename_right_thumb_hand = "right_thumb_hand_" + id + getExtensionForm (ImagePathProbe, "right_thumb_hand");
			this.filename_righthand = "righthand_" + id + getExtensionForm (ImagePathProbe, "righthand");;

			// right Path
			this.path_right_fore = Path.Combine(ImagePathProbe + "right_fore/", filename_right_fore);
			this.path_right_little = Path.Combine(ImagePathProbe + "right_little/", filename_right_little);
			this.path_right_middle = Path.Combine(ImagePathProbe + "right_middle/", filename_right_middle);
			this.path_right_ring = Path.Combine(ImagePathProbe + "right_ring/", filename_right_ring);
			this.path_right_thumb = Path.Combine(ImagePathProbe + "right_thumb/", filename_right_thumb);
			this.path_right_thumb_hand = Path.Combine(ImagePathProbe + "right_thumb_hand/", filename_right_thumb_hand);
			this.path_righthand = Path.Combine(ImagePathProbe + "righthand/", filename_righthand);

			logger.Debug("new PErson: "+path_right_thumb);

		}

		public void setId(string id){
			this.id = id;
		}

		public string getId(){
			return this.id;
		}

		public void setEmail(string email){
			this.email = email;
		}

		public string getEmail(){
			return this.email;
		}

		public void addFileToList() {
			//Add left file
			if (File.Exists(path_left_fore)) { filePathList.Add(path_left_fore); fileNameList.Add(filename_left_fore); }
			if (File.Exists(path_left_little)) { filePathList.Add(path_left_little); fileNameList.Add(filename_left_little); }
			if (File.Exists(path_left_middle)) { filePathList.Add(path_left_middle); fileNameList.Add(filename_left_middle); }
			if (File.Exists(path_left_ring)) { filePathList.Add(path_left_ring); fileNameList.Add(filename_left_ring); }
			if (File.Exists(path_left_thumb)) { filePathList.Add(path_left_thumb); fileNameList.Add(filename_left_thumb); }
			if (File.Exists(path_lefthand)) { filePathList.Add(path_lefthand); fileNameList.Add(filename_lefthand); }
			//Add right file
			if (File.Exists(path_right_fore)) { filePathList.Add(path_right_fore); fileNameList.Add(filename_right_fore); }
			if (File.Exists(path_right_little)) { filePathList.Add(path_right_little); fileNameList.Add(filename_right_little); }
			if (File.Exists(path_right_middle)) { filePathList.Add(path_right_middle); fileNameList.Add(filename_right_middle); }
			if (File.Exists(path_right_ring)) { filePathList.Add(path_right_ring); fileNameList.Add(filename_right_ring); }
			if (File.Exists(path_right_thumb)) { filePathList.Add(path_right_thumb); fileNameList.Add(filename_right_thumb); }
			if (File.Exists(path_righthand)) { filePathList.Add(path_righthand); fileNameList.Add(filename_righthand); }
			logger.Debug("Exist: " + File.Exists(path_right_thumb));
		}

		public List<string> getFilePathList(){
			return filePathList;
		}

		public List<string> getFileNameList(){
			return fileNameList;
		}

		public string getExtension(string ImagePath,string FingerPostion){
			logger.Debug ("getEx");
			string[] extensions = { "tif", "bmp", "jpg" };
			string extension = "";
			string temporary_finger_path = Path.Combine (ImagePath);

			//if (File.Exists (temporary_finger_path)) {
			string email_user = getEmail();
			logger.Debug ("Finger PATH : " +  temporary_finger_path+"/" +FingerPostion+"_"+email_user   +".*");
				//string[] fileArray = Directory.GetFiles (temporary_finger_path, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
			string[] fileArray = Directory.GetFiles (  temporary_finger_path ,FingerPostion+"_"+email_user   +".*");
				if(fileArray.Length > 0){
					FileInfo fi = new FileInfo(fileArray [0]);
					extension = fi.Extension;
				}
			//}

			logger.Debug ("File Extension : " + extension);
			return extension;

		}

		public string getExtensionForm(string ImagePath,string FingerPostion){
			logger.Debug ("getEx");
			string[] extensions = { "tif", "bmp", "jpg" };
			string extension = "";
		//	string temporary_finger_path = Path.Combine (ImagePath,FingerPostion);


			string temporary_finger_path = Path.Combine (ImagePath,FingerPostion+"/");
		//	string[] fileArray = Directory.GetFiles (temporary_finger_path, ".*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
		/*	for (int i = 0; i < extensions.Count (); i++) {
				if (File.Exists (temporary_finger_path + "_" + getId () + "."+extensions[i])) {
					/string[] fileArray = Directory.GetFiles ();
					logger.Debug ("Finger PATH : " + temporary_finger_path);
					if (fileArray.Count () > 0) {
						FileInfo fi = new FileInfo (fileArray [0]);
						extension = fi.Extension;
					}
				}
			}*/
			string[] fileArray = Directory.GetFiles (  temporary_finger_path,FingerPostion + "_" + getId ()  +".*");
			if (fileArray.Length > 0) {
			
				FileInfo fi = new FileInfo (fileArray [0]);
				extension = fi.Extension;
			}
			logger.Debug ("File Extension : " + extension);
			return extension;

		}
	}
}

