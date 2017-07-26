using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class AudioAnalysis : SpotifyObjectModel, ISpotifyObject {

        IReadOnlyList<Bar> Bars { get; } = new List<Bar>();
        IReadOnlyList<Beat> Beats { get; } = new List<Beat>();
        IReadOnlyList<Section> Sections { get; } = new List<Section>();
        IReadOnlyList<Segment> Segments { get; } = new List<Segment>();
        IReadOnlyList<Tatum> Tatums { get; } = new List<Tatum>();
        Meta Metadata { get; } = null;
        AnalysisTrack Track { get; } = null;

        public AudioAnalysis() {

        }

        public AudioAnalysis(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }


        /// <summary>
        /// Bar object of an Audio Analysis object
        /// Contains information like the start time (in milliseconds)
        /// The duration (in milliseconds)
        /// And the confidence score of the bar
        /// </summary>
        internal class Bar {

            /// <summary>
            /// Start time of this bar (in ms)
            /// </summary>
            double Start { get; } = 0;

            /// <summary>
            /// Length of this bar in milliseconds
            /// </summary>
            double Duration { get; } = 0;

            /// <summary>
            /// Spotify's confidence in this bar's values
            /// </summary>
            double Confidence { get; } = 0;

            internal Bar(JToken token) {
                this.Start = token.Value<double?>("start") ?? 0;
                this.Duration = token.Value<double?>("duration") ?? 0;
                this.Confidence = token.Value<double?>("confidence") ?? 0;
            }
        }

        /// <summary>
        /// Beat object of Audio Analysis object. Values measurd in milliseconds
        /// </summary>
        internal class Beat {

            /// <summary>
            /// Start time of this beat (in ms)
            /// </summary>
            double Start { get; } = 0;

            /// <summary>
            /// Length of this beat in milliseconds
            /// </summary>
            double Duration { get; } = 0;

            /// <summary>
            /// Spotify's confidence in this beats's values
            /// </summary>
            double Confidence { get; } = 0;

            internal Beat(JToken token) {
                this.Start = token.Value<double?>("start") ?? 0;
                this.Duration = token.Value<double?>("duration") ?? 0;
                this.Confidence = token.Value<double?>("confidence") ?? 0;
            }
        }

        /// <summary>
        /// Metadata information about the audio analysis
        /// </summary>
        internal class Meta {

            /// <summary>
            /// Version number of the analyzer that produced this analysis object
            /// </summary>
            string Analyzer_Version { get; } = string.Empty;

            /// <summary>
            /// OS object was analyzed on
            /// </summary>
            string Platform { get; } = string.Empty;

            /// <summary>
            /// Status message of the analysis
            /// </summary>
            string Detailed_Status { get; } = string.Empty;

            /// <summary>
            /// Return code of the analysis.
            /// </summary>
            int Status_Code { get; } = -1;

            /// <summary>
            /// UNIX datetime the analysis was performed
            /// </summary>
            long Timestamp { get; } = 0;

            /// <summary>
            /// Amount of time the analysis took
            /// </summary>
            double Analysis_Time { get; } = 0;

            /// <summary>
            /// Information related to the analysis command
            /// </summary>
            string Input_Process { get; } = string.Empty;

            internal Meta(JToken token) {
                this.Analyzer_Version = token.Value<string>("analyzer_version") ?? string.Empty;
                this.Platform = token.Value<string>("platform") ?? string.Empty;
                this.Status_Code = token.Value<int?>("status_code") ?? -1;
                this.Timestamp = token.Value<long?>("timestamp") ?? 0;
                this.Analysis_Time = token.Value<double?>("analysis_time") ?? 0;
                this.Input_Process = token.Value<string>("input_process") ?? string.Empty;
                this.Detailed_Status = token.Value<string>("detailed_status") ?? string.Empty;
            }
        }

        /// <summary>
        /// Analysis of a section of the track
        /// </summary>
        internal class Section {
            /// <summary>
            /// Start time of this section
            /// </summary>
            double Start { get; } = 0;

            /// <summary>
            /// Duration of this section
            /// </summary>
            double Duration { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the analysis of this section
            /// </summary>
            double Confidence { get; } = 0;

            /// <summary>
            /// Average loudness of this section (-100 : 100)
            /// </summary>
            double Loudness { get; } = 0;

            /// <summary>
            /// Spotify's confidence in this loudness analysis. n.b.: might not exist
            /// </summary>
            double Loudness_Confidence { get; } = 0;

            /// <summary>
            /// Average Tempo of this section (0 : 500)
            /// </summary>
            double Tempo { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the tempo analysis
            /// </summary>
            double Tempo_Confidence { get; } = 0;

            /// <summary>
            /// Average Key of this section (1-5)
            /// </summary>
            int Key { get; } = 0;

            /// <summary>
            /// Spotfiy's condfience in the key analysis
            /// </summary>
            double Key_Confidence { get; } = 0;

            /// <summary>
            /// Average Mode of the section (0-1)
            /// </summary>
            int Mode { get; } = 0;

            /// <summary>
            /// Spotfiy's confidence in the Mode of this section
            /// </summary>
            double Mode_Confidence { get; } = 0;

            /// <summary>
            /// Time Signature of this section (-1 : 5)
            /// </summary>
            int Time_Signature { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the time signature analysis
            /// </summary>
            double Time_Signature_Confidence { get; } = 0;

            internal Section(JToken token) {
                this.Start = token.Value<double?>("start") ?? 0;
                this.Duration = token.Value<double?>("duration") ?? 0;
                this.Confidence = token.Value<double?>("confidence") ?? 0;
                this.Loudness = token.Value<double?>("loudness") ?? 0;
                this.Loudness_Confidence = token.Value<double?>("loudness_confidence") ?? 0;
                this.Tempo = token.Value<double?>("tempo") ?? 0;
                this.Tempo_Confidence= token.Value<double?>("tempo_confidence") ?? 0;
                this.Key = token.Value<int?>("key") ?? 0;
                this.Key_Confidence = token.Value<double?>("key_confidence") ?? 0;
                this.Mode = token.Value<int?>("mode") ?? 0;
                this.Mode_Confidence = token.Value<double?>("mode_confidence") ?? 0;
                this.Time_Signature = token.Value<int?>("time_signature") ?? 0;
                this.Time_Signature_Confidence = token.Value<double?>("time_signature_confidence") ?? 0;
            }
        }

        /// <summary>
        /// Analysis of a segment of the track
        /// </summary>
        internal class Segment {
            /// <summary>
            /// Start time of this segment
            /// </summary>
            double Start { get; } = 0;

            /// <summary>
            /// Duration of this segment
            /// </summary>
            double Duration { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the analysis of this segment
            /// </summary>
            double Confidence { get; } = 0;

            /// <summary>
            /// Initial loudness of this segment
            /// </summary>
            double Loudness_Start { get; } = 0;

            /// <summary>
            /// Amount of time this segment is at its maximum loudness
            /// </summary>
            double Loudness_Max_Time { get; } = 0;

            /// <summary>
            /// The maximiaml loudness of this segment
            /// </summary>
            double Loudness_Max { get; } = 0;

            /// <summary>
            /// The final loduness of this segment
            /// </summary>
            double Loudness_End { get; } = 0;

            /// <summary>
            /// Pitches
            /// </summary>
            IReadOnlyList<double> Pitches { get; } = new List<double>();

            /// <summary>
            /// Timbres
            /// </summary>
            IReadOnlyList<double> Timbres { get; } = new List<double>();

            internal Segment(JToken token) {
                this.Start = token.Value<double?>("start") ?? 0;
                this.Duration = token.Value<double?>("duration") ?? 0;
                this.Confidence = token.Value<double?>("confidence") ?? 0;
                this.Loudness_Start = token.Value<double?>("loudness_start") ?? 0;
                this.Loudness_Max_Time = token.Value<double?>("loudness_max_time") ?? 0;
                this.Loudness_Max = token.Value<double?>("loudness_max") ?? 0;
                this.Loudness_End = token.Value<double?>("loudness_end") ?? 0;
                //Pitches
                JArray jarr_pitch = token.Value<JArray>("pitches");
                if(jarr_pitch != null) {
                    List<double> pitch = new List<double>();
                    foreach(JValue jv in jarr_pitch) {
                        double pitchVar;
                        bool success =double.TryParse(jv.ToString(), out pitchVar);
                        if(success) {
                            pitch.Add(pitchVar);
                        }
                    }
                    this.Pitches = pitch;
                }
                //Timbres
                JArray jarr_timbre = token.Value<JArray>("timbre");
                if (jarr_timbre != null) {
                    List<double> timbre = new List<double>();
                    foreach (JValue jv in jarr_timbre) {
                        double timVar;
                        bool success = double.TryParse(jv.ToString(), out timVar);
                        if (success) {
                            timbre.Add(timVar);
                        }
                    }
                    this.Timbres = timbre;
                }
            }
        }

        /// <summary>
        /// Tatum object of an Audio Analysis object
        /// Contains information like the start time (in milliseconds)
        /// The duration (in milliseconds)
        /// And the confidence score of the tatum
        /// </summary>
        internal class Tatum {

            /// <summary>
            /// Start time of this tatum (in ms)
            /// </summary>
            double Start { get; } = 0;

            /// <summary>
            /// Length of this tatum in milliseconds
            /// </summary>
            double Duration { get; } = 0;

            /// <summary>
            /// Spotify's confidence in this tatum's values
            /// </summary>
            double Confidence { get; } = 0;

            internal Tatum(JToken token) {
                this.Start = token.Value<double?>("start") ?? 0;
                this.Duration = token.Value<double?>("duration") ?? 0;
                this.Confidence = token.Value<double?>("confidence") ?? 0;
            }
        }

        /// <summary>
        /// Track data for this analysis object
        /// </summary>
        internal class AnalysisTrack {
            /// <summary>
            /// Number of samples in this track
            /// </summary>
            int Num_Samples { get; } = 0;

            /// <summary>
            /// Duration in seconds of this track
            /// </summary>
            double Duration { get; } = 0;

            /// <summary>
            /// MD5 of this sample
            /// </summary>
            string Sample_MD5 { get; } = string.Empty;

            /// <summary>
            /// Amount of time this sample has been offset from the the beginning the track (00:00)
            /// </summary>
            double Offset_Seconds { get; } = 0;

            /// <summary>
            /// TODO what is this
            /// </summary>
            double Window_Seconds { get; } = 0;

            /// <summary>
            /// Sampling rate of the analyzer
            /// </summary>
            int Analysis_Sample_Rate { get; } = 0;

            /// <summary>
            /// Amount of channels in this sample
            /// </summary>
            int Analysis_Channels { get; } = 0;

            /// <summary>
            /// Amount of seconds into the track that the fade-in (if any) ends and the music begins
            /// </summary>
            double End_Of_Fade_In { get; } = 0;

            /// <summary>
            /// Amount of seconds into the track that the tracks Fade-Out (if any) begin. This signifies the end of the track.
            /// </summary>
            double Start_Of_Fade_Out { get; } = 0;

            /// <summary>
            /// Average loudness of this track (-100 : 100)
            /// </summary>
            double Loudness { get; } = 0;

            /// <summary>
            /// Spotify's confidence in this loudness analysis. n.b.: might not exist
            /// </summary>
            double Loudness_Confidence { get; } = 0;

            /// <summary>
            /// Average Tempo of this track (0 : 500)
            /// </summary>
            double Tempo { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the tempo analysis
            /// </summary>
            double Tempo_Confidence { get; } = 0;

            /// <summary>
            /// Average Key of this track (1-5)
            /// </summary>
            int Key { get; } = 0;

            /// <summary>
            /// Spotfiy's condfience in the key analysis
            /// </summary>
            double Key_Confidence { get; } = 0;

            /// <summary>
            /// Average Mode of the track (0-1)
            /// </summary>
            int Mode { get; } = 0;

            /// <summary>
            /// Spotfiy's confidence in the Mode of this track
            /// </summary>
            double Mode_Confidence { get; } = 0;

            /// <summary>
            /// Time Signature of this track (-1 : 5)
            /// </summary>
            int Time_Signature { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the time signature analysis
            /// </summary>
            double Time_Signature_Confidence { get; } = 0;

            /// <summary>
            /// Code print of the track
            /// </summary>
            string Code_String { get; } = string.Empty;

            /// <summary>
            /// Version of the code-printer
            /// </summary>
            double Code_Version { get; } = 0;

            /// <summary>
            /// Echoprint string
            /// </summary>
            string Echoprint_String { get; } = string.Empty;

            /// <summary>
            /// version of the echo-printer
            /// </summary>
            double Echoprint_Version { get; } = 0;

            /// <summary>
            /// Synch String
            /// </summary>
            string Synch_String { get; } = string.Empty;

            /// <summary>
            /// Version of the Synch Printer
            /// </summary>
            double Sync_Version { get; } = 0;

            /// <summary>
            /// Rhythm String
            /// </summary>
            string Rhythm_String { get; } = string.Empty;

            /// <summary>
            /// version of the Rhythm Printer
            /// </summary>
            double Rhythm_Version { get; } = 0;

            internal AnalysisTrack(JToken token) {
                this.Num_Samples = token.Value<int?>("num_samples") ?? 0;
                this.Duration = token.Value<double?>("duration") ?? 0;
                this.Sample_MD5 = token.Value<string>("sample_md5") ?? string.Empty;
                this.Offset_Seconds = token.Value<double?>("offset_seconds") ?? 0;
                this.Window_Seconds = token.Value<double?>("window_seconds") ?? 0;
                this.Analysis_Sample_Rate = token.Value<int?>("analysis_sample_rate") ?? 0;
                this.Analysis_Channels = token.Value<int?>("analysis_channels") ?? 0;
                this.End_Of_Fade_In = token.Value<double?>("end_of_fade_in") ?? 0;
                this.Start_Of_Fade_Out = token.Value<double?>("start_of_fade_out") ?? 0;
                this.Loudness = token.Value<double?>("loudness") ?? 0;
                this.Loudness_Confidence = token.Value<double?>("loudness_confidence") ?? 0;
                this.Tempo = token.Value<double?>("tempo") ?? 0;
                this.Tempo_Confidence = token.Value<double?>("tempo_confidence") ?? 0;
                this.Key = token.Value<int?>("key") ?? 0;
                this.Key_Confidence = token.Value<double?>("key_confidence") ?? 0;
                this.Mode = token.Value<int?>("mode") ?? 0;
                this.Mode_Confidence = token.Value<double?>("mode_confidence") ?? 0;
                this.Time_Signature = token.Value<int?>("time_signature") ?? 0;
                this.Time_Signature_Confidence = token.Value<double?>("time_signature_confidence") ?? 0;
                this.Code_String = token.Value<string>("codestring") ?? string.Empty;
                this.Code_Version = token.Value<double?>("code_version") ?? 0;
                this.Echoprint_String = token.Value<string>("echoprintstring") ?? string.Empty;
                this.Echoprint_Version = token.Value<double?>("echoprint_version") ?? 0;
                this.Synch_String = token.Value<string>("synchstring") ?? string.Empty;
                this.Sync_Version = token.Value<double?>("synch_version") ?? 0;
                this.Rhythm_String = token.Value<string>("rhythmstring") ?? string.Empty;
                this.Rhythm_Version = token.Value<double?>("rhythm_version") ?? 0;
            }
        }

        public AudioAnalysis(JToken token) {
            //Metadata
            JObject meta = token.Value<JObject>("meta");
            if(meta != null) {
                this.Metadata = new Meta(meta);
            }

            //Analysis Track
            JObject track = token.Value<JObject>("track");
            if (track != null) {
                this.Track = new AnalysisTrack(track);
            }

            //Bars
            JArray jbars = token.Value<JArray>("bars");
            if(jbars != null) {
                List<Bar> arr = new List<Bar>();
                foreach(JObject jobj in jbars) {
                    Bar b = new Bar(jobj);
                    arr.Add(b);
                }
                this.Bars = arr;
            }

            //Segements
            JArray jSegs = token.Value<JArray>("segments");
            if (jSegs != null) {
                List<Segment> arr = new List<Segment>();
                foreach (JObject jobj in jSegs) {
                    Segment b = new Segment(jobj);
                    arr.Add(b);
                }
                this.Segments = arr;
            }

            //Sections
            JArray jsecs = token.Value<JArray>("sections");
            if (jsecs != null) {
                List<Section> arr = new List<Section>();
                foreach (JObject jobj in jsecs) {
                    Section b = new Section(jobj);
                    arr.Add(b);
                }
                this.Sections = arr;
            }
            //Tatums
            JArray jtatums = token.Value<JArray>("tatums");
            if (jtatums != null) {
                List<Tatum> arr = new List<Tatum>();
                foreach (JObject jobj in jtatums) {
                    Tatum b = new Tatum(jobj);
                    arr.Add(b);
                }
                this.Tatums = arr;
            }
        }
    }



}
