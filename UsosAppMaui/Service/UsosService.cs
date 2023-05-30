using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UsosAppMaui.Dto.User;
using UsosAppMaui.Utility;

namespace UsosAppMaui.Service
{
    public class UsosService
    {
        public async Task<UserDto> getUserData()
        {
            HttpClient httpClient = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Get, UsosProp.USER_DATA_URL))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", getAuthorizationValues());

                HttpResponseMessage response =  httpClient.Send(request);
                string responseData = await response.Content.ReadAsStringAsync();
                UserDto user = JsonConvert.DeserializeObject<UserDto>(responseData);
                return user;
            }
        }
        private string getAuthorizationValues()
        {
            return "oauth_consumer_key=" + UsosProp.CONSUMER_KEY + "," +
                    "oauth_signature_method=" + UsosProp.OAUTH_SIGNATURE_METHOD + "," +
                    "oauth_signature=" + UsosProp.CONSUMER_SECRET + "&" + App.AccessToken.oauth_token_secret +","+
                    "oauth_timestamp=" + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + "," +
                    "oauth_nonce=" + NonceGenerator.generateNonce() + "," +
                    "oauth_version=" + UsosProp.OAUTH_VERSION + "," +
                    "oauth_token=" + App.AccessToken.oauth_token;
        }
    }
}
