using System;
using System.Collections.Generic;
using DotNetStandardSpotifyWebApi.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    /// <summary>
    /// The offset-based paging object is a container for a set of objects.
    /// It contains a key called items (whose value is an array of the requested objects) 
    /// along with other keys like previous, next and limit that can be useful in future calls.
    /// </summary>
    /// <typeparam name="T">The type of SpotifyObeject that the paging represents</typeparam>
    public class Paging<T> : SpotifyObjectModel, ISpotifyObject where T : ISpotifyObject {

        /// <summary>
        /// A link to the Web API endpoint returning the full result of the request.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The requested data.
        /// </summary>
        public IReadOnlyList<T> Items { get; private set; } = new List<T>();

        /// <summary>
        /// The maximum number of items in the response (as set in the query or by default).
        /// </summary>
        public int Limit { get; } = 50;

        /// <summary>
        /// URL to the next page of items. (empty if none) 
        /// </summary>
        public string Next { get; } = string.Empty;

        /// <summary>
        /// URL to the previous page of items. (empty if none) 
        /// </summary>
        public string Previous { get; } = string.Empty;

        /// <summary>
        /// The offset of the items returned (as set in the query or by default).
        /// </summary>
        public int Offset { get; } = 0;

        /// <summary>
        /// The total number of items available to return. 
        /// </summary>
        public int Total { get; } = 0;


        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        internal Paging(JToken token) {
            Href = token.Value<string>("href") ?? string.Empty;
            Total = token.Value<int?>("total") ?? 0;
            Limit = token.Value<int?>("limit") ?? 0;
            Offset = token.Value<int?>("offset") ?? 0;
            Next = token.Value<string>("next") ?? string.Empty;
            Previous = token.Value<string>("previous") ?? string.Empty;

            Func<JObject, ISpotifyObject> generator;
            Type t = typeof(T);
            if (t == typeof(Playlist)) {
                generator = (tk) => { return new Playlist(tk); };
            }
            else if (t == typeof(PlaylistTrack)) {
                generator = (tk) => { return new PlaylistTrack(tk); };
            }
            else if (t == typeof(Track)) {
                generator = (tk) => { return new Track(tk); };
            }
            else if (t == typeof(SavedTrack)) {
                generator = (tk) => { return new SavedTrack(tk); };
            }
            else if (t == typeof(Artist)) {
                generator = (tk) => { return new Artist(tk); };
            }
            else if (t == typeof(Album)) {
                generator = (tk) => { return new Album(tk); };
            }
            else if (t == typeof(SavedAlbum)) {
                generator = (tk) => { return new SavedAlbum(tk); };
            }
            else if (t == typeof(Category)) {
                generator = (tk) => { return new Category(tk); };
            }
            else if (t == typeof(AudioFeatures)) {
                generator = (tk) => { return new AudioFeatures(tk); };
            }
            else if (t == typeof(AudioAnalysis)) {
                generator = (tk) => { return new AudioAnalysis(tk); };
            }
            else {
                generator = (tk) => {
                    throw new ArgumentException("Something happened while generating the paging item!");
                };
            }

            JArray jarr = token.Value<JArray>("items");
            if (jarr != null) {
                List<T> lst = new List<T>();
                foreach (JObject jobj in jarr) {
                    T item = (T)generator(jobj);
                    lst.Add(item);
                }
                Items = lst;
            }
        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        internal Paging(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        internal Paging() {

        }


        /// <summary>
        /// Returns a new paging object with the next {limit} number of items
        /// Throws an IndexOutOfRangeException if there are no more items
        /// </summary>
        /// <returns></returns>
        public async Task<Paging<T>> GetNext(string accessToken) {
            /* Offset is past the total - therefore there's nothing left to get */
            if(string.IsNullOrWhiteSpace(Next) || Offset >= Total) {
                throw new IndexOutOfRangeException($"Offset({Offset}) greater than Total number of Items({Total}). There are no more items left to get");
            }
            else { /* There are items left to get, so return a new paging object with that offset*/
                HttpRequestMessage message = WebRequestHelpers.SetupRequest(this.Next, accessToken);
                HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
                if (response.IsSuccessStatusCode) {
                    JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                    Paging<T> paging = new Paging<T>(token);
                    return paging;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a new paging object with the previous {limit} number of items
        /// Throws an IndexOutOfRangeException if there are no more items
        /// </summary>
        /// <returns></returns>
        public async Task<Paging<T>> GetPrevious(string accessToken) {
            if(string.IsNullOrWhiteSpace(Previous) || Offset <= 0) {
                throw new IndexOutOfRangeException($"Offset({Offset}) already at or below 0. There are no more items left to get");
            }
            else { /* There are items left to get, so return a new paging object with that offset*/
                HttpRequestMessage message = WebRequestHelpers.SetupRequest(this.Previous, accessToken);
                HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
                if (response.IsSuccessStatusCode) {
                    JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                    Paging<T> paging = new Paging<T>(token);
                    return paging;
                }
            }
            return null;

        }
    }
}
