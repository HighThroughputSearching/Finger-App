using System;
using System.Collections.Generic;

namespace WebFinger1{
		[System.Xml.Serialization.XmlRoot("MyPersons")]
		public class MyPersons
		{
			public List<MyPerson> Mypersons = new List<MyPerson>();
		}
}

