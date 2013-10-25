//
// Program.cs
//
// Author:
//       Eddy Zavaleta <eddy@mictlanix.org>
//
// Copyright (c) 2013 Copyright (C) 2013 Eddy Zavaleta, Mictlanix, and contributors.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DbMigration
{
	class MainClass
	{
		static string conn_string = "Server=localhost;" +
									"User ID=developer;" +
									"Password=123456;" +
									"Pooling=false";
		static string db_name = "mbe_db";
		static string db_alias = "mbe_ramos";

		public static void Main (string[] args)
		{
			string sql_tables = @"SELECT t.table_name
								FROM information_schema.tables t 
								WHERE t.table_schema = '{0}'
								ORDER BY t.table_name";
			string sql_columns = @"SELECT c.column_name
								FROM information_schema.columns c
								WHERE c.table_schema = '{0}' AND c.table_name = '{1}'
								ORDER BY c.ordinal_position";
			string sql_template = "INSERT INTO {1}\n\t({2})\nSELECT\n\t{2}\nFROM {0}.{1};";
			var tables = new List<string> (100);

			using (var dbcon = new MySqlConnection (conn_string)) {
				dbcon.Open ();

				using(var cmd = dbcon.CreateCommand ()) {
					cmd.CommandText = string.Format(sql_tables, db_name);
					using(var reader = cmd.ExecuteReader ()) {
						while (reader.Read()) {
							tables.Add (reader.GetString ("table_name"));
						}
					}
				}

				foreach(var table in tables) {
					string columns = string.Empty;

					using(var cmd = dbcon.CreateCommand ()) {
						cmd.CommandText = string.Format(sql_columns, db_name, table);
						using(var reader = cmd.ExecuteReader ()) {
							while (reader.Read()) {
								columns += reader ["column_name"] + ", ";
							}
						}
					}

					columns = columns.Trim (" ,".ToCharArray ());
					Console.WriteLine (sql_template, db_alias, table, columns);
					Console.WriteLine ();
				}
			}
		}
	}
}
