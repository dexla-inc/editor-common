using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Dexla.Common.Types;

namespace Dexla.Common.Utilities;

public static class HttpService
{
    public static HttpRequestMessage CreatePostRequestMessage(
        string relativeUrl,
        string? jsonContent = null,
        string? authHeaderValue = null,
        Dictionary<string, string>? headers = null,
        Dictionary<string, string>? parameters = null,
        string mediaType = MediaTypes.ApplicationJson)
    {
        return CreateRequestMessage(HttpMethod.Post, relativeUrl, jsonContent, authHeaderValue, headers, parameters, mediaType);
    }

    public static HttpRequestMessage CreatePutRequestMessage(
        string relativeUrl,
        string? jsonContent = null,
        string? authHeaderValue = null,
        Dictionary<string, string>? headers = null,
        Dictionary<string, string>? parameters = null,
        string mediaType = MediaTypes.ApplicationJson)
    {
        return CreateRequestMessage(HttpMethod.Put, relativeUrl, jsonContent, authHeaderValue, headers, parameters, mediaType);
    }

    public static HttpRequestMessage CreateGetRequestMessage(
        string relativeUrl,
        string? authHeaderValue = null,
        Dictionary<string, string>? headers = null,
        Dictionary<string, string>? parameters = null,
        string mediaType = MediaTypes.ApplicationJson)
    {
        return _buildRequest(HttpMethod.Get, relativeUrl, authHeaderValue, headers, parameters, mediaType);
    }

    private static HttpRequestMessage CreateRequestMessage(
        HttpMethod method,
        string relativeUrl,
        string? jsonContent,
        string? authHeaderValue,
        Dictionary<string, string>? headers,
        Dictionary<string, string>? parameters,
        string mediaType)
    {
        HttpRequestMessage request = _buildRequest(method, relativeUrl, authHeaderValue, headers, parameters, mediaType);

        // Adding the Accept header with the mediaType
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

        if (jsonContent == null)
            return request;

        switch (mediaType)
        {
            case MediaTypes.ApplicationJson:
                request.Content = new StringContent(jsonContent, Encoding.UTF8, mediaType);
                break;
            case MediaTypes.ApplicationFormUrlEncoded:
                {
                    List<KeyValuePair<string, string>> keyValueContent = Json.Deserialize<List<KeyValuePair<string, string>>>(jsonContent);
                    request.Content = new FormUrlEncodedContent(keyValueContent);
                    break;
                }
            default:
                throw new Exception("Unsupported media type");
        }

        return request;
    }

    private static HttpRequestMessage _buildRequest(
        HttpMethod method,
        string relativeUrl,
        string? authHeaderValue,
        Dictionary<string, string>? headers,
        Dictionary<string, string>? parameters,
        string mediaType)
    {
        HttpRequestMessage request = new(method, relativeUrl);

        if (!string.IsNullOrEmpty(authHeaderValue))
        {
            request.Headers.Add("Authorization", authHeaderValue);
        }

        request.Headers.Add("Accept", mediaType);

        if (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Patch)
        {
            HttpContent content = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>());
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue(mediaType);
            request.Content = content;
        }

        if (headers != null)
        {
            foreach ((string key, string value) in headers.Where(h => !string.IsNullOrEmpty(h.Key)))
            {
                request.Headers.Add(key, value);
            }
        }

        if (parameters != null)
        {
            NameValueCollection queryParameters = HttpUtility.ParseQueryString(string.Empty);

            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                queryParameters[parameter.Key] = parameter.Value;

                UriBuilder builder = new(relativeUrl)
                {
                    Query = queryParameters.ToString()
                };

                string url = builder.ToString();
                request.RequestUri = new Uri(url);
            }
        }

        return request;
    }
}
