using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Zealocity;

namespace WebKeyWordUpdater
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private		bool						StopProcessing				= false;

		private System.Windows.Forms.LinkLabel StartCrawl;
		private System.Windows.Forms.LinkLabel StopCrawl;
		private System.Windows.Forms.Label Message;
		private System.Windows.Forms.Label LinkIDs;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.StartCrawl = new System.Windows.Forms.LinkLabel();
			this.StopCrawl = new System.Windows.Forms.LinkLabel();
			this.Message = new System.Windows.Forms.Label();
			this.LinkIDs = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// StartCrawl
			// 
			this.StartCrawl.AutoSize = true;
			this.StartCrawl.Location = new System.Drawing.Point(120, 24);
			this.StartCrawl.Name = "StartCrawl";
			this.StartCrawl.Size = new System.Drawing.Size(60, 16);
			this.StartCrawl.TabIndex = 2;
			this.StartCrawl.TabStop = true;
			this.StartCrawl.Text = "Start Crawl";
			this.StartCrawl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StartCrawl_LinkClicked);
			// 
			// StopCrawl
			// 
			this.StopCrawl.AutoSize = true;
			this.StopCrawl.Location = new System.Drawing.Point(240, 24);
			this.StopCrawl.Name = "StopCrawl";
			this.StopCrawl.Size = new System.Drawing.Size(59, 16);
			this.StopCrawl.TabIndex = 5;
			this.StopCrawl.TabStop = true;
			this.StopCrawl.Text = "Stop Crawl";
			this.StopCrawl.Visible = false;
			this.StopCrawl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StopCrawl_LinkClicked);
			// 
			// Message
			// 
			this.Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.Message.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			this.Message.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Message.Location = new System.Drawing.Point(8, 72);
			this.Message.Name = "Message";
			this.Message.Size = new System.Drawing.Size(410, 23);
			this.Message.TabIndex = 6;
			this.Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LinkIDs
			// 
			this.LinkIDs.ForeColor = System.Drawing.Color.Maroon;
			this.LinkIDs.Location = new System.Drawing.Point(16, 120);
			this.LinkIDs.Name = "LinkIDs";
			this.LinkIDs.Size = new System.Drawing.Size(400, 23);
			this.LinkIDs.TabIndex = 7;
			this.LinkIDs.Text = "LinkIDs";
			this.LinkIDs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 174);
			this.Controls.Add(this.LinkIDs);
			this.Controls.Add(this.Message);
			this.Controls.Add(this.StopCrawl);
			this.Controls.Add(this.StartCrawl);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Web KeyWord Updater";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}


		#region Events

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			Message.Text				= "Start: " + DateTime.Now.ToString();
			StopCrawl.Visible			= true;
			StartCrawl.Enabled			= false;
			StopProcessing				= false;

			UpdateKeyWords();

			StopProcessing				= false;
			StartCrawl.Enabled			= true;
			StopCrawl.Visible			= false;
			Message.Text				+= "  - Stop: " + DateTime.Now.ToString();

			Application.Exit();
		}


		private void StartCrawl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Message.Text				= "Start: " + DateTime.Now.ToString();
			StopCrawl.Visible			= true;
			StartCrawl.Enabled			= false;
			StopProcessing				= false;

			UpdateKeyWords();

			StopProcessing				= false;
			StartCrawl.Enabled			= true;
			StopCrawl.Visible			= false;
			Message.Text				+= "  - Stop: " + DateTime.Now.ToString();

			Application.Exit();
		}		// StartCrawl_LinkClicked


		private void StopCrawl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			StopProcessing				= true;
		}		// StopCrawl_LinkClicked

		#endregion Events


		private void UpdateKeyWords()
		{
			System.Text.ASCIIEncoding AE	= new System.Text.ASCIIEncoding();
			DatabaseClass	_Database		= new DatabaseClass();
			SqlDataReader	Records			= null;
			SqlDataReader	Result			= null;
			string			PageData		= string.Empty;
			string			SQLQuery		= string.Empty;
			string			KeyWord			= "c#|vb|";
			string[]		KeyWords;
			string			TotalLinks		= "0";
			int				Total			= 0;
			int				Count			= 0;
			int				StatisticsCount	= 0;
			int				Affected		= 0;
			string			LinkID			= string.Empty;
			string			LastDate		= string.Empty;

			LastDate						= DateTime.Now.Subtract( TimeSpan.FromDays(1) ).ToString();

			LinkIDs.Text					= "";

			try
			{

				#region Get the Keywords

				// get all the key words
				SQLQuery					= "SELECT Word ";
				SQLQuery					+= " FROM KeyWords ";
				SQLQuery					+= " WHERE LEN(Word) > 2 ";
				Records						= _Database.RunSelectStatement(SQLQuery, "KeyWords");

				while ( Records.Read() )
				{
					KeyWord					+= Records[0].ToString().Trim().ToLower() + "|";
				}
				KeyWords					= KeyWord.Split('|');

				Records.Close();
				Application.DoEvents();


				// get the count of links to update
				SQLQuery					= "SELECT count(enabled) ";
				SQLQuery					+= " FROM Links l ";
				SQLQuery					+= " , PageData p  ";
				SQLQuery					+= " WHERE l.Enabled = 'Y'  ";
				SQLQuery					+= " AND  l.LinkGUID = p.LinkGUID  ";
				SQLQuery					+= " AND l.LinkGUID not in (SELECT k.LinkGuid FROM WebSearchIndex..IndexedLinks k WHERE k.LinkGuid = l.LinkGuid) ";
				TotalLinks					= _Database.FastSelectStatement(SQLQuery, "Links");
//				Records						= _Database.RunSelectStatement(SQLQuery, "Links");
//
//				while ( Records.Read() )
//				{
//					TotalLinks				= Records[0].ToString();
//				}
//				
//				Records.Close();
				Application.DoEvents();

				#endregion Get the Keywords


				#region Update New Links

				// loop thru the links
				LinkID						= "Zealocity";
				//while ( Records != null && StopProcessing == false )
				while ( !LinkID.Trim().Equals(string.Empty) && StopProcessing == false )
				{
					StatisticsCount++;
					if(StatisticsCount > 100)
					{
						SQLQuery					= "UPDATE STATISTICS Links";
						Affected					= _Database.RunSQLStatement(SQLQuery, "Links");

						SQLQuery					= "UPDATE STATISTICS IndexedLinks";
						Affected					= _Database.RunSQLStatement(SQLQuery, "IndexedLinks");

						StatisticsCount				= 0;
						Application.DoEvents();
					}

					// loop thru the links
					SQLQuery					= "SET ROWCOUNT 1 ";
					SQLQuery					+= " SELECT l.LinkGUID ";
					SQLQuery					+= " , p.pagedata ";
					SQLQuery					+= " FROM Links l ";
					SQLQuery					+= " , PageData p  ";
					//SQLQuery					+= " , WebSearchIndex..IndexedLinks k  ";
					SQLQuery					+= " WHERE l.Enabled = 'Y'  ";
					SQLQuery					+= " AND l.LinkGUID = p.LinkGUID  ";
					//SQLQuery					+= " AND l.LinkGUID = k.LinkGUID  ";
					//SQLQuery					+= " AND l.LinkGUID not in (SELECT top 1 k.LinkGuid FROM WebSearchIndex..IndexedLinks k WHERE k.LinkGuid = l.LinkGuid) ";
					SQLQuery					+= " AND l.LinkGUID not in (SELECT k.LinkGuid FROM WebSearchIndex..IndexedLinks k) ";
					//SQLQuery					+= " ORDER BY l.LinkGUID ";
					Records						= _Database.RunSelectStatement(SQLQuery, "Links");

					// do we have any links to process
					if( Records != null )
					{
						LinkID						= string.Empty;

//						while (Records.Read())
//						{
//							LinkID					= Records["LinkGUID"].ToString().Trim();
//						}
//						Records.Close();
//
//						SQLQuery					= "SELECT top 1 p.LinkGUID, p.pagedata ";
//						SQLQuery					+= " FROM PageData p  ";
//						SQLQuery					+= " WHERE p.LinkGUID = '" + LinkID + "'";
//						Records						= _Database.RunSelectStatement(SQLQuery, "PageData");

						Application.DoEvents();

						while (Records.Read() && StopProcessing == false)
						{
							LinkID					= Records[0].ToString().Trim();
							PageData				= Records[1].ToString().Trim().ToLower();

							// add an entry letting us know this link is being indexed
							//SQLQuery				= "EXEC sprUpdateIndexedLink '" + LinkID + "', 'Y' ";
							SQLQuery				= "EXEC sprSaveIndexedLink '" + LinkID + "' ";
							Result					= _Database.RunSelectStatement( SQLQuery, "IndexedLinks" );

							// is this an unprocessed link
							while ( !Result.Read() )
							{
								Total++;

								LinkIDs.Text			= "New Links Processed:  " + Total + " of " + TotalLinks;
								Application.DoEvents();

								// initialize the ClickThruCounts tabls
								SQLQuery				= "EXEC sprAddClickThruCounts '" + LinkID + "' ";
								Affected				= _Database.RunSQLStatement( SQLQuery, "ClickThruCounts" );

								// loop thru the key words
								for(int i=0; i < KeyWords.Length - 1 && StopProcessing == false; i++)
								{
									Count				= 0;

									for(int ii=0; ii < PageData.Length - KeyWords[i].Length; ii++)
									{
										ii				= PageData.IndexOf(KeyWords[i], ii + KeyWords[i].Length);

										if( ii > -1 )
											Count++;
										else
											ii			= PageData.Length;
									}

									// save the keyword count
									if( Count > 0 )
									{
										KeyWords[i]		= KeyWords[i].Replace("'", "");

										byte[] bArray	= AE.GetBytes(KeyWords[i].Substring(0,1).ToUpper()); 
										int ascii		= int.Parse(bArray[0].ToString());

										if(ascii >= 65 && ascii <= 90)
											SQLQuery	= "EXEC sprSaveKeyWordCounts_" + KeyWords[i].Substring(0,1) + " '" + KeyWords[i] + "','" + LinkID + "','" + Count + "' ";
										else
											SQLQuery	= "EXEC sprSaveKeyWordCounts '" + KeyWords[i] + "','" + LinkID + "','" + Count + "' ";

										Affected		= _Database.RunSQLStatement( SQLQuery, "IndexedLinks" );
									}

									if(Math.Abs(i / 1000) == (float)i/1000)
                                        Application.DoEvents();
								}		// loop thru the key words

								if(StopProcessing == false)
								{
									// add an entry letting us know this link has been indexed
									SQLQuery			= "EXEC sprUpdateIndexedLink '" + LinkID + "', 'Y' ";
									Affected			= _Database.RunSQLStatement( SQLQuery, "IndexedLinks" );
								}
								else
								{
									SQLQuery			= "DELETE FROM IndexedLinks WHERE LinkGUID = '" + LinkID + "'" ;
									Affected			= _Database.RunSQLStatement(SQLQuery, "IndexedLinks");
								}

								break;
							}		// is this an unprocessed link

							Result.Close();
						}		// loop thru the links

//						Application.DoEvents();
					}		// do we have any links to process

					Records.Close();
				}		// loop thru the links

				#endregion Update New Links

			}
			catch(Exception ex)
			{
				string error				= ex.Message;
				LinkIDs.Text				= error;

				try
				{
					SQLQuery				= "DELETE FROM IndexedLinks WHERE LinkGUID = '" + LinkID + "'" ;
					Affected				= _Database.RunSQLStatement(SQLQuery, "IndexedLinks");
				}
				catch
				{
				}
			}
			finally
			{
				LinkIDs.Text				= "Clearing memory";
				Application.DoEvents();

				if(Result != null)
					Result.Close();
				Result						= null;

				if(Records != null)
					Records.Close();
				Records						= null;

				_Database					= null;
				AE							= null;
			}

		}		// UpdateKeyWords

	}
}
