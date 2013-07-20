using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Zealocity;

namespace WebNewKeywordUpdater
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private		bool					StopProcessing			= false;
		private		String					_Keyword				= string.Empty;
		private		DataSet					_Records				= null;
		private		Zealocity.DatabaseClass	_Database				= new Zealocity.DatabaseClass();
		private		Zealocity.LoggingClass	_Logging				= new Zealocity.LoggingClass();

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
			this.Text = "Web New KeyWord Updater";
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
//			Message.Text				= "Start: " + DateTime.Now.ToString();
//			StopCrawl.Visible			= true;
//			StartCrawl.Enabled			= false;
//			StopProcessing				= false;
//
//			UpdateKeyWords();
//
//			StopProcessing				= false;
//			StartCrawl.Enabled			= true;
//			StopCrawl.Visible			= false;
//			Message.Text				+= "  - Stop: " + DateTime.Now.ToString();
//
//			Application.Exit();
		}		// StartCrawl_LinkClicked


		private void StopCrawl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			StopProcessing				= true;
		}		// StopCrawl_LinkClicked

		#endregion Events


		#region UpdateKeyWords

		private void UpdateKeyWords()
		{
//			System.Text.ASCIIEncoding AE	= new System.Text.ASCIIEncoding();
			SqlDataReader	Records			= null;
			string			PageData		= string.Empty;
//			string			DatabaseName	= string.Empty;
			string			SQLQuery		= string.Empty;
			string			KeyWord			= string.Empty;
			string[]		KeyWords;
			string			TotalLinks		= "0";
//			int				Total			= 0;
//			int				Count			= 0;
			int				Affected		= 0;
//			string			LinkID			= string.Empty;
			string			LastDate		= string.Empty;
			string			WorkingStatus	= string.Empty;
			string			ProcessRecords	= string.Empty;
			string	InsertSubmittedKeywords	= string.Empty;

			LastDate						= DateTime.Now.Subtract( TimeSpan.FromDays(1) ).ToString();
			//LastDate						= DateTime.Now.Subtract( TimeSpan.FromDays(4) ).ToString();

			LinkIDs.Text					= "";

			try
			{
				WorkingStatus				= System.Configuration.ConfigurationSettings.AppSettings.GetValues("WorkingStatus")[0];
				ProcessRecords				= System.Configuration.ConfigurationSettings.AppSettings.GetValues("ProcessRecords")[0];
				InsertSubmittedKeywords		= System.Configuration.ConfigurationSettings.AppSettings.GetValues("InsertSubmittedKeywords")[0];

				Message.Text				+= " - " + WorkingStatus;

				// write info out to a log file
				_Logging.LogMessage(Application.StartupPath, Application.ProductName, "Starting...");
				_Logging.LogMessage(Application.StartupPath, Application.ProductName, "Processing " + ProcessRecords + " " + WorkingStatus + " records.");

				#region Get the New Keywords

				if(InsertSubmittedKeywords.ToLower().Equals("true"))
				{
					// insert any/all submitted keywords
					SQLQuery					= " [sprInsertSubmittedKeywords] ";
					Affected					= _Database.RunSQLStatement(SQLQuery, "KeyWords");
				}

				// reset any previous runs
				SQLQuery					= " UPDATE KeyWords ";
				SQLQuery					+= " SET Indexed = 'N' ";
				SQLQuery					+= " WHERE Indexed = '" + WorkingStatus + "' ";
				Affected					= _Database.RunSQLStatement(SQLQuery, "KeyWords");

				// get the number of key words not processed
				SQLQuery					= " SELECT Count(Word) ";
				SQLQuery					+= " FROM KeyWords ";
				SQLQuery					+= " WHERE LEN(Word) > 1 ";
				//SQLQuery					+= " AND LastUpdated > '" + LastDate + "' ";
				SQLQuery					+= " AND Indexed = 'N' ";
				Records						= _Database.RunSelectStatement(SQLQuery, "KeyWords");

				while ( Records.Read() )
				{
					_Logging.LogMessage(Application.StartupPath, Application.ProductName, "Total new keywords not processed: " + Records[0].ToString().Trim());
				}

				Records.Close();
				Application.DoEvents();

				// set the new words to run
				SQLQuery					= "SET ROWCOUNT " + ProcessRecords;
				SQLQuery					+= " UPDATE KeyWords ";
				SQLQuery					+= " SET Indexed = '" + WorkingStatus + "' ";
				SQLQuery					+= " WHERE LEN(Word) > 1 AND Indexed = 'N' ";
				Affected					= _Database.RunSQLStatement(SQLQuery, "KeyWords");

				// get the new key words
				SQLQuery					= " SET ROWCOUNT 0 ";
				SQLQuery					+= " SELECT Word ";
				SQLQuery					+= " FROM KeyWords ";
				SQLQuery					+= " WHERE LEN(Word) > 1 ";
				//SQLQuery					+= " AND LastUpdated > '" + LastDate + "' ";
				SQLQuery					+= " AND Indexed = '" + WorkingStatus + "' ";
				Records						= _Database.RunSelectStatement(SQLQuery, "KeyWords");

				while ( Records.Read() )
				{
					KeyWord					+= Records[0].ToString().Trim().ToLower() + "|";
				}
				KeyWords					= KeyWord.Split('|');

				Records.Close();
				Application.DoEvents();


				// get the count of links to update
				SQLQuery					= "SELECT count(LastUpdated) ";
				SQLQuery					+= " FROM IndexedLinks ";
				Records						= _Database.RunSelectStatement(SQLQuery, "IndexedLinks");

				while ( Records.Read() )
				{
					TotalLinks				= Records[0].ToString();
				}
		
				Records.Close();
				Application.DoEvents();

				#endregion Get the New Keywords


				#region Update New Keywords
		
				// do we have any new keywords
				if( !KeyWord.Equals(string.Empty) )
				{
					SQLQuery					= "UPDATE STATISTICS PageData";
					Records						= _Database.RunSelectStatement(SQLQuery, "Links");

					SQLQuery					= "UPDATE STATISTICS IndexedLinks";
					Records						= _Database.RunSelectStatement(SQLQuery, "IndexedLinks");

					// loop thru the links
					SQLQuery					= "SELECT l.LinkGUID ";
//					SQLQuery					+= " , p.pagedata ";
					SQLQuery					+= " FROM ";
					_Database.GetServerAndDatabaseName("IndexedLinks");
					SQLQuery					+= _Database.DatabaseName + "..IndexedLinks l ";
//					_Database.GetServerAndDatabaseName("PageData");
//					SQLQuery					+= ", " + _Database.DatabaseName + "..PageData p ";
//					SQLQuery					+= " WHERE l.LinkGUID = p.LinkGUID  ";
					//SQLQuery					+= " ORDER BY l.LinkGUID ";
					_Records					= _Database.RunSelectStatement(SQLQuery, "IndexedLinks", "IndexedLinks");

					// loop thru the key words
					for(int i=0; i < KeyWords.Length - 1; i++)
					{
							_Keyword			= KeyWords[i];

							StartThread();

                            //Thread thread = new Thread( new ThreadStart( StartThread ) );
                            ////thread.IsBackground = true;
                            //thread.Start();

							//Thread.Sleep(1000);

////							while(thread.IsAlive)
////							{
////								// wait
////								Affected++;
////								Application.DoEvents();
////							}
//
////							while(thread.ThreadState != System.Threading.ThreadState.Running)
////							{
////								Application.DoEvents();
////							}

						Application.DoEvents();

					}		// loop thru the key words

				}		// do we have any new keywords

				#endregion Update New Keywords

			}
			catch(Exception ex)
			{
				string error				= ex.Message;

//				SQLQuery					= " UPDATE KeyWords ";
//				SQLQuery					+= " SET Indexed = 'N' ";
//				SQLQuery					+= " WHERE Indexed = '" + WorkingStatus + "' ";
//				Affected					= _Database.RunSQLStatement(SQLQuery, "KeyWords");
			}
			finally
			{
//				LinkIDs.Text				+= "  Clearing memory ...";
				Application.DoEvents();

//				if(Records != null)
//					Records.Close();
				Records						= null;

//				_Database					= null;
//				AE							= null;
			}

		}		// UpdateKeyWords

		#endregion UpdateKeyWords


		#region StartThread

		private void StartThread()
		{
			bool			Success			= false;
			int				Affected		= 0;
			string			Keyword			= _Keyword;
			string			SQLQuery		= string.Empty;
			string			WorkingStatus	= System.Configuration.ConfigurationSettings.AppSettings.GetValues("WorkingStatus")[0];

			NewKeyword		newKeyword		= new NewKeyword();
			
			newKeyword._Records				= this._Records;

			Success							= newKeyword.AddNewKeyword(Keyword);

			if(Success == true)
			{
				SQLQuery					= " UPDATE KeyWords ";
				SQLQuery					+= " SET Indexed = 'Y' ";
				//SQLQuery					+= " WHERE Indexed = '" + WorkingStatus + "' ";
				SQLQuery					+= " WHERE Word = '" + Keyword + "' ";
				Affected					= _Database.RunSQLStatement(SQLQuery, "KeyWords");

				// write out a log
				_Logging.LogMessage(Application.StartupPath, Application.ProductName, "Successfully processed " + Keyword);
			}
			else
			{
				SQLQuery					= " UPDATE KeyWords ";
				SQLQuery					+= " SET Indexed = 'N' ";
				//SQLQuery					+= " WHERE Indexed = '" + WorkingStatus + "' ";
				SQLQuery					+= " WHERE Word = '" + Keyword + "' ";
				Affected					= _Database.RunSQLStatement(SQLQuery, "KeyWords");

				// write out a log
				_Logging.LogMessage(Application.StartupPath, Application.ProductName, "Failed processing " + Keyword);
			}

		}		// StartThread

		#endregion StartThread

	}
}
