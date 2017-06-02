using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetStandardSpotifyWebApi.Tests
{
    public class SecretManager {
        public static string RefreshToken { get; set; } = string.Empty;
        private static string AccessToken { get; set; } = string.Empty;

        public static string SecretsFile = "AppSecrets.txt";
        public static string client_id { get; private set; }
        public static string client_secret { get; private set; }
        public static string redirect_uri { get; private set; }
        public const string authKey = "auth_state";
        public const int authStateLength = 64;

        public static void InitializeAppAPIKeys() {
            string[] text = System.IO.File.ReadAllLines(SecretsFile);
            client_id = text[0].Split('=')[1];
            client_secret = text[1].Split('=')[1];
            redirect_uri = text[2].Split('=')[1];
            /** new  */
            RefreshToken = text[3].Split('=')[1];
        }
    }
}
