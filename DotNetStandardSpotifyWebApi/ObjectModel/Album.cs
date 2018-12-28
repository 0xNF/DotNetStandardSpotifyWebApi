﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Album : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// The type of the album: one of "album", "single", or "compilation". 
        /// </summary>
        public string Album_Type { get; } = string.Empty;

        /// <summary>
        /// The artists of the album. Each artist object includes a link in href to more detailed information about the artist.
        /// </summary>
        public Artist[] Artists { get; } = new Artist[0];

        /// <summary>
        /// The markets in which the album is available: ISO 3166-1 alpha-2 country codes.
        /// Note that an album is considered available in a market when at least 1 of its tracks is available in that market.
        /// </summary>
        public string[] Available_Markets { get; } = new string[0];

        /// <summary>
        /// The copyright statements of the album.
        /// </summary>
        public Copyright[] Copyrights { get; } = new Copyright[0];

        /// <summary>
        /// Known external URLs for the album. 
        /// </summary>
        public Dictionary<string, string> External_Urls { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Known external IDs for the album.
        /// </summary>
        public Dictionary<string, string> External_Ids { get; } = new Dictionary<string, string>();

        /// <summary>
        /// A list of the genres used to classify the album. 
        /// For example: "Prog Rock", "Post-Grunge". (If not yet classified, the array is empty.) 
        /// </summary>
        public string[] Genres { get; } = new string[0];

        /// <summary>
        /// A link to the Web API endpoint providing full details of the album.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The Spotify ID for the album. 
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// The cover art for the album in various sizes, widest first.
        /// </summary>
        public Image[] Images { get; } = new Image[0];

        /// <summary>
        /// The label for the album.
        /// </summary>
        public string Label { get; } = string.Empty;

        /// <summary>
        /// The name of the album. 
        /// In case of an album takedown, the value may be an empty string.
        /// </summary>
        public string Name { get; } = string.Empty;

        /// <summary>
        /// The popularity of the album. 
        /// The value will be between 0 and 100, with 100 being the most popular. 
        /// The popularity is calculated from the popularity of the album's individual tracks.
        /// </summary>
        public int Popularity { get; } = 0;

        /// <summary>
        /// The date the album was first released, for example "1981-12-15". 
        /// Depending on the precision, it might be shown as "1981" or "1981-12".
        /// </summary>
        public string Release_Date { get; } = string.Empty;

        /// <summary>
        /// The precision with which release_date value is known: "year", "month", or "day".
        /// </summary>  bj
        public string Release_Date_Precision { get; } = string.Empty;

        /// <summary>
        /// The tracks of the album.
        /// TODO is a Paging object, not a raw list of Tracks
        /// </summary>
        public Paging<Track> Tracks { get; } = new Paging<Track>(true, "Default, nothing here yet");

        /// <summary>
        /// The object type: "album"
        /// </summary>
        public string Type { get; } = "album";

        /// <summary>
        /// The Spotify URI for the album.
        /// </summary>
        public string Uri { get; } = string.Empty;


        /// <summary>
        /// Field constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="album_type"></param>
        /// <param name="artists"></param>
        /// <param name="markets"></param>
        /// <param name="copyrights"></param>
        /// <param name="external_ids"></param>
        /// <param name="external_urls"></param>
        /// <param name="genres"></param>
        /// <param name="href"></param>
        /// <param name="images"></param>
        /// <param name="label"></param>
        /// <param name="name"></param>
        /// <param name="popularity"></param>
        /// <param name="release_date"></param>
        /// <param name="release_date_precision"></param>
        /// <param name="tracks"></param>
        public Album(string id, string album_type, Artist[] artists, string[] markets, Copyright[] copyrights, Dictionary<string, string> external_ids, Dictionary<string, string> external_urls, string[] genres, string href, Image[] images, string label, string name, int popularity, string release_date, string release_date_precision, Paging<Track> tracks) {
            this.Id = id;
            this.Album_Type = album_type;
            this.Artists = artists;
            this.Available_Markets = markets;
            this.Copyrights = copyrights;
            this.External_Ids = external_ids;
            this.External_Urls = external_urls;
            this.Genres = genres;
            this.Href = href;
            this.Images = images;
            this.Label = label;
            this.Name = name;
            this.Popularity = popularity;
            this.Release_Date = release_date;
            this.Release_Date_Precision = release_date_precision;
            this.Tracks = tracks;

        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public Album(JToken token) {
            /* simple fields */
            Album_Type = token.Value<string>("album_type") ?? string.Empty;
            Href = token.Value<string>("href") ?? string.Empty;
            Id = token.Value<string>("id") ?? string.Empty;
            Label = token.Value<string>("label") ?? string.Empty;
            Name = token.Value<string>("name") ?? string.Empty;
            Popularity = token.Value<int?>("popularity") ?? 0;
            Release_Date = token.Value<string>("release_date") ?? string.Empty;
            Release_Date_Precision = token.Value<string>("release_date_precision") ?? string.Empty;
            Uri = token.Value<string>("uri") ?? string.Empty;

            /* complex fields */
            JObject exturls = token.Value<JObject>("external_urls");
            if (exturls != null) {
                foreach (JProperty x in exturls.Properties()) {
                    External_Urls.Add(x.Name, x.Value<JToken>().ToString());
                }
            }

            /* External Ids */
            JObject extids = token.Value<JObject>("external_ids");
            if (extids != null) {
                foreach (JProperty x in extids.Properties()) {
                    External_Ids.Add(x.Name, x.Value<JToken>().ToString());
                }
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
            if (genres != null) {
                List<string> gens = new List<string>();
                foreach (JValue jv in genres) {
                    string s = jv.ToString();
                    gens.Add(s);
                }
                Genres = gens.ToArray();
            }

            /* Copyrights */
            JArray copyrights = token.Value<JArray>("copyright");
            if (copyrights != null) {
                List<Copyright> cps = new List<Copyright>();
                foreach (JObject jobj in copyrights.Values<JObject>()) {
                    Copyright i = new Copyright(jobj);
                    cps.Add(i);
                }
                Copyrights = cps.ToArray();
            }

            /* Available Markets */
            JArray markets = token.Value<JArray>("available_markets");
            if (markets != null) {
                this.Available_Markets = markets.Values<string>().ToArray();
            }

            /* Artists */
            JArray artists = token.Value<JArray>("artists");
            if (artists != null) {
                List<Artist> arts = new List<Artist>();
                foreach (JObject jobj in artists) {
                    arts.Add(new Artist(jobj));
                }
                Artists = arts.ToArray();
            }
            //tracks
            JToken paging = token.Value<JToken>("tracks");
            if (paging != null) {
                Tracks = new Paging<Track>(paging);
            }

        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Album() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Album(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }
    }
}
