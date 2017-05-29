using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    /// <summary>
    /// Object containing an image as seen on Spotify
    /// For profile pics, album covers, playlist images, etc
    /// </summary>
    public class Image : SpotifyObjectModel, ISpotifyObject {
        public int Height { get; } = 0;
        public int Width { get; } = 0;
        public string Url { get; } = string.Empty;

        /// <summary>
        /// Empty constreuctor
        /// </summary>
        public Image() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Image(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Value constructor
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="url"></param>
        public Image(int height, int width, string url) {
            this.Height = height;
            this.Width = width;
            this.Url = url;
        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public Image(JToken token) {
            this.Height = token.Value<int?>("height") ?? 0;
            this.Width = token.Value<int?>("width") ?? 0;
            this.Url = token.Value<string>("url") ?? string.Empty;
        }
    }
}
