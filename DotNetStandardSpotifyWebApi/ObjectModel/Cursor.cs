using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Cursor : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// The cursor to use as key to find the next page of items.
        /// </summary>
        public string After { get; } = string.Empty;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Cursor() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Cursor(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public Cursor(JToken token) {
            After = token.Value<string>("after") ?? string.Empty;
        }

        public JToken ToJson() {
            Dictionary<string, object> keys = new Dictionary<string, object>() {
                { "after", this.After }
            };
            return JObject.FromObject(keys);
        }

    }
}
