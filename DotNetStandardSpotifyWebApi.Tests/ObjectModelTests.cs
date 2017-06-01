using System;
using Xunit;
using DotNetStandardSpotifyWebApi.Helpers;
using DotNetStandardSpotifyWebApi.Authorization;
using DotNetStandardSpotifyWebApi.ObjectModel;
using System.Threading.Tasks;

namespace DotNetStandardSpotifyWebApi.Tests
{
    public class ObjectModelTests
    {
        //The user to test with
        private const string CurrentUserId = "nishumvar";

        //An Artist the current user follows
        private const string Artist_Follow = "13FGWDOwAoQyIBuZLtCjN9"; //Taku Takahashi

        //An Artist the current user does not follow
        private const string Artist_NoFollow = "13FGWDOwAoQyIBuZLtCjN9"; // The Rolling Stones

        //A User the current user follows
        private const string User_Follow = "22vlmkwgjsj2qt7wmztbymneq"; //Tanya Giang

        //A User the current user does not follow
        private const string User_NoFollow = "22vlmkwgjsj2qt7wmztbymneq"; //Paul Gasca

        //An Album the current user has saved
        private const string Album_Saved = "1BhxcmomhUniLCu173Rpn4"; //Tower of Heaven (Original Soundtrack) - flashygoodness

        //An Album the current user has not saved
        private const string Album_NoSaved = "4l4u9e9jSbotSXNjYfOugy"; //Let It Bleed - The Rolling Stones

        //A playlist the current user follows
        private const string Playlist_Follow = "5gS0M9C45kZvJfgLRKRTJ1"; //This House - nishumvar

        //A playlist the current user does not follow
        private const string Playlist_NoFollow = "06m5HzAGIkYyLsDdNpWoCp"; //Rolling Stones Best Of

        //A track the current user has in their library
        private const string Track_Saved = "6Prexw6BkRje5joeSDg0iN"; //Deezy Daisy - Oxford Remix

        //A track the current user does not have in their library
        private const string Track_NoSaved = "6Prexw6BkRje5joeSDg0iN"; //Gimme Shelter - The Rolling Stones

        private const string CategoryCheck = "party";

        //Credentials object to use in testing
        OAuthCredentials Creds = null;

        //Private setup function called by each test to ensure that the spotify api can be successfully called
        private async Task setupCreds(){
            if(Creds == null){
                Secrets.InitializeAppAPIKeys();
                Creds = await AuthorizationCodeFlow.RefreshAccessToken(Secrets.RefreshToken, Secrets.client_id, Secrets.client_secret);
            }
        }


        [Fact]
        public async void ShouldGetCurrentUser(){
            await setupCreds();
            User me = await Endpoints.GetCurrentUser(Creds.Access_token);
            Assert.False(me.WasError, "Object Error");
            Assert.True(me.Id == CurrentUserId, $"Expected {CurrentUserId}, got {me.Id}");
        }

        public async void ShouldGetAnAlbum(){
            await setupCreds();

        }

        public async void ShouldGetSeveralAlbums(){
            await setupCreds();

        }

        public async void ShouldGetAnAblumsTracks(){
            await setupCreds();

        }

        public async void ShouldGetAnArtist(){
            await setupCreds();

        }

        public async void ShouldGetSeveralArtists(){
            await setupCreds();

        }

        public async void ShouldGetAnArtistsAlbums(){
            await setupCreds();

        }

        public async void ShouldGetAnArtistsTopTracks(){
            await setupCreds();

        }

        public async void ShouldGetRelatedArtists(){
            await setupCreds();

        }

        public async void ShouldGetAudoAnalysis(){
            await setupCreds();

        }

        public async void ShouldGetAudioFeatures(){
            await setupCreds();

        }

        public async void ShoudlGetSeveralAudioFeatures(){
            await setupCreds();

        }

        public async void ShouldGetFeaturedPlaylists(){
            await setupCreds();

        }

        public async void ShouldGetNewReleases(){
            await setupCreds();

        }

        public async void ShouldGetCategories(){
            await setupCreds();

        }

        public async void ShouldGetACategory(){
            await setupCreds();

        }

        public async void ShouldGetCategoriesPlaylists(){
            await setupCreds();

        }

        public async void ShouldGetUsersFollowedArtists(){
            await setupCreds();

        }

        public async void ShouldFollowAnArtist(){
            await setupCreds();

        }

        public async void ShouldFollowAUser(){
            await setupCreds();

        }

        public async void ShouldUnfollowAnArtist(){
            await setupCreds();

        }

        public async void ShouldUnfollowAUser(){
            await setupCreds();

        }

        public async void ShouldCheckIfUserFollowsUser(){
            await setupCreds();

        }

        public async void ShouldCheckIfUserFollowsArtist(){
            await setupCreds();

        }

        public async void ShouldFollowAPlaylist(){
            await setupCreds();

        }

        public async void ShouldUnfollowAPlaylist(){
            await setupCreds();

        }

        public async void ShouldSaveTrackForUser(){
            await setupCreds();

        }

        public async void ShouldGetUsersSavedTracks(){
            await setupCreds();

        }

        public async void ShouldRemoveUsersSavedTrack(){
            await setupCreds();

        }

        public async void ShouldCheckUsersSavedTracks(){
            await setupCreds();

        }

        public async void ShouldSaveAlbumForUser(){
            await setupCreds();

        }

        public async void ShouldGetUsersSavedAlbums(){
            await setupCreds();

        }

        public async void ShoudlRemoveUsersSavedAlbum(){
            await setupCreds();

        }

        public async void ShoudlCheckUsersSavedAlbums(){
            await setupCreds();

        }


    }

}
