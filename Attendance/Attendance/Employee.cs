using System;
using System.Linq;
using System.Collections.Generic;

namespace Attendance
{
	public class Employee
	{
		public Employee ()
		{
			AttendanceDays = new List<AttendanceDay>(15);
		}
		
		public int Id { get; set; }
		public String Name { get; set; }
		public List<AttendanceDay> AttendanceDays { get; set; }

		public TimeSpan TotalTime {
			get { return new TimeSpan(AttendanceDays.Sum (x => x.Time.Ticks)); }
		}
	}
}

