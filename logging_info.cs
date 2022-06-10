using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace raptor
{
	/// <summary>
	/// This class keeps track of who edited the file and for how long.
	/// </summary>
	[Serializable()]
	public class logging_info : ISerializable
	{
		public enum event_kind {Opened,Saved,Autosaved,
			Pasted_From,Paste_Opened,Paste_Saved,Paste_Autosaved};
		[Serializable()]
		public class event_record
		{
			public string Username;
			public string Machine_Name;
			public event_kind Kind;
			public System.DateTime Time;
			public event_record(string user, string machine, event_kind k, System.DateTime t)
			{
				this.Username = user;
				this.Machine_Name = machine;
				this.Kind = k;
				this.Time = t;
			}
		}

		private ArrayList events = new ArrayList();

		public logging_info()
		{
			this.Record_Open();
		}

        [DllImport("kernel32.dll")]
        private static extern long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, 
            UInt32 VolumeNameSize, ref UInt32 VolumeSerialNumber, ref UInt32 MaximumComponentLength, 
            ref UInt32 FileSystemFlags, StringBuilder FileSystemNameBuffer, UInt32 FileSystemNameSize);
        public string GetVolumeSerial(string strDriveLetter)
        {
            uint serNum = 0;
            uint maxCompLen = 0;
            StringBuilder VolLabel = new StringBuilder(256); // Label
            UInt32 VolFlags = new UInt32();
            StringBuilder FSName = new StringBuilder(256); // File System Name
            strDriveLetter += ":\\"; // fix up the passed-in drive letter for the API call
            long Ret = GetVolumeInformation(strDriveLetter, VolLabel, (UInt32)VolLabel.Capacity, ref serNum, ref maxCompLen, ref VolFlags, FSName, (UInt32)FSName.Capacity);

            return Convert.ToString(serNum);
        }

        public string GetMachineName()
        {
            string s = System.Environment.MachineName;
            try
            {
                if (s.CompareTo("MININT-JVC") == 0)
                {
                    return GetVolumeSerial("c");
                }
            }
            catch
            {
            }
            return System.Environment.MachineName;
        }

		public logging_info Clone()
		{
			logging_info result = new logging_info();
			result.events = (System.Collections.ArrayList) 
				this.events.Clone();
			return result;
		}

		public logging_info(SerializationInfo info, StreamingContext ctxt)
		{
			int count;
			int incoming_serialization_version;

			events.Clear();
			incoming_serialization_version = (int)info.GetValue("_serialization_version", typeof(int));
			count = (int)info.GetValue("_count", typeof(int));
			for (int i=0; i<count; i++)
			{
				string name,machine;
				System.DateTime t;
				event_kind k;
				name = info.GetString("_user"+i);
				machine = info.GetString("_machine"+i);
				t = info.GetDateTime("_date"+i);
                k = (event_kind)info.GetValue("_kind"+i,typeof(event_kind));
				events.Add(new event_record(name,machine,k,t));
			}
		}

		//Serialization function.
		public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
			info.AddValue("_serialization_version", Component.current_serialization_version);
			info.AddValue("_count", this.events.Count);
			for (int i=0; i<this.events.Count; i++)
			{
				info.AddValue("_user"+i, ((event_record) this.events[i]).Username);
				info.AddValue("_machine"+i, ((event_record) this.events[i]).Machine_Name);
				info.AddValue("_date"+i, ((event_record) this.events[i]).Time);
				info.AddValue("_kind"+i, ((event_record) this.events[i]).Kind);
			}
		}

		public void Record_Save()
		{
			this.events.Add(new event_record(System.Environment.UserName,
				System.Environment.MachineName,event_kind.Saved,System.DateTime.Now));
		}
		public void Record_Autosave()
		{
			this.events.Add(new event_record(System.Environment.UserName,
				System.Environment.MachineName,event_kind.Autosaved,System.DateTime.Now));
		}
		public void Record_Open(string name)
		{
			this.events.Add(new event_record(
				name,
				System.Environment.MachineName,
				event_kind.Opened,System.DateTime.Now));
		}
		public void Record_Open()
		{
			this.events.Add(new event_record(
				System.Environment.UserName,
				System.Environment.MachineName,
				event_kind.Opened,System.DateTime.Now));
		}

		public static bool New_Pair(string username,
			string machinename,
			logging_info log,
			int i)
		{
			for (int j=0; j<i; j++)
			{
				if ((((event_record) log.events[j]).Kind==event_kind.Paste_Saved) &&
					(((event_record) log.events[j]).Username==username) &&
					(((event_record) log.events[j]).Machine_Name==machinename))
				{
					return false;
				}
			}
			return true;
		}
		public static bool Last_Pair(string username,
			string machinename,
			logging_info log,
			int i)
		{
			for (int j=i+1; j<log.events.Count; j++)
			{
				if ((((event_record) log.events[j]).Kind==event_kind.Paste_Saved) &&
					(((event_record) log.events[j]).Username==username) &&
					(((event_record) log.events[j]).Machine_Name==machinename))
				{
					return false;
				}
			}
			return true;
		}
		public void Record_Paste(logging_info log,
			int count_symbols,
			System.Guid guid)
		{
			this.events.Add(new event_record(
				count_symbols.ToString(),
				guid.ToString(),
				event_kind.Pasted_From,
				System.DateTime.Now));
			// loop through the log, changing everything to paste_XXX
			for (int i=0; i<log.events.Count; i++)
			{
				event_record e = (event_record) log.events[i];
				switch (e.Kind)
				{
					case event_kind.Opened:
						e.Kind = event_kind.Paste_Opened;
						break;
					case event_kind.Saved:
						e.Kind = event_kind.Paste_Saved;
						break;
					case event_kind.Autosaved:
						e.Kind = event_kind.Paste_Autosaved;
						break;
				}
			}
			for (int i=0; i<log.events.Count; i++)
			{
				event_record e = (event_record) log.events[i];
				if ((e.Username!=System.Environment.UserName ||
					e.Machine_Name!=System.Environment.MachineName) &&
					e.Kind==event_kind.Paste_Saved &&
					(New_Pair(e.Username,e.Machine_Name,log,i) || 
					Last_Pair(e.Username,e.Machine_Name,log,i)))
				{
					this.events.Add(e);
				}
			}
		}

		public void Clear()
		{
			this.events.Clear();
		}

		public TimeSpan Compute_Total_Time()
		{
			System.TimeSpan ts = new System.TimeSpan(0);
			System.DateTime last_open = ((event_record) this.events[0]).Time;

			for (int i=0; i<this.events.Count; i++)
			{
				event_record e = (event_record) this.events[i];
				if (e.Kind==event_kind.Opened)
				{
					last_open = e.Time;
				}
				else 
				{
					// this is based on the principle that
					// only a save or autosave could be the last thing
					// in a file before an open
					if ((i < this.events.Count-1) &&
						((event_record) this.events[i+1]).Kind == event_kind.Opened)
					{
						ts = ts + e.Time.Subtract(last_open);
					}
				}
			}
		
			ts = ts + System.DateTime.Now.Subtract(last_open);
			return ts;
		}

		public int Count_Saves()
		{
			int result = 0;
			for (int i=0; i<this.events.Count; i++)
			{
				event_record e = (event_record) this.events[i];
				if (e.Kind==event_kind.Saved||
					e.Kind==event_kind.Autosaved)
				{
					result++;
				}
			}
			return result;
		}

		public string Display(System.Guid file_guid, bool show_full_log)
		{
			System.TimeSpan ts;
			int autosave_count = 0;
			int total_autosaves = 0;
			string log_data="";

            log_data = "LOG for: Raptor(" +
                file_guid + ")" + "\n";
            for (int i=0; i<this.events.Count; i++)
			{
				event_record e = (event_record) this.events[i];
				if (e.Kind==event_kind.Autosaved)
				{
					autosave_count++;
					total_autosaves++;
                    if (show_full_log)
                    {
                        log_data = log_data + e.Kind + " by: " + e.Username + " on: " +
                            e.Machine_Name + " at: " + e.Time + '\n';
                    }

				}
				else if (e.Kind==event_kind.Paste_Autosaved)
				{
				}
				else
				{
					if (autosave_count!=0)
					{
						//Runtime.consoleWriteln(
						//	autosave_count + " autosaves.");
						log_data = log_data +
							autosave_count + " autosaves.\n";
						autosave_count = 0;
					}
					if (e.Kind!=event_kind.Pasted_From)
					{
						//Runtime.consoleWriteln(e.Kind + " by: " + e.Username + " on: " +
						//	e.Machine_Name + " at: " + e.Time);
						log_data = log_data + e.Kind + " by: " + e.Username + " on: " +
						  	e.Machine_Name + " at: " + e.Time + '\n';
					}
					else
					{
						//Runtime.consoleWriteln(e.Kind + " " +
						//	e.Machine_Name + " " +
						//	e.Username + " symbols.");
						log_data = log_data + e.Kind + " " +
							e.Machine_Name + " " +
							e.Username + " symbols at: " + e.Time + '\n';
					}
				}
			}
			if (autosave_count!=0)
			{
				//Runtime.consoleWriteln(
				//	autosave_count + " autosaves.");
				log_data = log_data + autosave_count + " autosaves.\n";
			}
			ts = this.Compute_Total_Time();
			//Runtime.consoleWriteln("Total time (minutes): " + String.Format("{0:F2}",ts.TotalMinutes));
			log_data = log_data + "Total time (minutes): " + String.Format("{0:F2}",ts.TotalMinutes) + "\n";
			//Runtime.consoleWriteln("Total autosaves: " + total_autosaves);
			log_data = log_data + "Total autosaves: " +
				total_autosaves + "\n";
			//Runtime.consoleWriteln(log_data);
			return log_data;
		}

		public String Last_Username()
		{
			return ((event_record) this.events[this.events.Count-1]).Username;
		}

		public String Total_Minutes()
		{
			System.TimeSpan ts = this.Compute_Total_Time();
			return String.Format("{0:F2}",ts.TotalMinutes);
		}

		public String Second_Author()
		{
			string last_author = this.Last_Username();
			if (this.events.Count>1)
			{
				for (int i=this.events.Count-1; i>=0; i--)
				{
					if (((event_record) this.events[i]).Username.ToLower()!=
						last_author.ToLower() && ((event_record) this.events[i]).Kind!=event_kind.Pasted_From)
					{
						return ((event_record) this.events[i]).Username;
					}
				}
			}
			return "";
		}
		public String Other_Authors()
		{
			string last_author = this.Last_Username();
			string result = "";
			if (this.events.Count>1)
			{
				for (int i=this.events.Count-1; i>=0; i--)
				{
					if (((event_record) this.events[i]).Username.ToLower()!=last_author.ToLower()
						&& ((event_record) this.events[i]).Kind!=event_kind.Pasted_From)
					{
						last_author = ((event_record) this.events[i]).Username;
						if (result!="")
						{
							result = result + "&" + ((event_record) this.events[i]).Username;
						}
						else
						{
							result = ((event_record) this.events[i]).Username;
						}
					}
				}
			}
			return result;
		}
		public String All_Authors()
		{
			string last_author = this.Last_Username();
			string result = last_author;
			if (this.events.Count>1)
			{
				for (int i=this.events.Count-1; i>=0; i--)
				{
					if (((event_record) this.events[i]).Username.ToLower()!=last_author.ToLower()
						&& ((event_record) this.events[i]).Kind!=event_kind.Pasted_From)
					{
						last_author = ((event_record) this.events[i]).Username;
						result = result + "&" + ((event_record) this.events[i]).Username;
					}
				}
			}
			return result;
		}
	}
}
