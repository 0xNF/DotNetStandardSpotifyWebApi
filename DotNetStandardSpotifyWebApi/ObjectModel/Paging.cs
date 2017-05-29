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


        private Paging(JToken token) {
            Href = token.Value<string>("href") ?? string.Empty;
            Total = token.Value<int?>("total") ?? 0;
            Limit = token.Value<int?>("limit") ?? 0;
            Offset = token.Value<int?>("offset") ?? 0;
            Next = token.Value<string>("next") ?? string.Empty;
            Previous = token.Value<string>("previous") ?? string.Empty;
        }


        /// <summary>
        /// Returns a new paging object with the next {limit} number of items
        /// Throws an IndexOutOfRangeException if there are no more items
        /// </summary>
        /// <returns></returns>
        public async Task<Paging<ISpotifyObject>> GetNext(string accessToken) {
            /* Offset is past the total - therefore there's nothing left to get */
            if(string.IsNullOrWhiteSpace(Next) || Offset >= Total) {
                throw new IndexOutOfRangeException($"Offset({Offset}) greater than Total number of Items({Total}). There are no more items left to get");
            }
            else { /* There are items left to get, so return a new paging object with that offset*/
                HttpRequestMessage message = WebRequestHelpers.SetupRequest(this.Next, accessToken);
                HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
                if (response.IsSuccessStatusCode) {
                    JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                    Paging<ISpotifyObject> paging = Paging<ISpotifyObject>.MakePlaylistPaging(token);
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
        public async Task<Paging<ISpotifyObject>> GetPrevious(string accessToken) {
            if(string.IsNullOrWhiteSpace(Previous) || Offset <= 0) {
                throw new IndexOutOfRangeException($"Offset({Offset}) already at or below 0. There are no more items left to get");
            }
            else { /* There are items left to get, so return a new paging object with that offset*/
                HttpRequestMessage message = WebRequestHelpers.SetupRequest(this.Previous, accessToken);
                HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
                if (response.IsSuccessStatusCode) {
                    JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                    Paging<ISpotifyObject> paging = Paging<Playlist>.MakePlaylistPaging(token); //TODO this is super wrong. Its calling playlist, but this is generic and should call others instead
                    return paging;
                }
            }
            return null;

        }

        public static Paging<ISpotifyObject> MakePlaylistPaging(JToken token) {
            Paging<ISpotifyObject> page = new Paging<ISpotifyObject>(token);
            List<Playlist> lst = new List<Playlist>();
            JArray jarr = token.Value<JArray>("items");
            if(jarr != null) {
                foreach(JObject jobj in jarr) {
                    Playlist item = new Playlist(jobj);
                    lst.Add(item);
                }
                page.Items = lst;
            }
            return page;
        }

        public static Paging<Artist> MakeArtistPaging(JToken token) {
            Paging<Artist> page = new Paging<Artist>(token);
            List<Artist> lst = new List<Artist>();
            JArray jarr = token.Value<JArray>("items");
            if (jarr != null) {
                foreach (JObject jobj in jarr) {
                    Artist item = new Artist(jobj);
                    lst.Add(item);
                }
                page.Items = lst;
            }

            return page;
        }

        public static Paging<Album> MakeAlbumPaging(JToken token) {
            Paging<Album> page = new Paging<Album>(token);
            List<Album> lst = new List<Album>();
            JArray jarr = token.Value<JArray>("items");
            if (jarr != null) {
                foreach (JObject jobj in jarr) {
                    Album item = new Album(jobj);
                    lst.Add(item);
                }
                page.Items = lst;
            }

            return page;
        }

        public static Paging<ISpotifyObject> MakeTrackPaging(JToken token) {
            Paging<ISpotifyObject> page = new Paging<ISpotifyObject>(token);
            List<Track> lst = new List<Track>();
            JArray jarr = token.Value<JArray>("items");
            if (jarr != null) {
                foreach (JObject jobj in jarr) {
                    Track item = new Track(jobj);
                    lst.Add(item);
                }
                page.Items = lst;
            }

            return page;
        }

        public static Paging<Category> MakeCategoryPaging(JToken token) {
            Paging<Category> page = new Paging<Category>(token);
            List<Category> lst = new List<Category>();
            JArray jarr = token.Value<JArray>("items");
            if (jarr != null) {
                foreach (JObject jobj in jarr) {
                    Category item = new Category(jobj);
                    lst.Add(item);
                }
                page.Items = lst;
            }
            return page;
        }
    }
}
