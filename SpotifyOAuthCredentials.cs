using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetStandardSpotifyWebApi
{
    public sealed class SpotifyOAuthCredentials {
        public string userId { get; }
        public string access_token { get; }
        public int expires_in { get; }
        public string refresh_token { get; }

        public bool WasError { get; }


        public SpotifyOAuthCredentials(string userid, string token, int expires, string refresh) {
            this.userId = userid;
            this.access_token = token;
            this.expires_in = expires;
            this.refresh_token = refresh;
            this.WasError = false;
        }

        public SpotifyOAuthCredentials() {
            this.userId = "Error";
            this.access_token = "error";
            this.expires_in = -1;
            this.refresh_token = "error";
            this.WasError = true;
        }
    }
}
