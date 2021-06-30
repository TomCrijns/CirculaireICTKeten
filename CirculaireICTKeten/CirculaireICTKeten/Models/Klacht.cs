using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CirculaireICTKeten.Models
{
	public class Klacht
	{
		public int KlachtID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Priority { get; set; }
		public DateTime CreationTime { get; set; }
		public bool StatusCompleted { get; set; }
		public int ProfielId { get; set; }

	}
}
