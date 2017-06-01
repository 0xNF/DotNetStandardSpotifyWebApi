using System;
using Xunit;
using DotNetStandardSpotifyWebApi.Helpers;
using DotNetStandardSpotifyWebApi.Authorization;
namespace DotNetStandardSpotifyWebApi.Tests
{
    public class AuthorizationTests
    {
        private const string FakeOAuthCode = "fakeOauth";
        private const string FakeClientId = "fakeClientId";
        private const string fakeClientSecret = "fakeSecret";
        private const string fakeRedirectUrl = "fakeUri";

        //Dummy app with full permissions for API testing purposes
        private const string Client_Id = "293676b585fa44739c6f334c184d6e96";
        private const string Client_Secret = "e0f16720c65e4298ba505abd37646434";
        private static string RefreshToken {get; set;}
        private static string AccessToken {get; set;}

        [Fact]
        public async void ShouldFailToAuthorize(){
            OAuthCredentials token = await AuthorizationCodeFlow.GetSpotifyTokenCredentials(FakeOAuthCode, FakeClientId, fakeClientSecret, fakeRedirectUrl);
            Assert.True(token.WasError, "Given false authorization codes, auth should fail");
        }

        // [Fact]
        // public async void ShouldSuccessfullyReturnOAuthToken(){
        //     // Helpers.AuthorizationInProgress
        //     Assert.True(true, "TBI");
        // }

    }
}
