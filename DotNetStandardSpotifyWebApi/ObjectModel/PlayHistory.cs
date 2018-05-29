using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class PlayHistory : SpotifyObjectModel, ISpotifyObject {
        /// <summary>
        /// The date and time the track was played.
        /// </summary>
        public string Played_At { get; } = string.Empty;

        /// <summary>
        /// The context the track was played from.
        /// </summary>
        public Context Context { get; } = new Context();

        /// <summary>
        /// The track the user listened to.
        /// </summary>
        public Track Track { get; } = new Track();

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public PlayHistory() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public PlayHistory(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public PlayHistory(JToken token) {
            Played_At = token.Value<string>("played_at") ?? string.Empty;

            JObject contextObject = token.Value<JObject>("context");
            if(contextObject != null) {
                Context = new Context(contextObject);
            }

            JObject trackObject = token.Value<JObject>("track");
            if(trackObject != null) {
                Track = new Track(trackObject);
            }
        }

        /// <summary>
        /// Fields Constructor
        /// </summary>
        /// <param name="played_at"></param>
        /// <param name="context"></param>
        /// <param name="track"></param>
        public PlayHistory(string played_at, Context context, Track track) {
            Played_At = played_at;
            Context = context;
            Track = track;
        }

        public JToken ToJson() {
            Dictionary<string, object> keys = new Dictionary<string, object>() {
                { "track", this.Track.ToSimpleJson() },
                { "played_at", this.Played_At },
                { "context", this.Context.ToJson() }
            };
            return JObject.FromObject(keys);
        }
    }

}
