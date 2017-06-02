﻿using Xunit;
using DotNetStandardSpotifyWebApi.Helpers;
using DotNetStandardSpotifyWebApi.Authorization;
using DotNetStandardSpotifyWebApi.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace DotNetStandardSpotifyWebApi.Tests {
    public class ObjectModelTests {
        //The user to test with
        private const string CurrentUserId = "nishumvar";

        //An Artist the current user follows
        private const string Artist_Follow = "13FGWDOwAoQyIBuZLtCjN9"; //Taku Takahashi

        //An Artist the current user does not follow
        private const string Artist_NoFollow = "22bE4uQ6baNwSHPVcDxLCe"; // The Rolling Stones

        //A User the current user follows
        private const string User_Follow = "22vlmkwgjsj2qt7wmztbymneq"; //Tanya Giang

        //A User the current user does not follow
        private const string User_NoFollow = "canaloff"; //canaloff

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
        private const string Track_NoSaved = "6H3kDe7CGoWYBabAeVWGiD"; //Gimme Shelter - The Rolling Stones

        private const string CategoryCheck = "party";

        //Credentials object to use in testing
        OAuthCredentials Creds = null;

        //Private setup function called by each test to ensure that the spotify api can be successfully called
        private async Task setupCreds() {
            if (Creds == null) {
                SecretManager.InitializeAppAPIKeys();
                Creds = await AuthorizationCodeFlow.RefreshAccessToken(SecretManager.RefreshToken, SecretManager.client_id, SecretManager.client_secret);
            }
        }


        [Fact]
        public async void ShouldGetCurrentUser() {
            await setupCreds();
            User me = await Endpoints.GetCurrentUser(Creds.Access_token);
            Assert.False(me.WasError, "Object Error");
            Assert.True(me.Id == CurrentUserId, $"Expected {CurrentUserId}, got {me.Id}");
        }

        public async void ShouldGetAnAlbum() {
            await setupCreds();

        }

        public async void ShouldGetSeveralAlbums() {
            await setupCreds();

        }

        public async void ShouldGetAnAblumsTracks() {
            await setupCreds();

        }

        public async void ShouldGetAnArtist() {
            await setupCreds();

        }

        public async void ShouldGetSeveralArtists() {
            await setupCreds();

        }

        public async void ShouldGetAnArtistsAlbums() {
            await setupCreds();

        }

        public async void ShouldGetAnArtistsTopTracks() {
            await setupCreds();

        }

        public async void ShouldGetRelatedArtists() {
            await setupCreds();

        }

        public async void ShouldGetAudoAnalysis() {
            await setupCreds();

        }

        public async void ShouldGetAudioFeatures() {
            await setupCreds();

        }

        public async void ShoudlGetSeveralAudioFeatures() {
            await setupCreds();

        }

        public async void ShouldGetFeaturedPlaylists() {
            await setupCreds();

        }

        public async void ShouldGetNewReleases() {
            await setupCreds();

        }

        public async void ShouldGetCategories() {
            await setupCreds();

        }

        public async void ShouldGetACategory() {
            await setupCreds();

        }

        public async void ShouldGetCategoriesPlaylists() {
            await setupCreds();

        }

        public async void ShouldGetUsersFollowedArtists() {
            await setupCreds();

        }

        public async void ShouldFollowAnArtist() {
            await setupCreds();

        }

        public async void ShouldFollowAUser() {
            await setupCreds();

        }

        public async void ShouldUnfollowAnArtist() {
            await setupCreds();

        }

        public async void ShouldUnfollowAUser() {
            await setupCreds();

        }

        public async void ShouldCheckIfUserFollowsUser() {
            await setupCreds();

        }

        public async void ShouldCheckIfUserFollowsArtist() {
            await setupCreds();

        }

        public async void ShouldFollowAPlaylist() {
            await setupCreds();

        }

        public async void ShouldUnfollowAPlaylist() {
            await setupCreds();

        }

        public async void ShouldSaveTrackForUser() {
            await setupCreds();

        }

        public async void ShouldGetUsersSavedTracks() {
            await setupCreds();

        }

        public async void ShouldRemoveUsersSavedTrack() {
            await setupCreds();

        }

        public async void ShouldCheckUsersSavedTracks() {
            await setupCreds();

        }

        public async void ShouldSaveAlbumForUser() {
            await setupCreds();

        }

        public async void ShouldGetUsersSavedAlbums() {
            await setupCreds();

        }

        public async void ShouldRemoveUsersSavedAlbum() {
            await setupCreds();

        }

        public async void ShouldCheckUsersSavedAlbums() {
            await setupCreds();

        }

        [Fact]
        public async void ShouldGetUsersTopArtists() {
            await setupCreds();
            Paging<Artist> page = await Endpoints.GetUsersTopArtists(Creds.Access_token);
            Assert.False(page.WasError, "Object Error");
            Assert.True(page.Items.Count == 20, $"Expected 20 items, got {page.Items.Count}");
        }

        [Fact]
        public async void ShouldGetUsersTopTracks() {
            await setupCreds();
            Paging<Track> page = await Endpoints.GetUsersTopTracks(Creds.Access_token);
            Assert.False(page.WasError, "Object Error");
            Assert.True(page.Items.Count == 20, $"Expected 20 items, got {page.Items.Count}");
        }

        public async void ShouldGetRecommendations() {
            await setupCreds();
        }

        public async void ShouldSearchForArtists() {
            await setupCreds();
        }

        public async void ShouldSearchForAlbums() {
            await setupCreds();

        }

        public async void ShouldSearchForTracks() {
            await setupCreds();

        }

        public async void ShouldSearchForPlaylists() {
            await setupCreds();

        }

        public async void ShoudlSearchForEverything() {
            await setupCreds();

        }

        [Fact]
        public async void ShouldGetATrack() {
            await setupCreds();
            Track t = await Endpoints.GetATrack(Creds.Access_token, Track_Saved);
            Assert.False(t.WasError, "Object Error");
            Assert.True(t.Name == "Deezy Daisy - Oxford Remix", $"Expected Deezy Daisy - Oxford Remix, got {t.Name}");
        }

        [Fact]
        public async void ShouldGetSeveralTracks() {
            await setupCreds();
            List<string> ids = new List<string>(){
              Track_Saved,
              Track_NoSaved,
            };
            List<Track> tracks = (await Endpoints.GetSeveralTracks(Creds.Access_token, ids)).ToList();
            Assert.True(tracks.Count == 2, $"Expected 2 tracks, got {tracks.Count}");
            Assert.True(tracks[0].Name == "Deezy Daisy - Oxford Remix", $"Expected Deezy Daisy - Oxford Remix, got {tracks[0].Name}");
            Assert.True(tracks[1].Name == "Gimme Shelter", $"Expected Gimme Shelter, got {tracks[1].Name}");
        }

        [Fact]
        public async void GetAUsersPublicProfile() {
            await setupCreds();
            User u = await Endpoints.GetUsersProfile(Creds.Access_token, User_Follow);
            Assert.False(u.WasError, "Object Error");
            Assert.True(u.DisplayName == "Tanya Giang", $"Expected Tanya Giang, got {u.DisplayName}");
        }

        [Fact]
        public async void GetAUsersPublicPlaylists() {
            await setupCreds();
            Paging<Playlist> page = await Endpoints.GetUsersPlaylists(Creds.Access_token, User_Follow);
            Assert.False(page.WasError, "Object Error");
            Assert.True(page.Total >= 7, $"Expected at least 7 items, got {page.Total}");
        }

        [Fact]
        public async void GetCurrentUsersPlaylists() {
            await setupCreds();
            Paging<Playlist> page = await Endpoints.GetCurrentUsersPlaylists(Creds.Access_token);
            Assert.False(page.WasError, "Object Error");
            Assert.True(page.Total >= 20, $"Expected at least 20 items, but got {page.Total}");
        }

        [Fact]
        public async void ShouldGetAPlaylist() {
            await setupCreds();
            Playlist p = await Endpoints.GetAPlaylist(Creds.Access_token, CurrentUserId, Playlist_Follow);
            Assert.False(p.WasError, "Object Error");
            Assert.True(p.Total == 5, $"Expected 5 tracks, got {p.Total}");
            Assert.True(p.Name == "This House");
        }

        [Fact]
        public async void ShouldGetAPlaylistsTracks() {
            await setupCreds();
            Paging<Track> page = await Endpoints.GetAPlaylistsTracks(Creds.Access_token, CurrentUserId, Playlist_Follow);
            Assert.False(page.WasError, "Object Error");
            Assert.True(page.Total == 5, $"Expected 5 tracks, got {page.Total}");
        }

        [Fact]
        public async void ShouldCreateAPlaylist() {
            await setupCreds();
            Playlist created = await Endpoints.CreateAPlaylist(Creds.Access_token, CurrentUserId, "TEST");
            Assert.False(created.WasError, "Object Error");
        }

        [Fact]
        public async void ShouldModifyAPlaylist() {
            await setupCreds();
            RegularError res = await Endpoints.ChangePlaylistDetails(Creds.Access_token, CurrentUserId, Playlist_Follow, null, null, "", "Songs for This House, baby.");
            Assert.False(res.WasError, "Object Error");
        }

        [Fact]
        public async void ShouldAddSongsToPlaylist() {
            await setupCreds();
            string pid = "58g0qfBM60xjsJadLkzumx";
            List<string> uris = new List<string>(){
                "spotify:track:7rXhnFjG74YKMgq0R89Bpz"
            };
            RegularError res = await Endpoints.AddTracksToPlaylist(Creds.Access_token, CurrentUserId, pid, uris);
            Assert.False(res.WasError, "Object Error");
        }

        [Fact]
        public async void ShouldRemoveSongsFromPlaylist() {
            await setupCreds();
            List<string> uris = new List<string>() {
                "spotify:track:7rXhnFjG74YKMgq0R89Bpz",//Papi
                "spotify:track:1TG5DvegcKAJOuKmKCKOIU",//Over You
                "spotify:track:3GK0gr4QMLeXSm50eCBWp8", //Dance - Oliver Remix
            };
            string pid = "6cFJgP266Kp31iY8ewuZrv"; // TEST playlist
            RegularError res = await Endpoints.RemoveTracksFromPlaylist(Creds.Access_token, CurrentUserId, pid, uris);
            Assert.False(res.WasError, "Failed to delete from playlist");
        }

    }
}
