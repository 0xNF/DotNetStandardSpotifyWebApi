using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class SavedAlbumn : SpotifyObjectModel, ISpotifyObject {
        /// <summary>
        /// The date and time the track was saved.
        /// </summary>
        public string Added_At { get; } = string.Empty;

        /// <summary>
        /// Information about the track.
        /// </summary>
        public Album Track { get; } = new Album();

        /// <summary>
        /// Empty constructor
        /// </summary>
        public SavedAlbumn() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public SavedAlbumn(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public SavedAlbumn(JToken token) {
            Added_At = token.Value<string>("added_at") ?? string.Empty;
            JObject album = token.Value<JObject>("track");
            if (album != null) {
                Track = new Album(album);
            }
        }
    }
}
