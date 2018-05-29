using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Device : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// The device ID. This may be empty.
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// If this device is the currently active device.
        /// </summary>
        public bool Is_Active { get; } = false;

        /// <summary>
        /// Whether controlling this device is restricted.
        /// At present if this is "true" then no Web API commands will be accepted by this device.
        /// </summary>
        public bool Is_Restricted { get; } = true;

        /// <summary>
        /// The name of the device.
        /// </summary>
        public string Name { get; } = string.Empty;

        /// <summary>
        /// Device type, such as "Computer", "Smartphone" or "Speaker".
        /// </summary>
        public string Type { get; } = string.Empty;

        /// <summary>
        /// The current volume in percent. This may be null.
        /// </summary>
        public int? Volume_Percent { get; } = 0;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Device() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Device(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public Device(JToken token) {
            Id = token.Value<string>("id") ?? string.Empty;
            Is_Active = token.Value<bool?>("is_active") ?? false;
            Is_Restricted = token.Value<bool?>("is_restricted") ?? true;
            Name = token.Value<string>("name") ?? string.Empty;
            Type = token.Value<string>("type") ?? string.Empty;
        }

        public JToken ToJson() {
            Dictionary<string, object> keys = new Dictionary<string, object>() {
                { "id", this.Id },
                { "is_active", this.Is_Active },
                { "is_restricted", this.Is_Restricted },
                { "name", this.Name },
                { "type", this.Type }
            };
            return JObject.FromObject(keys);
        }
    }

}
