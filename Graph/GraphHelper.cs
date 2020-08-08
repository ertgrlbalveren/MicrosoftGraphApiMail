
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace MicrosoftGraphApiMail
{
    public class GraphHelper
    {
        private static GraphServiceClient _graphClient;
        public static void Initialize(IAuthenticationProvider authProvider)
        {
            _graphClient = new GraphServiceClient(authProvider);
        }

        public static async Task<User> GetMeAsync(string mailAddress)
        {
            try
            {
                return await _graphClient.Users[mailAddress]
                    .Request()
                    .GetAsync();
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting signed-in user: {ex.Message}");
                return null;
            }
        }

        public static async Task<IEnumerable<Message>> GetInboxMessagesAsync(string mailAddress)
        {
            try
            {
                var messages = await _graphClient.Users[mailAddress].MailFolders.Inbox.Messages
                    .Request()
                    .GetAsync();

                return messages;
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting events: {ex.Message}");
                return null;
            }
        }
    }
}