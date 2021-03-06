﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using log4net;


namespace FingerprintApp
{
	[System.Xml.Serialization.XmlRoot("ImagePathManagement")]
	public class ImagePathManagement
	{
		string ImagePathProbe;

		string filename_left_fore;
		string filename_left_little;
		string filename_left_middle;
		string filename_left_ring;
		string filename_left_thumb;
		string path_left_fore;
		string path_left_little;
		string path_left_middle;
		string path_left_ring;
		string path_left_thumb;

		string filename_right_fore;
		string filename_right_little;
		string filename_right_middle;
		string filename_right_ring;
		string filename_right_thumb;
		string path_right_fore;
		string path_right_little;
		string path_right_middle;
		string path_right_ring;
		string path_right_thumb;

		string filename_unknown_1;
		string filename_unknown_2;
		string filename_unknown_3;
		string filename_unknown_4;
		string filename_unknown_5;
		string path_unknown_1;
		string path_unknown_2;
		string path_unknown_3;
		string path_unknown_4;
		string path_unknown_5;
	
		 
		/* FingerPosition_ID folder */
		string folder_right_thumb_id;
		string folder_right_fore_id;
		string folder_right_middle_id;
		string folder_right_ring_id;
		string folder_right_little_id;

		string folder_left_thumb_id;
		string folder_left_fore_id;
		string folder_left_middle_id;
		string folder_left_ring_id;
		string folder_left_little_id;

//		string folder_unknown_1;
//		string folder_unknown_2;
//		string folder_unknown_3;
//		string folder_unknown_4;
//		string folder_unknown_5;

		string[] extensions = { "tif", "bmp", "jpg" };
		string id;
		string email;
		string organisation;

		List<string> fileNameList = new List<string>();
		List<string> filePathList = new List<string>();
		/** Right Path & Right List **/
		List<string> filePath_right_thumb_List = new List<string>();
		List<string> fileName_right_thumb_List = new List<string>();
		List<string> fileName_right_fore_List = new List<string>();
		List<string> filePath_right_fore_List = new List<string>();
		List<string> fileName_right_middle_List = new List<string>();
		List<string> filePath_right_middle_List = new List<string>();
		List<string> fileName_right_ring_List = new List<string>();
		List<string> filePath_right_ring_List = new List<string>();
		List<string> fileName_right_little_List = new List<string>();
		List<string> filePath_right_little_List = new List<string>();

		/** Left Path & Left List **/
		List<string> filePath_left_thumb_List = new List<string>();
		List<string> fileName_left_thumb_List = new List<string>();
		List<string> fileName_left_fore_List = new List<string>();
		List<string> filePath_left_fore_List = new List<string>();
		List<string> fileName_left_middle_List = new List<string>();
		List<string> filePath_left_middle_List = new List<string>();
		List<string> fileName_left_ring_List = new List<string>();
		List<string> filePath_left_ring_List = new List<string>();
		List<string> fileName_left_little_List = new List<string>();
		List<string> filePath_left_little_List = new List<string>();


		/** Unknown List **/
		List<string> filePath_unknown_1_List = new List<string> ();
		List<string> filePath_unknown_2_List = new List<string> ();
		List<string> filePath_unknown_3_List = new List<string> ();
		List<string> filePath_unknown_4_List = new List<string> ();
		List<string> filePath_unknown_5_List = new List<string> ();
		List<string> fileName_unknown_1_List = new List<string> ();
		List<string> fileName_unknown_2_List = new List<string> ();
		List<string> fileName_unknown_3_List = new List<string> ();
		List<string> fileName_unknown_4_List = new List<string> ();
		List<string> fileName_unknown_5_List = new List<string> ();

		List<MyPerson> database = new List<MyPerson>();
		private static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");   

		public void setProbeFingerPrints(string emailUser)
		{
			setEmail (emailUser);
			/** local computer **/
			this.ImagePathProbe = Path.Combine("/Applications/XAMPP/xamppfiles/htdocs/fingerprintWebsite/assets/images/temporary/",emailUser+"/");   

			/** Server **/
			//this.ImagePathProbe = Path.Combine("/var/www/html/fingerprintWebsite/assets/images/temporary/",emailUser+"/");   

			//Check finferprints files
			logger.Debug(ImagePathProbe);

			// Left fileName
			this.filename_left_thumb =  emailUser +"_L0"+ getExtension (ImagePathProbe, "L0");
			this.filename_left_fore = emailUser+"_L1" + getExtension (ImagePathProbe, "L1");
			this.filename_left_middle =   emailUser + "_L2"+ getExtension (ImagePathProbe, "L2");
			this.filename_left_ring =  emailUser + "_L3"+ getExtension (ImagePathProbe, "L3");
			this.filename_left_little =  emailUser +"_L4"+ getExtension (ImagePathProbe, "L4");

			// Left Path
			this.path_left_thumb = Path.Combine(ImagePathProbe, filename_left_thumb);
			this.path_left_fore = Path.Combine(ImagePathProbe, filename_left_fore);
			this.path_left_middle = Path.Combine(ImagePathProbe, filename_left_middle);
			this.path_left_ring = Path.Combine(ImagePathProbe, filename_left_ring);
			this.path_left_little = Path.Combine(ImagePathProbe, filename_left_little);

			// right fileName
			this.filename_right_thumb = emailUser  +"_R0"+ getExtension (ImagePathProbe, "R0");
			this.filename_right_fore =  emailUser + "_R1" + getExtension (ImagePathProbe, "R1");
			this.filename_right_middle =   emailUser + "_R2" + getExtension (ImagePathProbe, "R2");
			this.filename_right_ring =  emailUser + "_R3" + getExtension (ImagePathProbe, "R3");
			this.filename_right_little =  emailUser + "_R4" + getExtension (ImagePathProbe, "R4");

			// right Path
			this.path_right_fore = Path.Combine(ImagePathProbe, filename_right_fore);
			this.path_right_little = Path.Combine(ImagePathProbe, filename_right_little);
			this.path_right_middle = Path.Combine(ImagePathProbe, filename_right_middle);
			this.path_right_ring = Path.Combine(ImagePathProbe, filename_right_ring);
			this.path_right_thumb = Path.Combine(ImagePathProbe, filename_right_thumb);


			//Unknown fileName
			this.filename_unknown_1 = emailUser + "_U1" + getExtension(ImagePathProbe,"U1");
			this.filename_unknown_2 = emailUser + "_U2" + getExtension (ImagePathProbe, "U2");
			this.filename_unknown_3 = emailUser + "_U3" + getExtension (ImagePathProbe, "U3");
			this.filename_unknown_4 = emailUser + "_U4" + getExtension (ImagePathProbe, "U4");
			this.filename_unknown_5 = emailUser + "_U5" + getExtension (ImagePathProbe, "U5");
			this.path_unknown_1 = Path.Combine(ImagePathProbe, filename_unknown_1);
			this.path_unknown_2 = Path.Combine(ImagePathProbe, filename_unknown_2);
			this.path_unknown_3 = Path.Combine(ImagePathProbe, filename_unknown_3);
			this.path_unknown_4 = Path.Combine(ImagePathProbe, filename_unknown_4);
			this.path_unknown_5 = Path.Combine(ImagePathProbe, filename_unknown_5);

			logger.Debug(path_right_thumb);

		}

		public void setNewPersonFingerPrints(string id)
		{
			/** Server **/
			//	this.ImagePathProbe = Path.Combine("/var/www/html/fingerprintWebsite/assets/images/form/");

			/** local computer **/
			this.ImagePathProbe = Path.Combine("/Applications/XAMPP/xamppfiles/htdocs/fingerprintWebsite/assets/images/form/");

			setId (id);
			logger.Debug("===== setNewPersonFingerPrints ======");

			/** Check finferprints fingerprint postion id folders */
			//Left position id folder
			this.folder_left_thumb_id =   id+"_left_thumb";
			this.folder_left_little_id = id+"_left_little";
			this.folder_left_middle_id =  id+"_left_middle";
			this.folder_left_ring_id = id+"left_ring";
			this.folder_left_fore_id = id+"_left_fore_";

			/** Path Combine */
			// Left Path
			this.path_left_fore = Path.Combine(ImagePathProbe + "left_fore/", folder_left_fore_id);
			this.path_left_little = Path.Combine(ImagePathProbe + "left_little/", folder_left_little_id);
			this.path_left_middle = Path.Combine(ImagePathProbe + "left_middle/", folder_left_middle_id);
			this.path_left_ring = Path.Combine(ImagePathProbe + "left_ring/", folder_left_ring_id);
			this.path_left_thumb = Path.Combine(ImagePathProbe + "left_thumb/", folder_left_thumb_id);


			//Right position id folder
			this.folder_right_thumb_id = id + "_right_thumb";
			this.folder_right_little_id = id + "_right_little";
			this.folder_right_middle_id = id + "_right_middle";
			this.folder_right_ring_id =  id + "_right_ring";
			this.folder_right_fore_id = id+"_right_fore";

			/** Path Combine */
			// right Path
			this.path_right_fore = Path.Combine(ImagePathProbe + "right_fore/", folder_right_fore_id);
			this.path_right_little = Path.Combine(ImagePathProbe + "right_little/", folder_right_little_id);
			this.path_right_middle = Path.Combine(ImagePathProbe + "right_middle/", folder_right_middle_id);
			this.path_right_ring = Path.Combine(ImagePathProbe + "right_ring/", folder_right_ring_id);
			this.path_right_thumb = Path.Combine(ImagePathProbe + "right_thumb/", folder_right_thumb_id);

			/** Path Combine */
			//Unknowm Path
			this.path_unknown_1 = Path.Combine(ImagePathProbe+"unknown/","u1");
			this.path_unknown_2 = Path.Combine(ImagePathProbe+"unknown/","u2");
			this.path_unknown_3 = Path.Combine(ImagePathProbe+"unknown/","u3");
			this.path_unknown_4 = Path.Combine(ImagePathProbe+"unknown/","u4");
			this.path_unknown_5 = Path.Combine(ImagePathProbe+"unknown/","u5");

			logger.Debug ("new PErson: " + path_right_thumb);


		}

		public void setId(string id){
			this.id = id;
		}

		public string getId(){
			return this.id;
		}

		public void setOrganisation(string organisation){
			this.organisation = organisation;
		}

		public string getOrganisation(){
			return this.organisation;
		}

		public void setEmail(string email){
			this.email = email;
		}

		public string getEmail(){
			return this.email;
		}

		public void addFileToList() {
			string[] fileArray = new string[1];
			/** Right Thumb ***/
			bool folderExists = Directory.Exists (path_right_thumb);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_right_thumb, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_right_thumb_List.Add (fileArray [i]); 
					fileName_right_thumb_List.Add (filePathSpit [11]);
					logger.Debug ("File name (right_thumb): " + filePathSpit [11]);
				}
			}

			/** Right Fore ***/
			folderExists = Directory.Exists (path_right_fore);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_right_fore, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_right_fore_List.Add (fileArray [i]); 
					fileName_right_fore_List.Add (filePathSpit [11]);
					logger.Debug ("File name (right_fore): " + filePathSpit [11]);
				}
			}

			/** Right Middle ***/
			folderExists = Directory.Exists (path_right_middle);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_right_middle, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_right_middle_List.Add (fileArray [i]); 
					fileName_right_middle_List.Add (filePathSpit [11]);
					logger.Debug ("File name (right_middle): " + filePathSpit [11]);
				}
			}

			/** Right Ring ***/
			folderExists = Directory.Exists (path_right_ring);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_right_ring, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_right_ring_List.Add (fileArray [i]); 
					fileName_right_ring_List.Add (filePathSpit [11]);
					logger.Debug ("File name (right_ring): " + filePathSpit [11]);
				}
			}

			/** Right Little ***/
			folderExists = Directory.Exists (path_right_little);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_right_little, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_right_little_List.Add (fileArray [i]); 
					fileName_right_little_List.Add (filePathSpit [11]);
					logger.Debug ("File name (right_little): " + filePathSpit [11]);
				}
			}

			/** left Thumb ***/
			folderExists = Directory.Exists (path_left_thumb);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_left_thumb, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_left_thumb_List.Add (fileArray [i]); 
					fileName_left_thumb_List.Add (filePathSpit [11]);
					logger.Debug ("File name (left_thumb): " + filePathSpit [11]);
				}
			}

			/** left Fore ***/
			folderExists = Directory.Exists (path_left_fore);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_left_fore, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_left_fore_List.Add (fileArray [i]); 
					fileName_left_fore_List.Add (filePathSpit [11]);
					logger.Debug ("File name (left_fore): " + filePathSpit [11]);
				}
			}

			/** left Middle ***/
			folderExists = Directory.Exists (path_left_middle);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_left_middle, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_left_middle_List.Add (fileArray [i]); 
					fileName_left_middle_List.Add (filePathSpit [11]);
					logger.Debug ("File name (left_middle): " + filePathSpit [11]);
				}
			}

			/** left Ring ***/
			folderExists = Directory.Exists (path_left_ring);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_left_ring, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_left_ring_List.Add (fileArray [i]); 
					fileName_left_ring_List.Add (filePathSpit [11]);
					logger.Debug ("File name (left_ring): " + filePathSpit [11]);
				}
			}

			/** left Little ***/
			folderExists = Directory.Exists (path_left_little);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_left_little, "*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_left_little_List.Add (fileArray [i]); 
					fileName_left_little_List.Add (filePathSpit [11]);
					logger.Debug ("File name (left_little): " + filePathSpit [11]);
				}
			}

			/** Unknown 1 **/
			folderExists = Directory.Exists (path_unknown_1);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_unknown_1, id+"*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_left_little_List.Add (fileArray [i]); 
					fileName_left_little_List.Add (filePathSpit [11]);
					logger.Debug ("File name (path_unknown_1): " + filePathSpit [11]);
				}
			}

			/** Unknown 2 **/
			folderExists = Directory.Exists (path_unknown_2);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_unknown_2, id+"*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_left_little_List.Add (fileArray [i]); 
					fileName_left_little_List.Add (filePathSpit [11]);
					logger.Debug ("File name (path_unknown_2): " + filePathSpit [11]);
				}
			}

			/** Unknown 3 **/
			folderExists = Directory.Exists (path_unknown_3);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_unknown_3, id+"*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_left_little_List.Add (fileArray [i]); 
					fileName_left_little_List.Add (filePathSpit [11]);
					logger.Debug ("File name (path_unknown_3): " + filePathSpit [11]);
				}
			}

			/** Unknown 4 **/
			folderExists = Directory.Exists (path_unknown_4);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_unknown_4, id+"*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_left_little_List.Add (fileArray [i]); 
					fileName_left_little_List.Add (filePathSpit [11]);
					logger.Debug ("File name (path_unknown_4): " + filePathSpit [11]);
				}
			}

			/** Unknown 5 **/
			folderExists = Directory.Exists (path_unknown_5);
			if (folderExists) {
				fileArray = Directory.GetFiles (path_unknown_5, id+"*.*", SearchOption.AllDirectories).Where (f => extensions.Contains (f.Split ('.').Last ().ToLower ())).ToArray ();
				for (int i = 0; i < fileArray.Length; i++) {
					string[] filePathSpit = fileArray [i].Split ('/');
					filePath_left_little_List.Add (fileArray [i]); 
					fileName_left_little_List.Add (filePathSpit [11]);
					logger.Debug ("File name (path_unknown_5): " + filePathSpit [11]);
				}
			}

				

		}

		public void addFileToListProbe() {
			logger.Debug (path_right_thumb);
			//Add left file to List
			if (File.Exists(path_left_thumb)) { filePathList.Add(path_left_thumb); fileNameList.Add(filename_left_thumb); }
			if (File.Exists(path_left_fore)) { filePathList.Add(path_left_fore); fileNameList.Add(filename_left_fore); }
			if (File.Exists(path_left_little)) { filePathList.Add(path_left_little); fileNameList.Add(filename_left_little); }
			if (File.Exists(path_left_middle)) { filePathList.Add(path_left_middle); fileNameList.Add(filename_left_middle); }
			if (File.Exists(path_left_ring)) { filePathList.Add(path_left_ring); fileNameList.Add(filename_left_ring); }

			//Add right file to List
			if (File.Exists(path_right_thumb)) { filePathList.Add(path_right_thumb); fileNameList.Add(filename_right_thumb); }
			if (File.Exists(path_right_fore)) { filePathList.Add(path_right_fore); fileNameList.Add(filename_right_fore); }
			if (File.Exists(path_right_middle)) { filePathList.Add(path_right_middle); fileNameList.Add(filename_right_middle); }
			if (File.Exists(path_right_ring)) { filePathList.Add(path_right_ring); fileNameList.Add(filename_right_ring); }
			if (File.Exists(path_right_little)) { filePathList.Add(path_right_little); fileNameList.Add(filename_right_little); }

			//Add Unknown file to List
			if(File.Exists(path_unknown_1)) { filePathList.Add(path_unknown_1); fileNameList.Add(filename_unknown_1); }
			if (File.Exists (path_unknown_2)){ filePathList.Add (path_unknown_2); fileNameList.Add (filename_unknown_2); }
			if (File.Exists (path_unknown_3)) { filePathList.Add (path_unknown_3); fileNameList.Add (filename_unknown_3); }
			if (File.Exists (path_unknown_4)) { filePathList.Add (path_unknown_4); fileNameList.Add (filename_unknown_4); }
			if (File.Exists (path_unknown_5)) { filePathList.Add (path_unknown_5); fileNameList.Add (filename_unknown_5); }
			logger.Debug("Exist: " + File.Exists(path_right_thumb));
		}

		public List<string> getFilePathList(){
			return filePathList;
		}


		public List<string> getFilePath_left_thumb_List(){
			return filePath_left_thumb_List;
		}

		public List<string> getFilePath_left_fore_List(){
			return filePath_left_fore_List;
		}

		public List<string> getFilePath_left_middle_List(){
			return filePath_left_middle_List;
		}

		public List<string> getFilePath_left_ring_List(){
			return filePath_left_ring_List;
		}

		public List<string> getFilePath_left_little_List(){
			return filePath_left_little_List;
		}

		public List<string> getFilePath_right_thumb_List(){
			return filePath_right_thumb_List;
		}

		public List<string> getFilePath_right_fore_List(){
			return filePath_right_fore_List;
		}

		public List<string> getFilePath_right_middle_List(){
			return filePath_right_middle_List;
		}

		public List<string> getFilePath_right_ring_List(){
			return filePath_right_ring_List;
		}

		public List<string> getFilePath_right_little_List(){
			return filePath_right_little_List;
		}

		public List<string> getFilePath_unknown_1_List(){
			return filePath_unknown_1_List;
		}

		public List<string> getFilePath_unknown_2_List(){
			return filePath_unknown_2_List;
		}

		public List<string> getFilePath_unknown_3_List(){
			return filePath_unknown_3_List;
		}

		public List<string> getFilePath_unknown_4_List(){
			return filePath_unknown_4_List;
		}

		public List<string> getFilePath_unknown_5_List(){
			return filePath_unknown_5_List;
		}
		 
		public List<string> getFileNameList(){
			return fileNameList;
		}

		public List<string> getFileName_left_thumb_List(){
			return fileName_left_thumb_List;
		}

		public List<string> getFileName_left_fore_List(){
			return fileName_left_fore_List;
		}

		public List<string> getFileName_left_middle_List(){
			return fileName_left_middle_List;
		}

		public List<string> getFileName_left_ring_List(){
			return fileName_left_ring_List;
		}

		public List<string> getFileName_left_little_List(){
			return fileName_left_little_List;
		}

		public List<string> getFileName_right_thumb_List(){
			return fileName_right_thumb_List;
		}

		public List<string> getFileName_right_fore_List(){
			return fileName_right_fore_List;
		}

		public List<string> getFileName_right_middle_List(){
			return fileName_right_middle_List;
		}

		public List<string> getFileName_right_ring_List(){
			return fileName_right_ring_List;
		}

		public List<string> getFileName_right_little_List(){
			return fileName_right_little_List;
		}

		public List<string> getFileName_unknown_1_List(){
			return fileName_unknown_1_List;
		}

		public List<string> getFileName_unknown_2_List(){
			return fileName_unknown_2_List;
		}

		public List<string> getFileName_unknown_3_List(){
			return fileName_unknown_3_List;
		}

		public List<string> getFileName_unknown_4_List(){
			return fileName_unknown_4_List;
		}

		public List<string> getFileName_unknown_5_List(){
			return fileName_unknown_5_List;
		}


		public string getExtension(string ImagePath,string FingerPostion){
			logger.Debug ("getEx");
			string extension = "";
			string temporary_finger_path = Path.Combine (ImagePath);
		
			string email_user = getEmail();
			logger.Debug ("Finger PATH : " +  temporary_finger_path+"/" +email_user+"_"+FingerPostion+".*");
			string[] fileArray = Directory.GetFiles (temporary_finger_path ,email_user+"_"+FingerPostion+".*");
				if(fileArray.Length > 0){
					FileInfo fi = new FileInfo(fileArray [0]);
					extension = fi.Extension;
				}

			logger.Debug ("File Extension : " + extension);
			return extension;

		}

		public string getExtensionForm(string ImagePath,string FingerPostion){
			logger.Debug ("...............getExtensionForm...............");
			string extension = "";
			string temporary_finger_path = Path.Combine (ImagePath,FingerPostion+"/");
			string[] fileArray = Directory.GetFiles (  temporary_finger_path,getId ()+"_"+ getOrganisation()+ "_"+FingerPostion   +".*");
			if (fileArray.Length > 0) {
			
				FileInfo fi = new FileInfo (fileArray [0]);
				extension = fi.Extension;
			}
			logger.Debug ("File Extension : " + extension);
			logger.Debug (".................................................");
			return extension;

		}
	}
}

