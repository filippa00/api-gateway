//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace api_gateway_tests
//{
//    public class GameTest : BaseTestClass
//    {
//        [SetUp]
//        public void AdminLogin()
//        {
//            _adminToken = GetAdminToken();
//        }

//        [Test]
//        public async Task GetGame_ShouldReturnGame_WithStatusOk()
//        {
//            //Arrange 
//            string endpoint = "/api/game";
//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(responseContent, Does.Contain("id"));
//            Assert.That(responseContent, Does.Contain("gameName"));
//            Assert.That(responseContent, Does.Contain("gameDescription"));
//            Assert.That(responseContent, Does.Contain("gameLocation"));
//            Assert.That(responseContent, Does.Contain("gameStartTime"));
//            Assert.That(responseContent, Does.Contain("gameMaster"));
//            Assert.That(responseContent, Does.Contain("gameAssistant"));
//        }

//        [Test]
//        public async Task PostToGame_ShouldReturnNewGame_WithStatusCreated()
//        {
//            //Arrange 
//            string endpoint = "/api/game";

//            int idNr = _random.Next(0, 1000);
//            int nameNr = _random.Next(0, 1000);
//            int gameDescriptionNr = _random.Next(0, 1000);
//            int gameLocationNr = _random.Next(0, 1000);
//            DateTime gameStartTimeNr = DateTime.Now;
//            int gameMasterNr = _random.Next(0, 1000);
//            int gameAssistantNr = _random.Next(0, 1000);

//            // Cards data to be sent in the request body
//            string cardsDataJson = $@"  
//            {{
//                ""id"": ""{idNr}"",
//                ""gameName"": ""New name for the Game {nameNr}"",
//                ""gameDescription"": ""New Game description {gameDescriptionNr}"",
//                ""gameLocation"": ""New Game Location {gameLocationNr}"",
//                ""gameStartTime"": ""{gameStartTimeNr.ToString()}"",
//                ""gameMaster"": ""New Game Master {gameMasterNr}"",
//                ""gameAssistant"": ""New Game Assistant {gameAssistantNr}"",
//            }}";


//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            var response = await _httpClient.PostAsync(url, new StringContent(cardsDataJson, Encoding.UTF8, "application/json"));
//            HttpResponseMessage getCreatedCard = await _httpClient.GetAsync(url);
//            string responseContent = getCreatedCard.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(responseContent, Does.Contain($"{idNr}"));
//            Assert.That(responseContent, Does.Contain($"New name for the Game {nameNr}"));
//            Assert.That(responseContent, Does.Contain($"New Game description {gameDescriptionNr}"));
//            Assert.That(responseContent, Does.Contain($"New Game Location {gameLocationNr}"));
//            Assert.That(responseContent, Does.Contain($"New Game Start Time{gameStartTimeNr}"));
//            Assert.That(responseContent, Does.Contain($"New Game Master {gameMasterNr}"));
//            Assert.That(responseContent, Does.Contain($"New Game Assistant {gameAssistantNr}"));
//        }

//        [TestCase(1)]
//        [TestCase(2)]
//        public async Task GetGameById_ShouldReturnAGame_WithStatusOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/game/";
//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint + id;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(responseContent, Does.Contain("id"));
//            Assert.That(responseContent, Does.Contain("gameName"));
//            Assert.That(responseContent, Does.Contain("gameDescription"));
//            Assert.That(responseContent, Does.Contain("gameLocation"));
//            Assert.That(responseContent, Does.Contain("gameStartTime"));
//            Assert.That(responseContent, Does.Contain("gameMaster"));
//            Assert.That(responseContent, Does.Contain("gameAssistant"));
//        }

//        [TestCase(2)]
//        public async Task UpdateGameById_ShouldReturnOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/game/";

//            int nameNr = _random.Next(0, 1000);
//            int gameDescriptionNr = _random.Next(0, 1000);
//            int gameLocationNr = _random.Next(0, 1000);
//            DateTime gameStartTimeNr = DateTime.Now;
//            int gameMasterNr = _random.Next(0, 1000);
//            int gameAssistantNr = _random.Next(0, 1000);

//            string gameDataJson = $@"  
//            {{
//                ""gameName"": ""Updated Game name {nameNr}"",
//                ""gameDescription"": ""Updated Game description {gameDescriptionNr}"",
//                ""gameLocation"": ""Updated Game Location {gameLocationNr}"",
//                ""gameStartTime"": ""{gameStartTimeNr.ToString()}"",
//                ""gameMaster"": ""Updated Game Master {gameMasterNr}"",
//                ""gameAssistant"": ""Updated Game Assistant {gameAssistantNr}"",
//            }}";

//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint + id;
//            var response = await _httpClient.PutAsync(url, new StringContent(gameDataJson, Encoding.UTF8, "application/json"));
//            HttpResponseMessage getUpdatedGame = await _httpClient.GetAsync(url);
//            string getUpdatedGameResponse = getUpdatedGame.Content.ReadAsStringAsync().Result;


//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(getUpdatedGameResponse, Does.Contain($"Updated Game name {nameNr}"));
//            Assert.That(getUpdatedGameResponse, Does.Contain($"Updated Game description {gameDescriptionNr}"));
//            Assert.That(getUpdatedGameResponse, Does.Contain($"Updated Game Location {gameLocationNr}"));
//            Assert.That(getUpdatedGameResponse, Does.Contain($"{gameStartTimeNr.ToString()}"));
//            Assert.That(getUpdatedGameResponse, Does.Contain($"Updated Game Master {gameMasterNr}"));
//            Assert.That(getUpdatedGameResponse, Does.Contain($"Updated Game Assistant {gameAssistantNr}"));
//        }

//        [Test]
//        public async Task DeleteGame_ShouldReturnOk()
//        {
//            //Arrange 
//            string endpoint = "/api/game/2";

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
//    }
//}
