using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class SavedTrack : SpotifyObjectModel, ISpotifyObject {
        /// <summary>
        /// The date and time the track was saved.
        /// </summary>
        public string Added_At { get; } = string.Empty;

        /// <summary>
        /// Information about the track.
        /// </summary>
        public Track Track { get; } = new Track();

        /// <summary>
        /// Empty constructor
        /// </summary>
        public SavedTrack() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public SavedTrack(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public SavedTrack(JToken token) {
            Added_At = token.Value<string>("added_at") ?? string.Empty;
            JObject track = token.Value<JObject>("track");
            if(track != null) {
                Track = new Track(track);
            }
        }
    }
}
