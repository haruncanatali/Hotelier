using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using Hotelier.Application.Auth.Queries.Dtos;
using Hotelier.Application.Common.Models;
using Hotelier.BackOffice.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Hotelier.BackOffice.Services;

public class RequestHelper
{
    private readonly UrlSetting _urlSetting;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestHelper(IOptions<UrlSetting> options, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _urlSetting = options.Value;
    }

    public async Task<LoginDto> SendLoginRequestAsync(LoginRequestModel model)
    {
        LoginDto? result = null;
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = new HttpResponseMessage();

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            response = await client
                .PostAsync( Debugger.IsAttached ? _urlSetting.LocalKey + _urlSetting.LoginPath 
                    : _urlSetting.HostKey + _urlSetting.LoginPath,content);
            
            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<LoginDto>(body);
            }
        }

        return result;
    }
    
    public async Task<List<TResponse>> SendRequestWithResponseListAsync<TResponse>(string url,Dictionary<string,string>? parameters)
    {
        List<TResponse> result = null;
        
        UriBuilder uriBuilder = new UriBuilder(
            Debugger.IsAttached
            ? _urlSetting.LocalKey + url
            : _urlSetting.HostKey + url
            );

        if (parameters != null)
        {
            string query = "?";
            var keys = parameters.Keys.ToList();

            for (int i = 0; i < keys.Count; i++)
            {
                if (i == 0)
                {
                    query += $"{keys[i]}={parameters[keys[i]]}";
                }
                else
                {
                    query += $"&{keys[i]}={parameters[keys[i]]}";
                }
            }

            uriBuilder.Query = query;
        }
        
        HttpResponseMessage response = new HttpResponseMessage();

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                _httpContextAccessor.HttpContext.User.FindFirstValue("AccessToken"));
            
            response = await client.GetAsync(uriBuilder.Uri);
            
            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<List<TResponse>>(body);
            }
        }

        return result;
    }
    
    public async Task<TResponse> SendRequestWithResponse<TResponse>(string url,Dictionary<string,string>? parameters)
    {
        TResponse? result = default(TResponse);
        
        UriBuilder uriBuilder = new UriBuilder(
            Debugger.IsAttached
                ? _urlSetting.LocalKey + url
                : _urlSetting.HostKey + url
        );

        if (parameters != null)
        {
            string query = "?";
            var keys = parameters.Keys.ToList();

            for (int i = 0; i < keys.Count; i++)
            {
                if (i == 0)
                {
                    query += $"{keys[i]}={parameters[keys[i]]}";
                }
                else
                {
                    query += $"&{keys[i]}={parameters[keys[i]]}";
                }
            }

            uriBuilder.Query = query;
        }
        
        HttpResponseMessage response = new HttpResponseMessage();

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                _httpContextAccessor.HttpContext.User.FindFirstValue("AccessToken"));
            
            response = await client.GetAsync(uriBuilder.Uri);
            
            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<TResponse>(body);
            }
        }

        return result;
    }
    
    public async Task SendRequestAsync(RequestType type, string url, object instance = null)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                _httpContextAccessor.HttpContext.User.FindFirstValue("AccessToken"));
            
            if (instance != null)
            {
                var properties = instance.GetType().GetProperties();

                var formFileProperties = properties.Where(p => typeof(IFormFile).IsAssignableFrom(p.PropertyType));

                if (formFileProperties.Any())
                {
                    MultipartFormDataContent content = new MultipartFormDataContent();

                    foreach (var formFileProperty in formFileProperties)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            var file = (formFileProperty.GetValue(instance) as IFormFile);
                            if (file != null)
                            {
                                await file!.CopyToAsync(memoryStream);
                                byte[] fileData = memoryStream.ToArray();
                        
                                ByteArrayContent fileContent = new ByteArrayContent(fileData);
            
                                string exp = file.ContentType.Split('/')[1];
                                string tempName = Guid.NewGuid().ToString() + "."+exp;

                                content.Add(fileContent, $"{formFileProperty.Name}", tempName);
                            }
                        }
                    }
                    
                    foreach (var property in properties)
                    {
                        if (!formFileProperties.Contains(property))
                        {
                            var propertyValue = property.GetValue(instance)?.ToString();

                            if (!string.IsNullOrEmpty(propertyValue))
                            {
                                content.Add(new StringContent(propertyValue), property.Name);
                            }
                        }
                    }

                    if (type == RequestType.Post)
                    {
                        await client.PostAsync(Debugger.IsAttached ? _urlSetting.LocalKey + url
                            : _urlSetting.HostKey + url, content);
                    }
                    else
                    {
                        var result = await client.PutAsync(Debugger.IsAttached ? _urlSetting.LocalKey + url 
                            : _urlSetting.HostKey + url, content);
                    }
                }
                else
                {
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(instance));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await client
                        .PostAsync(url,content);
                    
                    if (type == RequestType.Post)
                    {
                        await client.PostAsync(Debugger.IsAttached ? _urlSetting.LocalKey + url 
                            : _urlSetting.HostKey + url, content);
                    }
                    else
                    {
                        await client.PutAsync(Debugger.IsAttached ? _urlSetting.LocalKey + url 
                            : _urlSetting.HostKey + url, content);
                    }
                }
            }
        }
    }

    public async Task<TResponse> GetByIdAsync<TResponse>(string url,long id)
    {
        TResponse? result = default(TResponse);
        
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                _httpContextAccessor.HttpContext.User.FindFirstValue("AccessToken"));
            
            UriBuilder uriBuilder = new UriBuilder(
                Debugger.IsAttached
                    ? _urlSetting.LocalKey + url + $"/{id}"
                    : _urlSetting.HostKey + url + $"/{id}"
            );
            
            HttpResponseMessage api_response = await client.GetAsync(uriBuilder.Uri);
            
            if (api_response.IsSuccessStatusCode)
            {
                string body = await api_response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<TResponse>(body);
            }

            return result;
        }
    }

    public async Task SendDeleteByIdRequestAsync(string url, long id)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                _httpContextAccessor.HttpContext.User.FindFirstValue("AccessToken"));
            
            UriBuilder uriBuilder = new UriBuilder(
                Debugger.IsAttached
                    ? _urlSetting.LocalKey + url + $"/{id}"
                    : _urlSetting.HostKey + url + $"/{id}"
            );
            
            await client.DeleteAsync(uriBuilder.Uri);
        }
    }

    public async Task<AuthProperties> LoginCookieOperationsAsync(LoginDto model)
    {
        List<Claim> userClaims = new List<Claim>();

        userClaims.Add(new Claim(ClaimTypes.NameIdentifier, model.User.Id.ToString()));
        userClaims.Add(new Claim(ClaimTypes.Name, model.User.Firstname));
        userClaims.Add(new Claim(ClaimTypes.Surname, model.User.LastName));
        userClaims.Add(new Claim(ClaimTypes.Email, model.User.Email));
        userClaims.Add(new Claim("Photo", model.User.Photo));
        userClaims.Add(new Claim("AccessToken", model.AccessToken));
        userClaims.Add(new Claim(ClaimTypes.Role,model.User.Role));

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties authProperties = new AuthenticationProperties();

        return new AuthProperties
        {
            ClaimsIdentity = claimsIdentity,
            AuthenticationProperties = authProperties
        };
    }
}

public enum RequestType
{
    Get,
    Post,
    Put,
    Delete
}

public class AuthProperties
{
    public ClaimsIdentity ClaimsIdentity { get; set; }
    public AuthenticationProperties AuthenticationProperties { get; set; }
}