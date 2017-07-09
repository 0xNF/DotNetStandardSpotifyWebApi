using System.Collections.Generic;
using System.Text;
using DotNetStandardSpotifyWebApi.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace DotNetStandardSpotifyWebApi.ObjectModel {

    /// <summary>
    /// User representing both the Full and Simplified User Objects
    /// https://developer.spotify.com/web-api/object-model/#user-object-public
    /// https://developer.spotify.com/web-api/object-model/#user-object-private
    /// </summary>
    public class User : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// The user's date-of-birth.
        /// This field is only available when the current user has granted access to the USER_READ_BIRTHDATE scope.
        /// </summary>
        public string Birthdate { get; } = string.Empty;

        /// <summary>
        /// The country of the user, as set in the user's account profile.
        /// An ISO 3166-1 alpha-2 country code. 
        /// This field is only available when the current user has granted access to the USER_READ_PRIVATE scope.
        /// </summary>
        public string Country { get; } = string.Empty;

        /// <summary>
        /// The name displayed on the user's profile. null if not available.
        /// </summary>
        public string DisplayName { get; } = string.Empty;

        /// <summary>
        /// The user's email address, as entered by the user when creating their account.
        /// Important! This email address is unverified; there is no proof that it actually belongs to the user.
        /// This field is only available when the current user has granted access to the USER_READ_EMAIL scope.
        /// </summary>
        public string Email { get; } = string.Empty;

        /// <summary>
        /// Information about the followers of the user. 
        /// </summary>
        public Followers Followers { get; }

        /// <summary>
        /// Known external URLs for this user.
        /// </summary>
        public Dictionary<string, string> External_Urls { get; } = new Dictionary<string, string>();

        /// <summary>
        /// A link to the Web API endpoint for this user.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The Spotify user ID for the user. 
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// The user's profile image. 
        /// </summary>
        public Image[] Images { get; } = new Image[0];

        /// <summary>
        /// The user's Spotify subscription level: "premium", "free", etc. (The subscription level "open" can be considered the same as "free".) 
        /// This field is only available when the current user has granted access to the USER_READ_PRIVATE scope.
        /// </summary>
        public string Product { get; } = string.Empty;

        /// <summary>
        /// The object type: "user" 
        /// </summary>
        public string Type { get; } = "user";

        /// <summary>
        /// The Spotify URI for the user.
        /// </summary>
        public string Uri { get; } = string.Empty;

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        internal User(JToken token) {
            /* Simple fields */
            Birthdate = token.Value<string>("birthdate") ?? string.Empty;
            Country = token.Value<string>("country") ?? string.Empty;
            DisplayName = token.Value<string>("display_name") ?? string.Empty;
            Email = token.Value<string>("email") ?? string.Empty;
            Product = token.Value<string>("product") ?? string.Empty;
            Href = token.Value<string>("href") ?? string.Empty;
            Id = token.Value<string>("id") ?? string.Empty;
            Uri = token.Value<string>("uri") ?? string.Empty;

            /* External Urls*/
            JObject exturls = token.Value<JObject>("external_urls");
            if (exturls != null) {
                foreach (JProperty x in exturls.Properties()) {
                    External_Urls.Add(x.Name, x.Value<JToken>().ToString());
                }
            }

            /* Followers */
            JToken follower = token.Value<JToken>("followers");
            if(follower != null) {
                Followers = new Followers(follower);
            }

            /* Images */
            JArray images = token.Value<JArray>("images");
            if (images != null) {
                List<Image> ims = new List<Image>();
                foreach (JObject jobj in images.Values<JObject>()) {
                    Image i = new Image(jobj);
                    ims.Add(i);
                }
                Images = ims.ToArray();
            }

        }

        /// <summary>
        /// Error User, given an error message and an error flag
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        internal User(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Blank constructor using defaults
        /// </summary>
        internal User() {

        }

    }


    public class Recommendation : SpotifyObjectModel, ISpotifyObject {

    }



}
