using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {

    public class ExternalUrl : SpotifyObjectModel {
        
        /// <summary>
        /// The type of the URL, for example: "spotify"
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// An external, public URL to the object.
        /// </summary>
        public string Value { get; }

        public ExternalUrl(string key, string value) {
            this.Key = key;
            this.Value = value;
        }


        public static ExternalUrl[] FromToken(JObject token) {
            if(token == null){
                return new ExternalUrl[0];
            }else{
                List<ExternalUrl> urls = new List<ExternalUrl>();
                foreach(JProperty prop in token.Properties()){
                    string n = prop.Name;
                    string v = token.Value<string>(n) ?? string.Empty;               
                    urls.Add(new ExternalUrl(prop.Name, v));
                }
                return urls.ToArray();
            }
        }

    }
}
