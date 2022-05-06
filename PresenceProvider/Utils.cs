﻿using System;
using System.Diagnostics;

namespace OutlookPresenceProvider
{
    public class Utils
    {
        public static void LogException(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            Trace.TraceError(ex.Message);
            Trace.TraceError(ex.StackTrace);
        }
    }
}
