DotNetStandardSpotifyWebApi
===

A wrapper for the Spotify Web API written for .Net Standard.

Currently targets .Net Standard v1.4

### Authorization
Get an OAuth Token using the Authorization Code Flow
```
using DotNetStandardSpotifyWebApi.Authorization;

//Sets up a redirect to Spotify, which will send an OAUTH_CODE to the given REDIRECT_URI
 AuthorizationInProgress flow = AuthorizationCodeFlow.GetAuthStateAndRedirect(<CLIENT_ID>, <REDIRECT_URI>, <SCOPE>);

//Exchanges an OAuth code from a successful Spotify redirect for a OAuth Credentials
 OAuthCredentials token = await AuthorizationCodeFlow.GetSpotifyTokenCredentials(<OAUTH_CODE>, <CLIENT_ID>, <CLIENT_SECRET>, <REDIRECT_URI>);
 

//Refresh token with a previously received OAuthCredentials
 OAuthCredentials token = await AuthorizationCodeFlow.RefreshAccessToken(<OAUTHCREDENTIALS>, <CLIENT_ID>, <CLIENT_SECRET>);

//Or refresh with just a refresh token
 OAuthCredentials token = await AuthorizationCodeFlow.RefreshAccessToken(<REFRESH_TOKEN>, <CLIENT_ID>, <CLIENT_SECRET>);
```

### Spotify Objects
User
```
using DotNetStandardSpotifyWebApi.ObjectModel;

//Get the current Spotify User
//Returns a User object with a more complete profile
User currentUser = await User.GetCurrentUser(<ACCESS_TOKEN>);

//Get an arbitrary Spotify user
//Returns a User object with only publically available information
User someUser = await User.GetUser(<USER_ID>, <ACCESS_TOKEN>);
```

Playlists

```
using DotNetStandardSpotifyWebApi.ObjectModel;

//Get an arbitrary Spotify playlist belonging to some user
//Returns a full playlist object
Playlist pl = await Playlist.GetPlaylist(<USER_ID>, <PLAYLIST_ID>, <ACCESS_TOKEN>);


//Get a users public playlists
//Returns a paging object
Paging<ISpotifyObject> page = await Playlist.GetPublicPlaylists(<USER_ID>, <ACCESS_TOKEN>);
//page.Items contains the individual playlists

//Get current users public and private playlists
//Returns a paging object
User me = User.GetCurrentUser(<ACCESS_TOKEN>);
Paging<ISpotifyObject> page = await me.GetCurrentUserPlaylists(<ACCESS_TOKEN>);

//Create a playlist
//Returns a playlist object representing the newly created playlist
Playlist success = await me.CreatePlaylist(<PLAYLIST_NAME>, <IS_PUBLIC>, <IS_COLLABORATIVE>, <DESCRIPTION>, <ACCESS_TOKEN>);

```




