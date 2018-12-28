using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Context : SpotifyObjectModel, ISpotifyObject {
        /// <summary>
        /// The object type, e.g. "artist", "playlist", "album".
        /// </summary>
        string Type { get; } = string.Empty;

        /// <summary>
        /// A link to the Web API endpoint providing full details of the track.
        /// </summary>
        string Href { get; } = string.Empty;

        /// <summary>
        /// External URLs for this context.
        /// </summary>
        Dictionary<string, string> External_Urls { get; } = new Dictionary<string, string>();

        /// <summary>
        /// The Spotify URI for the context.
        /// </summary>
        string Uri { get; } = string.Empty;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Context() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Context(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public Context(JToken token) {
            Type = token.Value<string>("type") ?? string.Empty;
            Uri = token.Value<string>("uri") ?? string.Empty;
            Href = token.Value<string>("href") ?? string.Empty;

            /* External Urls*/
            JObject exturls = token.Value<JObject>("external_urls");
            if (exturls != null) {
                foreach (JProperty x in exturls.Properties()) {
                    External_Urls.Add(x.Name, x.Value<JToken>().ToString());
                }
            }
        }

        /// <summary>
        /// Fields Constructor
        /// </summary>
        /// <param name="played_at"></param>
        /// <param name="context"></param>
        /// <param name="track"></param>
        public Context(string type, string href, string uri, Dictionary<string,string> external_urls) {
            Type = type;
            Href = href;
            Uri = uri;
            External_Urls = external_urls;
        }
    }

}
