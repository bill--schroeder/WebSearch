using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Zealocity;

namespace WebCrawler
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private		WebCrawler			_webCrawler			= null;
		private		DatabaseClass		_DatabaseClass		= new DatabaseClass();

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel StartCrawl;
		private System.Windows.Forms.TreeView Links;
		private System.Windows.Forms.ComboBox SearchURL;
		private System.Windows.Forms.LinkLabel StopCrawl;
		private System.Windows.Forms.Label Message;
		private System.Windows.Forms.CheckBox SaveData;
		private System.Windows.Forms.Label PageCount;
		private System.Windows.Forms.LinkLabel CrawlLinks;
		private System.Windows.Forms.CheckBox DuplicateCheck;
		private System.Windows.Forms.CheckBox SingleWebPageSearch;

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
			_webCrawler			= null;

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
			this.label1 = new System.Windows.Forms.Label();
			this.StartCrawl = new System.Windows.Forms.LinkLabel();
			this.Links = new System.Windows.Forms.TreeView();
			this.SearchURL = new System.Windows.Forms.ComboBox();
			this.StopCrawl = new System.Windows.Forms.LinkLabel();
			this.Message = new System.Windows.Forms.Label();
			this.SaveData = new System.Windows.Forms.CheckBox();
			this.PageCount = new System.Windows.Forms.Label();
			this.CrawlLinks = new System.Windows.Forms.LinkLabel();
			this.SingleWebPageSearch = new System.Windows.Forms.CheckBox();
			this.DuplicateCheck = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Search URL";
			// 
			// StartCrawl
			// 
			this.StartCrawl.AutoSize = true;
			this.StartCrawl.Location = new System.Drawing.Point(232, 56);
			this.StartCrawl.Name = "StartCrawl";
			this.StartCrawl.Size = new System.Drawing.Size(60, 16);
			this.StartCrawl.TabIndex = 2;
			this.StartCrawl.TabStop = true;
			this.StartCrawl.Text = "Start Crawl";
			this.StartCrawl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StartCrawl_LinkClicked);
			// 
			// Links
			// 
			this.Links.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.Links.ImageIndex = -1;
			this.Links.Location = new System.Drawing.Point(8, 144);
			this.Links.Name = "Links";
			this.Links.SelectedImageIndex = -1;
			this.Links.Size = new System.Drawing.Size(434, 208);
			this.Links.TabIndex = 3;
			// 
			// SearchURL
			// 
			this.SearchURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.SearchURL.Location = new System.Drawing.Point(8, 24);
			this.SearchURL.Name = "SearchURL";
			this.SearchURL.Size = new System.Drawing.Size(434, 21);
			this.SearchURL.TabIndex = 4;
			this.SearchURL.Text = "comboBox1";
			// 
			// StopCrawl
			// 
			this.StopCrawl.AutoSize = true;
			this.StopCrawl.Location = new System.Drawing.Point(232, 80);
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
			this.Message.Location = new System.Drawing.Point(8, 112);
			this.Message.Name = "Message";
			this.Message.Size = new System.Drawing.Size(434, 23);
			this.Message.TabIndex = 6;
			this.Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SaveData
			// 
			this.SaveData.Checked = true;
			this.SaveData.CheckState = System.Windows.Forms.CheckState.Checked;
			this.SaveData.Location = new System.Drawing.Point(16, 56);
			this.SaveData.Name = "SaveData";
			this.SaveData.Size = new System.Drawing.Size(80, 24);
			this.SaveData.TabIndex = 7;
			this.SaveData.Text = "Save Data";
			// 
			// PageCount
			// 
			this.PageCount.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.PageCount.Location = new System.Drawing.Point(104, 56);
			this.PageCount.Name = "PageCount";
			this.PageCount.Size = new System.Drawing.Size(72, 23);
			this.PageCount.TabIndex = 8;
			this.PageCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CrawlLinks
			// 
			this.CrawlLinks.Location = new System.Drawing.Point(320, 56);
			this.CrawlLinks.Name = "CrawlLinks";
			this.CrawlLinks.Size = new System.Drawing.Size(120, 16);
			this.CrawlLinks.TabIndex = 9;
			this.CrawlLinks.TabStop = true;
			this.CrawlLinks.Text = "Re-Crawl  Bad Links";
			this.CrawlLinks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CrawlLinks.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CrawlLinks_LinkClicked);
			// 
			// SingleWebPageSearch
			// 
			this.SingleWebPageSearch.Location = new System.Drawing.Point(16, 88);
			this.SingleWebPageSearch.Name = "SingleWebPageSearch";
			this.SingleWebPageSearch.Size = new System.Drawing.Size(160, 24);
			this.SingleWebPageSearch.TabIndex = 11;
			this.SingleWebPageSearch.Text = "Search a Single Web Page";
			// 
			// DuplicateCheck
			// 
			this.DuplicateCheck.Checked = true;
			this.DuplicateCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.DuplicateCheck.Location = new System.Drawing.Point(16, 72);
			this.DuplicateCheck.Name = "DuplicateCheck";
			this.DuplicateCheck.Size = new System.Drawing.Size(160, 24);
			this.DuplicateCheck.TabIndex = 12;
			this.DuplicateCheck.Text = "Check for Duplicates";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 358);
			this.Controls.Add(this.SingleWebPageSearch);
			this.Controls.Add(this.DuplicateCheck);
			this.Controls.Add(this.CrawlLinks);
			this.Controls.Add(this.PageCount);
			this.Controls.Add(this.SaveData);
			this.Controls.Add(this.StopCrawl);
			this.Controls.Add(this.StartCrawl);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Message);
			this.Controls.Add(this.SearchURL);
			this.Controls.Add(this.Links);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Web Crawler";
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


		#region MainForm_Load

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			SearchURL.Text		= string.Empty;
			SearchURL.Items.Clear();

			SearchURL.Items.Add( string.Empty );
//			SearchURL.Items.Add("http://www.springlakechurch.org");
//			SearchURL.Items.Add("http://directory.crosswalk.com/default.aspx");
//			SearchURL.Items.Add("http://directory.crosswalk.com/directory/Churches/default.aspx");
//			SearchURL.Items.Add("http://crosswalk.com/");
//			SearchURL.Items.Add("http://www.family.org/");
//			SearchURL.Items.Add("http://www.blueletterbible.org/");
//			SearchURL.Items.Add("http://bible1.crosswalk.com/");
//			SearchURL.Items.Add("http://www.schroedertechnologies.com/default.aspx");
//			SearchURL.Items.Add("http://www.kohls.com/main/home.jsp");
//			SearchURL.Items.Add("http://www.gap.com/asp/home_gap.html?wdid=0");

			DatabaseClass	_DatabaseClass		= new DatabaseClass();
			SqlDataReader	Records				= null;
			string			SQLQuery			= string.Empty;

			try
			{
				// loop thru the links
				SQLQuery						= "SELECT DomainLink ";
				SQLQuery						+= " FROM Domains ";
				SQLQuery						+= " WHERE Enabled = 'Y'  ";
				SQLQuery						+= " ORDER BY DomainLink ";
				Records							= _DatabaseClass.RunSelectStatement(SQLQuery, "Domains");

				while (Records.Read())
				{
					SearchURL.Items.Add( Records[0].ToString().Trim() + @"/" );
				}
			}
			catch(Exception ex)
			{
				string error					= ex.Message;
			}
			finally
			{
				if(Records != null)
					Records.Close();
				Records							= null;

				_DatabaseClass					= null;
			}

		}		// MainForm_Load

		#endregion MainForm_Load


		#region Web Form Events

		private void StartCrawl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Message.Text						= "Start: " + DateTime.Now.ToString();
			StopCrawl.Visible					= true;
			StartCrawl.Enabled					= false;
			CrawlLinks.Enabled					= false;

			string SQLQuery						= "UPDATE STATISTICS PageData";
			int Affected						= _DatabaseClass.RunSQLStatement(SQLQuery, "PageData");

			_webCrawler							= new WebCrawler(this, SaveData.Checked);
			_webCrawler.DuplicateCheck			= this.DuplicateCheck.Checked;
			_webCrawler.SingleWebPageSearch		= this.SingleWebPageSearch.Checked;
			_webCrawler.SearchWebSite(this.SearchURL.Text);

			StartCrawl.Enabled					= true;
			StopCrawl.Visible					= false;
			CrawlLinks.Enabled					= true;
			Message.Text						+= "  - Stop: " + DateTime.Now.ToString();
		}		// StartCrawl_LinkClicked


		private void StopCrawl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			_webCrawler.StopCrawl				= true;
		}		// StopCrawl_LinkClicked


		private void CrawlLinks_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			SqlDataReader	Records				= null;
			string			SQLQuery			= string.Empty;
			string			Return				= string.Empty;
			string			LinkID				= string.Empty;
			string			LinkURL				= string.Empty;
			string			LastDate			= string.Empty;
			int				Total				= 0;

			Message.Text						= "Start: " + DateTime.Now.ToString();
			StopCrawl.Visible					= true;
			StartCrawl.Enabled					= false;
			CrawlLinks.Enabled					= false;

			LastDate							= DateTime.Now.Subtract( TimeSpan.FromDays(1) ).ToString();

			try
			{
				_webCrawler						= new WebCrawler(this, true);

				SQLQuery						= "UPDATE STATISTICS links";
				int Affected					= _DatabaseClass.RunSQLStatement(SQLQuery, "Links");

				// loop thru the links
				if( this.SearchURL.Text.Trim().Equals( string.Empty ) )
				{
					SQLQuery						= "SELECT l.LinkGUID, l.LinkURL ";
					SQLQuery						+= " FROM Links l ";
					//SQLQuery						+= " WHERE l.Enabled = 'D'  ";
					//SQLQuery						+= " WHERE l.Enabled = 'E'  ";
					//SQLQuery						+= " WHERE l.Enabled = 'M'  ";
					//SQLQuery						+= " WHERE l.Enabled = 'N'  ";
					//SQLQuery						+= " WHERE l.Enabled = 'Y'  ";
					//SQLQuery						+= " WHERE l.Enabled NOT IN ('Y','M')  ";
					SQLQuery						+= " WHERE l.Enabled IN ('D','E','N')  ";
					////SQLQuery						+= " AND l.LastUpdated < '" + LastDate + "' ";
					//SQLQuery						+= " ORDER BY l.LinkGUID ";
				}
				else
				{
					SQLQuery						= "SELECT l.LinkGUID, l.LinkURL ";
					SQLQuery						+= " FROM Links l, Domains d ";
					SQLQuery						+= " WHERE l.Enabled in ('Y')  ";
					SQLQuery						+= " AND d.domainguid = l.domainguid  ";
				}

				Records							= _DatabaseClass.RunSelectStatement(SQLQuery, "Links");

				while (Records.Read() && _webCrawler.StopCrawl == false )
				{
					Total++;
					LinkID						= Records[0].ToString().Trim();
					LinkURL						= Records[1].ToString().Trim();

					PageCount.Text				= Total.ToString();
					Application.DoEvents();

					Return						= _webCrawler.CheckLink(LinkID, LinkURL);
				}
			}
			catch(Exception ex)
			{
				string error					= ex.Message;
			}
			finally
			{
				if(Records != null)
                    Records.Close();
				Records							= null;
			}

			StartCrawl.Enabled					= true;
			StopCrawl.Visible					= false;
			CrawlLinks.Enabled					= true;
			Message.Text						+= "  - Stop: " + DateTime.Now.ToString();

		}		// CrawlLinks_LinkClicked

		#endregion Web Form Events

	}
}
