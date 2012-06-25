using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace ParisProductsABC
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string connectionString =
				  "Server=localhost;" +
				  "Database=ipv_db;" +
				  "User ID=eddy;" +
				  "Password=;";
			
			IDbConnection dbcon;
			dbcon = new MySqlConnection(connectionString);
			dbcon.Open();
			IDbCommand dbcmd = dbcon.CreateCommand();
			dbcmd.CommandTimeout = 60 * 60;

			string query =
				@"
					SELECT v.producto, v.codigo, p.nombre, SUM(v.importe_con_iva) importe_con_iva, SUM(v.importe_sin_iva) importe_sin_iva
					FROM ventas v INNER JOIN producto p ON v.producto = p.id_producto
					WHERE punto_venta IN ('97M05', '81M05') and codificacion = 5
					GROUP BY v.producto, v.codigo, p.nombre
					ORDER BY v.importe_sin_iva DESC
				";
			
			dbcmd.CommandText = query;
			IDataReader reader = dbcmd.ExecuteReader();
			
            StreamWriter sw = new StreamWriter("ventas.csv");
			
			sw.WriteLine("PRODUCTO_ID,CODIGO,NOMBRE,IMPORTE_CON_IVA,IMPORTE_SIN_IVA");
			
			while(reader.Read()) {
			    sw.WriteLine("{0},\"{1}\",\"{2}\",{3},{4}",
			                 reader["producto"],
			                 reader["codigo"],
			                 reader["nombre"].ToString().Replace("\"", "'"),
			                 reader["importe_con_iva"],
			                 reader["importe_sin_iva"]);
			}
			
			sw.Close();
			
			reader.Close();
			dbcmd.Dispose();
			dbcon.Close();
		}
	}
}

