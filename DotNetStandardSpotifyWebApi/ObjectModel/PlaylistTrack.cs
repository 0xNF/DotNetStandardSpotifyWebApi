using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class PlaylistTrack : SpotifyObjectModel, ISpotifyObject {
        /// <summary>
        /// The date and time the track was saved.
        /// Note that some very old playlists may return empty in this field.
        /// </summary>
        public string Added_At { get; } = string.Empty;

        /// <summary>
        /// The Spotify user who added the track.
        /// Note that some very old playlists may return an empty user in this field.
        /// </summary>
        public User Added_By { get; } = new User();

        /// <summary>
        /// Whether this track is a local file or not. 
        /// </summary>
        public bool Is_Local { get; } = false;

        /// <summary>
        /// Information about the track.
        /// </summary>
        public Track Track { get; } = new Track();

        /// <summary>
        /// Empty constructor
        /// </summary>
        public PlaylistTrack() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public PlaylistTrack(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public PlaylistTrack(JToken token) {
            Added_At = token.Value<string>("added_at") ?? string.Empty;

            Is_Local = token.Value<bool?>("is_local") ?? false;

            /* User Handling */
            JObject addedby = token.Value<JObject>("added_by");
            if(addedby != null) {
                Added_By = new User(addedby);
            }

            /* Track Handling */
            JObject track = token.Value<JObject>("track");
            if (track != null) {
                Track = new Track(track);
            }


            
        }
    }
}
