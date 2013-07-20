using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Zealocity
{
	/// <summary>
	/// Summary description for DatabaseClass.
	/// </summary>
	public class DatabaseClass
	{

		#region Member Variables

		private		int			TIMEOUT				= 600;					// 300 = 5 minutes
		private		string		DBUSER				= string.Empty;
		private		string		DBPASSWORD			= string.Empty;
		private		string		CONTROLSERVER		= string.Empty;
		private		string		CONTROLDATABASE		= string.Empty;

		public		string		ServerName			= string.Empty;
		public		string		DatabaseName		= string.Empty;

		#endregion Member Variables


		#region Constructor

		public DatabaseClass()
		{
			DBUSER					= ConfigurationSettings.AppSettings["DBUSER"];
			DBPASSWORD				= ConfigurationSettings.AppSettings["DBPASSWORD"];
			CONTROLSERVER			= ConfigurationSettings.AppSettings["CONTROLSERVER"];
			CONTROLDATABASE			= ConfigurationSettings.AppSettings["CONTROLDATABASE"];
		}

		#endregion Constructor


		#region Private Methods

		private string GetConnectionString(string TableName)
		{
			string	Return			= string.Empty;
			string	SQLServer		= string.Empty;
			string	SQLDatabase		= string.Empty;
//			string	User			= string.Empty;
//			string	Password		= string.Empty;

			try
			{
//				SQLServer			= ConfigurationSettings.AppSettings["SQLServer"];
//				SQLDatabase			= ConfigurationSettings.AppSettings["SQLDatabase"];
//				SQLServer			= "stech2";
//				SQLDatabase			= "WebSearch";

				GetServerAndDatabaseName(TableName);

//				User				= "sa";
//				Password			= "14stech";

				//Return			= "Driver={SQL Server};SERVER=" + SQLServer + ";DATABASE=" + SQLDatabase + ";UID=" + User + ";PWD=" + Password;
				//string sReturn = "Server=" + passedServer + ";database=" + passedDb + "; Trusted_Connection=Yes";
				//sReturn += ";pooling=true;connection lifetime=30 ;min pool size=1;max pool size=1000 " ;

				Return				= "SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + this.DBUSER + ";PWD=" + this.DBPASSWORD;
				Return				+= ";pooling=true;connection lifetime=30 ;min pool size=1;max pool size=1000 ";
			}
			catch(Exception ex)
			{
				string error		= ex.Message;
			}

			return Return;
		}		// GetConnectionString
		

		public void GetServerAndDatabaseName(string TableName)
		{
			string			Return			= string.Empty;
			string			SQLQuery		= string.Empty;
			SqlDataReader	Records			= null;

			try
			{
				if(TableName.Equals("DataLocations"))
				{
					ServerName				= CONTROLSERVER;
					DatabaseName			= CONTROLDATABASE;
				}
				else
				{
					// does this data already exist
					SQLQuery				= "SELECT Top 1 ServerName, DatabaseName ";
					SQLQuery				+= " FROM DataLocations ";
					SQLQuery				+= " WHERE TableName = '" + TableName + "'";
					SQLQuery				+= " AND Enabled = 'Y'";
					Records					= RunSelectStatement(SQLQuery, "DataLocations");

					// get the record id
					while (Records.Read())
					{
						ServerName			= Records[0].ToString().Trim();
						DatabaseName		= Records[1].ToString().Trim();
					}

					Records.Close();
				}
			}
			catch(Exception ex)
			{
				string error				= ex.Message;
			}
			finally
			{
				if(Records != null)
					Records.Close();
				Records						= null;
			}

		}		// GetServerAndDatabaseName

		#endregion Private Methods


		#region Public Methods

		public SqlConnection GetConnection(string TableName)
		{
			string			ConnectionString		= string.Empty;
			SqlConnection	Connection				= null;
			
			try
			{
				// Build connection string
				ConnectionString					= GetConnectionString(TableName);

				// create the connection object
				Connection							= new SqlConnection(ConnectionString);
				Connection.Open();
			}
			catch(Exception ex)
			{
				string error						= ex.Message;
				throw ex;
			}

			return Connection;
		}		// GetConnection


		public DataSet RunSelectStatement(string SQLQuery, string TableName, string TableID)
		{
			SqlConnection	Connection				= null;
			SqlDataAdapter	sqlDataAdapt			= null;
			DataSet			Return					= new DataSet();

			try
			{
				// get a connection to the server
				Connection							= GetConnection(TableName);
				//Connection.Open();

				// run the sql select statement
				sqlDataAdapt						= new SqlDataAdapter(SQLQuery, Connection);
				sqlDataAdapt.SelectCommand.CommandTimeout	= TIMEOUT;
				sqlDataAdapt.Fill(Return, TableID);
			}
			catch(Exception ex)
			{
				string error						= ex.Message;
			}
			finally
			{
				sqlDataAdapt						= null;
		
				if(Connection != null)
					Connection.Close();
				Connection							= null;
			}

			return Return;
		}		// RunSelectStatement


		public SqlDataReader RunSelectStatement(string SQLQuery, string TableName)
		{
			SqlConnection	Connection				= null;
			SqlCommand		Command					= null;
			SqlDataReader	Return					= null;

			try
			{
				// get a connection to the server
				Connection							= GetConnection(TableName);
				//Connection.Open();

				// run the sql select statement
				Command								= new SqlCommand(SQLQuery, Connection);
				Command.CommandTimeout				= TIMEOUT;
				Return								= Command.ExecuteReader();
			}
			catch(Exception ex)
			{
				string error						= ex.Message;
			}
			finally
			{
				//Command.Dispose();
				Command								= null;

//				if(Connection != null)
//					Connection.Close();
				Connection							= null;
			}

			return Return;
		}		// RunSelectStatement

		
		public int RunSQLStatement(string SQLQuery, string TableName)
		{
			SqlConnection	Connection				= null;
			SqlCommand		Command					= null;
			int				Return					= 0;

			try
			{
				// get a connection to the server
				Connection							= GetConnection(TableName);
				//Connection.Open();

				// run the sql select statement
				Command								= new SqlCommand(SQLQuery, Connection);
				Command.CommandTimeout				= TIMEOUT;
				Return								= Command.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				string error						= ex.Message;
			}
			finally
			{
				//Command.Dispose();
				Command								= null;

				if(Connection != null)
					Connection.Close();
				//Connection.Dispose();
				Connection							= null;
			}

			return Return;
		}		// RunSQLStatement
	
		#endregion Public Methods

	}
}
