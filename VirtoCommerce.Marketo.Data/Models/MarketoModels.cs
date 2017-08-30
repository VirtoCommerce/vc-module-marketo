using System;

namespace VirtoCommerce.Marketo.Data.Models
{
    public class PushLead
    {
        public string email { get; set; }
        public string country { get; set; }
        public string firstName { get; set; }
        public string website { get; set; }
        public int leadScore { get; set; }
        public string marketoSocialFacebookProfileURL { get; set; }
        public string jobTitle { get; set; }
    }

    public class Lead
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int leadScore { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
    }

    public class LeadsRequest
    {
        public string action { get; set; }
        public string lookupField { get; set; }
        public Lead[] input { get; set; }
    }

    public class PushLeadsRequest
    {
        public string programName { get; set; }
        public string source { get; set; }
        public string reason { get; set; }
        public string lookupField { get; set; }
        public PushLead[] input { get; set; }
    }

    public class LeadCreateResponse : IDisposable
    {
        public string requestId { get; set; }
        public bool success { get; set; }
        public OpResult[] result { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }
    }

    public class OpResult
    {
        public int id { get; set; }
        public string status { get; set; }
        public OpReason[] reasons { get; set; }
    }

    public class OpReason
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class TokenResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}
