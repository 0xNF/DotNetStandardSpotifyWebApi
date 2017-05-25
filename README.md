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
 
 //Refresh the OAuth Access Token
 OAuthCredentials token = await AuthorizationCodeFlow.RefreshAccessToken(<OAUTHCREDENTIALS>, <CLIENT_ID>, <CLIENT_SECRET>);
        //OR
 OAuthCredentials token = await AuthorizationCodeFlow.RefreshAccessToken(<REFRESH_TOKEN>, <CLIENT_ID>, <CLIENT_SECRET>);
```

### Spotify Objects
User
```
using DotNetStandardSpotifyWebApi.ObjectModel;

//Get the currently Spotify User
User currentUser = await User.GetCurrentUser(<ACCESS_TOKEN>);

//Get an arbitrary Spotify user
User someUser = await User.GetUser(<USER_ID>, <ACCESS_TOKEN>);
```



