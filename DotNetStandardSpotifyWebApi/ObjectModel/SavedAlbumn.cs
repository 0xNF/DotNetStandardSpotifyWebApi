using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class SavedAlbum : SpotifyObjectModel, ISpotifyObject {
        /// <summary>
        /// The date and time the track was saved.
        /// </summary>
        public string Added_At { get; } = string.Empty;

        /// <summary>
        /// Information about the track.
        /// </summary>
        public Album Album { get; } = new Album();

        /// <summary>
        /// Empty constructor
        /// </summary>
        public SavedAlbum() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public SavedAlbum(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public SavedAlbum(JToken token) {
            Added_At = token.Value<string>("added_at") ?? string.Empty;
            JObject album = token.Value<JObject>("track");
            if (album != null) {
                Album = new Album(album);
            }
        }

    }
}
