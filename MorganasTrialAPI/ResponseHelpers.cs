namespace MorganasTrialAPI
{
    public static class ResponseHelpers
    {
        public static object FormatFinalResponse(HttpResponseMessage response, string responseContent)
        {
            var headers = new
            {
                UmbGeneratedResource = (string)null,
                Location = (string)null,
                UmbNotifications = (string)null
            };

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                response.Headers.TryGetValues("Umb-Generated-Resource", out var generatedResource);
                headers = headers with { UmbGeneratedResource = generatedResource?.FirstOrDefault() };
            }

            response.Headers.TryGetValues("Location", out var location);
            response.Headers.TryGetValues("Umb-Notifications", out var notifications);

            headers = headers with
            {
                Location = location?.FirstOrDefault(),
                UmbNotifications = notifications?.FirstOrDefault()
            };

            var result = new
            {
                response.StatusCode,
                Body = responseContent,
                Headers = headers
            };
            return result;
        }
    }
}