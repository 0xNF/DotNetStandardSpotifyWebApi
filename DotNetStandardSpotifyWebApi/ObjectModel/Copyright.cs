using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Copyright : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// The copyright text for this album.
        /// </summary>
        public string Text { get; } = string.Empty;

        /// <summary>
        /// The type of copyright: C = the copyright, P = the sound recording (performance) copyright.
        /// </summary>
        public string Type { get; } = string.Empty;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Copyright() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Copyright(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public Copyright(JToken token) {
            this.Text = token.Value<string>("text") ?? string.Empty;
            this.Text = token.Value<string>("type") ?? string.Empty;
        }

        /// <summary>
        /// Field constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public Copyright(string text, string type) {
            this.Text = text;
            this.Type = type;
        }
    }
}
