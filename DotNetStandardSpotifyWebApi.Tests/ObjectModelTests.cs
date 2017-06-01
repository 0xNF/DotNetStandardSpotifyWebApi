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
        OAuthCredentials Creds = null;

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
            Assert.True(me.Id == "nishumvar", $"Expected nishumvar, got {me.Id}");
        }

    }
}
