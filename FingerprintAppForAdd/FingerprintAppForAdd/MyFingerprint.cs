﻿using System;
using SourceAFIS.Simple;
namespace FingerprintAppForAdd
{
	[System.Xml.Serialization.XmlRoot("MyFingerprint")]
	public class MyFingerprint : Fingerprint
	{
		public string filename{ get; set; }
	}
}

