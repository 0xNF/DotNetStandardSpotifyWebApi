using System;
using System.Collections.Generic;
using System.Text;
using DotNetStandardSpotifyWebApi.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {

    public class User : SpotifyObjectModel{

        private const string api_CurrentProfile = baseUrl + "/v1/me";
        private const string api_FollowedArtists = baseUrl + "/v1/me/following";
        private const string api_FollowingArtistsContains = baseUrl + "/v1/me/following/contains";
        private const string api_FollowPlaylist = baseUrl + "/v1/users/{0}/playlists/{1}/followers";
        private const string api_SaveTracksToLibrary = baseUrl + "/v1/me/tracks?ids={0}";
        private const string api_GetSavedTracks = baseUrl + "/v1/me/tracks";
        private const string api_CheckSavedTracks = baseUrl + "/v1/me/tracks/contains?ids={0}";
        private const string api_SaveAlbums = baseUrl + "/v1/me/albums?ids={0}";
        private const string api_GetSavedAlbums = baseUrl + "/v1/me/albums";
        private const string api_CheckSavedAlbums = baseUrl + "/v1/me/albums/contains?ids={0}";
        private const string api_GetTop = baseUrl + "/v1/me/top/{0}"; //either 'artists' or 'tracks'
        private const string api_GetPublicProfile = baseUrl + "/v1/users/{0}";
        private const string api_GetPublicPlaylists = baseUrl + "/v1/users/{0}/playlists";
        private const string api_GetPlaylists = baseUrl + "/v1/me/playlists";
        private const string api_CheckUserFollowsPlaylist = baseUrl + "/v1/users/{0}/playlists/{1}/followers/contains";
        private const string api_GetRecentlyPlayedTracks = baseUrl + "v1/me/player/recently-played";

        /// <summary>
        /// The user's date-of-birth.
        /// This field is only available when the current user has granted access to the USER_READ_BIRTHDATE scope.
        /// </summary>
        public string Birthdate { get; private set; } = string.Empty;

        /// <summary>
        /// The country of the user, as set in the user's account profile.
        /// An ISO 3166-1 alpha-2 country code. 
        /// This field is only available when the current user has granted access to the USER_READ_PRIVATE scope.
        /// </summary>
        public string Country { get; private set; } = string.Empty;

        /// <summary>
        /// The name displayed on the user's profile. null if not available.
        /// </summary>
        public string DisplayName { get; private set; } = string.Empty;

        /// <summary>
        /// The user's email address, as entered by the user when creating their account.
        /// Important! This email address is unverified; there is no proof that it actually belongs to the user.
        /// This field is only available when the current user has granted access to the USER_READ_EMAIL scope.
        /// </summary>
        public string Email { get; private set; } = string.Empty;

        /// <summary>
        /// Information about the followers of the user. 
        /// </summary>
        public Followers Followers { get; private set; }

        /// <summary>
        /// Known external URLs for this user.
        /// </summary>
        public ExternalUrl[] External_urls { get; private set; }

        /// <summary>
        /// A link to the Web API endpoint for this user.
        /// </summary>
        public string Href { get; private set; } = string.Empty;

        /// <summary>
        /// The Spotify user ID for the user. 
        /// </summary>
        public string Id { get; private set; } = string.Empty;

        //TODO - Images
        /// <summary>
        /// The user's profile image. 
        /// </summary>
        public string Images { get; private set; } = string.Empty;

        /// <summary>
        /// The user's Spotify subscription level: "premium", "free", etc. (The subscription level "open" can be considered the same as "free".) 
        /// This field is only available when the current user has granted access to the USER_READ_PRIVATE scope.
        /// </summary>
        public string Product { get; private set; } = string.Empty;

        /// <summary>
        /// The object type: "user" 
        /// </summary>
        public string Type { get; } = "user";

        /// <summary>
        /// The Spotify URI for the user.
        /// </summary>
        public string Uri { get; private set; } = string.Empty;


        public static async Task<User> GetCurrentUser(string accessToken) {
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(api_CurrentProfile, accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);

            User user;
            if (response.IsSuccessStatusCode) {
                JToken j = await WebRequestHelpers.ParseJsonResponse(response.Content);
                user = new User {
                    Birthdate = j.Value<string>("birthdate") ?? string.Empty,
                    Country = j.Value<string>("country") ?? string.Empty,
                    DisplayName = j.Value<string>("display_name") ?? string.Empty,
                    Email = j.Value<string>("email") ?? string.Empty,
                    External_urls = ExternalUrl.FromToken(j.Value<JObject>("external_urls")),
                    Followers = new Followers(j.Value<JToken>("followers")) ?? null,
                    // Images = j.Value<string>("images") ?? string.Empty,
                    Product = j.Value<string>("product") ?? string.Empty,

                    Href = j.Value<string>("href") ?? string.Empty,
                    Id = j.Value<string>("id") ?? string.Empty,
                    Uri = j.Value<string>("uri") ?? string.Empty
                };
            }
            else {
                user = new User {
                    WasError = true,
                    ErrorMessage = response.ReasonPhrase
                };
            }
            return user;
        }

        public static async Task<User> GetUser(string user_id, string accessToken) {
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(string.Format(api_GetPublicProfile, user_id), accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);

            User user;
            if (response.IsSuccessStatusCode) {
                JToken j = await WebRequestHelpers.ParseJsonResponse(response.Content);
                user = new User {
                    DisplayName = j.Value<string>("display_name") ?? string.Empty,

                    External_urls = ExternalUrl.FromToken(j.Value<JObject>("external_urls")),
                    Followers = new Followers(j.Value<JToken>("followers")) ?? null,
                    Images = j.Value<string>("images") ?? string.Empty,

                    Href = j.Value<string>("href") ?? string.Empty,
                    Id = j.Value<string>("id") ?? string.Empty,
                    Uri = j.Value<string>("uri") ?? string.Empty
                };
            }
            else {
                user = new User {
                    WasError = true,
                    ErrorMessage = response.ReasonPhrase
                };
            }
            return user;
        }

    }

    public class Album : SpotifyObjectModel {
        private const string api_GetAlbum = baseUrl + "/v1/albums/{0}";
        private const string api_GetAlbums = baseUrl + "/v1/albums?ids={0}";
        private const string api_GetAlbumsTracks = baseUrl + "/v1/albums/{0}/tracks";
    }

    public class Artist : SpotifyObjectModel {
        private const string api_GetArtist = baseUrl + "/v1/artists/{0}";
        private const string api_GetArtists = baseUrl + "/v1/artists?ids={0}";
        private const string api_GetArtistsAlbums = baseUrl + "/v1/artists/{0}/albums";
        private const string api_GetArtistsTopTracks = baseUrl + "/v1/artists/{0}/top-tracks";
        private const string api_GetRelatedArtists = baseUrl + "/v1/artists/{0}/related-artists";
    }

    public class Track : SpotifyObjectModel {
        private const string api_GetTrack = baseUrl + "/v1/tracks/{0}";
        private const string api_GetTracks = baseUrl + "/v1/tracks?ids={0}";

    }

    public class AudioAnalysis : SpotifyObjectModel {
        private const string api_GetAudioAnalysis = baseUrl + "/v1/audio-analysis/{0}";
    }

    public class AudioFeatures : SpotifyObjectModel {
        private const string api_GetAudioFeature = baseUrl + "/v1/audio-features/{0}";
        private const string api_GetAudioFeatures = baseUrl + "/v1/audio-features?ids={0}";
    }

    public class Browse : SpotifyObjectModel {
        private const string api_GetFeaturedPlaylsits = baseUrl + "/v1/browse/featured-playlists";
        private const string api_GetNewReleases = baseUrl + "/v1/browse/new-releases";
        private const string api_GetCategories = baseUrl + "/v1/browse/categories";
        private const string api_GetCategory = baseUrl + "/v1/browse/categories/{0}";
        private const string api_GetCategoryPlaylists = baseUrl + "/vi/browse/categories/{0}/playlists";
    }

    public class Recommendation : SpotifyObjectModel {
        private const string api_GetRecommendations = baseUrl + "/v1/recommendations";
    }

    public class Search : SpotifyObjectModel {
        private const string api_SearchAlbum = baseUrl + "/v1/search?type=album";
        private const string api_SearchArtist = baseUrl + "/v1/search?type=artist";
        private const string api_SearchTrack = baseUrl + "/v1/search?type=track";
        private const string api_SearchPlaylist = baseUrl + "/v1/search?type=playlist";
    }

    public class Playlist : SpotifyObjectModel {
        private const string api_GetPlaylist = baseUrl + "/v1/users/{0}/playlists/{1}";
        private const string api_GetPlaylistsTracks = baseUrl + "/v1/users/{0}/playlists/{1}/tracks";
        private const string api_CreatePlaylist = baseUrl + "/v1/users/{0}/playlists";
    }

    public class Playback : SpotifyObjectModel {
        private const string api_GetAvailableDevices = baseUrl + "/v1/me/player/devices";
        private const string api_GetPlayer = baseUrl + "/v1/me/player";
        private const string api_CurrentlyPlaying = baseUrl + "/v1/me/player/currently-playing";
        private const string api_StopStart = baseUrl + "/v1/me/player/play";
        private const string api_Pause = baseUrl + "/v1/me/player/pause";
        private const string api_Next = baseUrl + "/v1/me/player/next";
        private const string api_Previous = baseUrl + "/v1/me/player/previous";
        private const string api_Seek = baseUrl + "v1/me/player/seek";
        private const string api_SetRepeat = baseUrl + "/v1/me/player/repeat";
        private const string api_SetVolume = baseUrl + "/v1/me/player/volume";
        private const string api_SetShuffle = baseUrl + "/v1/me/player/shuffle";
    }
}
