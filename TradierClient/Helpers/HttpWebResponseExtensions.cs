using System.Net;

namespace Tradier.Client.Helpers
{
    public static class HttpWebResponseExtensions
    {
        public static bool IsSuccessStatusCode(this HttpWebResponse response)
        {
            var isSuccess = response.StatusCode.HasFlag(HttpStatusCode.OK)
                || response.StatusCode.HasFlag(HttpStatusCode.Created)
                || response.StatusCode.HasFlag(HttpStatusCode.Accepted)
                || response.StatusCode.HasFlag(HttpStatusCode.NonAuthoritativeInformation)
                || response.StatusCode.HasFlag(HttpStatusCode.NoContent)
                || response.StatusCode.HasFlag(HttpStatusCode.ResetContent)
                || response.StatusCode.HasFlag(HttpStatusCode.PartialContent);
            return isSuccess;
        }
    }
}
