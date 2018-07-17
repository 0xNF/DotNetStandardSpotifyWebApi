﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Track : SpotifyObjectModel, ISpotifyObject, ISimpleSpotifyObject, IFullSpotifyObject {

        private readonly bool IsTrackRelinkingApplied = false;

        /// <summary>
        /// The album on which the track appears. 
        /// The album object includes a link in href to full information about the album. 
        /// </summary>
        public Album Album { get; } = new Album(true, "Default, not yet populated");

        /// <summary>
        /// The artists who performed the track. 
        /// Each artist object includes a link in href to more detailed information about the artist. 
        /// </summary>
        public Artist[] Artists { get; } = new Artist[0];

        /// <summary>
        /// A list of the countries in which the track can be played, identified by their ISO 3166-1 alpha-2 code. 
        /// </summary>
        public string[] Available_Markets { get; } = new string[0];

        /// <summary>
        /// The disc number (usually 1 unless the album consists of more than one disc). 
        /// </summary>
        public int Disc_Number { get; } = 1;

        /// <summary>
        /// The track length in milliseconds. 
        /// </summary>
        public long Duration_Ms { get; } = 0;

        /// <summary>
        /// Whether or not the track has explicit lyrics 
        /// (true = yes it does; false = no it does not OR unknown). 
        /// </summary>
        public bool Explicit { get; } = false;

        /// <summary>
        /// Known external IDs for the track.
        /// </summary>
        public Dictionary<string, string> External_Ids { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Known external URLs for this track.
        /// </summary>
        public Dictionary<string, string> External_Urls { get; } = new Dictionary<string, string>();

        /// <summary>
        /// A link to the Web API endpoint providing full details of the track.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The Spotify ID for the track. 
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// Part of the response when Track Relinking is applied.
        /// If true, the track is playable in the given market. Otherwise false.
        /// </summary>
        public bool Is_Playable { get; } = true;

        /// <summary>
        /// Part of the response when Track Relinking is applied, and the requested track has been replaced with different track.
        /// The track in the linked_from object contains information about the originally requested track.
        /// </summary>
        public TrackLink Linked_From { get; } = new TrackLink(true, "Default, not yet populated");


        /// <summary>
        /// The name of the track.
        /// </summary>
        public string Name { get; } = string.Empty;

        /// <summary>
        /// The popularity of the track. The value will be between 0 and 100, with 100 being the most popular.
        ///  The popularity is calculated by algorithm and is based, in the most part, on the total number of plays the track has had and how recent those plays are.
        ///  Generally speaking, songs that are being played a lot now will have a higher popularity than songs that were played a lot in the past. 
        ///  Duplicate tracks (e.g. the same track from a single and an album) are rated independently. 
        ///  Artist and album popularity is derived mathematically from track popularity.
        ///  Note that the popularity value may lag actual popularity by a few days: the value is not updated in real time.
        /// </summary>
        public int Popularity { get; } = 0;

        /// <summary>
        /// Part of the response when Track Relinking is applied, the original track is not available in the given market, and Spotify did not have any tracks to relink it with. 
        /// The track response will still contain metadata for the original track, and a restrictions object containing the reason why the track is not available:
        /// "restrictions" : {"reason" : "market"}
        /// </summary>
        public Restriction Restrictions { get; }

        /// <summary>
        /// A link to a 30 second preview (MP3 format) of the track. Empty if not available.
        /// </summary>
        public string Preview_Url { get; } = string.Empty;

        /// <summary>
        /// The number of the track.
        /// If an album has several discs, the track number is the number on the specified disc.
        /// </summary>
        public int Track_Number { get; } = 0;

        /// <summary>
        /// The object type: "track".
        /// </summary>
        public string Type { get; } = "track";

        /// <summary>
        /// The Spotify URI for the track.
        /// </summary>
        public string Uri { get; } = string.Empty;


        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public Track(JToken token) {
            /* Simple Fields */
            Disc_Number = token.Value<int?>("disc_number ") ?? 1;
            Duration_Ms = token.Value<long?>("duration_ms") ?? 0;
            Explicit = token.Value<bool?>("explicit") ?? false;
            Href = token.Value<string>("href") ?? string.Empty;
            Is_Playable = token.Value<bool?>("is_playable") ?? true;
            Id = token.Value<string>("id") ?? string.Empty;
            Name = token.Value<string>("name") ?? string.Empty;
            Popularity = token.Value<int?>("popularity") ?? 0;
            Preview_Url = token.Value<string>("preview_url") ?? string.Empty;
            Track_Number = token.Value<int?>("track_number") ?? 1;
            Uri = token.Value<string>("uri") ?? string.Empty;

            /*Complex Fields */
            /* Available Markets */
            JArray markets = token.Value<JArray>("available_markets");
            if (markets != null) {
                this.Available_Markets = markets.Values<string>().ToArray();
            }

            /* Album */
            JObject album = token.Value<JObject>("album");
            if(album != null) {
                this.Album = new Album(album);
            }

            /* Artists */
            JArray artists = token.Value<JArray>("artists");
            if(artists != null) {
                List<Artist> arts = new List<Artist>();
                foreach(JObject jobj in artists) {
                    arts.Add(new Artist(jobj));
                }
                Artists = arts.ToArray();
            }

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

            /* Linked From */
            JObject linked = token.Value<JObject>("linked_from");
            if(linked != null) {
                Linked_From = new TrackLink(linked);
            }


            // Restrictions
            JToken restrictions = token.Value<JToken>("restrictions");
            if (restrictions != null) {
                Restrictions = new Restriction(restrictions);
            }


        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Track() {

        }

        /// <summary>
        /// Fields constructor
        /// </summary>
        public Track(string trackId, Artist[] artists, string[] availableMarkets, bool isPlayable, int discNumber, bool isExplicit, Dictionary<string, string> externalIds, string href, string name, string previewUrl, int trackNumber, string uri, int popularity) {
            this.Id = trackId;
            this.Artists = artists;
            this.Available_Markets = availableMarkets;
            this.Disc_Number = discNumber;
            this.Explicit = isExplicit;
            this.External_Ids = externalIds;
            this.Href = href;
            this.Name = name;
            this.Preview_Url = previewUrl;
            this.Track_Number = trackNumber;
            this.Uri = uri;
            this.Popularity = popularity;
            this.Restrictions = null;
            this.Is_Playable = isPlayable;
        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Track(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        public JObject ToSimpleJson() {

            JArray jartists = new JArray();
            foreach(Artist a in this.Artists) {
                jartists.Add(a.ToSimpleJson());
            }

            Dictionary<string, object> keys = new Dictionary<string, object>() {
                { "artists", jartists },
                { "available_markets", JArray.FromObject(this.Available_Markets) },
                { "disc_number", this.Disc_Number },
                { "duration_ms", this.Duration_Ms },
                { "explicit", this.Explicit },
                { "external_urls", JObject.FromObject(this.External_Urls) },
                { "href", this.Href },
                { "id", this.Id },
                { "name", this.Name },
                { "preview_url", this.Preview_Url },
                { "track_number", this.Track_Number },
                { "type", this.Type },
                { "uri", this.Uri }
            };
            if (IsTrackRelinkingApplied) {
                keys.Add("is_playable", this.Is_Playable);
                keys.Add("linked_from", this.Linked_From.ToJson());
                keys.Add("restrictions", this.Restrictions.ToJson());
            }
            return JObject.FromObject(keys);
        }

        public JObject ToFullJson() {
            JObject simple = this.ToSimpleJson();
            simple.Add("popularity", this.Popularity);
            simple.Add("album", this.Album.ToSimpleJson());
            return simple;
        }

        public JToken ToJson() {
            return this.ToFullJson();
        }

    }
}
