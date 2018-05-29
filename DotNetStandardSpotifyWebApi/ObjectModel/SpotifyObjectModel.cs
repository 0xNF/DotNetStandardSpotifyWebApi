using System;
using System.Collections.Generic;
using System.Text;
using DotNetStandardSpotifyWebApi.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public abstract class SpotifyObjectModel<T> {
        internal const string baseUrl = "https://api.spotify.com";
        public bool WasError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public abstract class SpotifyObjectModel {
        internal const string baseUrl = "https://api.spotify.com";
        public bool WasError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public interface ISpotifyObject {
        JToken ToJson();
    }

    public interface ISimpleSpotifyObject {
        JObject ToSimpleJson();
    }

    public interface IFullSpotifyObject {
        JObject ToFullJson();
    }


    /// <summary>
    /// Wrapper for strings or other such primitives that Spotify may return, but which we would like to return to the enduser as a RegularError
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SpotifyList<T> : SpotifyObjectModel, ISpotifyObject {

        public IReadOnlyList<T> Items { get; } = new List<T>();


        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public SpotifyList(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public SpotifyList() {

        }

        /// <summary>
        /// JSON constructor
        /// </summary>
        /// <param name="token"></param>
        public SpotifyList(JToken token) {
            List<T> items = new List<T>();
            IEnumerable<JToken> tokenvalues = token.Values();
            foreach (JToken val in tokenvalues) {
                T v = val.ToObject<T>();
                items.Add(v);
            }
            this.Items = items;
        }

        public JToken ToJson() {
            JArray jarr = new JArray();
            foreach(T t in Items) {
                jarr.Add(t);
            }
            return jarr;
        }
    }

}
