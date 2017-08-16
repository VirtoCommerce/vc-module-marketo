namespace VirtoCommerce.Marketo.Data.Services
{
    public interface IMarketoConnectionInfo
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string RestApiUrl { get; set; }
    }
}