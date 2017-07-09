using System;
using System.Collections.Generic;
using System.Text;
using static DotNetStandardSpotifyWebApi.Helpers.WebRequestHelpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
    public static class Endpoints {

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="accessToken">OAuth acccess token</param>
        /// <param name="id">Album Id</param>
        /// <param name="market">Market result is desired in</param>
        /// <returns></returns>
        public static async Task<Album> GetAnAlbum(string accessToken, string id, string market = "") {
            string endpoint = $"https://api.spotify.com/v1/albums/{id}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market",market}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint, id) + options;
            return await DoHTTP<Album>(req, accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// This endpoint can only take a maximum of 20 ids - excess ids will be ignored
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">List of Album Ids to get</param>
        /// <param name="market">Market results are desired in</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Album>> GetSeveralAlbums(string accessToken, IEnumerable<string> ids, string market = "") {
            int maxparams = 20;
            string endpoint = "https://api.spotify.com/v1/albums/?ids={0}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market",market}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint, string.Join(",",ids.Take(maxparams))) + options;
            return await DoSeveralHttp<Album>(req, "albums", accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information about an album’s tracks. Optional parameters can be used to limit the number of tracks returned.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <param name="limit">Optional. The maximum number of tracks to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first track to return. Default: 0 (the first object). Use with limit to get the next set of tracks. </param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public static async Task<Paging<Track>> GetAnAlbumsTracks(string accessToken, string id, int limit = 20, int offset = 0, string market = "") {
            string endpoint = "https://api.spotify.com/v1/albums/{0}/tracks";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market", market},
                {"limit", limit},
                {"offset", offset}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint, id) + options;
            return await DoHTTP<Paging<Track>>(req, accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <returns></returns>
        public static async Task<Artist> GetAnArtist(string accessToken, string id) {
            string endpoint = "https://api.spotify.com/v1/artists/{0}";
            string req = string.Format(endpoint, id);
            return await DoHTTP<Artist>(req, accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">Required. A list of the Spotify IDs for the artists. Maximum: 50 IDs.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Artist>> GetSeveralArtists(string accessToken, IEnumerable<string> ids) {
            int maxparams = 50;
            string endpoint = "https://api.spotify.com/v1/artists?ids={0}";
            string req = string.Format(endpoint, string.Join(",",ids.Take(maxparams)));
            return await DoSeveralHttp<Artist>(req, "artists", accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information about an artist’s albums. Optional parameters can be specified in the query string to filter and sort the response.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <param name="album_type">Optional. A comma-separated list of keywords that will be used to filter the response. 
        /// If not supplied, all album types will be returned. Valid values are: album, single, appears_on, compilation
        /// </param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Supply this parameter to limit the response to one particular geographical market. For example, for albums available in Sweden: market="SE".
        /// If not given, results will be returned for all markets and you are likely to get duplicate results per album, one for each market in which the album is available!</param>
        /// <param name="limit">Optional. The number of album objects to return. Default: 20. Minimum: 1. Maximum: 50. For example: limit=2</param>
        /// <param name="offset">Optional. The index of the first album to return. Default: 0 (i.e., the first album). Use with limit to get the next set of albums. </param>
        /// <returns></returns>
        public static async Task<Paging<Album>> GetArtistsAlbums(string accessToken, string id, string album_type = "", string market = "", int limit = 20, int offset = 0) {
            string endpoint = $"https://api.spotify.com/v1/artists/{id}/albums";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market", market},
                {"limit", limit},
                {"offset", offset},
                {"album_type", album_type }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Album>>(req, accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <param name="country">Required. The country: an ISO 3166-1 alpha-2 country code. </param>
        /// <returns></returns>
        public static async Task<IEnumerable<Track>> GetArtistsTopTracks(string accessToken, string id, string country="US") {
            string endpoint = $"https://api.spotify.com/v1/artists/{id}/top-tracks";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country}
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoSeveralHttp<Track>(req, "tracks", accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Artist>> GetRelatedArtists(string accessToken, string id) {
            string endpoint = $"https://api.spotify.com/v1/artists/{id}/related-artists";
            return await DoSeveralHttp<Artist>(endpoint, "artists", accessToken);
        }

        /// <summary>
        /// Get a detailed audio analysis for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">Required. A valid access token from the Spotify Accounts service: see the Web API Authorization Guide for details.</param>
        /// <returns></returns>
        public static async Task<AudioAnalysis> GetAudioAnalysis(string accessToken, string id) {
            string endpoint = $"https://api.spotify.com/v1/audio-analysis/{id}";
            return await DoHTTP<AudioAnalysis>(endpoint, accessToken);
        }

        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">Required. A valid access token from the Spotify Accounts service: see the Web API Authorization Guide for details.</param>
        /// <returns></returns>
        public static async Task<AudioFeatures> GetAudioFeatures(string accessToken, string id) {
            string endpoint = $"https://api.spotify.com/v1/audio-features/{id}";
            return await DoHTTP<AudioFeatures>(endpoint, accessToken);
        }

        /// <summary>
        /// Get audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">Required. A list of the Spotify IDs for the tracks. Maximum: 100 IDs.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<AudioFeatures>> GetSeveralAudioFeatures(string accessToken, IEnumerable<string> ids) {
            int maxparams = 100;
            string endpoint = "https://api.spotify.com/v1/audio-features/?ids={0}";
            string req = string.Format(endpoint, string.Join(",", ids.Take(maxparams)));
            return await DoSeveralHttp<AudioFeatures>(req, "audio_features", accessToken);
        }

        /// <summary>
        /// Get a list of Spotify featured playlists (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="locale">Optional. The desired language, consisting of a lowercase ISO 639 language code and an uppercase ISO 3166-1 alpha-2 country code, joined by an underscore. 
        /// For example: es_MX, meaning "Spanish (Mexico)". 
        /// Provide this parameter if you want the results returned in a particular language (where available).
        ///  Note that, if locale is not supplied, or if the specified language is not available, all strings will be returned in the Spotify default language (American English).
        /// The locale parameter, combined with the country parameter, may give odd results if not carefully matched. For example country=SE&locale=de_DE will return a list of categories relevant to Sweden but as German language strings.
        /// </param>
        /// <param name="country">Optional. A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want the list of returned items to be relevant to a particular country. If omitted, the returned items will be relevant to all countries.</param>
        /// <param name="timestamp">Optional. A timestamp in ISO 8601 format: yyyy-MM-ddTHH:mm:ss. Use this parameter to specify the user's local time to get results tailored for that specific date and time in the day. If not provided, the response defaults to the current UTC time. Example: "2014-10-23T09:00:00" for a user whose local time is 9AM.</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items. </param>
        /// <returns></returns>
        public static async Task<FeaturedPlaylists> GetFeaturedPlaylists(string accessToken, string locale = "", string country = "", string timestamp = "", int limit = 20, int offset = 0) {
            string endpoint = "https://api.spotify.com/v1/browse/featured-playlists";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country},
                {"locale", locale },
                {"timestamp", timestamp },
                {"limit",limit },
                {"offset", offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<FeaturedPlaylists>(req, accessToken);
        }

        /// <summary>
        /// Get a list of new album releases featured in Spotify (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="country">Optional. A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want the list of returned items to be relevant to a particular country. If omitted, the returned items will be relevant to all countries.</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items. </param>
        /// <returns></returns>
        public static async Task<Paging<Album>> GetNewReleases(string accessToken, string country = "", int limit = 20, int offset = 0) {
            string endpoint = "https://api.spotify.com/v1/browse/new-releases";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country},
                {"limit",limit },
                {"offset", offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Paging<Album>>(req, accessToken, key:"albums");
        }

        /// <summary>
        /// Get a list of categories used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="locale">Optional. The desired language, consisting of an ISO 639 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore. For example: es_MX, meaning "Spanish (Mexico)". Provide this parameter if you want the category metadata returned in a particular language.  Note that, if locale is not supplied, or if the specified language is not available, all strings will be returned in the Spotify default language (American English).  The locale parameter, combined with the country parameter, may give odd results if not carefully matched. For example country=SE&locale=de_DE will return a list of categories relevant to Sweden but as German language strings.</param>
        /// <param name="country">Optional. A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want to narrow the list of returned categories to those relevant to a particular country. If omitted, the returned items will be globally relevant.</param>
        /// <param name="limit">Optional. The maximum number of categories to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of categories. </param>
        /// <returns></returns>
        public static async Task<Paging<Category>> GetCategories(string accessToken, string locale = "", string country = "", int limit = 20, int offset = 0) {
            string endpoint = "https://api.spotify.com/v1/browse/categories";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country},
                {"locale", locale },
                {"limit",limit },
                {"offset", offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Paging<Category>>(req, accessToken, key:"categories");
        }

        /// <summary>
        /// Get a single category used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="category_id">The Spotify category ID for the category.</param>
        /// <param name="country">Optional. A country: an ISO 3166-1 alpha-2 country code. Provide this parameter to ensure that the category exists for a particular country.</param>
        /// <param name="locale">Optional. The desired language, consisting of an ISO 639 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore. For example: es_MX, meaning "Spanish (Mexico)". Provide this parameter if you want the category strings returned in a particular language.   Note that, if locale is not supplied, or if the specified language is not available, the category strings returned will be in the Spotify default language (American English).</param>
        /// <returns></returns>
        public static async Task<Category> GetACategory(string accessToken, string category_id, string country = "", string locale = "") {
            string endpoint = $"https://api.spotify.com/v1/browse/categories/{category_id}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country},
                {"locale", locale }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Category>(req, accessToken);
        }

        /// <summary>
        /// Get a list of Spotify playlists tagged with a particular category.
        /// </summary>
        /// <param name="accessToken">Oauth access token</param>
        /// <param name="category_id">The Spotify category ID for the category.</param>
        /// <param name="country">Optional. A country: an ISO 3166-1 alpha-2 country code.</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items. </param>
        /// <returns></returns>
        public static async Task<Paging<Playlist>> GetACategorysPlaylists(string accessToken, string category_id, string country = "", int limit = 20, int offset = 0) {
            string endpoint = $"https://api.spotify.com/v1/browse/categories/{category_id}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country},
                {"limit", limit },
                {"offset", offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Paging<Playlist>>(req, accessToken);
        }

        /// <summary>
        /// Get detailed profile information about the current user (including the current user’s username).
        /// Reading the user's email address requires the user-read-email scope; reading country and product subscription level requires the user-read-private scope. Reading the user's birthdate requires the user-read-birthdate scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <returns></returns>
        public static async Task<User> GetCurrentUser(string accessToken) {
            string endpoint = "https://api.spotify.com/v1/me";
            return await DoHTTP<User>(endpoint, accessToken);
        }

        /// <summary>
        /// Get the current user’s followed artists.
        /// Getting details of the artists or users the current user follows requires authorization of the user-follow-read scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="after">Optional. The last artist ID retrieved from the previous request.</param>
        /// <returns></returns>
        public static async Task<CursorBasedPaging<Artist>> GetUsersFollowedArtists(string accessToken, int limit = 20, string after = "") {
            string type = "artist";
            string endpoint = $"https://api.spotify.com/v1/me/following";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type},
                {"limit", limit},
                {"after", after}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<CursorBasedPaging<Artist>>(req, accessToken, key:"artists");
        }

        /// <summary>
        /// Add the current user as a follower of one or more artists.
        /// Modifying the list of artists the current user follows requires authorization of the user-follow-modify scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">A list of the artist Spotify IDs. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns></returns>
        public static async Task<RegularError> FollowArtists(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/following";
            string type = "artist";
            int maxParams = 50;
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;

            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"ids", ids.Take(maxParams) }
            };

            return await DoMethod(req, accessToken, $"successfully followed some {type}", HttpMethod.Put, putItems);
        }

        /// <summary>
        /// Add the current user as a follower of one or more other Spotify users.
        /// Modifying the list of Spotify users the current user follows requires authorization of the user-follow-modify scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">A list of the user Spotify IDs. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns></returns>
        public static async Task<RegularError> FollowUsers(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/following";
            string type = "user";
            int maxParams = 50;
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;

            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"ids", ids.Take(maxParams) }
            };

            return await DoMethod(req, accessToken, $"successfully followed some {type}", HttpMethod.Put, putItems);
        }

        /// <summary>
        /// Remove the current user as a follower of one or more artists.
        /// Modifying the list of artists the current user follows requires authorization of the user-follow-modify scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">A list of the artist or the user Spotify IDs.</param>
        /// <returns></returns>
        public static async Task<RegularError> UnfollowArtists(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/following";
            string type = "artist";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;

            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"ids", ids }
            };
            string onSuccess = $"successully unfollowed some {type}";
            return await DoMethod(req, accessToken, onSuccess, HttpMethod.Delete, putItems);
        }

        /// <summary>
        /// Remove the current user as a follower of one or more Spotify users.
        /// Modifying the list of users the current user follows requires authorization of the user-follow-modify scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">A list of the artist or the user Spotify IDs.</param>
        /// <returns></returns>
        public static async Task<RegularError> UnfollowUsers(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/following";
            string type = "user";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;

            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"ids", ids }
            };
            string onSuccess = $"successully unfollowed some {type}";
            return await DoMethod(req, accessToken, onSuccess, HttpMethod.Delete, putItems);
        }

        /// <summary>
        /// Check to see if the current user is following one or more artists.
        /// Getting details of the artists the current user follows requires authorization of the user-follow-read scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="artist_ids">Required. A list of the artist Spotify IDs to check.  A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>List of true or false values, in the same order in which the ids were specified. </returns>
        public static async Task<IReadOnlyList<bool>> CheckCurrentUserFollowsArtists(string accessToken, IEnumerable<string> artist_ids) {
            int maxparams = 50;
            string endpoint = "https://api.spotify.com/v1/me/following/contains";
            string type = "artist";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type},
                {"ids", string.Join(",", artist_ids.Take(maxparams)) }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHttpGetBools(accessToken, req);
        }

        /// <summary>
        /// Check to see if the current user is following one or more other Spotify users.
        /// Getting details of the users the current user follows requires authorization of the user-follow-read scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="user_ids">Required. A list of the user Spotify IDs to check.  A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>List of true or false values, in the same order in which the ids were specified. </returns>
        public static async Task<IReadOnlyList<bool>> CheckCurrentUserFollowsUsers(string accessToken, IEnumerable<string> user_ids) {
            int maxparams = 50;
            string endpoint = "https://api.spotify.com/v1/me/following/contains";
            string type = "user";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type},
                {"ids", string.Join(",", user_ids.Take(maxparams)) }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHttpGetBools(accessToken, req);
        }

        /// <summary>
        /// Add the current user as a follower of a playlist.
        /// Following a playlist publicly requires authorization of the playlist-modify-public scope; following it privately requires the playlist-modify-private scope. See Using Scopes. 
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="owner_id">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlist_id">The Spotify ID of the playlist. Any playlist can be followed, regardless of its public/private status, as long as you know its playlist ID.</param>
        /// <param name="isPublic">Optional, default true. If true the playlist will be included in user's public playlists, if false it will remain private. To be able to follow playlists privately, the user must have granted the playlist-modify-private scope.</param>
        /// <returns></returns>
        public static async Task<RegularError> FollowAPlaylist(string accessToken, string owner_id, string playlist_id, bool isPublic = true) {
            string endpoint = $"https://api.spotify.com/v1/users/{owner_id}/playlists/{playlist_id}/followers";
            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"public", isPublic }
            };
            return await DoMethod(endpoint, accessToken, $"successfully follows playlist {playlist_id}", HttpMethod.Put, putItems);
        }

        /// <summary>
        /// Remove the current user as a follower of a playlist.
        ///  Unfollowing a publicly followed playlist for a user requires authorization of the playlist-modify-public scope; unfollowing a privately followed playlist requires the playlist-modify-private scope. See Using Scopes.
        ///  Note that the scopes you provide relate only to whether the current user is following the playlist publicly or privately (i.e. showing others what they are following), not whether the playlist itself is public or private.
        /// </summary>
        /// <param name="accessToken">Oauth access token</param>
        /// <param name="owner_id">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlist_id">The Spotify ID of the playlist that is to be no longer followed.</param>
        /// <returns></returns>
        public static async Task<RegularError> UnfollowAPlaylist(string accessToken, string owner_id, string playlist_id) {
            string endpoint = $"https://api.spotify.com/v1/users/{owner_id}/playlists/{playlist_id}/followers";
            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Delete);
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully unfollowed playlist {playlist_id}");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Check to see if one or more Spotify users are following a specified playlist.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="owner_id">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlist_id">The Spotify ID of the playlist.</param>
        /// <param name="user_ids">A list of Spotify User ids that you want to check to see if they follow the playlist. Maximum 5 ids</param>
        /// <returns></returns>
        public static async Task<IReadOnlyList<bool>> CheckUsersFollowsPlaylist(string accessToken, string owner_id, string playlist_id, IEnumerable<string> user_ids) {
            int maxParams = 5;
            string endpoint = $"https://api.spotify.com/v1/users/{owner_id}/playlists/{playlist_id}/followers/contains";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"ids", string.Join(",", user_ids.Take(maxParams)) }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHttpGetBools(accessToken, req);
        }

        /// <summary>
        /// Save Tracks for User
        /// Modification of the current user's "Your Music" collection requires authorization of the user-library-modify scope.
        /// Trying to add a track when you do not have the user’s authorization, or when you have over 10.000 tracks in Your Music, returns error 403 Forbidden.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">A list of the Spotify IDs.</param>
        /// <returns></returns>
        public static async Task<RegularError> SaveTracksForUser(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/tracks";
            int maxparams = 50;
            string[] idarr = ids.Take(maxparams).ToArray();
            JArray putObject = JArray.FromObject(idarr);
            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Put);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully saved {idarr.Length} tracks");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Get a list of the songs saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects. </param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public static async Task<Paging<SavedTrack>> GetUsersSavedTracks(string accessToken, int limit = 20, int offset = 0, string market = "") {
            string endpoint = "https://api.spotify.com/v1/me/tracks";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit", limit},
                {"offset", offset},
                {"market", market}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Paging<SavedTrack>>(req, accessToken);
        }

        /// <summary>
        /// Remove one or more tracks from the current user’s “Your Music” library.
        ///  Modification of the current user's "Your Music" collection requires authorization of the user-library-modify scope.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">A list of Spotify Ids</param>
        /// <returns></returns>
        public static async Task<RegularError> RemoveUsersSavedTracks(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/tracks";
            int maxParams = 50;
            string[] idArr = ids.Take(maxParams).ToArray();
            JArray putObject = JArray.FromObject(idArr);
            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Delete);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully removed {idArr.Length} tracks from users library");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Check if one or more tracks is already saved in the current Spotify user’s “Your Music” library.
        /// The user-library-read scope must have been authorized by the user.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">List of Spotify Ids</param>
        /// <returns></returns>
        public static async Task<IReadOnlyList<bool>> CheckUsersSavedTracks(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/tracks/contains";
            int maxParams = 50;
            string[] idArr = ids.Take(maxParams).ToArray();
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"ids", string.Join(",",idArr) }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHttpGetBools(accessToken, req);
        }

        /// <summary>
        /// Save one or more albums to the current user’s “Your Music” library.
        /// Modification of the current user's "Your Music" collection requires authorization of the user-library-modify scope.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">List of Spotify Ids</param>
        /// <returns></returns>
        public static async Task<RegularError> SaveAlbumsForUser(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/albums";
            int maxparams = 50;
            string[] idarr = ids.Take(maxparams).ToArray();
            JArray putObject = JArray.FromObject(idarr);
            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Put);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully saved {idarr.Length} albums");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Get a list of the albums saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="accessToken">OAuth token</param>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects. </param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public static async Task<Paging<SavedAlbum>> GetUsersSavedAlbums(string accessToken, int limit = 20, int offset = 0, string market = "") {
            string endpoint = "https://api.spotify.com/v1/me/albums";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit", limit},
                {"offset", offset },
                {"market", market},
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Paging<SavedAlbum>>(req, accessToken);
        }

        /// <summary>
        /// Remove one or more albums from the current user’s “Your Music” library.
        /// Changes to a user’s saved albums may not be visible in other Spotify applications immediately.
        /// Modification of the current user's "Your Music" collection requires authorization of the user-library-modify scope.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">List of Spotify Ids</param>
        /// <returns></returns>
        public static async Task<RegularError> RemoveAlbumsForCurrentUser(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/albums";
            int maxParams = 50;
            string[] idArr = ids.Take(maxParams).ToArray();
            JArray putObject = JArray.FromObject(idArr);
            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Delete);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully removed {idArr.Length} albums from users library");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Check if one or more albums is already saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">List of Spotify Ids</param>
        /// <returns></returns>
        public static async Task<IReadOnlyList<bool>> CheckUsersSavedAlbums(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/albums/contains";
            int maxParams = 50;
            string[] idArr = ids.Take(maxParams).ToArray();
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"ids", string.Join(",",idArr) }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHttpGetBools(accessToken, req);
        }


        /// <summary>
        /// Get the current user’s top artists or tracks based on calculated affinity.
        /// Affinity is a measure of the expected preference a user has for a particular track or artist. 
        /// It is based on user behavior, including play history, but does not include actions made while in incognito mode. 
        /// Light or infrequent users of Spotify may not have sufficient play history to generate a full affinity data set.
        /// 
        /// As a user’s behavior is likely to shift over time, this preference data is available over three time spans.
        /// See time_range in the query parameter table for more information. 
        /// 
        /// For each time range, the top 50 tracks and artists are available for each user. 
        /// In the future, it is likely that this restriction will be relaxed. 
        /// This data is typically updated once each day for each user.
        /// 
        /// Getting details of a user's top artists and tracks requires authorization of the user-top-read scope. See Using Scopes.
        /// 
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="limit">Optional. The number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first entity to return. Default: 0 (i.e., the first track). Use with limit to get the next set of entities.</param>
        /// <param name="time_range">Optional. Over what time frame the affinities are computed. Valid values: long_term (calculated from several years of data and including all new data as it becomes available), medium_term (approximately last 6 months), short_term (approximately last 4 weeks). Default: medium_term. </param>
        /// <returns></returns>
        public static async Task<Paging<Artist>> GetUsersTopArtists(string accessToken, int limit = 20, int offset=0, string time_range = "medium_term") {
            string type = "artists";
            string endpoint = $"https://api.spotify.com/v1/me/top/{type}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit",limit },
                {"offset",offset },
                {"time_range",time_range}
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Artist>>(req, accessToken);
        }

        /// <summary>
        /// Get the current user’s top artists or tracks based on calculated affinity.
        /// Affinity is a measure of the expected preference a user has for a particular track or artist. 
        /// It is based on user behavior, including play history, but does not include actions made while in incognito mode. 
        /// Light or infrequent users of Spotify may not have sufficient play history to generate a full affinity data set.
        /// 
        /// As a user’s behavior is likely to shift over time, this preference data is available over three time spans.
        /// See time_range in the query parameter table for more information. 
        /// 
        /// For each time range, the top 50 tracks and artists are available for each user. 
        /// In the future, it is likely that this restriction will be relaxed. 
        /// This data is typically updated once each day for each user.
        /// 
        /// Getting details of a user's top artists and tracks requires authorization of the user-top-read scope. See Using Scopes.
        /// 
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="limit">Optional. The number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first entity to return. Default: 0 (i.e., the first track). Use with limit to get the next set of entities.</param>
        /// <param name="time_range">Optional. Over what time frame the affinities are computed. Valid values: long_term (calculated from several years of data and including all new data as it becomes available), medium_term (approximately last 6 months), short_term (approximately last 4 weeks). Default: medium_term. </param>
        /// <returns></returns>
        public static async Task<Paging<Track>> GetUsersTopTracks(string accessToken, int limit = 20, int offset = 0, string time_range = "medium_term") {
            string type = "tracks";
            string endpoint = $"https://api.spotify.com/v1/me/top/{type}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit",limit },
                {"offset",offset },
                {"time_range",time_range}
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Track>>(req, accessToken);
        }

        //TODO Recommendations
        //This is complicated
        //    public static async Task<Recommendation> GetRecommendationsBasedOnSeed(string accessToken, AudioFeatures MaxTuneableValues = null, AudioFeatures MinTuneableValues = null, AudioFeatures TargetValues = null,  IEnumerable<string> seed_artists = null, IEnumerable<string> seed_tracks, IEnumerable<string> seed_genres, int limit = 20, string market = "", ) {
        //        if(MaxTuneableValues == null) {
        //            MaxTuneableValues = new AudioFeatures(
        //                    AudioFeatures.Acousticness_Max, AudioFeatures.Danceability_Max, AudioFeatures.Energy_Max, AudioFeatures.Instrumentalness_Max,
        //                    AudioFeatures.Key_Max, AudioFeatures.Liveness_Max, AudioFeatures.Loudness_Max, AudioFeatures.Mode_Max, AudioFeatures.Speechiness_Max, 
        //                    AudioFeatures.Tempo_Max, AudioFeatures.Time_Signature_Max, AudioFeatures.Valence_Max,
        //                    "", int.MaxValue, "", "", "");
        //        }
        //        if (MinTuneableValues == null) {
        //            MinTuneableValues = new AudioFeatures(
        //                    AudioFeatures.Acousticness_Min, AudioFeatures.Danceability_Min, AudioFeatures.Energy_Min, AudioFeatures.Instrumentalness_Min,
        //                    AudioFeatures.Key_Min, AudioFeatures.Liveness_Min, AudioFeatures.Loudness_Min, AudioFeatures.Mode_Min, AudioFeatures.Speechiness_Min,
        //                    AudioFeatures.Tempo_Min, AudioFeatures.Time_Signature_Min, AudioFeatures.Valence_Min,
        //                    "", int.MaxValue, "", "", "");
        //        
        //    }
        public static async Task<RecommendationsResponse> GetRecommendationsBasedOnSeed(string accessToken) {
            await Task.Delay(1);
            throw new NotImplementedException("NYI");
        }

        /// <summary>
        /// TODO
        /// Returns a list of available genres that can be used as input to a recommendation request
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetAvailableGenreSeeds(string accessToken) {
            string endpoint = $"https://api.spotify.com/v1/recommendations/available-genre-seeds";
            //TODO this is incorrect - its just a list of strings, but DoSeveral goes through an ISpotify generator.
            //Need something simpler.
            //In addition, spotify has an inconsistent encoding - this particular endpoint is returning UTF8, instead of utf-8. Causes method to crash
            return new List<string>();
            //return await DoSeveralHttp<string>(endpoint, "genres", accessToken);
        }


        //TODO Search
        //Also complicated
        //What to do about the query param.
        public static async Task<Paging<Artist>> SearchArtists(string accessToken, string query, string market = "", int limit = 20, int offset = 0) {
            string endpoint = "https://api.spotify.com/v1/search";
            string type = "artist";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"q", query },
                {"type", type },
                {"market", market },
                {"limit",limit },
                {"offset",offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Artist>>(req, accessToken, "artists");
            throw new NotImplementedException("NYI");
        }
        public static async Task<Paging<Album>> SearchAlbums(string accessToken, string query, string market = "", int limit = 20, int offset = 0) {
            string endpoint = "https://api.spotify.com/v1/search";
            string type = "album";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"q", query },
                {"type", type },
                {"market", market },
                {"limit",limit },
                {"offset",offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Album>>(req, accessToken, "albums");
            throw new NotImplementedException("NYI");
        }
        public static async Task<Paging<Playlist>> SearchPlaylists(string accessToken, string query, string market = "", int limit = 20, int offset = 0) {
            string endpoint = "https://api.spotify.com/v1/search";
            string type = "playlist";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"q", query },
                {"type", type },
                {"market", market },
                {"limit",limit },
                {"offset",offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Playlist>>(req, accessToken, "playlists");
            throw new NotImplementedException("NYI");
        }
        public static async Task<Paging<Track>> SearchTracks(string accessToken, string query, string market = "", int limit = 20, int offset = 0) {
            string endpoint = "https://api.spotify.com/v1/search";
            string type = "track";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"q", query },
                {"type", type },
                {"market", market },
                {"limit",limit },
                {"offset",offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Track>>(req, accessToken, "tracks");
            throw new NotImplementedException("NYI");
        }


        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the track.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public static async Task<Track> GetATrack(string accessToken, string id, string market = "") {
                string endpoint = $"https://api.spotify.com/v1/tracks/{id}";
                Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                    {"market",market },
                };
                string options = EncodeRequestParams(paramDict);
                string req = endpoint + options;
                return await DoHTTP<Track>(req, accessToken);
            }

        /// <summary>
        /// Get Spotify catalog information for multiple tracks based on their Spotify IDs.
        /// 
        /// Objects are returned in the order requested. If an object is not found, a null value is returned in the appropriate position. 
        /// Duplicate ids in the query will result in duplicate objects in the response. 
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">A List of Spotify Ids</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Track>> GetSeveralTracks(string accessToken, IEnumerable<string> ids, string market = "") {
            string type = "tracks";
            string endpoint = "https://api.spotify.com/v1/tracks";
            int maxParams = 50;
            string[] idArr = ids.Take(maxParams).ToArray();
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"ids", string.Join(",", idArr)},
                {"market",market},
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoSeveralHttp<Track>(req, type, accessToken);
        }

        /// <summary>
        /// Get public profile information about a Spotify user.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="user_id">The user's Spotify user ID.</param>
        /// <returns></returns>
        public static async Task<User> GetUsersProfile(string accessToken, string user_id) {
            string endpoint = $"https://api.spotify.com/v1/users/{user_id}";
            return await DoHTTP<User>(endpoint, accessToken);
        }

        /// <summary>
        /// Get a list of the playlists owned or followed by a Spotify user.
        /// 
        /// Private playlists are only retrievable for the current user and requires the playlist-read-private scope to have been authorized by the user.
        /// Note that this scope alone will not return collaborative playlists, even though they are always private.
        /// 
        ///  Collaborative playlists are only retrievable for the current user and requires the playlist-read-collaborative scope to have been authorized by the user. 
        ///  
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="user_id">The user's Spotify user ID.</param>
        /// <param name="limit">Optional. The maximum number of playlists to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first playlist to return. Default: 0 (the first object). Maximum offset: 100.000. Use with limit to get the next set of playlists. </param>
        /// <returns></returns>
        public static async Task<Paging<Playlist>> GetUsersPlaylists(string accessToken, string user_id, int limit = 20, int offset = 0) {
            string endpoint = $"https://api.spotify.com/v1/users/{user_id}/playlists";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit",limit },
                {"offset",offset },
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Playlist>>(req, accessToken);
        }

        /// <summary>
        /// Get a list of the playlists owned or followed by the current Spotify user.
        /// 
        ///  Private playlists require the playlist-read-private scope to have been authorized by the user. 
        ///  Note that this scope alone will not return collaborative playlists, even though they are always private.
        ///  
        ///  Collaborative playlists require the playlist-read-collaborative scope to have been authorized by the user. 
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="limit">Optional. The maximum number of playlists to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first playlist to return. Default: 0 (the first object). Maximum offset: 100.000. Use with limit to get the next set of playlists. </param>
        /// <returns></returns>
        public static async Task<Paging<Playlist>> GetCurrentUsersPlaylists(string accessToken, int limit = 20, int offset = 0) {
            string endpoint = $"https://api.spotify.com/v1/me/playlists";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit",limit },
                {"offset",offset },
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Playlist>>(req, accessToken);
        }

        /// <summary>
        /// Get a playlist owned by a Spotify user.
        /// Both Public and Private playlists belonging to any user are retrievable on provision of a valid access token.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="user_id">The user's Spotify user ID.</param>
        /// <param name="playlist_id">The Spotify ID for the playlist.</param>
        /// <param name="fields">
        /// Optional. Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. For example, to get just the playlist's description and URI:
        /// fields="description,uri"
        /// A dot separator can be used to specify non-reoccurring fields, while parentheses can be used to specify reoccurring fields within objects. For example, to get just the added date and user ID of the adder: 
        /// fields="tracks.items(added_at,added_by.id)"
        /// Use multiple parentheses to drill down into nested objects, for example:
        /// fields="tracks.items(track(name,href,album(name,href)))"
        /// Fields can be excluded by prefixing them with an exclamation mark, for example:
        /// fields="tracks.items(track(name,href,album(!name,href)))"
        /// </param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public static async Task<Playlist> GetAPlaylist(string accessToken, string user_id, string playlist_id, string fields = "", string market = "") {
            string endpoint = $"https://api.spotify.com/v1/users/{user_id}/playlists/{playlist_id}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"fields",fields},
                {"market",market },
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Playlist>(req, accessToken);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        ///  Both Public and Private playlists belonging to any user are retrievable on provision of a valid access token.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="user_id">The user's Spotify user ID.</param>
        /// <param name="playlist_id">The Spotify ID for the playlist.</param>
        /// <param name="fields">
        /// Optional. Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. For example, to get just the total number of tracks and the request limit:
        ///     fields="total,limit"
        ///  A dot separator can be used to specify non-reoccurring fields, while parentheses can be used to specify reoccurring fields within objects. For example, to get just the added date and user ID of the adder: 
        ///      fields="items(added_at,added_by.id)"
        ///  Use multiple parentheses to drill down into nested objects, for example:
        ///       fields="items(track(name,href,album(name,href)))"
        ///  Fields can be excluded by prefixing them with an exclamation mark, for example:
        ///      fields="items.track.album(!external_urls,images)"
        /// </param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="limit">Optional. The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100. </param>
        /// <param name="offset">Optional. The index of the first track to return. Default: 0 (the first object). </param>
        /// <returns></returns>
        public static async Task<Paging<PlaylistTrack>> GetAPlaylistsTracks(string accessToken, string user_id, string playlist_id, string fields = "", string market = "", int limit = 20, int offset = 0) {
            string endpoint = $"https://api.spotify.com/v1/users/{user_id}/playlists/{playlist_id}/tracks";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"fields",fields},
                {"market",market },
                {"limit",limit },
                {"offset",offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<PlaylistTrack>>(req, accessToken);
        }

        /// <summary>
        /// Create a playlist for a Spotify user. 
        /// (The playlist will be empty until you add tracks.)
        /// Creating a public playlist for a user requires authorization of the playlist-modify-public scope; creating a private playlist requires the playlist-modify-private scope. See Using Scopes.
        /// To create collaborative playlists you must have granted playlist-modify-private and playlist-modify-public scopes.
        /// </summary>
        /// <param name="accessToken">Oauh access token</param>
        /// <param name="user_id">The user's Spotify user ID.</param>
        /// <param name="name">Required. The name for the new playlist, for example "Your Coolest Playlist". This name does not need to be unique; a user may have several playlists with the same name.</param>
        /// <param name="description"></param>
        /// <param name="isPublic">Optional, default true. If true the playlist will be public, if false it will be private. To be able to create private playlists, the user must have granted the playlist-modify-private scope.</param>
        /// <param name="isCollaborative">Optional, default false. If true the playlist will be collaborative. Note that to create a collaborative playlist you must also set public to false.</param>
        /// <returns></returns>
        public static async Task<Playlist> CreateAPlaylist(string accessToken, string user_id, string name, string description = "", bool isPublic = true, bool isCollaborative = false) {
            string endpoint = $"https://api.spotify.com/v1/users/{user_id}/playlists";
            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"name",name},
                {"public",isPublic},
                {"collaborative",isCollaborative },
                {"description",description }
            };

            JObject putObject = JObject.FromObject(putItems);

            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Post);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                JObject token = JObject.Parse(content);
                return new Playlist(token);
            }
            else {
                return new Playlist(true, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Change a playlist’s name and public/private state. (The user must, of course, own the playlist.)
        /// Changing a public playlist for a user requires authorization of the playlist-modify-public scope; 
        /// changing a private playlist requires the playlist-modify-private scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="user_id">The user's Spotify user ID.</param>
        /// <param name="playlist_id">The Spotify ID for the playlist.</param>
        /// <param name="isPublic">Optional. If true the playlist will be public, if false it will be private.</param>
        /// <param name="isCollaborative">Optional. If true, the playlist will become collaborative and other users will be able to modify the playlist in their Spotify client. Note: You can only set collaborative to true on non-public playlists.</param>
        /// <param name="name">Optional. The new name for the playlist, for example "My New Playlist Title".</param>
        /// <param name="description">Optional, value for playlist description as displayed in Spotify Clients and in the Web API.</param>
        /// <returns></returns>
        public static async Task<RegularError> ChangePlaylistDetails(string accessToken, string user_id, string playlist_id, bool? isPublic, bool? isCollaborative, string name = "", string description = "") {
            string endpoint = $"https://api.spotify.com/v1/users/{user_id}/playlists/{playlist_id}";
            Dictionary<string, object> putItems = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(name)) {
                putItems.Add("name", name);
            }
            if (!string.IsNullOrWhiteSpace(description)) {
                putItems.Add("description", description);
            }
            if (isPublic.HasValue) {
                putItems.Add("public", isPublic);
            }
            if (isCollaborative.HasValue) {
                putItems.Add("collaborative", isCollaborative);
            }
            if (!putItems.Any()) {
                return new RegularError(true, "no fields to modifiy");
            }

            JObject putObject = JObject.FromObject(putItems);

            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Put);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, "Succesfully modified playlist");
            }
            else {
                return new RegularError(true, response.ReasonPhrase);

            }
        }

        /// <summary>
        /// Add one or more tracks to a user’s playlist.
        /// Note that local tracks can’t be added.
        ///  Adding tracks to the current user's public playlists requires authorization of the playlist-modify-public scope; 
        ///  adding tracks to the current user's private playlist (including collaborative playlists) requires the playlist-modify-private scope. 
        ///  See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="user_id">The user's Spotify user ID.</param>
        /// <param name="playlist_id">The Spotify ID for the playlist.</param>
        /// <param name="uris">Spotify URIs to add. NOTE: This is the whole Spotify URI, not just the track id</param>
        /// <param name="position">
        /// Optional. The position to insert the tracks, a zero-based index. 
        /// For example, to insert the tracks in the first position: position=0;
        /// to insert the tracks in the third position: position=2. 
        /// If omitted, the tracks will be appended to the playlist.
        /// Tracks are added in the order they are listed in the query string or request body.</param>
        /// <returns></returns>
        public static async Task<RegularError> AddTracksToPlaylist(string accessToken, string user_id, string playlist_id, IEnumerable<string> uris, int? position = null) {
            string endpoint = $"https://api.spotify.com/v1/users/{user_id}/playlists/{playlist_id}/tracks";
            int maxParams = 100;
            string[] uriArr = uris.Take(maxParams).ToArray();
            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"uris", uriArr },
            };
            if (position.HasValue) {
                putItems.Add("position", position);
            }
            

            JObject putObject = JObject.FromObject(putItems);

            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Post);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                return new RegularError(false, content);
            }
            else {
                return new RegularError(true, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Remove one or more tracks from a user’s playlist.
        /// This removes all instances of matching URIs, meaning that duplicates inside a playlist will be completely removed.
        ///  Removing tracks from a user's public playlist requires authorization of the playlist-modify-public scope; 
        ///  removing tracks from a private playlist requires the playlist-modify-private scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="user_id">The user's Spotify user ID.</param>
        /// <param name="playlist_id">The Spotify ID for the playlist.</param>
        /// <param name="uris">Spotify URIs to add. NOTE: This is the whole Spotify URI, not just the track id</param>
        /// <returns></returns>
        public static async Task<RegularError> RemoveTracksFromPlaylist(string accessToken, string user_id, string playlist_id, IEnumerable<string> uris) {
            string endpoint = $"https://api.spotify.com/v1/users/{user_id}/playlists/{playlist_id}/tracks";
            int maxParams = 100;
            IEnumerable<JObject> jobjss = uris.Take(maxParams).Select(x => JObject.FromObject(new Dictionary<string, string>() { { "uri", x } }));
            JObject messageBody = JObject.FromObject(new Dictionary<string, JToken>() {
                {"tracks", JArray.FromObject(jobjss) }
            });

            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Delete);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(messageBody.ToString());
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                JObject jobj = JObject.Parse(content);
                return new RegularError(false, jobj.Value<string>("snapshot_id"));
            }
            else {
                return new RegularError(true, response.ReasonPhrase);
            }

        }

        public static async Task<RegularError> RemoveSpecificOccurranceOfTracksFromPlaylist(string accessToken, string user_id, string playlist_id, IEnumerable<Tuple<string, string>> uris) {
            await Task.Delay(1);
            throw new NotImplementedException("NYI");
        }

        public static async Task<RegularError> RemoveSpecificOccuranceInSpecificSnapshotOfPlaylist(string accessToken, string user_id, string playlist_id, string snapshot_id) {
            await Task.Delay(1);
            throw new NotImplementedException("NYI");
        }

        public static async Task<RegularError> RemoveTrackAtPositionInPlaylistSnapshot(string accessToken, string user_id, string playlist_id, string snapshot_id) {
            await Task.Delay(1);
            throw new NotImplementedException("NYI");
        }

        /// <summary>
        /// Reorder a track or a group of tracks in a playlist.
        /// 
        /// When reordering tracks, the timestamp indicating when they were added and the user who added them will be kept untouched. 
        /// In addition, the users following the playlists won’t be notified about changes in the playlists when the tracks are reordered.
        /// 
        ///  Reordering tracks in the current user's public playlists requires authorization of the playlist-modify-public scope; 
        ///  reordering tracks in the current user's private playlist (including collaborative playlists) requires the playlist-modify-private scope. 
        ///  See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="user_id">The user's Spotify user ID.</param>
        /// <param name="playlist_id">The Spotify ID for the playlist.</param>
        /// <param name="range_start">Required. The position of the first track to be reordered.</param>
        /// <param name="insert_before">
        /// Optional. The amount of tracks to be reordered. Defaults to 1 if not set.
        /// The range of tracks to be reordered begins from the range_start position, and includes the range_length subsequent tracks.
        /// </param>
        /// <param name="range_length">
        /// Required. The position where the tracks should be inserted. 
        ///  To reorder the tracks to the end of the playlist, simply set insert_before to the position after the last track.
        ///  </param>
        /// <param name="snapshot_id">Optional. The playlist's snapshot ID against which you want to make the changes. </param>
        /// <returns></returns>
        public static async Task<RegularError> ReorderPlaylistsTracks(string accessToken, string user_id, string playlist_id, int range_start, int insert_before, int range_length = 1, string snapshot_id = "") {
            string endpoint = $"https://api.spotify.com/v1/users/{user_id}/playlists/{playlist_id}/tracks";
            Dictionary<string, object> messageBody = new Dictionary<string, object>() {
                {"range_start", range_start },
                {"range_length", range_length },
                {"insert_before", insert_before },
            };
            if (!string.IsNullOrWhiteSpace(snapshot_id)) {
                messageBody.Add("snapshot_id", snapshot_id);
            }

            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Put);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(JObject.FromObject(messageBody).ToString());
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                JObject jobj = JObject.Parse(content);
                return new RegularError(false, jobj.Value<string>("snapshot_id"));
            }
            else {
                return new RegularError(true, response.ReasonPhrase);
            }

        }

        /// <summary>
        /// Replace all the tracks in a playlist, overwriting its existing tracks.
        /// This powerful request can be useful for replacing tracks, re-ordering existing tracks, or clearing the playlist.
        /// 
        /// Setting tracks in the current user's public playlists requires authorization of the playlist-modify-public scope;
        /// setting tracks in the current user's private playlist (including collaborative playlists) requires the playlist-modify-private scope.
        /// See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="user_id">The user's Spotify user ID.</param>
        /// <param name="playlist_id">The Spotify ID for the playlist.</param>
        /// <param name="uris">A list of Spotify URIs to set. Maximum of 100 per request</param>
        /// <returns></returns>
        public static async Task<RegularError> ReplacePlaylistTracks(string accessToken, string user_id, string playlist_id, IEnumerable<string> uris) {
            int maxParams = 100;
            string endpoint = $"https://api.spotify.com/v1/users/{user_id}/playlists/{playlist_id}/tracks";
            Dictionary<string, object> messageBody = new Dictionary<string, object>() {
                {"uris", JArray.FromObject(uris.Take(maxParams)) }
            };

            HttpRequestMessage message = SetupRequest(endpoint, accessToken, HttpMethod.Put);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            JObject mbody = JObject.FromObject(messageBody);
            message.Content = new StringContent(mbody.ToString());
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                JObject jobj = JObject.Parse(content);
                return new RegularError(false, jobj.Value<string>("snapshot_id"));
            }
            else {
                return new RegularError(true, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Get tracks from the current user’s recently played tracks.
        /// 
        /// Returns the most recent 50 tracks played by a user. Note that a track currently playing will not be visible in play history until it has completed. 
        /// A track must be played for more than 30 seconds to be included in play history.
        /// 
        /// Any tracks listened to while the user had “Private Session” enabled in their client will not be returned in the list of recently played tracks.
        /// 
        /// The endpoint uses a bidirectional cursor for paging. 
        /// Follow the next field with the before parameter to move back in time, or use the after parameter to move forward in time. 
        /// If you supply no before or after parameter, the endpoint will return the most recently played songs, and the next link will page back in time.
        /// 
        ///  The access token must have the user-read-recently-played scope authorized in order to read the user's recently played track.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="after">Optional. A Unix timestamp in milliseconds. Returns all items after (but not including) this cursor position. If after is specified, before must not be specified.</param>
        /// <param name="before">Optional. A Unix timestamp in milliseconds. Returns all items before (but not including) this cursor position. If before is specified, after must not be specified.</param>
        /// <returns></returns>
        public static async Task<CursorBasedPaging<PlayHistory>> GetCurrentUsersRecentlyPlayedTracks(string accessToken, int limit = 20, long? after = null, long? before = null ) {
            if(after.HasValue && before.HasValue) {
                throw new ArgumentException("After and Before cannot both be specified. Please only specify one or the other.");
            }
            string endpoint = "https://api.spotify.com/v1/me/player/recently-played";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit", limit }
            };
            if (after.HasValue) {
                paramDict.Add("after", after);
            }
            if (before.HasValue) {
                paramDict.Add("before", before);
            }
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<CursorBasedPaging<PlayHistory>>(req, accessToken);
        }

        public static async Task<IReadOnlyList<Device>> GetUsersAvailableDevices(string accessToken) {
            string endpoint = "https://api.spotify.com/v1/me/player/devices";

            HttpRequestMessage message = SetupRequest(endpoint, accessToken);
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                List<Device> devices = new List<Device>();
                JToken token = await ParseJsonResponse(response.Content);
                JArray jarr = token.Value<JArray>("devices");
                foreach(JToken jobj in jarr) {
                    Device d = new Device(jobj);
                    devices.Add(d);
                }
                return devices;
            }
            throw new HttpRequestException("Failed to get users devices. See inner exception for more details", new Exception(response.ReasonPhrase));
        }

        public static async Task<CurrentlyPlayingContext> GetUsersCurrentlyPlayingInformation(string accessToken, string market = "") {
            string endpoint = "https://api.spotify.com/v1/me/player";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market", market }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<CurrentlyPlayingContext>(req, accessToken);
        }

        //UNTESTED
        /// <summary>
        /// Transfer playback to a new device and determine if it should start playing.
        /// 
        ///  The access token must have the user-modify-playback-state scope authorized in order to control playback.
        ///  
        ///  Note that a value of false for the play parameter when also transferring to another device_id will not pause playback.
        ///  To ensure that playback is paused on the new device you should send a pause command to the currently active device before transferring to the new device_id.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="device_ids">A list (Single-Element) containing the ID of the device on which playback should be started/transferred.
        /// Note: Although an array is accepted, only a single device_id is currently supported. Supplying more than one will return 400 Bad Request</param>
        /// <param name="play">Optional:  true: ensure playback happens on new device; false or not provided: keep the current playback state.</param>
        /// <returns></returns>
        public static async Task<RegularError> TransferUsersPlayback(string accessToken, IEnumerable<string> device_ids, bool play = false) {
            string endpoint = "https://api.spotify.com/v1/me/player";
            Dictionary<string, object> messageBody = new Dictionary<string, object>() {
                { "device_ids", JArray.FromObject(device_ids)},
                {"play", play }
            };
            return await DoMethod(endpoint, accessToken, "Success", HttpMethod.Put, messageBody);
        }


        /// <summary>
        /// Start a new context or resume current playback on the user’s active device.
        ///  The access token must have been issued on behalf of a premium user. 
        ///  The access token must have the user-modify-playback-state scope authorized in order to control playback.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="device_id">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <param name="context_uri">Spotify URI of the context to play. Valid contexts are albums, artists & playlists. NOT TRACKS. Please use a singleton uris list instead.</param>
        /// <param name="uris">Optional: List of the Spotify track URIs to play.</param>
        /// <param name="offset">Optional.String Or Int. Indicates from where in the context playback should start. Only available when context_uri corresponds to an album or playlist object, or when the uris parameter is used.</param>
        /// <returns></returns>
        public static async Task<RegularError> StartOrResumePlayback(string accessToken, string device_id="", string context_uri = "", IEnumerable<string> uris = null, object offset= null) {
            if(!string.IsNullOrEmpty(context_uri) && uris != null) {
                throw new ArgumentException("uris and context_uri cannot both be defined. Please select one or the other.");
            }
            string endpoint = "https://api.spotify.com/v1/me/player/play";

            Dictionary<string, object> messageBody = new Dictionary<string, object>() {
            };
            if (!string.IsNullOrEmpty(device_id)) {
                messageBody.Add("device_id", device_id);
            }
            if(uris != null) {
                messageBody.Add("uris", JArray.FromObject(uris));
            }
            if (!string.IsNullOrWhiteSpace(context_uri)) {
                messageBody.Add("context_uri", context_uri);
            }
            if(offset != null) {
                switch (offset) {
                    case string s:
                        if (!string.IsNullOrWhiteSpace(s)) {
                            messageBody.Add("offset", JObject.FromObject(new Dictionary<string, string>() { { "uri", s } }));
                        }
                        else {
                            throw new ArgumentException("Offset was empty. Either do not supply an offset, or set a valid string");
                        }
                        break;
                    case int i:
                        if(i > -1) {
                            messageBody.Add("position", i);
                        }
                        else {
                            throw new ArgumentException("Offset was negative. Either do not supply an offset, or set a non-negative integer");
                        }
                        break;
                    default:
                        throw new ArgumentException("Offset was neither an int nor a string. Please ensure that offset is one of those two types.");
                }
                if(offset.GetType() == typeof(string)) {
                    messageBody.Add("offset", JObject.FromObject(new Dictionary<string, string>() { { "uri", offset as string} }));
                }
                else if (offset.GetType() == typeof(int)) {

                }
            }
            return await DoMethod(endpoint, accessToken, $"Toggled playback on {device_id}.", HttpMethod.Put, messageBody);
        }

        /// <summary>
        /// Pause playback on the user’s account.
        /// The access token must have been issued on behalf of a premium user. 
        /// The access token must have the user-modify-playback-state scope authorized in order to control playback.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="device_id">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <returns></returns>
        public static async Task<RegularError> PauseUsersPlayback(string accessToken, string device_id = "") {
            string endpoint = "https://api.spotify.com/v1/me/player/pause";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"device_id", device_id }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoMethod(req, accessToken, "Paused playback", HttpMethod.Put);
        }


        /// <summary>
        /// Skips to next track in the user’s queue.
        /// After a successful skip operation, playback will automatically start.
        /// The access token must have been issued on behalf of a premium user. 
        /// The access token must have the user-modify-playback-state scope authorized in order to control playback.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="device_id">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <returns></returns>
        public static async Task<RegularError> SkipPlaybackToUsersNextTrack(string accessToken, string device_id = "") {
            string endpoint = "https://api.spotify.com/v1/me/player/next";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"device_id", device_id }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoMethod(req, accessToken, "Skipped to next song", HttpMethod.Post);
        }

        /// <summary>
        /// Skips to next previous in the user’s queue.
        ///  Note that this will ALWAYS skip to the previous track, regardless of the current track’s progress. 
        ///  Returning to the start of the current track should be performed using the https://api.spotify.com/v1/me/player/seek endpoint.
        /// After a successful skip operation, playback will automatically start.
        /// The access token must have been issued on behalf of a premium user. 
        /// The access token must have the user-modify-playback-state scope authorized in order to control playback.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="device_id">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <returns></returns>
        public static async Task<RegularError> SkipPlaybackToUsersPreviousTrack(string accessToken, string device_id = "") {
            string endpoint = "https://api.spotify.com/v1/me/player/previous";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"device_id", device_id }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoMethod(req, accessToken, "Skipped to previous song", HttpMethod.Post);
        }

        /// <summary>
        /// Seeks to the given position in the user’s currently playing track.
        /// After a successful skip operation, playback will automatically start.
        /// The access token must have been issued on behalf of a premium user. 
        /// The access token must have the user-modify-playback-state scope authorized in order to control playback.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="position_ms">Required. The position in milliseconds to seek to. Must be a positive number. Passing in a position that is greater than the length of the track will cause the player to start playing the next song.</param>
        /// <param name="device_id">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <returns></returns>
        public static async Task<RegularError> SeekToPositionInCurrentlyPlayingTrack(string accessToken, long position_ms, string device_id = "") {
            if (position_ms < 0) {
                throw new ArgumentException("position_ms must be a positive int or long.");
            }
            string endpoint = "https://api.spotify.com/v1/me/player/seek";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"position_ms", position_ms },
                {"device_id", device_id }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoMethod(req, accessToken, $"Seeked to {position_ms}", HttpMethod.Put);
        }

        /// <summary>
        /// Set the repeat mode for the user’s playback. Options are repeat-track, repeat-context, and off.
        /// </summary>
        /// <param name="accessToken">Oauth access token</param>
        /// <param name="position_ms">Required. The position in milliseconds to seek to. Must be a positive number. Passing in a position that is greater than the length of the track will cause the player to start playing the next song.</param>
        /// <param name="device_id">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <returns></returns>
        public static async Task<RegularError> SetRepeatModeOnUsersPlayback(string accessToken, RepeatEnum state, string device_id = "") {
            string endpoint = "https://api.spotify.com/v1/me/player/repeat";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"state", state.Name},
                {"device_id", device_id }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoMethod(req, accessToken, $"Switched repeat to {state.ToString()}", HttpMethod.Put);
        }

        /// <summary>
        /// Set the volume for the user’s current playback device.
        /// </summary>
        /// <param name="accessToken">Oauth access token</param>
        /// <param name="position_ms">Required. Integer. The volume to set. Must be a value from 0 to 100 inclusive.</param>
        /// <param name="device_id">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <returns></returns>
        public static async Task<RegularError> SetVolumeOnUsersPlayback(string accessToken, int volume_percent, string device_id = "") {
            if(volume_percent < 0 || volume_percent > 100) {
                throw new ArgumentException("Volume percent must be a positive integer between 0 and 100, inclusive.");
            }
            string endpoint = "https://api.spotify.com/v1/me/player/volume";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"volume_percent", volume_percent},
                {"device_id", device_id }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoMethod(req, accessToken, $"Turned volume to {volume_percent}", HttpMethod.Put);
        }

        /// <summary>
        /// Toggle shuffle on or off for user’s playback.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="shuffle">True: Shuffle On. False: Shuffle off</param>
        /// <param name="device_id">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <returns></returns>
        public static async Task<RegularError> SetShuffleOnPlayback(string accessToken, bool shuffle, string device_id = "") {
            string endpoint = "https://api.spotify.com/v1/me/player/shuffle";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"state", shuffle},
                {"device_id", device_id }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoMethod(req, accessToken, $"Set shuffle to {shuffle}", HttpMethod.Put);
        }
    }
}
