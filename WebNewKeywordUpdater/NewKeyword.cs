using System;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using Zealocity;

namespace WebNewKeywordUpdater
{
	/// <summary>
	/// Summary description for NewKeyword.
	/// </summary>
	public class NewKeyword
	{
		private	DatabaseClass	_Database		= new DatabaseClass();
		private	System.Text.ASCIIEncoding AE	= new System.Text.ASCIIEncoding();
		private	String			_Keyword		= string.Empty;

		public	bool			StopProcessing	= false;

		public	DataSet			_Records		= null;


		public NewKeyword()
		{
		}

		public NewKeyword(string Keyword)
		{
			this._Keyword = Keyword;
		}


		#region		AddNewKeyword

//		public bool AddNewKeyword()
//		{
//			bool			Success			= false;
//
//			Success							= AddNewKeyword(this._Keyword);
//
//			return Success;
//		}

		public bool AddNewKeyword(string Keyword)
		{
			SqlConnection	Connection		= null;
			SqlCommand		Command			= null;
			DatabaseClass	database		= new DatabaseClass();
//			SqlDataReader	Records			= null;
			string			PageData		= string.Empty;
			string			SQLQuery		= string.Empty;
			int				Total			= 0;
			int				Count			= 0;
			int				Affected		= 0;
			string			LinkID			= string.Empty;
			bool			Success			= false;

			try
			{
				// get a connection to the server
				Connection					= database.GetConnection("PageData");

//				// loop thru the links
//				SQLQuery					= "SELECT l.LinkGUID ";
//				SQLQuery					+= " , p.pagedata ";
//				SQLQuery					+= " FROM ";
//				_Database.GetServerAndDatabaseName("IndexedLinks");
//				SQLQuery					+= _Database.DatabaseName + "..IndexedLinks l, ";
//				_Database.GetServerAndDatabaseName("PageData");
//				SQLQuery					+= _Database.DatabaseName + "..PageData p ";
//				SQLQuery					+= " WHERE l.LinkGUID = p.LinkGUID  ";
//				//SQLQuery					+= " ORDER BY l.LinkGUID ";
//				Records						= _Database.RunSelectStatement(SQLQuery, "IndexedLinks");

//				while (_Records.Read() && StopProcessing == false)
				foreach(DataRow dr in _Records.Tables[0].Rows)
				{
					Total++;
					Count					= 0;
					LinkID					= string.Empty;
					PageData				= string.Empty;

					try
					{
						LinkID				= dr[0].ToString().Trim();

						// loop thru the links
						SQLQuery			= "SELECT p.pagedata ";
						SQLQuery			+= " FROM ";
//						_Database.GetServerAndDatabaseName("PageData");
//						SQLQuery			+= _Database.DatabaseName + "..PageData p ";
						SQLQuery			+= "PageData p ";
						SQLQuery			+= " WHERE p.LinkGUID = '" + LinkID + "'";
//						Records				= _Database.RunSelectStatement(SQLQuery, "PageData");
//		
//						while (Records.Read() && StopProcessing == false)
//						{
//							PageData		= Records[0].ToString().Trim().ToLower();
//						}

//						PageData			= _Database.FastSelectStatement(SQLQuery, "PageData");

                        // check to make sure our sql connection is still opened
                        if (Connection.State == ConnectionState.Closed)
                            Connection.Open();

						// run the sql select statement
						Command								= new SqlCommand(SQLQuery, Connection);
						//Command.CommandTimeout				= TIMEOUT;
						PageData							= Command.ExecuteScalar().ToString();
					}
					catch(Exception ex)
					{
						string error		= ex.Message;
						//StopProcessing		= true;
					}

                    if (!PageData.Equals(string.Empty))
                    {
                        // loop thru the page and count the number of times the word is found on the page
                        for (int ii = 0; ii < PageData.Length - Keyword.Length; ii++)
                        {
                            ii = PageData.IndexOf(Keyword, ii + Keyword.Length);

                            if (ii > -1)
                                Count++;
                            else
                                ii = PageData.Length;
                        }
                    }

					// save the keyword count
					if( Count > 0 )
					{
						Keyword				= Keyword.Replace("'", "");

						byte[] bArray		= AE.GetBytes(Keyword.Substring(0,1).ToUpper()); 
						int ascii			= int.Parse(bArray[0].ToString());

						if(ascii >= 65 && ascii <= 90)
							SQLQuery		= "EXEC sprSaveKeyWordCounts_" + Keyword.Substring(0,1) + " '" + Keyword + "','" + LinkID + "','" + Count + "' ";
						else
							SQLQuery		= "EXEC sprSaveKeyWordCounts '" + Keyword + "','" + LinkID + "','" + Count + "' ";

						Affected			= _Database.RunSQLStatement( SQLQuery, "IndexedLinks" );
					}

				}		// loop thru the links

				if(StopProcessing == false && Total > 0)
				{
					// success
					Success	= true;
				}
			}
			catch(Exception ex)
			{
				string error = ex.Message;
			}
			finally
			{
//				if(Records != null)
//					Records.Close();
//				Records		= null;

				Command								= null;

				if(Connection != null)
					Connection.Close();
				Connection							= null;
			}

			return Success;
		}		// AddNewKeyword

		#endregion		AddNewKeyword

	}
}
