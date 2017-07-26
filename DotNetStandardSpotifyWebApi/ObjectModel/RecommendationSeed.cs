using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    /// <summary>
    /// Recommendations seed object, containing a bunch of metadata about the recommendation.
    /// </summary>
    public class RecommendationSeed : SpotifyObjectModel, ISpotifyObject {
        /// <summary>
        /// The number of tracks available after min_* and max_* filters have been applied.
        /// </summary>
        public int AfterFilteringSize { get; } = -1;

        /// <summary>
        /// The number of tracks available after relinking for regional availability.
        /// </summary>
        public int AfterRelinkingSize { get; } = -1;

        /// <summary>
        /// A link to the full track or artist data for this seed.
        /// For tracks this will be a link to a Track Object.
        /// For artists a link to an Artist Object.
        /// For genre seeds, this value will be null.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The id used to select this seed. 
        /// This will be the same as the string used in the seed_artists, seed_tracks or seed_genres parameter.
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// The number of recommended tracks available for this seed.
        /// </summary>
        public int InitialPoolSize { get; } = -1;

        /// <summary>
        /// The entity type of this seed. One of artist, track or genre.
        /// </summary>
        public string Type { get; } = string.Empty;


        /// <summary>
        /// Blank constructor
        /// </summary>
        public RecommendationSeed() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public RecommendationSeed(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JSON constructor
        /// </summary>
        /// <param name="token"></param>
        public RecommendationSeed(JToken token) {
            this.AfterFilteringSize = token.Value<int?>("afterFilteringSize") ?? -1;
            this.AfterRelinkingSize = token.Value<int?>("afterRelinkingSize") ?? -1;
            this.Href = token.Value<string>("href") ?? string.Empty;
            this.Id = token.Value<string>("id") ?? string.Empty;
            this.InitialPoolSize = token.Value<int?>("initialPoolSize") ?? -1;
            this.Type = token.Value<string>("type") ?? string.Empty;
        }


    }
}
