using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class TrackLink : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// Known external URLs for this track.
        /// </summary>
        public External_Url[] External_Urls { get; } = new External_Url[0];

        /// <summary>
        /// A link to the Web API endpoint providing full details of the track.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The Spotify ID for the track. 
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// The object type: "track".
        /// </summary>
        public string Type { get; } = "track";

        /// <summary>
        /// The Spotify URI for the track.
        /// </summary>
        public string Uri { get; } = string.Empty;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public TrackLink() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public TrackLink(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public TrackLink(JToken token) {
            Href = token.Value<string>("href") ?? string.Empty;
            Id = token.Value<string>("id") ?? string.Empty;
            Uri = token.Value<string>("uri") ?? string.Empty;

            JObject exturls = token.Value<JObject>("external_urls");
            if(exturls != null) {
                this.External_Urls = External_Url.FromJObject(exturls);
            }
        }


    }
}
