using System;
using System.Linq;
using System.Collections.Generic;

namespace Attendance
{
	public class AttendanceDay
	{
		public AttendanceDay ()
		{
			CheckIns = new List<DateTime>(4);
			CheckOuts = new List<DateTime>(4);
		}
		
		public List<DateTime> CheckIns { get; private set; }
		public List<DateTime> CheckOuts { get; private set; }

		public DateTime Date { get; set; }

		public DateTime? FirstCheckIn { 
			get { return CheckIns.Count > 0 ? CheckIns.First() : (DateTime?)null; }
		}

		public DateTime? FirstCheckOut { 
			get { return CheckOuts.Count > 1 ? CheckOuts.First() : (DateTime?)null; }
		}

		public DateTime? LastCheckIn { 
			get { return CheckIns.Count > 1 ? CheckIns.Last() : (DateTime?)null; }
		}
		
		public DateTime? LastCheckOut { 
			get { return CheckOuts.Count > 0 ? CheckOuts.Last() : (DateTime?)null; }
		}

		public TimeSpan Time { 
			get {
				TimeSpan? ts = null;

				if (FirstCheckOut == null && LastCheckIn == null) {
					ts = (LastCheckOut - FirstCheckIn);
				} else {
					ts = (FirstCheckOut - FirstCheckIn) + (LastCheckOut - LastCheckIn);
				}

				return ts.HasValue ? ts.Value : TimeSpan.Zero;
			}
		}
	}
}
