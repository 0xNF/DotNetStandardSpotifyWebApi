using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {

    public class External_Url : SpotifyObjectModel, ISpotifyObject {
        
        /// <summary>
        /// The type of the URL, for example: "spotify"
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// An external, public URL to the object.
        /// </summary>
        public string Value { get; }

        public External_Url(string key, string value) {
            this.Key = key;
            this.Value = value;
        }


        public static External_Url[] FromJObject(JObject token) {
            if(token == null){
                return new External_Url[0];
            }else{
                List<External_Url> urls = new List<External_Url>();
                foreach(JProperty prop in token.Properties()){
                    string n = prop.Name;
                    string v = token.Value<string>(n) ?? string.Empty;               
                    urls.Add(new External_Url(prop.Name, v));
                }
                return urls.ToArray();
            }
        }

    }


    public class External_Id : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// The identifier type, for example:
        ///    "isrc" - International Standard Recording Code
        ///    "ean" - International Article Number
        ///    "upc" - Universal Product Code
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// An external identifier for the object.
        /// </summary>
        public string Value { get; }

        public External_Id(string key, string value) {
            this.Key = key;
            this.Value = value;
        }


        public static External_Id[] FromJObject(JObject token) {
            if (token == null) {
                return new External_Id[0];
            }
            else {
                List<External_Id> urls = new List<External_Id>();
                foreach (JProperty prop in token.Properties()) {
                    string n = prop.Name;
                    string v = token.Value<string>(n) ?? string.Empty;
                    urls.Add(new External_Id(prop.Name, v));
                }
                return urls.ToArray();
            }
        }

    }
}
