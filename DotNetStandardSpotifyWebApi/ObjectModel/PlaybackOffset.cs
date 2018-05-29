using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

namespace DotNetStandardSpotifyWebApi.ObjectModel {

    public abstract class PlaybackOffset : SpotifyObjectModel, ISpotifyObject {
        //public abstract Dictionary<string,string> ToDictionary();

        public JToken ToJson() {
            return "";
        }
    }

    public abstract class PlaybackOffset<T> : PlaybackOffset, ISpotifyObject {
        public abstract T Offset { get; }
    }


    public class PositionOffset : PlaybackOffset<int> {
        public override int Offset { get; } = 0;

        public PositionOffset(int offset) {
            Offset = offset;
        }

        public override string ToString() {
            Dictionary<string, int> retDict = new Dictionary<string, int>() {
                {"position",  Offset}
            };
            JObject jobj = JObject.FromObject(retDict);
            return jobj.ToString();
        }

        public new JToken ToJson() {
            return ToString();
        }

    }

    public class UriOffset : PlaybackOffset<string> {

        public override string Offset { get; } = string.Empty;

        public UriOffset(string offset) {
            Offset = offset;
        }

        public UriOffset(Track t) {
            Offset = t.Uri;
        }

        public override string ToString() {
            Dictionary<string, string> offsetDict = new Dictionary<string, string>() {
                {"uri", Offset }
            };
            Dictionary<string, string> retDict = new Dictionary<string, string>() {
                {"offset",  JObject.FromObject(offsetDict).ToString() }
            };
            JObject jobj = JObject.FromObject(retDict);
            return jobj.ToString();
        }

        public new JToken ToJson() {
            return ToString();
        }
    }
}
