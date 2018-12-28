using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class FeaturedPlaylists : SpotifyObjectModel, ISpotifyObject{
        public string Message { get; } = string.Empty;
        public Paging<Playlist> Playlists { get; } = new Paging<Playlist>(true, "Not initialized yet");

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public FeaturedPlaylists() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public FeaturedPlaylists(bool wasError, string errorMessage) {
            WasError = WasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public FeaturedPlaylists(JToken token) {
            Message = token.Value<string>("message") ?? string.Empty;
            JToken pls = token.Value<JToken>("playlists");
            if(pls != null) {
                Playlists = new Paging<Playlist>(pls);
            }
        }
    }
}
