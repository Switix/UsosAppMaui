using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UsosAppMaui.Model;
using UsosAppMaui.Model.Course;
using UsosAppMaui.Model.Map;
using UsosAppMaui.Model.TimeTable;
using UsosAppMaui.Utility;

namespace UsosAppMaui.Service
{
    public class UsosService
    {
        public async Task<Person> getUserData()
        {
            using(HttpClient httpClient = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, UsosProp.USER_DATA_URL))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", getAuthorizationValues());

                HttpResponseMessage response =  httpClient.Send(request);
                string responseData = await response.Content.ReadAsStringAsync();
                Person user = JsonConvert.DeserializeObject<Person>(responseData);
                return user;
            }
        }

        public Dictionary<string, List<Term>> getUserCourses()
        {

            using (HttpClient httpClient = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, UsosProp.COURSES_URL))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", getAuthorizationValues());
                var data = new Dictionary<string, string>
                {
                    {"fields", UsosProp.COURSES_FIELDS},
                    {"active_terms_only", UsosProp.COURSES_ACTIVE_TERMS_ONLY },
                };
                request.Content = new FormUrlEncodedContent(data);

                HttpResponseMessage response = httpClient.Send(request);
                string responseData = response.Content.ReadAsStringAsync().Result;

                Course course = JsonConvert.DeserializeObject<Course>(responseData);
                Dictionary<string, List<Term>> terms = new Dictionary<string, List<Term>>();

                foreach (var term in course.course_editions.Terms)
                {
                    var groups = term.Value.ToObject<List<Term>>();
                    terms.Add(term.Key, groups);
                }
                return terms;
            }
           

            
        }

        public List<Lesson> getTimeTable(string start ="")
        {
            if(start == "")
            {
                start = DateTime.Now.ToString("yyyy-MM-dd");
            }
            
            using (HttpClient httpClient = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, UsosProp.TIMETABLE_URL))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", getAuthorizationValues());
                var data = new Dictionary<string, string>
                {
                    {"start",start}
                };
                request.Content = new FormUrlEncodedContent(data);

                HttpResponseMessage response = httpClient.Send(request);
                string responseData = response.Content.ReadAsStringAsync().Result;

                List<Lesson> lessons = JsonConvert.DeserializeObject<List<Lesson>>(responseData);
                return lessons;
            }
        }

        public  List<Building> getBuildings()
        {   
            using (HttpClient httpClient = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, UsosProp.BUILDINGS_URL))
            {

                HttpResponseMessage response = httpClient.Send(request);
                string responseData =  response.Content.ReadAsStringAsync().Result;
                List<Building> buildings = JsonConvert.DeserializeObject<List<Building>>(responseData);
                return buildings;
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
