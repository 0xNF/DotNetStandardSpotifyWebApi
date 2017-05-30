using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public class Album : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetAlbum = baseUrl + "/v1/albums/{0}";
        private const string api_GetAlbums = baseUrl + "/v1/albums?ids={0}";
        private const string api_GetAlbumsTracks = baseUrl + "/v1/albums/{0}/tracks";

        /// <summary>
        /// The type of the album: one of "album", "single", or "compilation". 
        /// </summary>
        public string Album_Type { get; } = string.Empty;

        /// <summary>
        /// The artists of the album. Each artist object includes a link in href to more detailed information about the artist.
        /// </summary>
        public Artist[] Artists { get; } = new Artist[0];

        /// <summary>
        /// The markets in which the album is available: ISO 3166-1 alpha-2 country codes.
        /// Note that an album is considered available in a market when at least 1 of its tracks is available in that market.
        /// </summary>
        public string[] Available_Markets { get; } = new string[0];

        /// <summary>
        /// The copyright statements of the album.
        /// </summary>
        public Copyright[] Copyrights { get; } = new Copyright[0];

        /// <summary>
        /// Known external URLs for the album. 
        /// </summary>
        public External_Url[] External_Urls { get; } = new External_Url[0];

        /// <summary>
        /// Known external IDs for the album.
        /// </summary>
        public External_Id[] External_Ids { get; } = new External_Id[0];

        /// <summary>
        /// A list of the genres used to classify the album. 
        /// For example: "Prog Rock", "Post-Grunge". (If not yet classified, the array is empty.) 
        /// </summary>
        public string[] Genres { get; } = new string[0];

        /// <summary>
        /// A link to the Web API endpoint providing full details of the album.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The Spotify ID for the album. 
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// The cover art for the album in various sizes, widest first.
        /// </summary>
        public Image[] Images { get; } = new Image[0];

        /// <summary>
        /// The label for the album.
        /// </summary>
        public string Label { get; } = string.Empty;

        /// <summary>
        /// The name of the album. 
        /// In case of an album takedown, the value may be an empty string.
        /// </summary>
        public string Name { get; } = string.Empty;

        /// <summary>
        /// The popularity of the album. 
        /// The value will be between 0 and 100, with 100 being the most popular. 
        /// The popularity is calculated from the popularity of the album's individual tracks.
        /// </summary>
        public int Popularity { get; } = 0;

        /// <summary>
        /// The date the album was first released, for example "1981-12-15". 
        /// Depending on the precision, it might be shown as "1981" or "1981-12".
        /// </summary>
        public string Release_Date { get; } = string.Empty;

        /// <summary>
        /// The precision with which release_date value is known: "year", "month", or "day".
        /// </summary>  bj
        public string Release_Date_Precision { get; } = string.Empty;

        /// <summary>
        /// The tracks of the album.
        /// TODO is a Paging object, not a raw list of Tracks
        /// </summary>
        public Track[] Tracks { get; } = new Track[0];

        /// <summary>
        /// The object type: "album"
        /// </summary>
        public string Type { get; } = "album";

        /// <summary>
        /// The Spotify URI for the album.
        /// </summary>
        public string Uri { get; } = string.Empty;


        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public Album(JToken token) {
            /* simple fields */
            Album_Type = token.Value<string>("album_type") ?? string.Empty;
            Href = token.Value<string>("href") ?? string.Empty;
            Id = token.Value<string>("id") ?? string.Empty;
            Label = token.Value<string>("label") ?? string.Empty;
            Name = token.Value<string>("name") ?? string.Empty;
            Popularity = token.Value<int?>("popularity") ?? 0;
            Release_Date = token.Value<string>("release_date") ?? string.Empty;
            Release_Date_Precision = token.Value<string>("release_date_precision") ?? string.Empty;
            Uri = token.Value<string>("uri") ?? string.Empty;

            /* complex fields */
            //Genre
            //Artists
            //available markets
            //copyrights
            //extids
            //extruls
            //images
            //tracks

        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Album() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Album(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }
    }
}
