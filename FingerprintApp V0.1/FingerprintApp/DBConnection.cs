using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using log4net;

namespace FingerprintApp
{
	public class DBConnection
	{
		private static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");  
		public List<List<String>> getdb()
		{
			//int count = 0;
			List<List<String>> candidate = new List<List<String>>();
			string connectionString =
				"Server=127.0.0.1;" +
				"Port=3307;" +
				"DATABASE=fingerprintWeb2;" +
				"UID=root;";
			
			string sql = "select id,email,organisation from form";
			MySqlConnection connection = new MySqlConnection(connectionString);
			try
			{
				connection.Open();
				MySqlCommand command = new MySqlCommand(sql, connection);
				MySqlDataReader rdr = command.ExecuteReader();
				while (rdr.Read())
				{
					List<String> person = new List<string>();
					person.Add(Convert.ToString(rdr["id"]));
					person.Add(Convert.ToString(rdr["email"]));
					person.Add(Convert.ToString(rdr["organisation"]));
					candidate.Add(person);
				}
				//count = Convert.ToInt32(command.ExecuteScalar());
				connection.Close();
				logger.Debug ("ExecuteNonQuery in SqlCommand executed !!");
				//MessageBox.Show(" ExecuteNonQuery in SqlCommand executed !!");
			}
			catch (Exception ex)
			{
				logger.Debug ("Can not open connection !!");
				//MessageBox.Show("Can not open connection ! "+ex);
			}
			return candidate;
		}
	

	public void connect()
	{
		string connectionString = null;
		//SqlConnection connection;

		MySqlCommand command;
		string sql = null;
		//connetionString = GetConnectionString();
		//connectionString = "Data Source=localhost;Initial Catalog=fingerprint;User ID=root;Integrated Security=SSPI;";
		connectionString = "Server=localhost;Port=3306;DATABASE=fingerprint2;UID=root;";
		sql = "select * from users";

		//connection = new SqlConnection(connectionString);
		MySqlConnection connection = new MySqlConnection(connectionString);
		MySqlDataReader rdr = null;
		try
		{
			connection.Open();
			command = new MySqlCommand(sql, connection);
			//command.ExecuteNonQuery();
			//command.Dispose();
			rdr = command.ExecuteReader();
			while (rdr.Read())
			{
				Console.WriteLine(rdr.GetInt32(0) + ": "
					+ rdr.GetString(2));
			}
			//string version = Convert.ToString(command.ExecuteScalar());
			connection.Close();
		//	MessageBox.Show(" ExecuteNonQuery in SqlCommand executed !!");
		}
		catch (Exception ex)
		{
			//MessageBox.Show("Can not open connection ! ");
		}
		Console.ReadLine();
	}
  }
}

