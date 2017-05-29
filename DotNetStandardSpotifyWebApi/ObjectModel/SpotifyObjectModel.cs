using System;
using System.Collections.Generic;
using System.Text;
using DotNetStandardSpotifyWebApi.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public abstract class SpotifyObjectModel {
        internal const string baseUrl = "https://api.spotify.com";
        public bool WasError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

    }

    public interface ISpotifyObject {

    }
}
