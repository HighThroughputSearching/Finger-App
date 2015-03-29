using System;
using SourceAFIS.Simple;
using System.Collections.Generic;


namespace FingerprintApp{
	[System.Xml.Serialization.XmlRoot("MyPerson")]
	public class MyPerson : Person
	{
		public int Id{ get; set; }
		public string name{ get; set; }
		public float score{ get; set; }
		public string organisation { get; set;}
		public string fileName { get; set;}
		public MyFingerprint fp{ get; set;}

	 
	}
}	
