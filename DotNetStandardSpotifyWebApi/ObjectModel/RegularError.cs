using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    /// <summary>
    /// Apart from the response code, 
    /// unsuccessful responses return information about the error as an error JSON object containing error information
    /// </summary>
    public class RegularError : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// The HTTP status code 
        /// (also returned in the response header; see Response Status Codes for more information).
        /// </summary>
        public int Status { get; } = 0;

        /// <summary>
        /// A short description of the cause of the error. 
        /// </summary>
        public string Message { get; } = string.Empty;


        /// <summary>
        /// Empty Constructor
        /// </summary>
        public RegularError() {
            WasError = true;
            ErrorMessage = "Default Error Message";
        }

        /// <summary>
        /// Fields constructor
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public RegularError(int status, string message) {
            Status = status;
            message = message;
        }

        /// <summary>
        /// Error Constructor
        /// nb Kind of loses its meaning when it is a specific error object
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public RegularError(bool wasError, string errorMessage) {
            WasError = wasError;
            Message = errorMessage;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public RegularError(JToken token) {
            WasError = true;
            Status = token.Value<int?>("status") ?? 0;
            Message = token.Value<string>("message") ?? string.Empty;
            ErrorMessage = Message;
        }
    }
}
