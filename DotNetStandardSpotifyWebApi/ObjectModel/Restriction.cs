using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Restriction : SpotifyObjectModel, ISpotifyObject {
        public string Reason { get; }

        public Restriction() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Restriction(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        public Restriction(string reason) {
            this.Reason = reason;
        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public Restriction(JToken token) {
            string reason = token.Value<string>("reason");
            this.Reason = reason;
        }

        public JToken ToJson() {
            Dictionary<string, object> keys = new Dictionary<string, object>() {
                { "reason", Reason }
            };
            return JObject.FromObject(keys);
        }
    }
}
