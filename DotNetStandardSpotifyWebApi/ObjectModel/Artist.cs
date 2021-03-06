﻿using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Artist : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// Known external URLs for this artist.
        /// </summary>
        public Dictionary<string, string> External_Urls { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Information about the followers of the artist. 
        /// </summary>
        public Followers Followers { get; set;  } = new Followers(true, "empty followers");

        /// <summary>
        /// A list of the genres the artist is associated with. For example: "Prog Rock", "Post-Grunge". (If not yet classified, the array is empty.) 
        /// </summary>
        public string[] Genres { get; set;  } = new string[0];

        /// <summary>
        /// A link to the Web API endpoint providing full details of the artist.
        /// </summary>
        public string Href { get; set;  } = string.Empty;

        /// <summary>
        /// The Spotify ID for the artist. 
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Images of the artist in various sizes, widest first.
        /// </summary>
        public Image[] Images { get; set;  } = new Image[0];

        /// <summary>
        /// The name of the artist 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The popularity of the artist.
        /// The value will be between 0 and 100, with 100 being the most popular. 
        /// The artist's popularity is calculated from the popularity of all the artist's tracks.
        /// </summary>
        public int Popularity { get; set;  } = 0;

        /// <summary>
        /// The object type: "artist"
        /// </summary>
        public string Type { get; } = "artist";

        /// <summary>
        /// The Spotify URI for the artist.
        /// </summary>
        public string Uri { get; set; } = string.Empty;


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
    }
}
