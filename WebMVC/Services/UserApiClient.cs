using WebMVC.Models;

namespace WebMVC.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly HttpClient _http;

        public UserApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<User>> GetAll()
        {
            return await _http.GetFromJsonAsync<List<User>>("api/Users")
                   ?? new List<User>();
        }

        public async Task<User?> GetById(int id)
        {
            return await _http.GetFromJsonAsync<User>($"api/Users/{id}");
        }

        public async Task<User?> Create(User user)
        {
            var response = await _http.PostAsJsonAsync("api/Users", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task Update(int id, User user)
        {
            var response = await _http.PutAsJsonAsync($"api/Users/{id}", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            var response = await _http.DeleteAsync($"api/Users/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
