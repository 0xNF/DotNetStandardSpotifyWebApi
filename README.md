# .NET Standard Spotify Web API

### A C# wrapper for the Spotify Web API written for .Net Standard (v1.4).  

Compatible with .NET Core, ASP.NET Core, and UWP.  

#### Completion
89% Complete
Todo: 
    1) implement the remaining endpoints
    2) Make Authorization easier
    3) Add useful helper methods
  

#### Available Endpoints
Currently Implements the following endpoints:
* [Get an album](https://developer.spotify.com/web-api/get-album/)
* [Get several albums](https://developer.spotify.com/web-api/get-several-albums/)
* [Get an album's tracks](https://developer.spotify.com/web-api/get-albums-tracks/)
* [Get an artist](https://developer.spotify.com/web-api/get-artist/)
* [Get several artists](https://developer.spotify.com/web-api/get-several-artists/)
* [Get an artist's albums](https://developer.spotify.com/web-api/get-artists-albums/)
* [Get an artist's top tracks](https://developer.spotify.com/web-api/get-artists-top-tracks/)
* [Get an artist's related artists](https://developer.spotify.com/web-api/get-related-artists/)
* [Get audio features for a track](https://developer.spotify.com/get-audio-features/)
* [Get audio features for several tracks](https://developer.spotify.com/get-several-audio-features/)
* [Get a list of featured playlists ](https://developer.spotify.com/web-api/get-list-featured-playlists/)
* [Get a list of new releases](https://developer.spotify.com/web-api/get-list-new-releases/)
* [Get a list of categories](https://developer.spotify.com/web-api/get-list-categories/)
* [Get a category](https://developer.spotify.com/web-api/get-category/)
* [Get a category's playlists](https://developer.spotify.com/web-api/get-categorys-playlists/)
* [Get current user's profile](https://developer.spotify.com/web-api/get-current-users-profile/)
* [Get Followed Artists](https://developer.spotify.com/web-api/get-followed-artists/)
* [Follow Artists or Users](https://developer.spotify.com/web-api/follow-artists-users/)
* [Unfollow Artists or Users](https://developer.spotify.com/web-api/unfollow-artists-users/)
* [Check if User Follows Users or Artists](https://developer.spotify.com/web-api/check-current-user-follows/)
* [Follow a Playlist](https://developer.spotify.com/web-api/follow-playlist/)
* [Unfollow a Playlist](https://developer.spotify.com/web-api/unfollow-playlist/)
* [Save tracks for user](https://developer.spotify.com/web-api/save-tracks-user/)
* [Get user's saved tracks](https://developer.spotify.com/web-api/get-users-saved-tracks/)
* [Remove user's saved tracks](https://developer.spotify.com/web-api/remove-tracks-user/)
* [Check user's saved tracks](https://developer.spotify.com/web-api/check-users-saved-tracks/)
* [Save albums for user](https://developer.spotify.com/web-api/save-albums-user/)
* [Get user's saved albums](https://developer.spotify.com/web-api/get-users-saved-albums/)
* [Remove user's saved albums](https://developer.spotify.com/web-api/remove-albums-user/)
* [Check user's saved albums](https://developer.spotify.com/web-api/check-users-saved-albums/)
* [Get a user's top artists or tracks](https://developer.spotify.com/web-api/get-users-top-artists-and-tracks/)
* [Get a track](https://developer.spotify.com/web-api/get-track/)
* [Get several tracks](https://developer.spotify.com/web-api/get-several-tracks/)
* [Get a user's profile](https://developer.spotify.com/web-api/get-users-profile/)
* [Get a list of a user's playlists](https://developer.spotify.com/web-api/get-list-users-playlists/)
* [Get a list of the current user's playlists ](https://developer.spotify.com/web-api/get-a-list-of-current-users-playlists/)
* [Get a playlist](https://developer.spotify.com/web-api/get-playlist/)
* [Get a playlist's tracks](https://developer.spotify.com/web-api/get-playlists-tracks/)
* [Create a playlist](https://developer.spotify.com/web-api/create-playlist/)
* [Change a playlist's details](https://developer.spotify.com/web-api/change-playlist-details/)
* [Add tracks to a playlist](https://developer.spotify.com/web-api/add-tracks-to-playlist/)
* [Reorder a playlist's tracks](https://developer.spotify.com/web-api/reorder-playlists-tracks/)
* [Replace a playlist's tracks](https://developer.spotify.com/web-api/replace-playlists-tracks/)
* [Check if Users Follow a Playlist](https://developer.spotify.com/web-api/check-user-following-playlist/)
* [Get Current User’s Recently Played Tracks](https://developer.spotify.com/web-api/web-api-personalization-endpoints/get-recently-played/)
* [Get a User’s Available Devices](https://developer.spotify.com/web-api/get-a-users-available-devices/)
* [Get Information About The User’s Current Playback](https://developer.spotify.com/web-api/get-information-about-the-users-current-playback/)
* [Get the User’s Currently Playing Track](https://developer.spotify.com/web-api/get-the-users-currently-playing-track/)
* [Transfer a User’s Playback](https://developer.spotify.com/web-api/transfer-a-users-playback/)
* [Start/Resume a User’s Playback](https://developer.spotify.com/web-api/start-a-users-playback/)
* [Pause a User’s Playback](https://developer.spotify.com/web-api/pause-a-users-playback/)
* [Skip User’s Playback To Next Track](https://developer.spotify.com/web-api/skip-users-playback-to-next-track/)
* [Skip User’s Playback To Previous Track](https://developer.spotify.com/web-api/skip-users-playback-to-previous-track/)
* [Seek To Position In Currently Playing Track](https://developer.spotify.com/web-api/seek-to-position-in-currently-playing-track/)
* [Set Repeat Mode On User’s Playback](https://developer.spotify.com/web-api/set-repeat-mode-on-users-playback/)
* [Set Volume For User’s Playback](https://developer.spotify.com/web-api/set-volume-for-users-playback/)
* [Toggle Shuffle For User’s Playback ](https://developer.spotify.com/web-api/toggle-shuffle-for-users-playback/)

The following endpoints are not yet implemeted:
* [Get recommendations based on seeds](https://developer.spotify.com/get-recommendations/)
* [Search for an album](https://developer.spotify.com/web-api/search-item/)
* [Search for an artist](https://developer.spotify.com/web-api/search-item/)
* [Search for a playlist](https://developer.spotify.com/web-api/search-item/)
* [Search for a track](https://developer.spotify.com/web-api/search-item/)
* [Remove tracks from a playlist](https://developer.spotify.com/web-api/remove-tracks-playlist/)
* [Get Audio Analysis for a Track](https://developer.spotify.com/web-api/get-audio-analysis/)



### Examples:

##### Authenticating:
```c#
using DotNetStandardSpotifyWebApi.Authorization;
string Client_Id = "<your client id>";
string Client_Secret = "<your client secret>";
string Redirect_URI = "<your redirect uri>";
List<SpotifyScopeEnum> Scopes = new List<SpotifyScopEnum>(){
	<...>,
	SpotifyScopeEnum.PLAYLIST_READ_PRIVATE,
    SpotifyScopeEnum.STREAMING,
    SpotifyScopeEnum.USER_LIBRARY_MODIFY,
    <...>,
};

//Use the AuthorizationInProgress to send the redirect url to spotify and get user authentication
AuthorizationInProgress authProg = AuthorizationCodeFlow.GetAuthStateAndRedirect(Client_Id, Redirect_URI, Scopes);

//Using an OAuth token taken from the redirected url, exchange for a full authenticated credentials object
OAuthCredentials token = await AuthorizationCodeFlow.GetSpotifyTokenCredentials(OAuthToken, Client_Id, Client_Secret, Redirect_URI);

//Console.WriteLine(token.Access_token);
//> <access_token>
//Console.WriteLine(token.Refresh_token);
//> <refresh_token>
//Console.WriteLine(token.Expires_in);
//> <expires_in>

```
#### Refreshing an access token:
```c#
//With a refresh token
string refresh_token = "<your refresh token>";
OAuthCredentials refreshed = AuthorizationCodeFlow.RefreshAccessToken(<refresh_token>, Client_Id, Client_Secret);
//With an OAuthCredentials
OAuthCredentials creds = <Credentials Object with expired access token>;
creds = AuthorizationCodeFlow.RefreshAccessToken(expired, Client_Id, Client_Secret);
```

#### Getting Spotify Objects
##### Current User
```c#
using DotNetStandardSpotifyWebApi.ObjectModel;
OAuthCredentials Creds = <...>;

User me = await Endpoints.GetCurrentUser(Creds.Access_token);
```
##### Getting a Track
```c#
Track YouStreet = await Endpoints.GetATrack(Creds.Access_token, "7o5dTvQk2Nia65kASf2Ezo");
```
Getting a track with from a specific market. Used in [track re-linking](https://developer.spotify.com/web-api/track-relinking-guide/)
```c#
Track DareYori = await Endpoints.GetATrack(Creds.Access_token, "4FwzvpVpgzI3tj9FHVPJQh", market="JP");
```

##### Getting a Playlist
```c#
User currentUser = <...>;
string PlaylistToGet = "5XDtox3aWN1U8hczfRZuJm";
Playlist Lounge = await Endpoints.GetAPlaylist(Creds.Access_token, currentUser.Id, PlaylistToGet);
```

For more complete examples, see the [ObjectModelTests.cs](https://github.com/0xNF/DotNetStandardSpotifyWebApi/blob/master/DotNetStandardSpotifyWebApi.Tests/ObjectModelTests.cs) file.
