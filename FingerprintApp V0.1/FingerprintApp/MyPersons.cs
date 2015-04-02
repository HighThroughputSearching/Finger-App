using System;
using System.Collections.Generic;

namespace FingerprintApp{
		[System.Xml.Serialization.XmlRoot("MyPersons")]
		public class MyPersons
		{
			public List<MyPerson> Mypersons = new List<MyPerson>();
		}
}

