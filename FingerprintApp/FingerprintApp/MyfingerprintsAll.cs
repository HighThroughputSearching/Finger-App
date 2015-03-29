using System;
using System.Collections.Generic;

namespace FingerprintApp
{
	[System.Xml.Serialization.XmlRoot("MyfingerprintsAll")]
	public class MyfingerprintsAll
	{
		public List<MyFingerprints> myfingerprintsAll = new List<MyFingerprints>();
	}
}

