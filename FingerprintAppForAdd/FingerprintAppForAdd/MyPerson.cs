using System;
using SourceAFIS.Simple;

namespace FingerprintAppForAdd{

	[System.Xml.Serialization.XmlRoot("Person")]
	public class MyPerson : Person
	{
		public int Id{ get; set; }
		public string name{ get; set; }
		public float score{ get; set; }
		public string organisation { get; set;}
		public MyFingerprint fp{ get; set;}
	 
	}

}
