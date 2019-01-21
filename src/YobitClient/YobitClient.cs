using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YobitClient.Helpers;
using YobitClient.Nonce;

namespace YobitClient
{
    public partial class YobitClient
    {
        private static readonly Uri BaseAddress = new Uri("https://yobit.io/");
        private static readonly Dictionary<string, string> EmptyDictionary = new Dictionary<string, string>(0);
        private readonly string _apiKey;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly SecureString _privateKey;
        private readonly HMACSHA512 _sha512PrivateKey;
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
        private static readonly int AdditionalPrivateQueryArgs = 2;
        private readonly NonceService _nonceService;
        

        public YobitClient(string apiKey, string privateKey) : this(BaseAddress, apiKey, privateKey)
        {
        }


        public YobitClient(string baseAddress, string apiKey, string privateKey) : this(new Uri(baseAddress), apiKey,
            privateKey)
        {
        }

        public YobitClient(Uri baseAddress, string apiKey, string privateKey)
        {
            _apiKey = apiKey ?? string.Empty;
            _privateKey = (privateKey ?? string.Empty).ToSecureString();
            _nonceService = new NonceService(new NonceFileWriter(), new NonceFileReader());

            _httpClient.BaseAddress = baseAddress;
            //_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            //_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            //_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:57.0) Gecko/20100101 Firefox/57.0");

            //_sha512PrivateKey = new HMACSHA512(Convert.FromBase64String(_privateKey));
            _sha512PrivateKey = new HMACSHA512(Encoding.ASCII.GetBytes(_privateKey.ToUnsecureString()));
        }

        public async Task<YobitResponse<T>> QueryPublic<T>(string requestUrl, Dictionary<string, string> args = null)
        {
            if (requestUrl == null) throw new ArgumentNullException(nameof(requestUrl));

            args = args ?? EmptyDictionary;
            var encodedArgs = ArgsEncode(args);

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl + (!string.IsNullOrEmpty(encodedArgs) ? "?" + encodedArgs : string.Empty));
            return await SendRequest<T>(request, false);
        }

        public async Task<YobitResponse<T>> QueryPrivate<T>(string requestUrl, Dictionary<string, string> args = null)
        {
            if (requestUrl == null) throw new ArgumentNullException(nameof(requestUrl));

            args = args ?? EmptyDictionary;
            //args["nonce"] = _nonceService.GenerateNonce(_apiKey).ToString(Culture);
            if (GetNonce != null)
            {
                var nonce = (await GetNonce().ConfigureAwait(false)).ToString(Culture);
                args["nonce"] = nonce;
            }

            var encodedArgs = ArgsEncode(args);

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = new StringContent(encodedArgs, Encoding.UTF8, "application/x-www-form-urlencoded")
            };

            request.Headers.Add("Key", _apiKey);
            var argsHash = _sha512PrivateKey.ComputeHash(Encoding.ASCII.GetBytes(encodedArgs));
            request.Headers.Add("Sign", ToHex(argsHash).ToLower());

            return await SendRequest<T>(request).ConfigureAwait(false);
        }

        private static string ToHex(byte[] value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string hexAlphabet = "0123456789ABCDEF";
            if (value != null)
            {
                foreach (byte b in value)
                {
                    stringBuilder.Append(hexAlphabet[(int) (b >> 4)]);
                    stringBuilder.Append(hexAlphabet[(int) (b & 0xF)]);
                }
            }
            return stringBuilder.ToString();
        }

        private static string ArgsEncode(Dictionary<string, string> args)
        {
            return string.Join("&",
                args.Where(x => !string.IsNullOrEmpty(x.Value))
                    .Select(x => x.Key + "=" + WebUtility.UrlEncode(x.Value)));
        }

        public Func<Task<long>> GetNonce { get; set; } = () =>
        {
            Thread.Sleep(1000);
            return Task.FromResult((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
        };

        //public Func<Task<long>> GetNonce { get; set; } = () => Task.FromResult(DateTime.UtcNow.Ticks);
        //private static long GetNonce()
        //{
        //    return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        //}

            
        private async Task<YobitResponse<T>> SendRequest<T>(HttpRequestMessage request, bool isPrivate = true)
        {
            var result = new YobitResponse<T>();
            var res = await RunRequest(request);
            result.RawJson = Encoding.UTF8.GetString(await res.Content.ReadAsByteArrayAsync().ConfigureAwait(false));

            try
            {
                if (isPrivate)
                {
                    var jObject = JObject.Parse(result.RawJson);
                    var isSuccess = Convert.ToInt32(jObject["success"].ToString());
                    if (isSuccess > 0)
                    {
                        var returnObject = JObject.Parse(jObject["return"].ToString());
                        result.Result = JsonConvert.DeserializeObject<T>(returnObject.ToString());
                    }
                    else
                    {
                        throw new YobitException((string)jObject["error"]);
                    }
                }
                else
                {
                    result.Result = JsonConvert.DeserializeObject<T>(result.RawJson);
                }
            }
            catch (JsonSerializationException)
            {
                var error = JObject.Parse(result.RawJson);
                throw new YobitException((string) error["error"]);
            }
            return result;
        }

        private async Task<HttpResponseMessage> RunRequest(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}