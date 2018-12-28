using System;
using System.Collections.Generic;
using DotNetStandardSpotifyWebApi.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    /// <summary>
    /// The cursor-based paging object is a container for a set of objects. 
    /// It contains a key called items (whose value is an array of the requested objects) 
    /// along with other keys like next and cursors that can be useful in future calls.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CursorBasedPaging<T> : SpotifyObjectModel, ISpotifyObject where T : ISpotifyObject {

        /// <summary>
        /// A link to the Web API endpoint returning the full result of the request.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The requested data.
        /// </summary>
        public IReadOnlyList<T> Items { get; } = new List<T>();

        /// <summary>
        /// The maximum number of items in the response (as set in the query or by default).
        /// </summary>
        public int Limit { get; } = 20;

        /// <summary>
        /// URL to the next page of items. (null if none) 
        /// </summary>
        public string Next { get; } = string.Empty;

        /// <summary>
        /// The cursors used to find the next set of items.
        /// </summary>
        public Cursor Cursors { get; } = new Cursor();

        /// <summary>
        /// The total number of items available to return. 
        /// </summary>
        public int Total { get; } = 0;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public CursorBasedPaging() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public CursorBasedPaging(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public CursorBasedPaging(JToken token) {
            Href = token.Value<string>("href") ?? string.Empty;
            Limit = token.Value<int?>("limit") ?? 20;
            Next = token.Value<string>("next") ?? string.Empty;
            Total = token.Value<int?>("total") ?? 0;

            JToken cobj = token.Value<JToken>("cursors");
            if(cobj != null) {
                Cursors = new Cursor(cobj);
            }

            Func<JObject, ISpotifyObject> generator = WebRequestHelpers.CreateSpotifyObjectGenerator(typeof(T));

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
        /// Returns a new Cursor-Based Paging object with the next {Limit} number of items, 
        /// starting after the {After} fields
        /// Throws an IndexOutOfRangeException if there are no more items
        /// </summary>
        /// <returns></returns>
        public async Task<CursorBasedPaging<T>> GetNext(string accessToken) {
            /* Offset is past the total - therefore there's nothing left to get */
            if (string.IsNullOrWhiteSpace(Next)) {
                throw new IndexOutOfRangeException($"There are no more items left to get");
            }
            else { /* There are items left to get, so return a new paging object with that offset*/
                HttpRequestMessage message = WebRequestHelpers.SetupRequest(this.Next, accessToken);
                HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
                if (response.IsSuccessStatusCode) {
                    JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                    CursorBasedPaging<T> paging = new CursorBasedPaging<T>(token);
                    return paging;
                }
            }
            return null;
        }

    }
}
