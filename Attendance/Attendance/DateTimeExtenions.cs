using System;
using System.Collections.Generic;

namespace Attendance
{
	public static class DateTimeExtenions
	{
		public static IEnumerable<DateTime> ListAllDates(this DateTime lhs, DateTime futureDate)
		{
			var dates = new List<DateTime>();
			int days = (futureDate - lhs).Days;

			for(int i = 0; i <= days; i++) {
				dates.Add (lhs.AddDays(i).Date);
			}

			return dates;
		}
	}
}

