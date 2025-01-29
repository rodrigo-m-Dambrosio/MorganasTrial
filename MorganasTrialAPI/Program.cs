using MorganasTrialAPI;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

const string host = "localhost:5001";

// GET /Healthcheck

app.MapGet("/Healthcheck", async () =>
{
    var token = await AuthService.GetAuthToken();

    using (var client = new HttpClient())
    {
        var url = $"https://{host}/umbraco/management/api/v1/health-check-group?skip=0&take=100";

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync(url);

        var responseBody = await response.Content.ReadAsStringAsync();

        return Results.Json(responseBody);
    }
})
.WithName("HealthcheckGet");

// POST /DocumentType

app.MapPost("/DocumentType", async (HttpRequest request) =>
{
    var token = await AuthService.GetAuthToken();

    using var reader = new StreamReader(request.Body);
    var requestBody = await reader.ReadToEndAsync();

    using (var client = new HttpClient())
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync($"https://{host}/umbraco/management/api/v1/document-type", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        var result = ResponseHelpers.FormatFinalResponse(response, responseContent);

        return Results.Json(result);
    }
}).
WithName("DocumentTypePost");

// DELETE /DocumentType/{Id}

app.MapDelete("/DocumentType/{id}", async (string id) =>
{
    var token = await AuthService.GetAuthToken();

    using (var client = new HttpClient())
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await client.DeleteAsync($"https://{host}/umbraco/management/api/v1/document-type/{id}");

        string responseContent = await response.Content.ReadAsStringAsync();

        return Results.Content(responseContent, "application/json");
    }
}).
WithName("DocumentTypeDelete");

// GET /isOk

app.MapGet("/isOk", async (bool value) =>
{
    var token = await AuthService.GetAuthToken();

    var client = new HttpClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    var response = await client.GetAsync($"https://{host}/umbraco/management/api/v1/isOk?value={value}");

    if (response.IsSuccessStatusCode)
    {
        return Results.Ok($"The backoffice returned: {response.StatusCode}");
    }
    else
    {
        return Results.BadRequest($"The backoffice returned: {response.StatusCode}");
    }
}).WithName("IsOkGet");
;

app.Run();