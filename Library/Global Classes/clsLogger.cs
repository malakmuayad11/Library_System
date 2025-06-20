using System;
using System.Diagnostics;

namespace Library
{
    public class clsLogger
    {
        /// <summary>
        /// This method loggs a message in the event viewer.
        /// </summary>
        /// <param name="Message">The message to be written in the event viewer.</param>
        /// <param name="Type">The type of the message in the event viewer.</param>
        public static void Log(string Message, EventLogEntryType Type)
        {
            if (!EventLog.Exists("Library"))
                EventLog.CreateEventSource("Library", "Application");
            EventLog.WriteEntry("Library", Message, Type);
        }
    }
}
