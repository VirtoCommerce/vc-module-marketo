using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using VirtoCommerce.Marketo.Data.Extensions;
using VirtoCommerce.Marketo.Data.Models;

namespace VirtoCommerce.Marketo.Data.Services
{
    public class MarketoConnectionInfo : IMarketoConnectionInfo
    {
        public string RestApiUrl { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }

    public class MarketoService
    {
        public MarketoService(MarketoConnectionInfo connection)
        {
            Connection = connection;
        }

        public MarketoConnectionInfo Connection { get; }

        public async Task<LeadCreateResponse> CreateOrUpdateLeads(LeadsRequest request)
        {
            var token = GetToken().Result;
            string url = String.Format("{0}/rest/v1/leads.json?access_token={1}", Connection.RestApiUrl, token);
            var fullUri = new Uri(url, UriKind.Absolute);

            LeadCreateResponse leadCreateResponse;
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(fullUri, request.AsJson()).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(stringResponse);
                    leadCreateResponse = jsonObject.ToObject<LeadCreateResponse>();
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                        throw new AuthenticationException("Invalid username/password combination.");
                    else
                        throw new AuthenticationException("Not able to get token");
                }
            }

            return leadCreateResponse;
        }

        private async Task<string> GetToken()
        {
            string url = String.Format("{0}/identity/oauth/token?grant_type=client_credentials&client_id={1}&client_secret={2}", Connection.RestApiUrl, Connection.ClientId, Connection.ClientSecret);
            var fullUri = new Uri(url, UriKind.Absolute);

            TokenResponse tokenResponse;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(fullUri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(stringResponse);
                    tokenResponse = jsonObject.ToObject<TokenResponse>();
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                        throw new AuthenticationException("Invalid username/password combination.");
                    else
                        throw new AuthenticationException("Not able to get token");
                }
            }

            return tokenResponse.access_token;
        }
    }
}
