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
using UsosAppMaui.Model.Grade;
using UsosAppMaui.Model.Map;
using UsosAppMaui.Model.TimeTable;
using UsosAppMaui.Model.Unit;
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

        public List<Unit> getUserGrades()
        {
            using (HttpClient httpClient = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, UsosProp.GRADE_TERM_URL))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", getAuthorizationValues());
                var data = new Dictionary<string, string>
                {
                    {"term_ids",getUserTerms() },
                    {"fields", UsosProp.GRADE_FIELDS},
                    {"active_terms_only", UsosProp.COURSES_ACTIVE_TERMS_ONLY },
                };
                request.Content = new FormUrlEncodedContent(data);

                HttpResponseMessage response = httpClient.Send(request);
                string responseData = response.Content.ReadAsStringAsync().Result;

                GradeRoot gradeRoot = JsonConvert.DeserializeObject<GradeRoot>(responseData);

                // unit, ocena
                Dictionary<string, string> unit_grade = new Dictionary<string, string>();

                List<GradeCourse> courses = new List<GradeCourse>();
                List<CourseUnitsGrades> courseUnitsGrades = new List<CourseUnitsGrades>();  
                List<GradeAttempt> gradeAttempts = new List<GradeAttempt>();
                List<Grade> grades = new List<Grade>();
                List<GradeValue> gradeValues = new List<GradeValue>();

                foreach (var term in gradeRoot.Terms)
                {
                    GradeCourse course = JsonConvert.DeserializeObject<GradeCourse>(term.Value.ToString());
                    courses.Add(course);
                }
                foreach (var course in courses)
                {
                    foreach (var c in course.course_units_grades)
                    {
                        CourseUnitsGrades courseUnitsGrade = JsonConvert.DeserializeObject<CourseUnitsGrades>(c.Value.ToString());
                        courseUnitsGrades.Add(courseUnitsGrade);
                    }
                }
                foreach (var courseUnitGrade in courseUnitsGrades)
                {
                    foreach (var cug in courseUnitGrade.CourseUnitGrade)
                    {
                        if(cug.Key == "course_grades")
                        {
                            continue;
                        }

                        GradeAttempt gradeAttempt = JsonConvert.DeserializeObject<GradeAttempt>(cug.Value.ToString());
                        gradeAttempts.Add(gradeAttempt);
                    }
                }
                List<string> units = new List<string>();
                foreach (var gradeAttempt in gradeAttempts)
                {
                    foreach (var ga in gradeAttempt.GradeAttempts)
                    {
                        units.Add(ga.Key.ToString());
                        Grade gr = JsonConvert.DeserializeObject<Grade>(ga.Value.ToString());
                        grades.Add(gr);
                    }
                }
                foreach (var grade in grades)
                {
                    GradeValue gradeValue = new GradeValue();
                    foreach (var g in grade.Grades)
                    {
                        if(g.Value.ToString() == "")
                        {
                            break;
                        }
                        gradeValue = JsonConvert.DeserializeObject<GradeValue>(g.Value.ToString());                     
                    }
                    gradeValues.Add(gradeValue);
                }
                for(int i=0; i<units.Count; i++)
                {
                    if(gradeValues[i].gradeValues is null)
                    {
                        unit_grade.Add(units[i],"-");
                        continue;
                    }
                    foreach(var g in gradeValues[i].gradeValues) 
                    {
                        unit_grade.Add(units[i],g.Value.ToString());
                    }   
                }
               return getUnitInfo(unit_grade);
            }
        }

        private List<Unit> getUnitInfo(Dictionary<string, string> unit_grade)
        {
            List<Unit> result = new List<Unit>();
            string unitsString = "";
            foreach (var unit in unit_grade.Keys)
            {
                unitsString += unit + "|";
            }
           unitsString = unitsString.Remove(unitsString.Length - 1);

            using (HttpClient httpClient = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, UsosProp.UNITS_URL))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", getAuthorizationValues());
                var data = new Dictionary<string, string>
                {
                    {"unit_ids",unitsString},
                    {"fields", UsosProp.UNITS_FIELDS},
                };
                request.Content = new FormUrlEncodedContent(data);

                HttpResponseMessage response = httpClient.Send(request);
                string responseData = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, UnitDto> units_info = JsonConvert.DeserializeObject<Dictionary<string, UnitDto>>(responseData);
                
                foreach (var unit in units_info)
                {
                    Unit u = new Unit
                    {
                        classtype_id = unit.Value.classtype_id,
                        course_id = unit.Value.course_id,
                        course_name = unit.Value.course_name,
                        term_id = unit.Value.term_id,
                        grade_value = unit_grade[unit.Key]
                    };
                    result.Add(u);
                }
            }
            return result;
        }

        private string getUserTerms()
        {      
            using (HttpClient httpClient = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, UsosProp.COURSES_URL))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", getAuthorizationValues());
                var data = new Dictionary<string, string>
                {
                    {"fields", "terms[id]"},
                    {"active_terms_only", UsosProp.COURSES_ACTIVE_TERMS_ONLY },
                };
                request.Content = new FormUrlEncodedContent(data);

                HttpResponseMessage response = httpClient.Send(request);
                string responseData = response.Content.ReadAsStringAsync().Result;

                UserTermHolder userTerms = JsonConvert.DeserializeObject<UserTermHolder>(responseData);
                string result = "";

                foreach (var userTerm in userTerms.terms)
                {
                    result += userTerm.id+"|";
                }
                result =result.Remove(result.Length-1);
                return result;
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
