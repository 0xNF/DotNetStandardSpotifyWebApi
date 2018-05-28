using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class AudioAnalysis : SpotifyObjectModel, ISpotifyObject {

        public IReadOnlyList<Bar> Bars { get; } = new List<Bar>();
        public IReadOnlyList<Beat> Beats { get; } = new List<Beat>();
        public IReadOnlyList<Section> Sections { get; } = new List<Section>();
        public IReadOnlyList<Segment> Segments { get; } = new List<Segment>();
        public IReadOnlyList<Tatum> Tatums { get; } = new List<Tatum>();
        public Meta Metadata { get; } = null;
        public AnalysisTrack Track { get; } = null;

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
        public class Bar {

            /// <summary>
            /// Start time of this bar (in ms)
            /// </summary>
            public double Start { get; } = 0;

            /// <summary>
            /// Length of this bar in milliseconds
            /// </summary>
            public double Duration { get; } = 0;

            /// <summary>
            /// Spotify's confidence in this bar's values
            /// </summary>
            public double Confidence { get; } = 0;

            internal Bar(JToken token) {
                this.Start = token.Value<double?>("start") ?? 0;
                this.Duration = token.Value<double?>("duration") ?? 0;
                this.Confidence = token.Value<double?>("confidence") ?? 0;
            }

            public JObject ToJson() {
                Dictionary<string, JToken> keys = new Dictionary<string, JToken>() {
                    {"start", this.Start },
                    {"duration", this.Duration },
                    {"confidence", this.Confidence }
                };
                return JObject.FromObject(keys);
            }
        }

        /// <summary>
        /// Beat object of Audio Analysis object. Values measurd in milliseconds
        /// </summary>
        public class Beat {

            /// <summary>
            /// Start time of this beat (in ms)
            /// </summary>
            public double Start { get; } = 0;

            /// <summary>
            /// Length of this beat in milliseconds
            /// </summary>
            public double Duration { get; } = 0;

            /// <summary>
            /// Spotify's confidence in this beats's values
            /// </summary>
            public double Confidence { get; } = 0;

            internal Beat(JToken token) {
                this.Start = token.Value<double?>("start") ?? 0;
                this.Duration = token.Value<double?>("duration") ?? 0;
                this.Confidence = token.Value<double?>("confidence") ?? 0;
            }

            public JObject ToJson() {
                Dictionary<string, JToken> keys = new Dictionary<string, JToken>() {
                    {"start", this.Start },
                    {"duration", this.Duration },
                    {"confidence", this.Confidence }
                };
                return JObject.FromObject(keys);
            }
        }

        /// <summary>
        /// Metadata information about the audio analysis
        /// </summary>
        public class Meta {

            /// <summary>
            /// Version number of the analyzer that produced this analysis object
            /// </summary>
            public string Analyzer_Version { get; } = string.Empty;

            /// <summary>
            /// OS object was analyzed on
            /// </summary>
            public string Platform { get; } = string.Empty;

            /// <summary>
            /// Status message of the analysis
            /// </summary>
            public string Detailed_Status { get; } = string.Empty;

            /// <summary>
            /// Return code of the analysis.
            /// </summary>
            public int Status_Code { get; } = -1;

            /// <summary>
            /// UNIX datetime the analysis was performed
            /// </summary>
            public long Timestamp { get; } = 0;

            /// <summary>
            /// Amount of time the analysis took
            /// </summary>
            public double Analysis_Time { get; } = 0;

            /// <summary>
            /// Information related to the analysis command
            /// </summary>
            public string Input_Process { get; } = string.Empty;

            internal Meta(JToken token) {
                this.Analyzer_Version = token.Value<string>("analyzer_version") ?? string.Empty;
                this.Platform = token.Value<string>("platform") ?? string.Empty;
                this.Status_Code = token.Value<int?>("status_code") ?? -1;
                this.Timestamp = token.Value<long?>("timestamp") ?? 0;
                this.Analysis_Time = token.Value<double?>("analysis_time") ?? 0;
                this.Input_Process = token.Value<string>("input_process") ?? string.Empty;
                this.Detailed_Status = token.Value<string>("detailed_status") ?? string.Empty;
            }

            public JObject ToJson() {
                Dictionary<string, JToken> keys = new Dictionary<string, JToken>() {
                    {"analyzer_version", this.Analyzer_Version },
                    {"platform", this.Platform },
                    {"status_code", this.Status_Code },
                    {"timestamp", this.Timestamp },
                    {"analysis_time", this.Analysis_Time },
                    {"input_process", this.Input_Process },
                    {"detailed_status", this.Detailed_Status }
                };
                return JObject.FromObject(keys);
            }
        }

        /// <summary>
        /// Analysis of a section of the track
        /// </summary>
        public class Section {
            /// <summary>
            /// Start time of this section
            /// </summary>
            public double Start { get; } = 0;

            /// <summary>
            /// Duration of this section
            /// </summary>
            public double Duration { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the analysis of this section
            /// </summary>
            public double Confidence { get; } = 0;

            /// <summary>
            /// Average loudness of this section (-100 : 100)
            /// </summary>
            public double Loudness { get; } = 0;

            /// <summary>
            /// Spotify's confidence in this loudness analysis. n.b.: might not exist
            /// </summary>
            public double Loudness_Confidence { get; } = 0;

            /// <summary>
            /// Average Tempo of this section (0 : 500)
            /// </summary>
            public double Tempo { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the tempo analysis
            /// </summary>
            public double Tempo_Confidence { get; } = 0;

            /// <summary>
            /// Average Key of this section (1-5)
            /// </summary>
            public int Key { get; } = 0;

            /// <summary>
            /// Spotfiy's condfience in the key analysis
            /// </summary>
            public double Key_Confidence { get; } = 0;

            /// <summary>
            /// Average Mode of the section (0-1)
            /// </summary>
            public int Mode { get; } = 0;

            /// <summary>
            /// Spotfiy's confidence in the Mode of this section
            /// </summary>
            public double Mode_Confidence { get; } = 0;

            /// <summary>
            /// Time Signature of this section (-1 : 5)
            /// </summary>
            public int Time_Signature { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the time signature analysis
            /// </summary>
            public double Time_Signature_Confidence { get; } = 0;

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

            public JObject ToJson() {
                Dictionary<string, JToken> keys = new Dictionary<string, JToken>() {
                    {"start", this.Start },
                    {"duration", this.Duration },
                    {"confidence", this.Confidence },
                    {"loudness", this.Loudness },
                    {"loudness_confidence", this.Loudness_Confidence },
                    {"tempo", this.Tempo },
                    {"tempo_confidence", this.Tempo_Confidence },
                    {"key", this.Key },
                    {"key_confidence", this.Key_Confidence },
                    {"mode", this.Mode },
                    {"mode_confidence", this.Mode_Confidence },
                    {"time_signature", this.Time_Signature },
                    {"time_signature_confidence", this.Time_Signature_Confidence },
                };
                return JObject.FromObject(keys);
            }
        }

        /// <summary>
        /// Analysis of a segment of the track
        /// </summary>
        public class Segment {
            /// <summary>
            /// Start time of this segment
            /// </summary>
            public double Start { get; } = 0;

            /// <summary>
            /// Duration of this segment
            /// </summary>
            public double Duration { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the analysis of this segment
            /// </summary>
            public double Confidence { get; } = 0;

            /// <summary>
            /// Initial loudness of this segment
            /// </summary>
            public double Loudness_Start { get; } = 0;

            /// <summary>
            /// Amount of time this segment is at its maximum loudness
            /// </summary>
            public double Loudness_Max_Time { get; } = 0;

            /// <summary>
            /// The maximiaml loudness of this segment
            /// </summary>
            public double Loudness_Max { get; } = 0;

            /// <summary>
            /// The final loduness of this segment
            /// </summary>
            public double Loudness_End { get; } = 0;

            /// <summary>
            /// Pitches
            /// </summary>
            public IReadOnlyList<double> Pitches { get; } = new List<double>();

            /// <summary>
            /// Timbres
            /// </summary>
            public IReadOnlyList<double> Timbres { get; } = new List<double>();

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

            public JObject ToJson() {
                JArray pitchArr = new JArray();
                foreach(double p in this.Pitches) {
                    pitchArr.Add(p);
                }
                JArray timbreArr = new JArray();
                foreach(double t in this.Timbres) {
                    timbreArr.Add(t);
                }
                Dictionary<string, JToken> keys = new Dictionary<string, JToken>() {
                    {"start", this.Start },
                    {"duration", this.Duration },
                    {"confidence", this.Confidence },
                    {"loudness_start", this.Loudness_Start },
                    {"loudness_max_time", this.Loudness_Max_Time },
                    {"loudness_max", this.Loudness_Max },
                    {"loudness_end", this.Loudness_End },
                    {"pitches", pitchArr },
                    {"timbre", timbreArr }
                };
                return JObject.FromObject(keys);
            }
        }

        /// <summary>
        /// Tatum object of an Audio Analysis object
        /// Contains information like the start time (in milliseconds)
        /// The duration (in milliseconds)
        /// And the confidence score of the tatum
        /// </summary>
        public class Tatum {

            /// <summary>
            /// Start time of this tatum (in ms)
            /// </summary>
            public double Start { get; } = 0;

            /// <summary>
            /// Length of this tatum in milliseconds
            /// </summary>
            public double Duration { get; } = 0;

            /// <summary>
            /// Spotify's confidence in this tatum's values
            /// </summary>
            public double Confidence { get; } = 0;

            internal Tatum(JToken token) {
                this.Start = token.Value<double?>("start") ?? 0;
                this.Duration = token.Value<double?>("duration") ?? 0;
                this.Confidence = token.Value<double?>("confidence") ?? 0;
            }

            public JObject ToJson() {
                Dictionary<string, JToken> keys = new Dictionary<string, JToken>() {
                    {"start", this.Start },
                    {"duration", this.Duration },
                    {"confidence", this.Confidence }
                };
                return JObject.FromObject(keys);
            }
        }

        /// <summary>
        /// Track data for this analysis object
        /// </summary>
        public class AnalysisTrack {
            /// <summary>
            /// Number of samples in this track
            /// </summary>
            public int Num_Samples { get; } = 0;

            /// <summary>
            /// Duration in seconds of this track
            /// </summary>
            public double Duration { get; } = 0;

            /// <summary>
            /// MD5 of this sample
            /// </summary>
            public string Sample_MD5 { get; } = string.Empty;

            /// <summary>
            /// Amount of time this sample has been offset from the the beginning the track (00:00)
            /// </summary>
            public double Offset_Seconds { get; } = 0;

            /// <summary>
            /// TODO what is this
            /// </summary>
            public double Window_Seconds { get; } = 0;

            /// <summary>
            /// Sampling rate of the analyzer
            /// </summary>
            public int Analysis_Sample_Rate { get; } = 0;

            /// <summary>
            /// Amount of channels in this sample
            /// </summary>
            public int Analysis_Channels { get; } = 0;

            /// <summary>
            /// Amount of seconds into the track that the fade-in (if any) ends and the music begins
            /// </summary>
            public double End_Of_Fade_In { get; } = 0;

            /// <summary>
            /// Amount of seconds into the track that the tracks Fade-Out (if any) begin. This signifies the end of the track.
            /// </summary>
            public double Start_Of_Fade_Out { get; } = 0;

            /// <summary>
            /// Average loudness of this track (-100 : 100)
            /// </summary>
            public double Loudness { get; } = 0;

            /// <summary>
            /// Spotify's confidence in this loudness analysis. n.b.: might not exist
            /// </summary>
            public double Loudness_Confidence { get; } = 0;

            /// <summary>
            /// Average Tempo of this track (0 : 500)
            /// </summary>
            public double Tempo { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the tempo analysis
            /// </summary>
            public double Tempo_Confidence { get; } = 0;

            /// <summary>
            /// Average Key of this track (1-5)
            /// </summary>
            public int Key { get; } = 0;

            /// <summary>
            /// Spotfiy's condfience in the key analysis
            /// </summary>
            public double Key_Confidence { get; } = 0;

            /// <summary>
            /// Average Mode of the track (0-1)
            /// </summary>
            public int Mode { get; } = 0;

            /// <summary>
            /// Spotfiy's confidence in the Mode of this track
            /// </summary>
            public double Mode_Confidence { get; } = 0;

            /// <summary>
            /// Time Signature of this track (-1 : 5)
            /// </summary>
            public int Time_Signature { get; } = 0;

            /// <summary>
            /// Spotify's confidence in the time signature analysis
            /// </summary>
            public double Time_Signature_Confidence { get; } = 0;

            /// <summary>
            /// Code print of the track
            /// </summary>
            public string Code_String { get; } = string.Empty;

            /// <summary>
            /// Version of the code-printer
            /// </summary>
            public double Code_Version { get; } = 0;

            /// <summary>
            /// Echoprint string
            /// </summary>
            public string Echoprint_String { get; } = string.Empty;

            /// <summary>
            /// version of the echo-printer
            /// </summary>
            public double Echoprint_Version { get; } = 0;

            /// <summary>
            /// Synch String
            /// </summary>
            public string Synch_String { get; } = string.Empty;

            /// <summary>
            /// Version of the Synch Printer
            /// </summary>
            public double Sync_Version { get; } = 0;

            /// <summary>
            /// Rhythm String
            /// </summary>
            public string Rhythm_String { get; } = string.Empty;

            /// <summary>
            /// version of the Rhythm Printer
            /// </summary>
            public double Rhythm_Version { get; } = 0;

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

            public JObject ToJson() {
                Dictionary<string, JToken> keys = new Dictionary<string, JToken>() {
                    {"num_samples", this.Num_Samples },
                    { "duration", this.Duration },
                    {"sample_md5", this.Sample_MD5 },
                    {"offset_seconds", this.Offset_Seconds },
                    {"window_seconds", this.Window_Seconds },
                    {"analysis_sample_rate", this.Analysis_Sample_Rate },
                    {"analysis_channels", this.Analysis_Channels },
                    {"end_of_fade_in", this.End_Of_Fade_In },
                    {"start_of_fade_out", this.Start_Of_Fade_Out },
                    {"loudness", this.Loudness },
                    {"loudness_confidence", this.Loudness_Confidence },
                    {"tempo", this.Tempo },
                    {"tempo_confidence", this.Tempo_Confidence },
                    {"key", this.Key },
                    {"key_confidence", this.Key_Confidence },
                    {"mode", this.Mode },
                    {"mode_confidence", this.Mode_Confidence },
                    {"time_signature", this.Time_Signature },
                    {"time_signature_confidence", this.Time_Signature_Confidence },
                    {"codestring", this.Code_String },
                    {"code_version", this.Code_Version },
                    {"echoprintstring", this.Echoprint_String },
                    {"echoprint_version", this.Echoprint_Version },
                    {"synchstring", this.Synch_String },
                    {"synch_version", this.Sync_Version },
                    {"rhythmstring", this.Rhythm_String },
                    {"rhythm_version", this.Rhythm_Version }
                };
                return JObject.FromObject(keys);
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

        public JObject ToJson() {
            Dictionary<string, JToken> keys = new Dictionary<string, JToken>() {
                {"meta", this.Metadata.ToJson()},
                {"track", this.Track.ToJson()}
            };
            JArray barArray = new JArray();
            foreach(Bar b in this.Bars) {
                barArray.Add(b.ToJson());
            }
            JArray segArray = new JArray();
            foreach (Segment seg in this.Segments) {
                segArray.Add(seg.ToJson());
            }
            JArray secArray = new JArray();
            foreach (Section sec in this.Sections) {
                secArray.Add(sec.ToJson());
            }
            JArray tatArray = new JArray();
            foreach (Tatum tat in this.Tatums) {
                tatArray.Add(tat.ToJson());
            }
            keys.Add("bars", barArray);
            keys.Add("segments", segArray);
            keys.Add("sections", secArray);
            keys.Add("tatums", tatArray);

            return JObject.FromObject(keys);
        }
    }



}
