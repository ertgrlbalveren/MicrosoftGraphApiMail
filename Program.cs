using System;
using Microsoft.Extensions.Configuration;

namespace MicrosoftGraphApiMail
{
    class Program
    {
        static void Main(string[] args)
        {
            var appConfig = LoadAppSettings();

            if (appConfig == null)
            {
                Console.WriteLine("Missing or invalid appsettings.json...exiting");
                return;
            }


            var appId = appConfig["appId"];
            var scopes = appConfig["scopes"];
            var tenantId = appConfig["tenantId"];
            var clientSecret = appConfig["clientSecret"];

            try
            {
                // Initialize the auth provider with values from appsettings.json
                var authProvider = new ClientSecretAuthProvider(appId, new[] { scopes }, tenantId, clientSecret);

                // Request a token to sign in the user
                var accessToken = authProvider.GetAccessToken().Result;

                GraphHelper.Initialize(authProvider);
                //type mail address which you want to read mails example: "ertugrul.balveren@balsoft.de"
                string mailAddress = "";
                var messages = GraphHelper.GetInboxMessagesAsync(mailAddress).Result;

                foreach (var message in messages)
                {
                    System.Console.WriteLine(message.Sender.EmailAddress.Address);
                    System.Console.WriteLine(message.BodyPreview);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting token: {ex.Message}");
            }

            static IConfigurationRoot LoadAppSettings()
            {
                var appConfig = new ConfigurationBuilder()
                    .AddUserSecrets<Program>()
                    .Build();

                // Check for required settings
                if (string.IsNullOrEmpty(appConfig["appId"]) ||
                    string.IsNullOrEmpty(appConfig["scopes"]))
                {
                    return null;
                }

                return appConfig;
            }
        }
    }
}
