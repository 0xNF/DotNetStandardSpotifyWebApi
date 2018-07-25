using Xunit;
using DotNetStandardSpotifyWebApi.Helpers;
using DotNetStandardSpotifyWebApi.Authorization;
using DotNetStandardSpotifyWebApi.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace DotNetStandardSpotifyWebApi.Tests {
    //    public class ObjectModelTests {

    //        //The user to test with
    //        private const string CurrentUserId = "nishumvar";

    //        //An Artist the current user follows
    //        private const string Artist_Follow = "13FGWDOwAoQyIBuZLtCjN9"; //Taku Takahashi

    //        //An Artist the current user does not follow
    //        private const string Artist_NoFollow = "22bE4uQ6baNwSHPVcDxLCe"; // The Rolling Stones

    //        //A User the current user follows
    //        private const string User_Follow = "22vlmkwgjsj2qt7wmztbymneq"; //Tanya Giang

    //        //A User the current user does not follow
    //        private const string User_NoFollow = "canaloff"; //canaloff

    //        //An Album the current user has saved
    //        private const string Album_Saved = "1BhxcmomhUniLCu173Rpn4"; //Tower of Heaven (Original Soundtrack) - flashygoodness

    //        //An Album the current user has not saved
    //        private const string Album_NoSaved = "4l4u9e9jSbotSXNjYfOugy"; //Let It Bleed - The Rolling Stones

    //        //A playlist the current user follows
    //        private const string Playlist_Follow = "5gS0M9C45kZvJfgLRKRTJ1"; //This House - nishumvar

    //        //A playlist the current user does not follow
    //        private const string Playlist_NoFollow = "06m5HzAGIkYyLsDdNpWoCp"; //Rolling Stones Best Of

    //        //A track the current user has in their library
    //        private const string Track_Saved = "6Prexw6BkRje5joeSDg0iN"; //Deezy Daisy - Oxford Remix

    //        //A track the current user does not have in their library
    //        private const string Track_NoSaved = "6H3kDe7CGoWYBabAeVWGiD"; //Gimme Shelter - The Rolling Stones

    //        //Track URIs for endpoints that require multiple track uris
    //        private static List<string> TrackUris = new List<string>() {
    //                "spotify:track:7rXhnFjG74YKMgq0R89Bpz",//Papi
    //                "spotify:track:1TG5DvegcKAJOuKmKCKOIU",//Over You
    //                "spotify:track:3GK0gr4QMLeXSm50eCBWp8", //Dance - Oliver Remix
    //            };

    //        //A sample Category to get
    //        private const string CategoryCheck = "party";

    //        //Credentials object to use in testing
    //        OAuthCredentials Creds = null;

    //        //Private setup function called by each test to ensure that the spotify api can be successfully called
    //        private async Task SetupCredentials() {
    //            if (Creds == null) {
    //                SecretManager.InitializeAppAPIKeys();
    //                Creds = await AuthorizationCodeFlow.RefreshAccessToken(SecretManager.RefreshToken, SecretManager.client_id, SecretManager.client_secret);
    //            }
    //        }


    //        [Fact]
    //        public async void ShouldGetCurrentUser() {
    //            await SetupCredentials();
    //            WebResult<User> me = await Endpoints.GetCurrentUser(Creds.Access_token);
    //            Assert.False(me.WasError, "Object Error");
    //            Assert.True(me.Id == CurrentUserId, $"Expected {CurrentUserId}, got {me.Id}");
    //        }

    //        public async void ShouldGetAnAlbum() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetSeveralAlbums() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetAnAblumsTracks() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetAnArtist() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetSeveralArtists() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetAnArtistsAlbums() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetAnArtistsTopTracks() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetRelatedArtists() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetAudoAnalysis() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetAudioFeatures() {
    //            await SetupCredentials();

    //        }

    //        public async void ShoudlGetSeveralAudioFeatures() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetFeaturedPlaylists() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetNewReleases() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetCategories() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetACategory() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetCategoriesPlaylists() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetUsersFollowedArtists() {
    //            await SetupCredentials();

    //        }

    //        [Fact]
    //        public async void ShouldFollowAnArtist() {
    //            await SetupCredentials();
    //            List<string> ids = new List<string>() {
    //                Artist_NoFollow
    //            };
    //            RegularError res = await Endpoints.FollowArtists(Creds.Access_token, ids);
    //            Assert.False(res.WasError, "Object Error");
    //            IReadOnlyList<bool> bools = await Endpoints.CheckCurrentUserFollowsArtists(Creds.Access_token, ids);
    //            Assert.True(bools[0], $"Expected to follow artist {ids[0]}, but don't");
    //            //Restoring to default state
    //            await Endpoints.UnfollowArtists(Creds.Access_token, ids);
    //        }

    //        [Fact]
    //        public async void ShouldFollowAUser() {
    //            await SetupCredentials();
    //            List<string> ids = new List<string>() {
    //                User_NoFollow
    //            };
    //            RegularError res = await Endpoints.FollowUsers(Creds.Access_token, ids);
    //            Assert.False(res.WasError, "Object Error");
    //            IReadOnlyList<bool> bools = await Endpoints.CheckCurrentUserFollowsUsers(Creds.Access_token, ids);
    //            Assert.True(bools[0], $"Expected to follow User {ids[0]}, but don't");
    //            //Restoring to default state
    //            await Endpoints.UnfollowUsers(Creds.Access_token, ids);

    //        }

    //        [Fact]
    //        public async void ShouldUnfollowAnArtist() {
    //            await SetupCredentials();
    //            List<string> ids = new List<string>() {
    //                Artist_NoFollow
    //            };
    //            RegularError res = await Endpoints.UnfollowArtists(Creds.Access_token, ids);
    //            Assert.False(res.WasError, "Object Error");
    //            IReadOnlyList<bool> bools = await Endpoints.CheckCurrentUserFollowsArtists(Creds.Access_token, ids);
    //            Assert.False(bools[0], $"Expected to not follow artist {ids[0]}, but do");
    //        }

    //        [Fact]
    //        public async void ShouldUnfollowAUser() {
    //            await SetupCredentials();
    //            List<string> ids = new List<string>() {
    //                User_NoFollow
    //            };
    //            RegularError res = await Endpoints.UnfollowUsers(Creds.Access_token, ids);
    //            Assert.False(res.WasError, "Object Error");
    //            IReadOnlyList<bool> bools = await Endpoints.CheckCurrentUserFollowsUsers(Creds.Access_token, ids);
    //            Assert.False(bools[0], $"Expected to not follow artist {ids[0]}, but do");
    //        }

    //        public async void ShouldCheckIfUserFollowsUser() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldCheckIfUserFollowsArtist() {
    //            await SetupCredentials();

    //        }

    //        [Fact]
    //        public async void ShouldFollowAPlaylist() {
    //            await SetupCredentials();
    //            //https://open.spotify.com/user/rollingstonesmusic/playlist/06m5HzAGIkYyLsDdNpWoCp
    //            RegularError res = await Endpoints.FollowAPlaylist(Creds.Access_token, "rollingstonesmusic", Playlist_NoFollow);
    //            Assert.False(res.WasError, "Object Error");
    //            IReadOnlyList<bool> bools = await Endpoints.CheckUsersFollowsPlaylist(Creds.Access_token, "rollingstonesmusic", Playlist_NoFollow, new string[] { CurrentUserId });
    //            Assert.True(bools[0], $"Expected to follow playlist {Playlist_NoFollow}, but don't");
    //            //Restoring to default state
    //            await Endpoints.UnfollowAPlaylist(Creds.Access_token, "rollingstonesmusic", Playlist_NoFollow);
    //        }

    //        public async void ShouldUnfollowAPlaylist() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldSaveTrackForUser() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetUsersSavedTracks() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldRemoveUsersSavedTrack() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldCheckUsersSavedTracks() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldSaveAlbumForUser() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldGetUsersSavedAlbums() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldRemoveUsersSavedAlbum() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldCheckUsersSavedAlbums() {
    //            await SetupCredentials();

    //        }

    //        [Fact]
    //        public async void ShouldGetUsersTopArtists() {
    //            await SetupCredentials();
    //            Paging<Artist> page = await Endpoints.GetUsersTopArtists(Creds.Access_token);
    //            Assert.False(page.WasError, "Object Error");
    //            Assert.True(page.Items.Count == 20, $"Expected 20 items, got {page.Items.Count}");
    //        }

    //        [Fact]
    //        public async void ShouldGetUsersTopTracks() {
    //            await SetupCredentials();
    //            Paging<Track> page = await Endpoints.GetUsersTopTracks(Creds.Access_token);
    //            Assert.False(page.WasError, "Object Error");
    //            Assert.True(page.Items.Count == 20, $"Expected 20 items, got {page.Items.Count}");
    //        }

    //        public async void ShouldGetRecommendations() {
    //            await SetupCredentials();
    //        }

    //        public async void ShouldSearchForArtists() {
    //            await SetupCredentials();
    //        }

    //        public async void ShouldSearchForAlbums() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldSearchForTracks() {
    //            await SetupCredentials();

    //        }

    //        public async void ShouldSearchForPlaylists() {
    //            await SetupCredentials();

    //        }

    //        public async void ShoudlSearchForEverything() {
    //            await SetupCredentials();

    //        }

    //        [Fact]
    //        public async void ShouldGetATrack() {
    //            await SetupCredentials();
    //            Track t = await Endpoints.GetATrack(Creds.Access_token, Track_Saved);
    //            Assert.False(t.WasError, "Object Error");
    //            Assert.True(t.Name == "Deezy Daisy - Oxford Remix", $"Expected Deezy Daisy - Oxford Remix, got {t.Name}");
    //        }

    //        [Fact]
    //        public async void ShouldGetSeveralTracks() {
    //            await SetupCredentials();
    //            List<string> ids = new List<string>(){
    //              Track_Saved,
    //              Track_NoSaved,
    //            };
    //            List<Track> tracks = (await Endpoints.GetSeveralTracks(Creds.Access_token, ids)).ToList();
    //            Assert.True(tracks.Count == 2, $"Expected 2 tracks, got {tracks.Count}");
    //            Assert.True(tracks[0].Name == "Deezy Daisy - Oxford Remix", $"Expected Deezy Daisy - Oxford Remix, got {tracks[0].Name}");
    //            Assert.True(tracks[1].Name == "Gimme Shelter", $"Expected Gimme Shelter, got {tracks[1].Name}");
    //        }

    //        [Fact]
    //        public async void GetAUsersPublicProfile() {
    //            await SetupCredentials();
    //            User u = await Endpoints.GetUsersProfile(Creds.Access_token, User_Follow);
    //            Assert.False(u.WasError, "Object Error");
    //            Assert.True(u.DisplayName == "Tanya Giang", $"Expected Tanya Giang, got {u.DisplayName}");
    //        }

    //        [Fact]
    //        public async void GetAUsersPublicPlaylists() {
    //            await SetupCredentials();
    //            Paging<Playlist> page = await Endpoints.GetUsersPlaylists(Creds.Access_token, User_Follow);
    //            Assert.False(page.WasError, "Object Error");
    //            Assert.True(page.Total >= 7, $"Expected at least 7 items, got {page.Total}");
    //        }

    //        [Fact]
    //        public async void GetCurrentUsersPlaylists() {
    //            await SetupCredentials();
    //            Paging<Playlist> page = await Endpoints.GetCurrentUsersPlaylists(Creds.Access_token);
    //            Assert.False(page.WasError, "Object Error");
    //            Assert.True(page.Total >= 20, $"Expected at least 20 items, but got {page.Total}");
    //        }

    //        [Fact]
    //        public async void ShouldGetAPlaylist() {
    //            await SetupCredentials();
    //            Playlist p = await Endpoints.GetAPlaylist(Creds.Access_token, CurrentUserId, Playlist_Follow);
    //            Assert.False(p.WasError, "Object Error");
    //            Assert.True(p.Total == 5, $"Expected 5 tracks, got {p.Total}");
    //            Assert.True(p.Name == "This House");
    //        }

    //        [Fact]
    //        public async void ShouldGetAPlaylistsTracks() {
    //            await SetupCredentials();
    //            Paging<PlaylistTrack> page = await Endpoints.GetAPlaylistsTracks(Creds.Access_token, CurrentUserId, Playlist_Follow);
    //            Assert.False(page.WasError, "Object Error");
    //            Assert.True(page.Total == 5, $"Expected 5 tracks, got {page.Total}");
    //        }

    //        [Fact]
    //        public async void ShouldCreateAPlaylist() {
    //            await SetupCredentials();
    //            Playlist created = await Endpoints.CreateAPlaylist(Creds.Access_token, CurrentUserId, "TEST");
    //            Assert.False(created.WasError, "Object Error");
    //        }

    //        [Fact]
    //        public async void ShouldModifyAPlaylist() {
    //            await SetupCredentials();
    //            RegularError res = await Endpoints.ChangePlaylistDetails(Creds.Access_token, CurrentUserId, Playlist_Follow, null, null, "", "Songs for This House, baby.");
    //            Assert.False(res.WasError, "Object Error");
    //        }

    //        [Fact]
    //        public async void ShouldAddSongsToPlaylist() {
    //            await SetupCredentials();
    //            string pid = "58g0qfBM60xjsJadLkzumx";
    //            List<string> uris = new List<string>(){
    //                "spotify:track:7rXhnFjG74YKMgq0R89Bpz"
    //            };
    //            RegularError res = await Endpoints.AddTracksToPlaylist(Creds.Access_token, CurrentUserId, pid, uris);
    //            Assert.False(res.WasError, "Object Error");
    //        }

    //        [Fact]
    //        public async void ShouldRemoveSongsFromPlaylist() {
    //            await SetupCredentials();
    //            string pid = "6cFJgP266Kp31iY8ewuZrv"; // TEST playlist
    //            RegularError res = await Endpoints.RemoveTracksFromPlaylist(Creds.Access_token, CurrentUserId, pid, TrackUris);
    //            Assert.False(res.WasError, "Failed to delete from playlist");
    //        }

    //        [Fact]
    //        public async void ShouldReorderAPlaylistsTracks() {
    //            await SetupCredentials();
    //            //First create a new playlist
    //            Playlist p = await Endpoints.CreateAPlaylist(Creds.Access_token, CurrentUserId, "Test Reordering");
    //            //Then Add songs in a certain order
    //            RegularError AddTrackError = await Endpoints.AddTracksToPlaylist(Creds.Access_token, CurrentUserId, p.Id, TrackUris);
    //            Assert.False(AddTrackError.WasError, "Failed to add tracks to playlist");
    //            //Then reorder
    //            RegularError  ReorderError = await Endpoints.ReorderPlaylistsTracks(Creds.Access_token, CurrentUserId, p.Id, 0, 3); //Should move Papi to end
    //            Assert.False(ReorderError.WasError, "Failed to issue reorder command");
    //            //Then check the reorder
    //            Paging<PlaylistTrack> page = await Endpoints.GetAPlaylistsTracks(Creds.Access_token, CurrentUserId, p.Id);
    //            Assert.True(page.Items[0].Track.Uri.Equals(TrackUris[1]), "Expected the uris to be different.");
    //            Assert.True(page.Items[1].Track.Uri.Equals(TrackUris[2]), "Expected the uris to be different.");
    //            Assert.True(page.Items[2].Track.Uri.Equals(TrackUris[0]), "Expected the uris to be different.");
    //            //Then unfollow the playlist
    //            await Endpoints.UnfollowAPlaylist(Creds.Access_token, CurrentUserId, p.Id);
    //        }

    //        [Fact]
    //        public async void ShouldReplaceAPlaylistsTracks() {
    //            await SetupCredentials();
    //            //First create a new playlist
    //            Playlist p = await Endpoints.CreateAPlaylist(Creds.Access_token, CurrentUserId, "Test Replacement");
    //            //Then Add songs in a certain order
    //            RegularError AddTrackError = await Endpoints.AddTracksToPlaylist(Creds.Access_token, CurrentUserId, p.Id, TrackUris);
    //            Assert.False(AddTrackError.WasError, "Failed to add tracks to playlist");
    //            //Then remove every song
    //            RegularError ReorderError = await Endpoints.ReplacePlaylistTracks(Creds.Access_token, CurrentUserId, p.Id, new List<string>() { "spotify:track:1TG5DvegcKAJOuKmKCKOIU"}); //Should have only Over You in it
    //            Assert.False(ReorderError.WasError, "Failed to issue reorder command at first");
    //            //Then check the reorder
    //            Paging<PlaylistTrack> page = await Endpoints.GetAPlaylistsTracks(Creds.Access_token, CurrentUserId, p.Id);
    //            Assert.True(page.Total == 1, $"Expected there to be 1 item, but playlist still has {TrackUris.Count}.");
    //            //Test for clearing the list
    //            ReorderError = await Endpoints.ReplacePlaylistTracks(Creds.Access_token, CurrentUserId, p.Id, new List<string>()); //Should now be empty
    //            Assert.False(ReorderError.WasError, "Failed to issue replace command at second");
    //            //Then check the reorder
    //            page = await Endpoints.GetAPlaylistsTracks(Creds.Access_token, CurrentUserId, p.Id);
    //            Assert.True(page.Total == 0, $"Expected there to be no items, but playlist still has {TrackUris.Count}.");
    //            //Then unfollow the playlist
    //            await Endpoints.UnfollowAPlaylist(Creds.Access_token, CurrentUserId, p.Id);
    //        }

    //        [Fact]
    //        public async void ShouldGetUsersRecentlyPlayedTracks() {
    //            await SetupCredentials();
    //            CursorBasedPaging<PlayHistory> page = await Endpoints.GetCurrentUsersRecentlyPlayedTracks(Creds.Access_token);
    //            Assert.False(page.WasError, "Object Error");
    //        }

    //        [Fact]
    //        public async void ShouldGetUsersAvailableDevices() {
    //            await SetupCredentials();
    //            IReadOnlyList<Device> devices = await Endpoints.GetUsersAvailableDevices(Creds.Access_token);
    //            Assert.True(devices.Any(), "Expected at least 1 device, got none");
    //        }

    //        [Fact]
    //        public async void ShouldGetCurrentlyPlayingSong() {
    //            await SetupCredentials();
    //            CurrentlyPlayingContext current = await Endpoints.GetUsersCurrentlyPlayingInformation(Creds.Access_token);
    //            Assert.False(current.WasError, "Object Error");
    //        }

    //        [Fact]
    //        public async void ShouldToggleUsersPlayback() {
    //            await SetupCredentials();
    //            //First get current playback devices
    //            IReadOnlyList<Device> devices = await Endpoints.GetUsersAvailableDevices(Creds.Access_token);
    //            Assert.True(devices.Any(), "Expected at least 1 playback device. Got none");

    //            //Get currently playing song so we can restore it later.
    //            CurrentlyPlayingContext current = await Endpoints.GetUsersCurrentlyPlayingInformation(Creds.Access_token);
    //            //Stop playback
    //            RegularError reg = await Endpoints.StartOrResumePlayback(Creds.Access_token, devices[0].Id);
    //            Assert.False(reg.WasError, "Expected no error, got an error");
    //            //Wait 4 seconds
    //            await Task.Delay(4000);
    //            //Restart playback
    //            reg = await Endpoints.StartOrResumePlayback(Creds.Access_token, devices[0].Id, uris:new List<string>() {current.Item.Uri });
    //            Assert.False(reg.WasError, "Expected no error, got an error");

    //        }

    //        [Fact]
    //        public async void ShouldStartPlaybackAtSecondOffset() {
    //            await SetupCredentials();
    //            //First get current playback devices
    //            IReadOnlyList<Device> devices = await Endpoints.GetUsersAvailableDevices(Creds.Access_token);
    //            Assert.True(devices.Any(), "Expected at least 1 playback device. Got none");

    //            //Get currently playing song so we can restore it later.
    //            CurrentlyPlayingContext current = await Endpoints.GetUsersCurrentlyPlayingInformation(Creds.Access_token);

    //            //Start playback
    //            RegularError reg = await Endpoints.StartOrResumePlayback(Creds.Access_token, devices[0].Id, uris:TrackUris, offset:1 );
    //            Assert.False(reg.WasError, "Expected no error, got an error");
    //        }

    //        [Fact]
    //        public async void ShouldPauseUsersPlayback() {
    //            await SetupCredentials();
    //            RegularError res = await Endpoints.PauseUsersPlayback(Creds.Access_token);
    //            Assert.False(res.WasError, $"Expected to puase users playback, but failed. See: {res.Message}");
    //        }

    //        [Fact]
    //        public async void ShouldSkipToNextTrack() {
    //            await SetupCredentials();
    //            RegularError res = await Endpoints.SkipPlaybackToUsersNextTrack(Creds.Access_token);
    //            Assert.False(res.WasError, $"Expected to skip users playback forwards, but failed. See: {res.Message}");

    //        }

    //        [Fact]
    //        public async void ShouldSkipToPreviousTrack() {
    //            await SetupCredentials();
    //            RegularError res = await Endpoints.SkipPlaybackToUsersPreviousTrack(Creds.Access_token);
    //            Assert.False(res.WasError, $"Expected to skip users playback backwards, but failed. See: {res.Message}");
    //        }

    //        [Fact]
    //        public async void ShouldSeekTo0() {
    //            await SetupCredentials();
    //            RegularError res = await Endpoints.SeekToPositionInCurrentlyPlayingTrack(Creds.Access_token, 0);
    //            Assert.False(res.WasError, $"Expected to seek to 0, but failed. See: {res.Message}");
    //        }

    //        [Fact]
    //        public async void ShouldTurnRepeatTrackOn() {
    //            await SetupCredentials();
    //            RegularError res = await Endpoints.SetRepeatModeOnUsersPlayback(Creds.Access_token, RepeatEnum.TRACK);
    //            Assert.False(res.WasError, $"Expected to set repeat, but failed. See: {res.Message}");
    //        }

    //        [Fact]
    //        public async void ShouldTurnVolumeDown() {
    //            await SetupCredentials();
    //            RegularError res = await Endpoints.SetVolumeOnUsersPlayback(Creds.Access_token, 0);
    //            Assert.False(res.WasError, $"Expected to set volume, but failed. See: {res.Message}");
    //        }

    //        [Fact]
    //        public async void ShouldSetShuffleToTrue() {
    //            await SetupCredentials();
    //            RegularError res = await Endpoints.SetShuffleOnPlayback(Creds.Access_token, true);
    //            Assert.False(res.WasError, $"Expected to set shuffle, but failed. See: {res.Message}");
    //        }

    //        [Fact]
    //        public async void ShouldTransferPaybackSession() {
    //            await SetupCredentials();
    //            IReadOnlyCollection<Device> devices = await Endpoints.GetUsersAvailableDevices(Creds.Access_token);
    //            if(devices.Count > 1) {
    //                Device active = devices.Where(x => x.Is_Active).First();
    //                Device inactive = devices.Where(x => !x.Is_Active && !x.Is_Restricted).FirstOrDefault();
    //                if(inactive != null) {
    //                    RegularError res = await Endpoints.TransferUsersPlayback(Creds.Access_token, new List<string>() { inactive.Id }, true);
    //                    Assert.False(res.WasError, "expected to transfer playback, but failed");
    //                }
    //            }
    //        }

    //        [Fact]
    //        public async void ShouldGetAnAudioAnalysis() {
    //            await SetupCredentials();
    //            AudioAnalysis aa = await Endpoints.GetAudioAnalysis(Creds.Access_token, Track_Saved);
    //            Assert.True(aa != null);
    //        }

    //        [Fact]
    //        public async void ShouldGetAvailableSeedGenres() {
    //            await SetupCredentials();
    //            SpotifyList<string> genres = await Endpoints.GetAvailableGenreSeeds(Creds.Access_token);
    //            Assert.False(genres.WasError, "Expected to get genre seeds, but got an error instead");
    //        }

    //        [Fact]
    //        public async void ShoudlChangePlaylistCover() {
    //            string image = "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";
    //            await SetupCredentials();
    //            Playlist created = await Endpoints.CreateAPlaylist(Creds.Access_token, CurrentUserId, "TEST");
    //            Assert.False(created == null, "Expected to create a test playlist ");
    //            RegularError response = await Endpoints.UploadCustomPlaylistCoverimage(Creds.Access_token, CurrentUserId, created.Id, image);
    //            Assert.False(response.WasError, "Expected to change playlists image");
    //            RegularError unfollowed =  await Endpoints.UnfollowAPlaylist(Creds.Access_token, CurrentUserId, created.Id);
    //            Assert.False(unfollowed.WasError, "Expected to unfollow a playlist");
    //        }

    //    //}
}
