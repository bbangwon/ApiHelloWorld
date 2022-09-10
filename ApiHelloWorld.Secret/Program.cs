using IdentityModel.Client;
using IdentityServer4.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryApiScopes(new List<ApiScope>
    {
        new ApiScope("api1", "My API")
    })
    .AddInMemoryClients(new List<Client>
    {
        new Client
        {
            ClientId = "client",

            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },

            // scopes that client has access to
            AllowedScopes = { "api1" }
        }
    });

var app = builder.Build();

app.UseIdentityServer();

app.MapGet("/", ()=> "ApiHelloWorld.Secret");

app.MapGet("/token", async context => {
    var client = new HttpClient();
    var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7283");
    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    {
        Address = disco.TokenEndpoint,
        ClientId = "client",
        ClientSecret = "secret",
        Scope = "api1"
    });

    await context.Response.WriteAsync($"token : {tokenResponse.AccessToken}");
});

app.Run();
