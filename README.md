DotNetStandardSpotifyWebApi
===

A wrapper for the Spotify Web API written for .Net Standard

Currently targets .Net Standard v1.4

### Authorization
Get an OAuth Token using the Authorization Code Flow
```
 AuthorizationInProgress flow = AuthorizationCodeFlow.GetAuthStateAndRedirect(<CLIENT_ID>, <REDIRECT_URI>, <SCOPE>);

 OAuthCredentials token = await AuthorizationCodeFlow.GetSpotifyTokenCredentials(<OAUTH_CODE>, <CLIENT_ID>, <CLIENT_SECRET>, <REDIRECT_URI>);
```

Refresh the OAuth Access Token
```
 OAuthCredentials token = await AuthorizationCodeFlow.RefreshAccessToken(<OAUTHCREDENTIALS>, <CLIENT_ID>, <CLIENT_SECRET>);
 //OR
 OAuthCredentials token = await AuthorizationCodeFlow.RefreshAccessToken(<REFRESH_TOKEN>, <CLIENT_ID>, <CLIENT_SECRET>);
```

### Spotify Objects
Get the current spotify user
```
ObjectModel.User currentUser = ObjectModel.User.GetCurrentUser(token.Access_token);
```


