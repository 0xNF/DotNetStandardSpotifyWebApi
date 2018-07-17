using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Artist : SpotifyObjectModel, ISpotifyObject, ISimpleSpotifyObject, IFullSpotifyObject {

        private readonly bool IsTrackRelinkingApplied = false;

        /// <summary>
        /// Known external URLs for this artist.
        /// </summary>
        public Dictionary<string, string> External_Urls { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Information about the followers of the artist. 
        /// </summary>
        public Followers Followers { get; } = new Followers(true, "empty followers");

        /// <summary>
        /// A list of the genres the artist is associated with. For example: "Prog Rock", "Post-Grunge". (If not yet classified, the array is empty.) 
        /// </summary>
        public string[] Genres { get; } = new string[0];

        /// <summary>
        /// A link to the Web API endpoint providing full details of the artist.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The Spotify ID for the artist. 
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// Images of the artist in various sizes, widest first.
        /// </summary>
        public Image[] Images { get; } = new Image[0];

        /// <summary>
        /// The name of the artist 
        /// </summary>
        public string Name { get; } = string.Empty;

        /// <summary>
        /// The popularity of the artist.
        /// The value will be between 0 and 100, with 100 being the most popular. 
        /// The artist's popularity is calculated from the popularity of all the artist's tracks.
        /// </summary>
        public int Popularity { get; } = 0;

        /// <summary>
        /// The object type: "artist"
        /// </summary>
        public string Type { get; } = "artist";

        /// <summary>
        /// The Spotify URI for the artist.
        /// </summary>
        public string Uri { get; } = string.Empty;

        


        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public Artist(JToken token) {
            /* Simple Fields */
            Uri = token.Value<string>("uri") ?? string.Empty;
            Popularity = token.Value<int?>("popularity") ?? 0;
            Name = token.Value<string>("name") ?? string.Empty;
            Id = token.Value<string>("id") ?? string.Empty;
            Href = token.Value<string>("href") ?? string.Empty;

            /* Complex Fields */
            JObject exturls = token.Value<JObject>("external_urls");
            if (exturls != null) {
                foreach(JProperty x in exturls.Properties()) {
                    External_Urls.Add(x.Name, x.Value<JToken>().ToString());
                }
            }

            /* Followers */
            JToken follower = token.Value<JToken>("followers");
            if (follower != null) {
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

            /* Genres */
            JArray genres = token.Value<JArray>("genres");
            if(genres != null) {
                List<string> gens = new List<string>();
                foreach(JValue jv in genres) {
                    string s = jv.ToString();
                    gens.Add(s);
                }
                Genres = gens.ToArray();
            }
        }

        /// <summary>
        /// Fields constructor
        /// </summary>
        /// <param name="artistId"></param>
        /// <param name="artistName"></param>
        /// <param name="externalUrls"></param>
        /// <param name="followers"></param>
        /// <param name="genres"></param>
        /// <param name="href"></param>
        /// <param name="images"></param>
        /// <param name="popularity"></param>
        /// <param name="uri"></param>
        public Artist(string artistId, string artistName, Dictionary<string, string> externalUrls, Followers followers, string[] genres, string href, Image[] images, int popularity, string uri) {
            this.Id = artistId;
            this.Name = artistName;
            this.External_Urls = externalUrls;
            this.Followers = followers;
            this.Genres = genres;
            this.Href = href;
            this.Images = images;
            this.Popularity = popularity;
            this.Uri = uri;
        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Artist() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Artist(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        public JObject ToSimpleJson() {
            Dictionary<string, object> keys = new Dictionary<string, object>() {
                { "external_urls",  JObject.FromObject(this.External_Urls) },
                { "href", this.Href },
                { "id", this.Id },
                { "name", this.Name },
                { "type", this.Type },
                { "uri", this.Uri }
            };
            return JObject.FromObject(keys);
        }

        public JObject ToFullJson() {
            JArray jimages = new JArray();
            foreach (Image i in this.Images) {
                jimages.Add(i.ToJson());
            }

            JObject simple = this.ToSimpleJson();
            simple.Add("followers", this.Followers.ToJson());
            simple.Add("genres", JArray.FromObject(this.Genres));
            simple.Add("images", jimages);
            simple.Add("popularity", this.Popularity);

            return simple;
        }

        public JToken ToJson() {
            return ToFullJson();
        }
    }
}
