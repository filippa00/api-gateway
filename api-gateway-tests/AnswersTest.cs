//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace api_gateway_tests
//{
//    public class AnswersTest : BaseTestClass
//    {
//        [SetUp]
//        public void AdminLogin()
//        {
//            _adminToken = GetAdminToken();
//        }

//        [Test]
//        public async Task GetAnswers_ShouldReturnAnswers_WithStatusOk()
//        {
//            //Arrange 
//            string endpoint = "/api/answers";
//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//            //Assert
//            assets(response, responseContent);
//        }

//        [Test]
//        public async Task PostToAnswers_ShouldReturnNewAnswer_WithStatusCreated()
//        {
//            //Arrange 
//            string endpoint = "/api/answers";

//            int idNr = _random.Next(0, 1000);
//            int gameIdNr = _random.Next(0, 1000);
//            int nameNr = _random.Next(0, 1000);
//            int questionNr = _random.Next(0, 1000);
//            int gameDescriptionNr = _random.Next(0, 1000);
//            int questionIdNr = _random.Next(0, 1000);
//            int answerTextNr = _random.Next(0, 1000);
//            DateTime dateAnsweredNr = DateTime.Now;
//            int writtenByUserNr = _random.Next(0, 1000);
//            int writtenByUserIdNr = _random.Next(0, 1000);

//            // Cards data to be sent in the request body
//            string answersDataJson = $@"  
//            {{
//                ""id"": ""{idNr}"",
//                ""gameId"": ""{gameIdNr}"",
//                ""gameName"": ""New name for the game {nameNr}"",
//                ""question"": ""New question for the game {questionNr}"",
//                ""questionId"": ""{questionIdNr}"",
//                ""answerText"": ""answer{answerTextNr}"",
//                ""dateAnswered"": ""{dateAnsweredNr}"",
//                ""writtenByUser"": ""User{writtenByUserNr}"",
//                ""writtenByUserId"": ""{writtenByUserIdNr}"",
//            }}";


//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            var response = await _httpClient.PostAsync(url, new StringContent(answersDataJson, Encoding.UTF8, "application/json"));
//            HttpResponseMessage getCreatedCard = await _httpClient.GetAsync(url);
//            string responseContent = getCreatedCard.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(responseContent, Does.Contain($"{idNr}"));
//            Assert.That(responseContent, Does.Contain($"{gameIdNr}"));
//            Assert.That(responseContent, Does.Contain($"New name for the game {nameNr}"));
//            Assert.That(responseContent, Does.Contain($"New question for the game {questionNr}"));
//            Assert.That(responseContent, Does.Contain($"{questionIdNr}"));
//            Assert.That(responseContent, Does.Contain($"answer{answerTextNr}"));
//            Assert.That(responseContent, Does.Contain($"{dateAnsweredNr.ToString()}"));
//            Assert.That(responseContent, Does.Contain($"User{writtenByUserNr}"));
//            Assert.That(responseContent, Does.Contain($"{writtenByUserIdNr}"));
//        }

//        [TestCase(1)]
//        [TestCase(2)]
//        public async Task GetAnswerById_ShouldReturnAnAnswer_WithStatusOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/answers/";
//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint + id;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//            //Assert
//            assets(response, responseContent);
//        }

//        [TestCase(2)]
//        public async Task UpdateAnswerById_ShouldReturnOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/answers/";

//            int idNr = _random.Next(0, 1000);
//            int gameIdNr = _random.Next(0, 1000);
//            int nameNr = _random.Next(0, 1000);
//            int questionNr = _random.Next(0, 1000);
//            int gameDescriptionNr = _random.Next(0, 1000);
//            int questionIdNr = _random.Next(0, 1000);
//            int answerTextNr = _random.Next(0, 1000);
//            DateTime dateAnsweredNr = DateTime.Now;
//            int writtenByUserNr = _random.Next(0, 1000);
//            int writtenByUserIdNr = _random.Next(0, 1000);

//            string answersDataJson = $@"  
//            {{
//                ""id"": ""{idNr}"",
//                ""gameId"": ""{gameIdNr}"",
//                ""gameName"": ""Updated name for the game {nameNr}"",
//                ""question"": ""Updated question for the game {questionNr}"",
//                ""questionId"": ""{questionIdNr}"",
//                ""answerText"": ""Updated answer{answerTextNr}"",
//                ""dateAnswered"": ""{dateAnsweredNr}"",
//                ""writtenByUser"": ""Updated User{writtenByUserNr}"",
//                ""writtenByUserId"": ""{writtenByUserIdNr}"",
//            }}";

//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint + id;
//            var response = await _httpClient.PutAsync(url, new StringContent(answersDataJson, Encoding.UTF8, "application/json"));
//            HttpResponseMessage getUpdatedAnswer = await _httpClient.GetAsync(url);
//            string getUpdatedAnswersResponse = getUpdatedAnswer.Content.ReadAsStringAsync().Result;


//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(getUpdatedAnswersResponse, Does.Contain($"{idNr}"));
//            Assert.That(getUpdatedAnswersResponse, Does.Contain($"{gameIdNr}"));
//            Assert.That(getUpdatedAnswersResponse, Does.Contain($"Updated name for the game {nameNr}"));
//            Assert.That(getUpdatedAnswersResponse, Does.Contain($"Updated question for the game {questionNr}"));
//            Assert.That(getUpdatedAnswersResponse, Does.Contain($"{questionIdNr}"));
//            Assert.That(getUpdatedAnswersResponse, Does.Contain($"Updated answer{answerTextNr}"));
//            Assert.That(getUpdatedAnswersResponse, Does.Contain($"{dateAnsweredNr.ToString()}"));
//            Assert.That(getUpdatedAnswersResponse, Does.Contain($"Updated User{writtenByUserNr}"));
//            Assert.That(getUpdatedAnswersResponse, Does.Contain($"{writtenByUserIdNr}"));
//        }

//        [Test]
//        public async Task DeleteAnswer_ShouldReturnOk()
//        {
//            //Arrange 
//            string endpoint = "/api/answers/2";

//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            var response = await _httpClient.DeleteAsync(url);
//            HttpResponseMessage getGame = await _httpClient.GetAsync(url);

//            //HttpResponseMessage getAllCards = await _httpClient.GetAsync(url);
//            //string allCardsContent = getAllCards.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(getGame.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

//        }

//        [TestCase(1)]
//        [TestCase(2)]
//        public async Task GetAnswersByGameId_ShouldReturnAnswersFromTheGame_WithStatusOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/answers/game/";
//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint + id;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//            //Assert
//            assets(response, responseContent);
//        }

//        [TestCase(1)]
//        [TestCase(2)]
//        public async Task GetAnswersByQuestionId_ShouldReturnAnswersFromTheQuestions_WithStatusOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/answers/game/";
//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint + id;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//            //Assert
//            assets(response, responseContent);
//        }

//        public void assets(HttpResponseMessage response, string responseContent)
//        {
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(responseContent, Does.Contain("id"));
//            Assert.That(responseContent, Does.Contain("gameId"));
//            Assert.That(responseContent, Does.Contain("gameName"));
//            Assert.That(responseContent, Does.Contain("question"));
//            Assert.That(responseContent, Does.Contain("questionId"));
//            Assert.That(responseContent, Does.Contain("answerText"));
//            Assert.That(responseContent, Does.Contain("dateAnswered"));
//            Assert.That(responseContent, Does.Contain("writtenByUser"));
//            Assert.That(responseContent, Does.Contain("writtenByUserId"));
//        }

//        [Test]
//        public async Task GetAnswers_WithoutAuthentication_ShouldReturnUnauthorized()
//        {
//            //Arrange 
//            string endpoint = "/api/answers";
//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//            //Assert
//            assets(response, responseContent);
//        }
//    }
//}
