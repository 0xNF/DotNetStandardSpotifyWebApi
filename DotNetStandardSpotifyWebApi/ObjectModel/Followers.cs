using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetStandardSpotifyWebApi.ObjectModel
{

    /// <summary>
    /// Information about the followers of the user. 
    /// </summary>
    public class Followers : SpotifyObjectModel, ISpotifyObject{
        /// <summary>
        /// A link to the Web API endpoint providing full details of the followers; null if not available. 
        /// Please note that this will always be set to null, as the Web API does not support it at the moment.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The total number of followers.
        /// </summary>
        public int Total { get; } = 0;


        public Followers(string href, int total) {
            this.Href = href;
            this.Total = total;
        }

        public Followers(int total) {
            this.Total = total;
        }

        public Followers(JToken token) {
            this.Total = token.Value<int?>("total") ?? -1;
            this.Href = token.Value<string>("href") ?? string.Empty;
        }
    }
}
