using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Zealocity;

namespace WebNewKeywordUpdater
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
			this.Text = "Web Search Duplicate Finder";
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

			FindDups();

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


		private void FindDups()
		{
			System.Text.ASCIIEncoding AE	= new System.Text.ASCIIEncoding();
			DatabaseClass	_Database		= new DatabaseClass();
			WebSearchingClass _WebSearchClass = new WebSearchingClass();
			SqlDataReader	Records			= null;
			string			SQLQuery		= string.Empty;
			string			Results			= string.Empty;
			string			TotalLinks		= "0";
			int				Start			= 0;
			int				Stop			= 0;
			int				Total			= 0;
			int				Affected		= 0;
			string			DomainID		= string.Empty;
			string			LinkID			= string.Empty;
			string			Title			= string.Empty;
			string			LinkURL			= string.Empty;
			string			LastDate		= string.Empty;
			bool			Success			= false;

//			string			WorkingStatus	= System.Configuration.ConfigurationSettings.AppSettings.GetValues("WorkingStatus")[0];
//			string			ProcessRecords	= System.Configuration.ConfigurationSettings.AppSettings.GetValues("ProcessRecords")[0];
//			Message.Text					+= " - " + WorkingStatus;

			LastDate						= DateTime.Now.Subtract( TimeSpan.FromDays(1) ).ToString();
			LinkIDs.Text					= string.Empty;

			try
			{
				#region Get the total

				// update statistics to increase speed
				SQLQuery					= "UPDATE STATISTICS Links";
				Affected					= _Database.RunSQLStatement(SQLQuery, "Links");

				// get the count of links to update
				SQLQuery					= "SELECT count(Enabled) ";
				//SQLQuery					+= " , p.pagedata ";
				SQLQuery					+= " FROM ";
				_Database.GetServerAndDatabaseName("Links");
				SQLQuery					+= _Database.DatabaseName + "..Links l ";
				//_Database.GetServerAndDatabaseName("PageData");
				//SQLQuery					+= ", " + _Database.DatabaseName + "..PageData p ";
				SQLQuery					+= " WHERE l.enabled = 'Y'  ";
				//SQLQuery					+= " AND l.LinkGUID = p.LinkGUID  ";
				//SQLQuery					+= " AND l.DomainGUID = '{09C95DAE-F313-432C-9A4C-F9A34DA37533}' ";
				SQLQuery					+= " AND l.DomainGUID NOT IN ('{A0489F7F-9698-435C-B48A-B54EB6D22439}','{429F844F-028C-4654-91A8-BE61F001DB5F}','{9DEFF49E-0B22-4A45-B7DC-5ED2E619574E}','{C3D88181-96DD-4CD0-8A2A-8F2CE898438B}') ";
				SQLQuery					+= " AND DuplicateChecked = 'N'  ";
				Records						= _Database.RunSelectStatement(SQLQuery, "Links");

				while ( Records.Read() )
				{
					TotalLinks				= Records[0].ToString();
				}
		
				Records.Close();
				Application.DoEvents();

				#endregion Get the total


				#region check for duplicates
		
				while( Records != null && StopProcessing == false )
				{
					// loop thru the links
					SQLQuery					= "SELECT top 1 l.DomainGUID, l.LinkGUID, l.Title, l.LinkURL ";
//					SQLQuery					+= " , p.pagedata ";
					SQLQuery					+= " FROM ";
					_Database.GetServerAndDatabaseName("Links");
					SQLQuery					+= _Database.DatabaseName + "..Links l ";
//					_Database.GetServerAndDatabaseName("PageData");
//					SQLQuery					+= ", " + _Database.DatabaseName + "..PageData p ";
					SQLQuery					+= " WHERE l.enabled = 'Y'  ";
//					SQLQuery					+= " AND l.LinkGUID = p.LinkGUID  ";
//					SQLQuery					+= " AND l.DomainGUID = '{09C95DAE-F313-432C-9A4C-F9A34DA37533}' ";
					SQLQuery					+= " AND l.DomainGUID NOT IN ('{A0489F7F-9698-435C-B48A-B54EB6D22439}','{429F844F-028C-4654-91A8-BE61F001DB5F}','{9DEFF49E-0B22-4A45-B7DC-5ED2E619574E}','{C3D88181-96DD-4CD0-8A2A-8F2CE898438B}') ";
					SQLQuery					+= " AND DuplicateChecked = 'N'  ";

					Records						= _Database.RunSelectStatement(SQLQuery, "Links");

					// do we have any links to process
					if( Records != null )
					{
						while (Records.Read() && StopProcessing == false)
						{
							Total++;
							DomainID					= Records[0].ToString().Trim();
							LinkID						= Records[1].ToString().Trim();
							Title						= Records[2].ToString().Trim();
							LinkURL						= Records[3].ToString().Trim().ToLower();
							Start						= LinkURL.LastIndexOf(@"/") + 1;
							Stop						= LinkURL.LastIndexOf(@".");
							if(Stop > Start)
                                LinkURL					= LinkURL.Substring(Start, Stop - Start);

							LinkIDs.Text				= "New Duplicate Links Processed:  " + Total + " of " + TotalLinks;
							Application.DoEvents();

							SQLQuery					= "sprUpdateLinkDuplicateCheck '" + LinkID + "'";
							Results						= _Database.FastSelectStatement(SQLQuery, "Links");
							Application.DoEvents();

							// is this link already being checked
							if(!Results.Equals(string.Empty))
							{
								Success					= _WebSearchClass.RemoveDuplicateWebPages(DomainID, LinkID, LinkURL, Title);

								if(Success)
									StopProcessing		= false;
								else
									StopProcessing		= true;

								Application.DoEvents();

								float check	= (float)Total / 1000;
								if(check == Math.Abs(Total / 1000))
								{
									// update statistics to increase speed
									SQLQuery				= "UPDATE STATISTICS Links";
									Affected				= _Database.RunSQLStatement(SQLQuery, "Links");
									Application.DoEvents();
								}
							}
							else
							{
								// this link is already being checked
								string test	= string.Empty;
							}		// is this link already being checked
						}		// loop thru the links

						Records.Close();
						Application.DoEvents();
					}		// do we have any links to process
				}

				#endregion check for duplicates


				if(StopProcessing == true)
				{
					// set this link to not checked for duplicates
					Affected			= _WebSearchClass.UpdateLinkStatus(LinkID, "N", "N");
				}

			}
			catch(Exception ex)
			{
				string error			= ex.Message;

				// set this link to not checked for duplicates
				Affected				= _WebSearchClass.UpdateLinkStatus(LinkID, "N", "N");
			}
			finally
			{
				LinkIDs.Text			+= "  Clearing memory ...";
				Application.DoEvents();

				if(Records != null)
					Records.Close();
				Records					= null;

				_WebSearchClass			= null;
				_Database				= null;
				AE						= null;
			}

		}		// FindDups

	}
}
