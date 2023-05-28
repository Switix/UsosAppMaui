using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsosAppMaui.Utility
{
    public static class UsosProp
    {
        public const string CONSUMER_KEY = "FbxCJejmkL8md6fe2s8f";
        public const string CONSUMER_SECRET = "66R5XZZauwSLbsh4dTcns8CRc5pj2stLxd5h5KLD";
        public const string CALLBACK_URL = "http://localhost:8080/callback/";
        public const string REQUEST_TOKEN_URL = "https://api.ukw.edu.pl/services/oauth/request_token";
        public const string ACCESS_TOKEN_URL = "https://api.ukw.edu.pl/services/oauth/access_token";
        public const string AUTHORIZATION_URL = "https://api.ukw.edu.pl/services/oauth/authorize";
        public const string OAUTH_VERSION = "1.0";
        public const string OAUTH_SIGNATURE_METHOD = "PLAINTEXT";
        public const string SCOPES = "email|grades|photo|studies";
    }
}
