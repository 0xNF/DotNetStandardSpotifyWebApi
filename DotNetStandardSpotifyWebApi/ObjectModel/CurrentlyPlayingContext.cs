using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class CurrentlyPlayingContext : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// The device that is currently active
        /// </summary>
        public Device Device { get; } = new Device();

        /// <summary>
        /// off, track, context
        /// </summary>
        public string Repeat_State { get; } = "off";

        /// <summary>
        /// If shuffle is on or off
        /// </summary>
        public bool Shuffle_State { get; } = false;

        /// <summary>
        /// A Context Object. Can be null.
        /// </summary>
        public Context Context { get; } = new Context();

        /// <summary>
        /// Timestamp when data was fetched
        /// </summary>
        public long Timestamp { get; } = 0;

        /// <summary>
        /// Progress into the currently playing track. Can be null.
        /// </summary>
        public long Progress_Ms { get; } = 0;

        /// <summary>
        /// If something is currently playing.
        /// </summary>
        public bool Is_Playing { get; } = false;

        /// <summary>
        /// The currently playing track. Can be null.
        /// </summary>
        public Track Item { get; } = new Track();


        /// <summary>
        /// Empty Constructor
        /// </summary>
        public CurrentlyPlayingContext() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public CurrentlyPlayingContext(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public CurrentlyPlayingContext(JToken token) {
            /* Simple Fields */
            Is_Playing = token.Value<bool?>("is_playing") ?? false;
            Progress_Ms = token.Value<long?>("progress_ms") ?? 0;
            Timestamp = token.Value<long?>("timestamp") ?? 0;
            Shuffle_State = token.Value<bool?>("shuffle_state") ?? false;
            Repeat_State = token.Value<string>("repeat_state") ?? "off";

            JObject contextObject = token.Value<JObject>("context");
            if(contextObject != null) {
                Context = new Context(contextObject);
            }

            JObject trackObject = token.Value<JObject>("item");
            if(trackObject != null) {
                Item = new Track(trackObject);
            }
        }


        public JToken ToJson() {
            Dictionary<string, object> keys = new Dictionary<string, object>() {
                { "is_playing", this.Is_Playing },
                { "progress_ms", this.Progress_Ms },
                { "timestamp", this.Timestamp },
                { "shuffle_state", this.Shuffle_State },
                { "repeat_state", this.Repeat_State },
                { "context",  this.Context.ToJson() },
                { "item",  Item.ToFullJson() }
            };
            return JObject.FromObject(keys);
        }

    }

}
