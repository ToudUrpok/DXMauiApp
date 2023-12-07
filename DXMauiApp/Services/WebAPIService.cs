using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using DXMauiApp.Data;
using ON = DevExpress.Maui.Core.ON;

namespace DXMauiApp.Services;

public class WebAPIService : IDataStore<Post> 
{
#if DEBUG
    public static readonly string ApiUrl = ON.Platform("http://10.0.2.2:5000/api/", "http://localhost:5000/api/");
#else
    public static readonly string ApiUrl = ON.Platform("https://10.0.2.2:5001/api/", "https://localhost:5001/api/");
#endif

    private readonly HttpClient _httpClient = new HttpClient() { Timeout = new TimeSpan(0, 0, 10) };

    private readonly string _postEndPointUrl;

    public WebAPIService()
    {
        _postEndPointUrl = ApiUrl + "odata/" + nameof(Post);
    }

    public async Task<bool> UserCanDeletePostAsync()
    {
        var jsonString = await _httpClient.GetStringAsync($"{ApiUrl}CustomEndpoint/CanDeletePost?typeName={typeof(Post).Name}");
        return (bool)JsonNode.Parse(jsonString);
    }
    public async Task<bool> DeletePostAsync(int postId)
    {
        var response = await _httpClient.DeleteAsync($"{_postEndPointUrl}({postId})");
        return response.IsSuccessStatusCode;
    }

    public async Task<Post> GetItemAsync(string id)
        => (await RequestItemsAsync($"?$filter={nameof(Post.PostId)} eq {id}")).FirstOrDefault();

    public async Task<IEnumerable<Post>> GetItemsAsync(bool forceRefresh = false)
        => await RequestItemsAsync($"?$expand=Author($expand=Photo)");


    private async Task<IEnumerable<Post>> RequestItemsAsync(string query = null)
    {
        return JsonNode.Parse(await _httpClient.GetStringAsync($"{_postEndPointUrl}{query}"))!["value"].Deserialize<IEnumerable<Post>>();
    }

    public async Task<string> Authenticate(string userName, string password)
    {
        HttpResponseMessage tokenResponse;
        try
        {
            tokenResponse = await RequestTokenAsync(userName, password);
        }
        catch (Exception e)
        {
#if DEBUG
            await Application.Current.MainPage.DisplayAlert("Couldn't reach the WebAPI service", $"{e.Message} \n\nPotential reasons: \n\n- The WebAPI project is not started. Please right-click the WebAPI project and choose Debug -> Start New Instance \n\n- You debug the project using a physical device. Please try using an emulator \n\n- IIS Express on Windows blocks the access. Please follow the recommendations in the example description", "OK");
#endif
            return "An error occurred when processing the request";
        }
        if (tokenResponse.IsSuccessStatusCode)
        {
            string token = await tokenResponse.Content.ReadAsStringAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Preferences.Set("authToken", token);
            return null;
        }
        else
        {
            return await tokenResponse.Content.ReadAsStringAsync();
        }
    }

    private async Task<HttpResponseMessage> RequestTokenAsync(string userName, string password)
    {
        return await _httpClient.PostAsync($"{ApiUrl}Authentication/Authenticate",
                                            new StringContent(JsonSerializer.Serialize(new { userName, password }), Encoding.UTF8,
                                            "application/json"));
    }

    public async Task<ApplicationUser> CurrentUser()
    {
        ApplicationUser user;
        try
        {
            string  stringResponse = await _httpClient.GetStringAsync($"{ApiUrl}CustomEndpoint/CurrentUser");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            user = JsonSerializer.Deserialize<ApplicationUser>(stringResponse, options);
        }
        catch (Exception)
        {
            return null;
        }

        return user;
    }
}

