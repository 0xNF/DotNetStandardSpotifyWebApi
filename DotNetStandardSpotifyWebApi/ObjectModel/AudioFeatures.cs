using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class AudioFeatures : SpotifyObjectModel, ISpotifyObject {


        /// <summary>
        /// A confidence measure from 0.0 to 1.0 of whether the track is acoustic. 
        /// 1.0 represents high confidence the track is acoustic.
        /// </summary>
        public double? Acousticness { get; } = 0.0;
        public const double Acousticness_Max = 1;
        public const double Acousticness_Min = 0;

        /// <summary>
        /// An HTTP URL to access the full audio analysis of this track.
        /// An access token is required to access this data. 
        /// </summary>
        public string Analysis_Url { get; } = string.Empty;

        /// <summary>
        /// Danceability describes how suitable a track is for dancing 
        /// based on a combination of musical elements including tempo,
        /// rhythm stability, beat strength, and overall regularity.
        /// A value of 0.0 is least danceable and 1.0 is most danceable.
        /// </summary>
        public double? Danceability { get; } = 0.0;
        public const double Danceability_Max = 1;
        public const double Danceability_Min = 0;

        /// <summary>
        /// The duration of the track in milliseconds.
        /// </summary>
        public int? Duration_MS { get; } = 0;

        /// <summary>
        /// Energy is a measure from 0.0 to 1.0 and represents a perceptual measure of intensity and activity. 
        /// Typically, energetic tracks feel fast, loud, and noisy.
        /// For example, death metal has high energy, while a Bach prelude scores low on the scale. 
        /// Perceptual features contributing to this attribute include dynamic range, perceived loudness, 
        /// timbre, onset rate, and general entropy.
        /// </summary>
        public double? Energy { get; } = 0.0;
        public const double Energy_Max = 1;
        public const double Energy_Min = 0;

        /// <summary>
        /// The Spotify ID for the track. 
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// Predicts whether a track contains no vocals. 
        /// "Ooh" and "aah" sounds are treated as instrumental in this context.
        /// Rap or spoken word tracks are clearly "vocal". 
        /// The closer the instrumentalness value is to 1.0, 
        /// the greater likelihood the track contains no vocal content. 
        /// Values above 0.5 are intended to represent instrumental tracks,
        /// but confidence is higher as the value approaches 1.0.
        /// </summary>
        public double? Instrumentalness { get; } = 0.0;
        public const double Instrumentalness_Max = 1;
        public const double Instrumentalness_Min = 0;

        /// <summary>
        /// The key the track is in. 
        /// Integers map to pitches using standard Pitch Class notation. 
        /// E.g. 0 = C, 1 = C♯/D♭, 2 = D, and so on.
        /// </summary>
        public int? Key { get; } = 0;
        public const int Key_Max = 12;
        public const int Key_Min = 0;

        /// <summary>
        /// Detects the presence of an audience in the recording.
        /// Higher liveness values represent an increased probability that the track was performed live.
        /// A value above 0.8 provides strong likelihood that the track is live.
        /// </summary>
        public double? Liveness { get; } = 0.0;
        public const double Liveness_Max = 1;
        public const double Liveness_Min = 0;

        /// <summary>
        /// The overall loudness of a track in decibels (dB). 
        /// Loudness values are averaged across the entire track and are useful
        /// for comparing relative loudness of tracks. Loudness is the quality of a 
        /// sound that is the primary psychological correlate of physical strength (amplitude).
        /// Values typical range between -60 and 0 db.
        /// </summary>
        public double? Loudness { get; } = 0.0;
        public const double Loudness_Max = 100;
        public const double Loudness_Min = -100;

        /// <summary>
        /// Mode indicates the modality (major or minor) of a track, 
        /// the type of scale from which its melodic content is derived. 
        /// Major is represented by 1 and minor is 0.
        /// </summary>
        public int? Mode { get; } = 0;
        public const int Mode_Max = 1;
        public const int Mode_Min = 0;

        /// <summary>
        /// Speechiness detects the presence of spoken words in a track.
        /// The more exclusively speech-like the recording (e.g. talk show, audio book, poetry), 
        /// the closer to 1.0 the attribute value. 
        /// Values above 0.66 describe tracks that are probably made entirely of spoken words. 
        /// Values between 0.33 and 0.66 describe tracks that may contain both music and speech,
        /// either in sections or layered, including such cases as rap music. 
        /// Values below 0.33 most likely represent music and other non-speech-like tracks.
        /// </summary>
        public double? Speechiness { get; } = 0.0;
        public const double Speechiness_Max = 1;
        public const double Speechiness_Min = 0;

        /// <summary>
        /// The overall estimated tempo of a track in beats per minute (BPM).
        /// In musical terminology, tempo is the speed or pace of a given piece 
        /// and derives directly from the average beat duration.
        /// </summary>
        public double? Tempo { get; } = 0.0;
        public const double Tempo_Max = 500;
        public const double Tempo_Min = 0;

        /// <summary>
        /// An estimated overall time signature of a track. 
        /// The time signature (meter) is a notational convention to specify how many beats are in each bar (or measure).
        /// </summary>
        public int? Time_Signature { get; } = 0;
        public const int Time_Signature_Max = -1;
        public const int Time_Signature_Min = 7;

        /// <summary>
        /// A link to the Web API endpoint providing full details of the track.
        /// </summary>
        public string Track_Href { get; } = string.Empty;
        
        /// <summary>
        /// The object type: "audio_features"
        /// </summary>
        public string Type { get; } = "audio_features";

        /// <summary>
        /// The Spotify URI for the track. 
        /// </summary>
        public string Uri { get; } = string.Empty;

        /// <summary>
        /// A measure from 0.0 to 1.0 describing the musical positiveness conveyed by a track.
        /// Tracks with high valence sound more positive (e.g. happy, cheerful, euphoric), 
        /// while tracks with low valence sound more negative (e.g. sad, depressed, angry).
        /// </summary>
        public double? Valence { get; } = 0.0;
        public const double Valence_Max = 1;
        public const double Valence_Min = 0;

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public AudioFeatures(JToken token) {
            Acousticness = token.Value<double?>("acousticness") ?? 0.0;
            Analysis_Url = token.Value<string>("analysis_url") ?? string.Empty;
            Danceability = token.Value<double?>("danceability") ?? 0.0;
            Duration_MS = token.Value<int?>("duration_ms") ?? 0;
            Energy = token.Value<double?>("energy") ?? 0.0;
            Id = token.Value<string>("id") ?? string.Empty;
            Instrumentalness = token.Value<double?>("instrumentalness") ?? 0.0;
            Key = token.Value<int?>("key") ?? 0;
            Liveness = token.Value<double?>("liveness") ?? 0.0;
            Loudness = token.Value<double?>("loudness") ?? 0.0;
            Mode = token.Value<int?>("mode") ?? 0;
            Speechiness = token.Value<double?>("speechiness") ?? 0.0;
            Tempo = token.Value<double?>("tempo") ?? 0.0;
            Time_Signature = token.Value<int?>("time_signature") ?? 0;
            Track_Href = token.Value<string>("track_href") ?? string.Empty;
            Uri = token.Value<string>("uri") ?? string.Empty;
            Valence = token.Value<double?>("valence") ?? 0.0;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public AudioFeatures() {
            Acousticness = -1;
            Danceability = -1;
            Duration_MS = -1;
            Energy = -1;
            Instrumentalness = -1;
            Key = -1;
            Liveness = -1;
            Loudness = -1;
            Mode = -1;
            Speechiness = -1;
            Tempo = -1;
            Time_Signature = -1;
            Valence = -1;
        }

        public AudioFeatures(string id) : base() {
            this.Id = id;
        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public AudioFeatures(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Fields Constructor
        /// Allows for nullable values, in case of use in Recommendations endpoints
        /// </summary>
        public AudioFeatures(double? acousticness, double? danceability, double? energy, double? instrumentalness, 
                             int? key, double? liveness, double? loudness, int? mode,
                             double? speechiness, double? tempo, int? time_signature, double? valence,
                             string analysis_url, int? duration, string id, string uri, string track_href) {
            this.Acousticness = acousticness;
            this.Analysis_Url = analysis_url;
            this.Danceability = danceability;
            this.Duration_MS = duration;
            this.Energy = energy;
            this.Id = id;
            this.Instrumentalness = instrumentalness;
            this.Key = key;
            this.Liveness = liveness;
            this.Loudness = loudness;
            this.Mode = mode;
            this.Speechiness = speechiness;
            this.Tempo = tempo;
            this.Time_Signature = time_signature;
            this.Track_Href = track_href;
            this.Uri = uri;
            this.Valence = valence;
        }


    }

}
