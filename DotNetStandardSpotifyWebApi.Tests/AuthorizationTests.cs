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

        // [Fact]
        // public async void ShouldFailToAuthorize(){
        //     OAuthCredentials token = await AuthorizationCodeFlow.GetSpotifyTokenCredentials(FakeOAuthCode, FakeClientId, fakeClientSecret, fakeRedirectUrl);
        //     Assert.True(token.WasError, "Given false authorization codes, auth should fail");
        // }

        // [Fact]
        // public async void ShouldSuccessfullyReturnOAuthToken(){
        //     // Helpers.AuthorizationInProgress
        //     Assert.True(true, "TBI");
        // }

    }
}
