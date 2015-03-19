using System;
using SourceAFIS.Simple;

namespace FingerprintAppForAdd{

	[System.Xml.Serialization.XmlRoot("Person")]
	public class MyPerson : Person
	{
		public string name{ get; set; }
	 
	}

}
