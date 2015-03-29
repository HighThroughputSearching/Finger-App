using System;
using SourceAFIS.Simple;
using System.Collections.Generic;

namespace FingerprintAppForAdd
{
	[System.Xml.Serialization.XmlRoot("MyFingerprints")]
	public class MyFingerprints
	{
		public List<Fingerprint> Myfingerprints = new List<Fingerprint>();
	}
}

