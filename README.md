# MicrosoftGraphApiMail
.net core sample console project to read mails from O365 account.

## About The Project

This is a sample .net core 3.1 console project to read mail in inbox folder from Office 365 account.

### Prerequisites
1. Register an app in [Azure] in Azure Active Directory

![1]

![2]

2. Copy "Application (client) ID" and "Directory (tenant) ID"

![3]

3. Assign "Mail.Read" and "User.Read.All" permission

![4]

4. Remove "User.Read" it is not necessary 

![5]

5. An azure admin should grant these permissions for the app. 

![6]

6. Create a client secret.

![7]

![8]

7. Copy client secret.

![9]





### Installation


#### * npm

```sh
npm i microsoftgraphapimail
```

#### * init user-secrets

execute following commands

```sh
dotnet user-secrets init
```

```sh
dotnet user-secrets set appId "paste Application (client) ID from step 2"  
dotnet user-secrets set tenantId "paste Directory (tenant) ID from step 2"  
dotnet user-secrets set clientSecret "paste Client secret from step 7"
dotnet user-secrets set scopes "https://graph.microsoft.com/.default" 
```
#### * read mails

```csharp

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
  
```

<!-- LINKS -->

[Azure]: https://portal.azure.com/#home
[1]: ss/1.png
[2]: ss/2.png
[3]: ss/3.png
[4]: ss/4.png
[5]: ss/5.png
[6]: ss/6.png
[7]: ss/7.png
[8]: ss/8.png
[9]: ss/9.png

