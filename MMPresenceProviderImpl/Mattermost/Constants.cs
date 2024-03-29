﻿using UCCollaborationLib;

namespace MMPresenceProviderImpl.Mattermost
{
    public class Constants
    {
        public const string MattermostServerURL = "MattermostServerURL";
        public const string MattermostSecret = "MattermostSecret";
        public const string MattermostWebsocketTimeout = "MattermostWebsocketReconnectionTimeoutInSeconds";
        public const double MattermostDefaultWebsocketTimeout = 30;
        public const string MattermostPluginId = "com.mattermost.outlook-presence";
        public const string MattermostRequestParamSecret = "secret";
        public const string MattermostRequestParamPage = "page";
        public const string MattermostEmail = "email";
        public const string MattermostStatus = "status";

        public static ContactAvailability StatusAvailabilityMap(string status)
        {
            switch (status)
            {
                case "online": return ContactAvailability.ucAvailabilityFree;
                case "away": return ContactAvailability.ucAvailabilityAway;
                case "dnd": return ContactAvailability.ucAvailabilityDoNotDisturb;
                case "offline": return ContactAvailability.ucAvailabilityOffline;
            }
            return ContactAvailability.ucAvailabilityNone;
        }

        public static string AvailabilityActivityIdMap(ContactAvailability availability)
        {
            switch (availability)
            {
                case ContactAvailability.ucAvailabilityFree: return "Free";
                case ContactAvailability.ucAvailabilityAway: return "Away";
                case ContactAvailability.ucAvailabilityDoNotDisturb: return "DoNotDisturb";
                case ContactAvailability.ucAvailabilityOffline: return "Offline";
            }
            return string.Empty;
        }
    }
}
