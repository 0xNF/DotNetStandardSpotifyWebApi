namespace DotNetStandardSpotifyWebApi.Authorization {
    public class AuthorizationInProgress {
        public string StateValue { get; }
        public string RedirectUrl { get; }

        internal AuthorizationInProgress(string state, string redirect) {
            this.StateValue = state;
            this.RedirectUrl = redirect;
        }

    }
}

