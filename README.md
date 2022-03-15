# ccoe-aspnetcore-mvc-cdts-aad-sample
Sample repository to get started up with AAD using ASP.NET Core 3.1 MVC, Microsoft Graph API, and CDTS Web Template.

It is meant to help developers (originally from ESDC) new to AAD, Graph API, and CDTS to have the bare minimum to start their project using these technologies.

# Getting started
## Azure Active Directory
The first step is to create the AAD application that the web server will use to authenticate users.\
You will need the Azure role **Application Developer**. At ESDC, you can request it via NSD.

1. Once you have the role, go to [Privileged Identity Management (PIM)](https://portal.azure.com/#blade/Microsoft_Azure_PIMCommon/CommonMenuBlade/quickStart) in Azure Portal.
1. Go to "My roles" and activate the **Application Developer** role.
1. This will give you access to Azure Active Directory in the Azure Portal.

### Create the AAD app
You can find the official steps from Microsoft in the **References** section below. The steps below are designed for you to clone the repo and start using the code right away.
1. Go to [Azure Active Directory](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Overview)
1. In the left pane choose "App Registration" and click "New registration".
1. Give it a meaningful name. 
1. In "Supported account type" choose "Single tenant" option. 
    - If you need people from outside your organization to be able to sign-in, you need to choose "Multitenant" mode.
1. In redirect URI, select "Web" and put "https://localhost:{port}/signin-oidc. If you use Visual Studio, the port would be "44325" if you use the code of this repo as is.
    - This is for development purpose. You can also put the development domain, if any. You can modify/add more URI later on in the Authentication panel of your AAD app.
    - In production environment, localhost should not be in the possible redirect URIs, since it can open security breach.
1. Click register.
1. In the overview panel, take note of "Application (client) ID" and "Directory (tenant) ID". We will use them later.
1. Go to "Certificates & secrets" and create a new Client Secret. Take care, this value is sensitive and you will only see it once. We will use it in the next steps.
    - In our scenario, the client secret is use to ask an access token on-behalf of the user to use Microsoft Graph API.
1. In token configuration panel, you choose which claims you want AAD to give to your application. In our case, we want the default ones and the email.
    - Click "Add optional claim", select "ID", "email" and then click "Add". It might prompt you that you need to add the API permissions for email. Check the checkbox and click "Add".
1. Go to the "API Permissions" panel.
1. This panel is made to specify which scopes you want the user's access token to have when the server request one. In AAD, this panel is for the "static" permissions. You application can also use incremental permissions. You can mix static and incremental permissions.
    - The demo code use the incremental approach for the Microsoft Graph API call in the **PrivateController**.
1. The AAD application is now setup.

## ASP.NET Core MVC code setup
1. Open the solution.
1. Go to the appsettings.json file and fill the **TenantId**, **ClientId**, and **ClientSecret** fields with the values you took notes from the app creation section.
1. Start the application!
\
The first time you will log in the web application, you will see a consent from Microsoft about the static permissions that were in the "API Permissions" panel.
After that, the first time you click on the button to go to the private page, you will get another Microsoft consent screen.
This is normal because we are using incremental consent. That is, we ask the user to consent for the web application to request an access token on their behalf with that permission the first time they reach a page that needs it.
\
\
It can be useful when you have features that require more permissions that are only accessible from specific users.
Also, it is more secure as the application only request an access token for specific scopes(permissions).

# References

[Centrally Deployed Templates Solution (CDTS) Website](https://cenw-wscoe.github.io/sgdc-cdts/docs/index-en.html)\
[Microsoft Tutorial "Web app that signs in users"](https://docs.microsoft.com/en-ca/azure/active-directory/develop/scenario-web-app-sign-user-overview?tabs=aspnetcore)\
[.NET MSAL (Microsoft.Identity.Web package) Wiki](https://github.com/AzureAD/microsoft-identity-web/wiki/web-apps)\
[AAD Authorization Code Flow](https://docs.microsoft.com/en-gb/azure/active-directory/develop/v2-oauth2-auth-code-flow)\
[AAD Incremental Consent](https://github.com/AzureAD/microsoft-identity-web/wiki/Managing-incremental-consent-and-conditional-access#in-mvc-controllers)
