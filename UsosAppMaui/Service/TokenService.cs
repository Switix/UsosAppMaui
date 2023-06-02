using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UsosAppMaui.Dto;
using UsosAppMaui.Utility;

namespace UsosAppMaui.Service
{
    public class TokenService
    {
        private TokenDto requestToken;
        public async Task<string> getRequestToken()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization","OAuth "+getRequestTokenValues());

            var data = new Dictionary<string, string>
            {
                {"scopes", UsosProp.SCOPES}
            }; 

            HttpResponseMessage response = await httpClient.PostAsync(UsosProp.REQUEST_TOKEN_URL, new FormUrlEncodedContent(data));
            string responseData = await response.Content.ReadAsStringAsync();
            requestToken = TokenDto.from(responseData);
            return UsosProp.AUTHORIZATION_URL + "?oauth_token=" + requestToken.oauth_token + "&oauth_token_secret=" + requestToken.oauth_token_secret;
            


        }

        public async void getAccessToken(string oAuthToken, string oAuthVerifier)
        {
            HttpClient httpClient = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Post, UsosProp.ACCESS_TOKEN_URL))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", getAccessTokenValues(oAuthToken,oAuthVerifier));
                

                HttpResponseMessage response = await httpClient.SendAsync(request);
                string responseData = await response.Content.ReadAsStringAsync();
                App.AccessToken = TokenDto.from(responseData);
                //save to file
                string accesTokenString = JsonConvert.SerializeObject(App.AccessToken);
                Preferences.Default.Set(nameof(TokenDto), accesTokenString);
            }
        }

        private string getRequestTokenValues()
        {
            return "oauth_consumer_key=" + UsosProp.CONSUMER_KEY + "," +
                    "oauth_signature_method=" + UsosProp.OAUTH_SIGNATURE_METHOD + "," +
                    "oauth_signature=" + UsosProp.CONSUMER_SECRET + "&," +
                    "oauth_timestamp=" + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + "," +
                    "oauth_nonce=" + NonceGenerator.generateNonce() + "," +
                    "oauth_version=" + UsosProp.OAUTH_VERSION + "," +
                    "oauth_callback=" + UsosProp.CALLBACK_URL;
        }
        private string getAccessTokenValues(string oAuthToken, string oAuthVerifier)
        {
            return  "oauth_consumer_key=" + UsosProp.CONSUMER_KEY + "," +
                    "oauth_signature_method=" + UsosProp.OAUTH_SIGNATURE_METHOD + "," +
                    "oauth_signature=" + UsosProp.CONSUMER_SECRET + "&" + requestToken.oauth_token_secret + ", " +
                    "oauth_timestamp=" + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + "," +
                    "oauth_nonce=" + NonceGenerator.generateNonce() + "," +
                    "oauth_version=" + UsosProp.OAUTH_VERSION + "," +
                    "oauth_token=" + oAuthToken + "," +
                    "oauth_verifier=" + oAuthVerifier;
        }
    }
}
