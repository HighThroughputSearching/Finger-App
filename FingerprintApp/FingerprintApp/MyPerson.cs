﻿using System;
using SourceAFIS.Simple;
namespace FingerprintApp{
	[System.Xml.Serialization.XmlRoot("MyPerson")]
	public class MyPerson : Person
	{
		public int Id{ get; set; }
		public string name{ get; set; }
		public float score{ get; set; }
	 
	}
}	
