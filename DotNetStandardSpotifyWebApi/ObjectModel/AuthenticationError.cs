using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    /// <summary>
    /// Whenever the application makes requests to the API which are related to authentication or authorization, 
    /// e.g. retrieving an access token or refreshing an access token, the error response follows RFC 6749 on The OAuth 2.0 Authorization Framework.
    /// </summary>
    public class AuthenticationError : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// A high level description of the error as specified in RFC 6749 Section 5.2.
        /// </summary>
        public string Error { get; } = string.Empty;

        /// <summary>
        /// A more detailed description of the error as specified in RFC 6749 Section 4.1.2.1.
        /// </summary>
        public string Error_Description { get; } = string.Empty;


        /// <summary>
        /// Empty Constructor
        /// </summary>
        public AuthenticationError() {
            WasError = true;
            ErrorMessage = "Default Error Message";
        }

        /// <summary>
        /// Error Constructor
        /// nb Kind of loses its meaning when it is a specific error object
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public AuthenticationError(bool wasError, string errorMessage) {
            WasError = true;
            Error = errorMessage;
            Error_Description = errorMessage;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public AuthenticationError(JToken token) {
            WasError = true;
            Error = token.Value<string>("error") ?? string.Empty;
            Error_Description = token.Value<string>("error_description") ?? string.Empty;
            ErrorMessage = Error_Description;
        }

        public JToken ToJson() {
            Dictionary<string, object> keys = new Dictionary<string, object>() {
                { "error", Error },
                { "error_description", this.Error_Description}
            };
            return JObject.FromObject(keys);
        }
    }
}
