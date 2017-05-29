using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetStandardSpotifyWebApi.Helpers
{
    /// <summary>
    /// Holds authenticated OAuth2 credentials, including an access token, refresh token, expiry time, and whether the response was an error.
    /// </summary>
    public sealed class OAuthCredentials {
        /// <summary>
        /// Optional UserId for services that provide user ids
        /// </summary>
        public string UserId { get; } = string.Empty;
        /// <summary>
        /// OAuth2 Access Token
        /// Typically expires after a set period time, defined by Expires_in
        /// </summary>
        public string Access_token { get; } = string.Empty;
        /// <summary>
        /// Time in seconds that an OAuth Access Token expires
        /// </summary>
        public int Expires_in { get; } = -1;
        /// <summary>
        /// Refresh token that can be used to obtain a new Access Token. 
        /// Often has no expiry
        /// </summary>
        public string Refresh_token { get; } = string.Empty;

        /// <summary>
        /// True if the server response was an error
        /// </summary>
        public bool WasError { get; private set; } = true;

        /// <summary>
        /// Error class - use if the server returned invalid credentials
        /// </summary>
        public static OAuthCredentials CrendentialError { get; } = new OAuthCredentials("Error", "Error", -1, "Error") { WasError = true };

        public OAuthCredentials(string userid, string token, int expires, string refresh) {
            this.UserId = userid;
            this.Access_token = token;
            this.Expires_in = expires;
            this.Refresh_token = refresh;
            this.WasError = false;
        }

        public OAuthCredentials() {

        }
    }
}
