using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Zealocity;

namespace WebCrawler
{
	/// <summary>
	/// Summary description for WebCrawler.
	/// </summary>
	public class WebCrawler
	{
		private		System.Windows.Forms.Label		PageCount;
		private		System.Windows.Forms.TreeView	Links;


		#region Member Variables

		private		WebSearchingClass				_WebSearchingClass;
		private		Internet						_Internet;
		private		TreeNode						_TreeNode;
		private		Hashtable						_Links;
		private		DatabaseClass					_Database;
		private		LoggingClass					_Logging;
		private		bool							_SaveData;
		private		int								_PageCount;

		private		string							_DomainNameSuffix			= string.Empty;
		private		string							_DomainName					= string.Empty;
		private		string							_DomainGUID					= string.Empty;
		
		public		bool							StopCrawl;
		public		bool							DuplicateCheck;
		public		bool							SingleWebPageSearch;

		#endregion Member Variables


		#region Constructor

		public WebCrawler(MainForm mainForm, bool SaveData)
		{
			_WebSearchingClass			= new WebSearchingClass();
			_Logging					= new LoggingClass();
			_Internet					= new Internet();
			_SaveData					= SaveData;
			_Database					= new DatabaseClass();
			string ControlName			= string.Empty;

			// search thru the forms control and find the parent control
			ControlName					= "Links";
			for (int i = 0; i < mainForm.Controls.Count; i++)
			{
				if (mainForm.Controls[i].Name.ToUpper() == ControlName.ToUpper())
				{
					// update the value of the control
					Links				= (TreeView)mainForm.Controls[i];
				}
			}		// search thru the forms control and find the parent control

			ControlName					= "PageCount";
			for (int i = 0; i < mainForm.Controls.Count; i++)
			{
				if (mainForm.Controls[i].Name.ToUpper() == ControlName.ToUpper())
				{
					// update the value of the control
					PageCount			= (Label)mainForm.Controls[i];
				}
			}		// search thru the forms control and find the parent control
		}

		#endregion Constructor


		#region Private Methods

		private bool DoesLinkAlreadyExist(TreeNode treeNode, string Link)
		{
			bool	Return				= false;
			string	LinkName			= string.Empty;
			Link						= Link.ToUpper();

			try
			{
//				// does this link already exist
//				if( !_Links.Contains(Link) )
//				{
				_Links.Add(Link, Link);

//				if(treeNode != null)
//				{
//					if(treeNode.Nodes.Count > 0)
//					{
//						for(int p=0; p < treeNode.Nodes.Count; p++)
//						{
//							TreeNode node			= treeNode.Nodes[p];
//
//							if( node.Text.ToUpper().IndexOf(Link) > -1 )
//							{
//								Return				= true;
//							}
//							else
//							{
//								// loop thru all of the links
//								for(int i=0; i < node.Nodes.Count; i++)
//								{
//									LinkName		= node.Nodes[i].Text.ToString().ToUpper();
//
//									//							if( Link.EndsWith(LinkName) )
//									//								Return		= true;
//
//									if( Link.IndexOf(LinkName) > -1 )
//										Return		= true;
//
//									//							if( Link.Equals(LinkName) )
//									//								Return		= true;
//								}		// loop thru all of the tables
//							}
//						}
//					}
//
//					if(Return == false)
//					{
//						if(_TreeNode.Nodes.Count > 0)
//						{
//							for(int p=0; p < this._TreeNode.Nodes.Count; p++)
//							{
//								TreeNode node		= this._TreeNode.Nodes[p];
//
//								if( node.Text.ToUpper().IndexOf(Link) > -1 )
//								{
//									Return			= true;
//								}
//								else
//								{
//									// loop thru all of the links
//									for(int i=0; i < node.Nodes.Count; i++)
//									{
//										LinkName	= node.Nodes[i].Text.ToString().ToUpper();
//
//										//								if( Link.EndsWith(LinkName) )
//										//									Return	= true;
//
//										if( Link.IndexOf(LinkName) > -1 )
//											Return	= true;
//
//										//								if( Link.Equals(LinkName) )
//										//									Return	= true;
//									}		// loop thru all of the tables
//								}
//							}
//						}
//					}
//				}
//				}		// does this link already exist
			}
			catch( StackOverflowException ex )
			{
				string error				= ex.Message;
				Return						= true;
			}			
			catch(Exception ex)
			{
				string error				= ex.Message;
				Return						= true;
			}
			finally
			{
				treeNode					= null;
			}

			return Return;
		}		// DoesLinkAlreadyExist

		#endregion Private Methods


		#region CheckLink

		public string CheckLink(string LinkID, string LinkURL)
		{
			string			Return				= string.Empty;
			string			Title				= string.Empty;
			int				Start				= 0;
			int				Stop				= 0;
			bool			Success;
			bool			Valid;

			try
			{
				// is this a valid URL
				if( _WebSearchingClass.ValidateURL(LinkURL) )
				{
					// should we save to the database
					if( LinkID.Equals(string.Empty) && _SaveData == true )
						LinkID					= _WebSearchingClass.SaveLink(_DomainGUID, LinkURL, string.Empty, "N");

					Return						= _Internet.PostData(LinkURL, string.Empty);
			
					Application.DoEvents();

					// did we receive an error retreiving the url
					if( Return.ToUpper().StartsWith("ERROR") )
					{
						// we had an error
						_WebSearchingClass.UpdateLink(LinkID, string.Empty, "E");
					}
					else if( Return.Trim().Equals(string.Empty) )
					{
						// we found an empty page
						_WebSearchingClass.UpdateLink(LinkID, string.Empty, "D");
					}
					else
					{
						// should we save to the database
						if( LinkID.Equals(string.Empty) || _SaveData == false )
						{
							// we had an error saving the link
						}
						else
						{
							if(Return.Length > 7)
							{
								// save the page data
								Valid				= _WebSearchingClass.SavePage(LinkID, Return);

								// save the link title
								Start				= Return.ToUpper().IndexOf("<TITLE>") + 7 ;
								Stop				= Return.ToUpper().IndexOf("</TITLE>", Start);
								if(Start < 7 || Stop < 0)
									Title			= string.Empty;
								else
									Title			= Return.Substring(Start, Stop - Start).Trim();

								// is the title blank
								if( !Title.Trim().Equals(string.Empty) )
								{
									// is the page not found
									if( Title.IndexOf("Page Not Found") < 0 || Title.IndexOf("File Not Found") < 0 || Title.IndexOf("Document Not Found") < 0 || Title.IndexOf("404 Error") < 0 || Title.IndexOf("Page Cannot Be Found") < 0 )
									{
										// does the page contain "M"ature content
										if(Valid == true)
										{
											// NOPE, YAHOO!
											_WebSearchingClass.UpdateLink(LinkID, Title, "Y");

											_WebSearchingClass.UpdateLinkStatus(LinkID, "Y", "Y");

											if(this.DuplicateCheck)
											{
												// now very there aren't any duplicates for this page
												LinkURL				= LinkURL.ToLower();
												Start				= LinkURL.LastIndexOf(@"/") + 1;
												Stop				= LinkURL.LastIndexOf(@".");
												if(Stop > Start)
													LinkURL			= LinkURL.Substring(Start, Stop - Start);
												Success				= _WebSearchingClass.RemoveDuplicateWebPages(this._DomainGUID, LinkID, LinkURL, Title);
											}
										}
										else
											_WebSearchingClass.UpdateLink(LinkID, Title, "M");
									}
									else
									{
										// this page was not found
										_WebSearchingClass.UpdateLink(LinkID, Title, "D");
									}		// is the page not found
								}		// is the title blank
							}
						}
					}		// did we receive an error retreiving the url

				}		// is this a valid URL
			}
			catch(Exception ex)
			{
				string error					= ex.Message;
			}
			finally
			{
			}

			return Return;
		}		// CheckLink

		#endregion CheckLink


		#region CrawlWebSite

		private void CrawlWebSite(TreeNode ParentNode, string URL)
		{
			TreeNode treeNode						= new TreeNode(URL);

			string Return							= string.Empty;
			string Temp								= string.Empty;
			string Title							= string.Empty;
			int Index								= 0;
			int FindFrameLinkStart					= 0;
			int FindLinkStart						= 1;
			int FindLinkStop						= 0;
			string LinkID							= string.Empty;
			//			int Start								= 0;
			//			int Stop								= 0;
			//			bool Valid								= false;

			_PageCount++;
			PageCount.Text							= _PageCount.ToString();
			Application.DoEvents();

			if(StopCrawl == false)
			{

				Return								= CheckLink(string.Empty, URL);

				//				// is this a valid URL
				//				if( _WebSearchingClass.ValidateURL(URL) )
				//				{
				//					// should we save to the database
				//					if( _SaveData == true )
				//					{
				//						LinkID						= _WebSearchingClass.SaveLink(URL, string.Empty, "N");
				//					}		// should we save to the database
				//
				//					Return							= _Internet.PostData(URL, string.Empty);
				
				if(this.SingleWebPageSearch)
				{
					// let's stop here, since we are only searching this single web page
				}
				// did we receive an error retreiving the url
				else if( Return.ToUpper().StartsWith("ERROR") )
				{
					// we had an error
					string Error				= Return;
				}
				else if( Return.Trim().Equals(string.Empty) )
				{
					string Error				= "yikes";
				}
				else if( Return.Length > 7 )
				{
					Temp						= Return.ToUpper();
					int PageLength				= Return.Length;

					//						_Logging.LogMessage(Application.StartupPath, Application.ProductName, Temp);

					for(int i=0; i < 3; i++)
					{
						Index					= URL.IndexOf(@"/", Index) + 1;
					}
					URL							= URL.Substring(0, Index);

					// loop thru the web page and search for links
					while(FindLinkStart > 0 & StopCrawl == false)
					{
						// find the beginning of the link
						FindLinkStart			= Temp.IndexOf("HREF=", FindLinkStart) + 6;

						if(FindLinkStart < FindLinkStop)
							FindLinkStart		= 0;

						try
						{
							if( Temp.Length < FindLinkStart )
							{
								if( Temp.Substring(FindLinkStart, 1).Equals(@"/") )
									FindLinkStart++;
							}
						}
						catch(Exception ex)
						{
							string error		= ex.Message;
						}

						if(FindLinkStart > 0 || FindFrameLinkStart > 0)
						{
							// find the end of the link
							FindLinkStop		= Temp.IndexOf("</A>", FindLinkStart);

							//							if(Temp.IndexOf("?", FindLinkStart) < FindLinkStop)
							//								FindLinkStop	= Temp.IndexOf("?", FindLinkStart) - 0;

							//							if(Temp.IndexOf(" ", FindLinkStart) < FindLinkStop)
							//								FindLinkStop	= Temp.IndexOf(" ", FindLinkStart) - 1;

							if(Temp.IndexOf("'", FindLinkStart + 4) < FindLinkStop && Temp.IndexOf("'", FindLinkStart + 4) > 0)
								FindLinkStop	= Temp.IndexOf("'", FindLinkStart + 4);

							if(Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 4) < FindLinkStop && Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 4) > 0)
								FindLinkStop	= Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 4);

							if(Temp.IndexOf(">", FindLinkStart) < FindLinkStop && Temp.IndexOf(">", FindLinkStart) > 0)
								FindLinkStop	= Temp.IndexOf(">", FindLinkStart) - 1;

							if( FindLinkStop < 1 )
							{
								FindFrameLinkStart	= Temp.IndexOf("<FRAME ", FindFrameLinkStart);
									
								if( FindFrameLinkStart > 0 )
								{
									FindLinkStart	= Temp.IndexOf("SRC=", FindFrameLinkStart) + 5;
									FindLinkStop	= Temp.IndexOf(">", FindLinkStart) - 1;

									if(Temp.IndexOf("'", FindLinkStart + 1) < FindLinkStop && Temp.IndexOf("'", FindLinkStart + 1) > 0)
										FindLinkStop	= Temp.IndexOf("'", FindLinkStart + 1);

									if(Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 1) < FindLinkStop && Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 1) > 0)
										FindLinkStop	= Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 1);

									if(Temp.IndexOf(">", FindLinkStart) < FindLinkStop && Temp.IndexOf(">", FindLinkStart) > 0)
										FindLinkStop	= Temp.IndexOf(">", FindLinkStart) - 1;

									FindFrameLinkStart	= FindLinkStart;
								}
							}

							if(FindLinkStop < FindLinkStart)
								FindLinkStop	= 0;

							// did we find the end of the link
							if( FindLinkStop > 0 )
							{
								// what is the link
								string Link		= Return.Substring(FindLinkStart, FindLinkStop - FindLinkStart).Trim();

								int tempStart	= FindLinkStart - 10;
								if( tempStart < 0 )
									tempStart =	0;
								int tempStop	= FindLinkStop + 10;
								if( tempStart > PageLength )
									tempStart =	PageLength;
								string temp		= Return.Substring(tempStart, tempStop - tempStart);

								// is this a script opening a page
								if( Link.ToUpper().IndexOf("SCRIPT") > -1 )
								{
									int FindScriptLinkStart		= Link.ToUpper().IndexOf("OPEN");
									if( FindScriptLinkStart > -1 )
									{
										FindLinkStart			= FindLinkStop + 1;

										if(Temp.IndexOf("'", FindLinkStart + 1) < PageLength && Temp.IndexOf("'", FindLinkStart + 1) > 0)
											FindLinkStop		= Temp.IndexOf("'", FindLinkStart + 1);

										if(Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 1) < FindLinkStop && Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 1) > 0)
										{
											//												FindLinkStop		= Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 1) + 50;
											FindLinkStart		= Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 1);
											FindLinkStop		= Temp.IndexOf(Convert.ToChar(34).ToString(), FindLinkStart + 1) + 100;

											FindLinkStop		= FindLinkStart;
										}

										Link					= Return.Substring(FindLinkStart, FindLinkStop - FindLinkStart).Trim();
									}
								}		// is this a script opening a page

								if( !Link.ToUpper().StartsWith("HTTP") )
									Link	= URL + Link;

								//if( Link.ToUpper().StartsWith("HTTP") )
								if( Link.ToUpper().StartsWith("HTTP") && !Link.ToUpper().StartsWith(_DomainName) && Link.ToUpper().IndexOf(_DomainNameSuffix) < 0 )
								{
									// should we save to the database
									if( _SaveData == true )
									{
										Index						= 0;
										try
										{
											for(int i=0; i < 3; i++)
											{
												Index				= Link.IndexOf(@"/", Index) + 1;
											}
											Link					= Link.Substring(0, Index - 1);
										}
										catch(Exception ex)
										{
											// ignore error
											string error			= ex.Message;
										}
										_WebSearchingClass.SubmitLink(_DomainGUID, Link);
									}		// should we save to the database
								}
									// do we have a proper link
									//									else if( _WebSearchingClass.ValidateURL(URL + Link) )
								else if( _WebSearchingClass.ValidateURL(Link) )
								{
									// does the link already exist
									if( !DoesLinkAlreadyExist(treeNode, Link) )
									{
										TreeNode node	= new TreeNode(Link);
										treeNode.Nodes.Add(node);

										if( !Link.ToUpper().StartsWith("HTTP") )
											CrawlWebSite(treeNode, URL + Link);
										else
											CrawlWebSite(treeNode, Link);
									}
								}		// do we have a proper link
							}
							else
							{
								FindLinkStop	= 0;
							}
                    
							FindLinkStart		= FindLinkStop;
						}

						Application.DoEvents();
					}		// loop thru the web page and search for links

				}		// did we receive an error retreiving the url
				else
				{
					string Error				= "yikes";
				}

				_TreeNode.Nodes.Add(treeNode);

				//				}		// is this a valid URL
			}

			treeNode					= null;

			Application.DoEvents();
		}		// CrawlWebSite

		#endregion CrawlWebSite


		#region SearchWebSite
		public void SearchWebSite(string URL)
		{
			Links.Nodes.Clear();
			_Links							= new Hashtable();
			StopCrawl						= false;
			_PageCount						= 0;

			Application.DoEvents();

			// save this domain name
			int Index						= 0;
			for(int i=0; i < 3; i++)
			{
				Index						= URL.IndexOf(@"/", Index) + 1;
			}
			_DomainName						= URL.Substring(0, Index).ToUpper();

			Index							= _DomainName.IndexOf(@".", 0) + 1;
			_DomainNameSuffix				= _DomainName.Substring(Index, _DomainName.Length - Index);

			// save this domain
			_DomainGUID						= _WebSearchingClass.SaveDomain(URL);

			// initialize the tree for this doamin
			_TreeNode						= new TreeNode(URL);

			if( !_DomainGUID.Equals(string.Empty) )
			{
				// crawl this domain
				CrawlWebSite(_TreeNode, URL.Trim());
			}

			Links.Nodes.Add( _TreeNode );

			_Links							= null;
		}
		#endregion SearchWebSite

	}
}
