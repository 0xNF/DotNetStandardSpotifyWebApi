using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Category : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// A link to the Web API endpoint returning full details of the category.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The category icon, in various sizes.
        /// </summary>
        public Image[] Icons { get; } = new Image[0];

        /// <summary>
        /// The Spotify category ID of the category. 
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Name { get; } = string.Empty;


        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public Category(JToken token) {
            Href = token.Value<string>("href") ?? string.Empty;
            Id = token.Value<string>("id") ?? string.Empty;
            Name = token.Value<string>("name") ?? string.Empty;

            /* Image Handling */
            JArray images = token.Value<JArray>("images");
            if (images != null) {
                List<Image> ims = new List<Image>();
                foreach (JObject jobj in images.Values<JObject>()) {
                    Image i = new Image(jobj);
                    ims.Add(i);
                }
                Icons = ims.ToArray();
            }

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Category(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Category() {

        }
    }

}
