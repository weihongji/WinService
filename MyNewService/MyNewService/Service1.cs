using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyNewService
{
	partial class MyNewService : ServiceBase
	{
		private Timer timer;

		public MyNewService() {
			InitializeComponent();

			timer = new Timer(5000);
			timer.Elapsed += timer_Elapsed;

			var logSource = "MySource";
			var logName = "MyNewLog";
			if (!EventLog.SourceExists(logSource)) {
				EventLog.CreateEventSource(logSource, logName);
			}
			eventLog.Source = logSource;
			eventLog.Log = logName;

			//if (System.Diagnostics.EventLog.Exists(logName)) {
			//	System.Diagnostics.EventLog.Delete(logName);
			//}
		}

		protected override void OnStart(string[] args) {
			eventLog.WriteEntry("Service started", EventLogEntryType.Information);
			timer.Enabled = true;
		}

		protected override void OnStop() {
			eventLog.WriteEntry("Service stopped", EventLogEntryType.Information);
		}

		protected override void OnPause() {
			eventLog.WriteEntry("Service paused", EventLogEntryType.Information);
		}

		protected override void OnContinue() {
			eventLog.WriteEntry("Service continued", EventLogEntryType.Information);
		}

		protected override void OnShutdown() {
			eventLog.WriteEntry("Service shutdowned", EventLogEntryType.Information);
		}

		void timer_Elapsed(object sender, ElapsedEventArgs e) {
			eventLog.WriteEntry("Ticked at " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), EventLogEntryType.Information);
		}
	}
}
