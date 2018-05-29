using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    /// <summary>
    /// Recommendations as returned by Spotify
    /// </summary>
    public class RecommendationsResponse : SpotifyObjectModel, ISpotifyObject{

        /// <summary>
        /// An array of recommendation seed objects.
        /// </summary>
        public IReadOnlyList<RecommendationSeed> Seeds { get; } = new List<RecommendationSeed>();

        /// <summary>
        /// An array of track object (simplified) ordered according to the parameters supplied.
        /// </summary>
        public IReadOnlyList<Track> Tracks { get; } = new List<Track>();

        /// <summary>
        /// Blank constructor
        /// </summary>
        public RecommendationsResponse() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public RecommendationsResponse(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JSON constructor
        /// </summary>
        /// <param name="token"></param>
        public RecommendationsResponse(JToken token) {
            //Populating Seeds
            JArray seedsArray = token.Value<JArray>("seeds");
            if(seedsArray != null) {
                List<RecommendationSeed> seeders = new List<RecommendationSeed>();
                foreach(JObject jobj in seedsArray) {
                    RecommendationSeed seed = new RecommendationSeed(jobj);
                    seeders.Add(seed);
                }
                this.Seeds = seeders;
            }

            //Populating Tracks
            JArray tracksArray = token.Value<JArray>("tracks");
            if (tracksArray != null) {
                List<Track> tracks = new List<Track>();
                foreach (JObject jobj in tracksArray) {
                    Track track= new Track(jobj);
                    tracks.Add(track);
                }
                this.Tracks = tracks;
            }
        }

        public JToken ToJson() {
            JArray jseeds = new JArray();
            foreach(RecommendationSeed seed in this.Seeds) {
                jseeds.Add(seed.ToJson());
            }
            JArray jtracks = new JArray();
            foreach(Track t in this.Tracks) {
                jtracks.Add(t.ToFullJson());
            }
            Dictionary<string, object> keys = new Dictionary<string, object>() {
                { "seeds", jseeds },
                { "track", jtracks }
            };
            return JObject.FromObject(keys);
        }
    }
}
