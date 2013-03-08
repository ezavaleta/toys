using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;

namespace Attendance
{
	class MainClass
	{
		static string conn_string = "Server=localhost;" +
									"Database=attendance_db;" +
									"User ID=developer;" +
									"Password=123456;" +
									"Pooling=false";
		static string sql_times = @"SELECT status, time 
									FROM attendance 
									WHERE employee = {0} AND 
										  time >= '{1:s}' AND time <= '{2:s}' 
									ORDER BY time";
		static List<Employee> employees = GetEmployees ();

#region *** Templates ***
		static string HTML_TEMPLATE = @"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/>
    <meta charset='UTF-8'/>
    <meta name='author' content='Eddy Zavaleta (eddy@mictlanix.org)'/>
    <meta name='robots' content='index, follow'/>
    <meta name='Googlebot' content='index,follow'/>
    <meta name='distribution' content='global'/>
    <meta name='rating' content='general'/>
    <title>Dataprint - TClock</title>
    <link href='css/main.css' rel='stylesheet' type='text/css'/>
</head>
<body>
	<h1>Reporte de Asistencia</h1>
	$$DATA$$
</body>
</html>
";
		static string TABLE_TEMPLATE = @"
	<h2>$$EMPLOYEE_NAME$$<h2>
	<table>
		<tr>
			<th rowspan='2'>Día</th>
			<th class='blue' colspan='2'>Mañana</th>
			<th class='blue' colspan='2'>Tarde</th>
			<th class='blue' rowspan='2'>Tiempo</th>
		</tr>
		<tr>
			<th class='blue'>Entrada</th>
			<th class='blue'>Salida</th>
			<th class='blue'>Entrada</th>
			<th class='blue'>Salida</th>
		</tr>
		$$ROWS$$
		<tr>
			<th class='borderless'></th>
			<th class='total' colspan='4'>Totales</th>
			<td class='total'>$$TOTAL_TIME$$</td>
		</tr>
	</table>";
		static string ROW_TEMPLATE = @"		<tr><th>{0:D}</th><td>{1:HH:mm}</td><td>{2:HH:mm}</td><td>{3:HH:mm}</td><td>{4:HH:mm}</td><td>{5:hh} hrs. {5:mm} min.</td></tr>";
#endregion

		public static void Main (string[] args)
		{
			var start_date = new DateTime (2012,10,21);
			var end_date = new DateTime (2012,11,6);
			var sb_html = new StringBuilder(HTML_TEMPLATE);
			var sb_data = new StringBuilder();

			using (var dbcon = new MySqlConnection (conn_string)) {
				dbcon.Open ();
				foreach (var employee in employees) {
					var sb_employee = new StringBuilder(TABLE_TEMPLATE);
					var sb_rows = new StringBuilder();

					sb_employee.Replace ("$$EMPLOYEE_NAME$$", employee.Name);

					foreach (var date in start_date.ListAllDates(end_date)) {
						string sql = string.Format(sql_times, employee.Id, date.AddHours(6), date.AddDays(1).AddHours(6));
						AttendanceDay day = null;

						using(var dbcmd = dbcon.CreateCommand ()) {
							dbcmd.CommandText = sql;

							using(var reader = dbcmd.ExecuteReader ()) {
								day = new AttendanceDay {
									Date = date
								};

								while (reader.Read()) {
									if(reader ["status"].ToString() == "In")
										day.CheckIns.Add ((DateTime)reader ["time"]);
									else
										day.CheckOuts.Add ((DateTime)reader ["time"]);
								}

								employee.AttendanceDays.Add (day);
							}
						}

						sb_rows.AppendFormat (ROW_TEMPLATE, day.Date, day.FirstCheckIn, day.FirstCheckOut, day.LastCheckIn, day.LastCheckOut, day.Time);
					}

					sb_employee.Replace ("$$EMPLOYEE_NAME$$", employee.Name);
					sb_employee.Replace ("$$ROWS$$", sb_rows.ToString());
					sb_employee.Replace ("$$TOTAL_TIME$$", string.Format ("{0:0} hrs. {1} min.", employee.TotalTime.TotalHours, employee.TotalTime.Minutes));
					sb_data.Append (sb_employee.ToString ());
				}
				sb_html.Replace ("$$DATA$$", sb_data.ToString());
			}

			File.WriteAllText("/Users/eddy/Downloads/attendance/attendance.html", sb_html.ToString ());
		}

		public static List<Employee> GetEmployees ()
		{
			var items = new List<Employee> ();
			string sql = "SELECT employee_id, name " +
						 "FROM employee";

			using (var dbcon = new MySqlConnection (conn_string)) {
				dbcon.Open ();

				using(var dbcmd = dbcon.CreateCommand ()) {
					dbcmd.CommandText = sql;

					using(var reader = dbcmd.ExecuteReader ()) {
						while (reader.Read()) {
							items.Add (new Employee {
								Id = (int)reader ["employee_id"],
								Name = (string)reader ["name"]
							});
						}
					}
				}
			}
			
			return items;
		}
	}
}
