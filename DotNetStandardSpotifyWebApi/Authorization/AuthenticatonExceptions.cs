using System;
using System.Runtime.Serialization;

namespace DotNetStandardSpotifyWebApi.Authorization
{

    public class DnsswapiException : Exception {

        public DnsswapiException() {
        }

        public DnsswapiException(string message) : base(message) {
        }

        public DnsswapiException(string message, Exception innerException) : base(message, innerException) {
        }
    }

    [Serializable]
    public class TokenRevokedException : DnsswapiException {


        public TokenRevokedException() {
        }

        public TokenRevokedException(string message) : base(message) {
        }

        public TokenRevokedException(string message, Exception innerException) : base(message, innerException) {
        }
    }

    [Serializable]
    public class RateLimitException : DnsswapiException {

        public double TryAgainInSeconds { get; } //5 minutes
        public DateTime TryAgainAt { get; }
        public string MinutesFromNow {
            get {
                TimeSpan ts = TryAgainAt.Subtract(DateTime.Now);
                string s = "";
                if(ts.Hours > 0) {
                    s += $"{ts.Hours} hour{(ts.Hours > 1 ? "s" : String.Empty)}{(ts.Minutes > 0 ? " " : String.Empty)}";
                } if (ts.Minutes > 0) {
                    s += $"{ts.Minutes} minutes";
                }
                return s;
            }
        }

        public RateLimitException() {
            TryAgainInSeconds = 5 * 60;
            TryAgainAt = DateTime.Now.AddSeconds(TryAgainInSeconds);
        }

        public RateLimitException(double tryAgainIn) {
            TryAgainInSeconds = tryAgainIn;
            TryAgainAt = DateTime.Now.AddSeconds(TryAgainInSeconds);
        }

        public RateLimitException(string message, double tryAgainIn) : base(message) {
            TryAgainInSeconds = tryAgainIn;
            TryAgainAt = DateTime.Now.AddSeconds(TryAgainInSeconds);
        }

        public RateLimitException(string message, double tryAgainIn, Exception innerException) : base(message, innerException) {
            TryAgainInSeconds = tryAgainIn;
            TryAgainAt = DateTime.Now.AddSeconds(TryAgainInSeconds);
        }
    }
}
