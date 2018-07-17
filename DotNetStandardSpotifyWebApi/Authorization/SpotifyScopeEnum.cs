using System.Collections.Generic;

namespace DotNetStandardSpotifyWebApi.Authorization {
    public sealed class SpotifyScopeEnum { 

        public int Value { get; }
        public string Name { get; }
        public string Description { get; }
        public string Prompt { get; }
        

        #region Enum Stuff
        private static Dictionary<int, SpotifyScopeEnum> _typesById { get; } = new Dictionary<int, SpotifyScopeEnum>();
        private static Dictionary<string, SpotifyScopeEnum> _typesByName { get; } = new Dictionary<string, SpotifyScopeEnum>();
        private static int currentId { get; set; } = -1;

        private SpotifyScopeEnum(int value, string name) {
            this.Name = name;
            this.Value = value;
            _typesById.Add(value, this);
            _typesByName.Add(name, this);
            currentId += 1;
        }
        
        private SpotifyScopeEnum(string name, string description, string prompt) {
            this.Name = name;
            this.Value = currentId;
            this.Prompt = prompt;
            this.Description = description;
            _typesById.Add(this.Value, this);
            _typesByName.Add(name, this);
            currentId += 1;
        }

        public static SpotifyScopeEnum fromInt(int value) {
            return _typesById[value];
        }

        public static SpotifyScopeEnum fromName(string name) {
            return _typesByName[name];
        }


        public override string ToString() {
            return Name;
        }
        #endregion

        #region list of Scopes
        public readonly static SpotifyScopeEnum PLAYLIST_READ_PRIVATE = new SpotifyScopeEnum("playlist-read-private", "Read access to user's private playlists.", "Access your private playlists");
        public readonly static SpotifyScopeEnum PLAYLIST_READ_COLLABORATIVE = new SpotifyScopeEnum("playlist-read-collaborative", "Include collaborative playlists when requesting a user's playlists.", "Access your collaborative playlists");
        public readonly static SpotifyScopeEnum PLAYLIST_MODIFY_PUBLIC = new SpotifyScopeEnum("playlist-modify-public", "Write access to a user's public playlists.", "Manage your public playlists");
        public readonly static SpotifyScopeEnum PLAYLIST_MODIFY_PRIVATE = new SpotifyScopeEnum("playlist-modify-private", "Write access to a user's private playlists.", "Manage your private playlists");
        public readonly static SpotifyScopeEnum STREAMING = new SpotifyScopeEnum("streaming", "Control playback of a Spotify track. This scope is currently available to Spotify Playback SDKs, including the iOS SDK, Android SDK and Web Playback SDK. The user must have a Spotify Premium account.", "Play music and control playback on your other devices");
        public readonly static SpotifyScopeEnum USER_FOLLOW_MODIFY = new SpotifyScopeEnum("user-follow-modify", "Write/delete access to the list of artists and other users that the user follows.", "Manage who you are following");
        public readonly static SpotifyScopeEnum USER_FOLLOW_READ = new SpotifyScopeEnum("user-follow-read", "Read access to the list of artists and other users that the user follows.", "Access your followers and who you are following");
        public readonly static SpotifyScopeEnum USER_LIBRARY_READ = new SpotifyScopeEnum("user-library-read", "Read access to a user's \"Your Music\" library.", "Access your saved tracks and albums");
        public readonly static SpotifyScopeEnum USER_LIBRARY_MODIFY = new SpotifyScopeEnum("user-library-modify", "Write/delete access to a user's \"Your Music\" library.", "Manage your saved tracks and albums");
        public readonly static SpotifyScopeEnum USER_READ_PRIVATE = new SpotifyScopeEnum("user-read-private", "Read access to user’s subscription details (type of user account).", "Access your subscription details");
        public readonly static SpotifyScopeEnum USER_READ_BIRTHDATE = new SpotifyScopeEnum("user-read-birthdate", "Read access to the user's birthdate.", "Receive your birthdate");
        public readonly static SpotifyScopeEnum USER_READ_EMAIL = new SpotifyScopeEnum("user-read-email", "Read access to user’s email address.", "Get your real email address");
        public readonly static SpotifyScopeEnum USER_TOP_READ = new SpotifyScopeEnum("user-top-read", "Read access to a user's top artists and tracks", "Read your top artists and tracks");
        public readonly static SpotifyScopeEnum USER_READ_PLAYBACK_STATE = new SpotifyScopeEnum("user-read-playback-state", "Read access to a user’s player state.", "Read your currently playing track and Spotify Connect devices information.");
        public readonly static SpotifyScopeEnum USER_MODIFY_PLAYBACK_STATE = new SpotifyScopeEnum("user-modify-playback-state", "Write access to a user’s playback state", "Control playback on your Spotify clients and Spotify Connect devices.");
        public readonly static SpotifyScopeEnum USER_READ_RECENTLY_PLAYED = new SpotifyScopeEnum("user-read-recently-played", "Read access to a user’s recently played tracks.", "Access your recently played items.");
        public readonly static SpotifyScopeEnum UGC_IMAGE_UPLOAD = new SpotifyScopeEnum("ugc-image-uploa", "Upload playlist cover image.", "Upload images to Spotify on your behalf");
        public readonly static SpotifyScopeEnum APP_REMOTE_CONTROL = new SpotifyScopeEnum("app-remote-control", "Remote control playback of Spotify. This scope is currently available to Spotify iOS and Android App Remote SDKs.", "Communicate with the Spotify app on your device.");

        #endregion

    }
}

