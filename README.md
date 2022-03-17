# ccoe-aspnetcore-mvc-cdts-aad-sample
Here is a sample repository to get started with AAD using ASP.NET Core 3.1 MVC, Microsoft Graph API, and CDTS Web Template.

It is meant to help developers (originally from ESDC) new to AAD, Graph API, and CDTS to have the minimum knowledge needed to start their project using these technologies.

# Getting Started
## Azure Active Directory
The first step is to create the AAD application that the web server will use to authenticate users.\
You will need the Azure role **Application Developer**. At ESDC, you can request the role via NSD.

**Note: In sandbox, you cannot request that role. You need to do the steps on Azure with a Cloud Ops.**

### NSD ticket (for ESDC)
1. Click [here](https://014gc.sharepoint.com/sites/OI-CO/SitePages/FAQs/Azure-AAD-How-do-I-request-an-AAD-Admin-Role.aspx) and follow the instructions.

### Activate the role
1. Once you have the role, go to [Privileged Identity Management (PIM)](https://portal.azure.com/#blade/Microsoft_Azure_PIMCommon/CommonMenuBlade/quickStart) in the Azure Portal.
1. Go to "My roles" and activate the **Application Developer** role.
1. This will give you access to Azure Active Directory in the Azure Portal.

### Create the AAD App
The following steps are designed you to clone the repo and start using the code right away.

**Note:** You can find more formalized steps from Microsoft in the **References** section below.
1. Go to [Azure Active Directory](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Overview)
1. In the left pane choose "App Registration" and click "New registration".
1. Give the registration a meaningful name. 
1. In "Supported account type" choose the "Single tenant" option.

    **Note:** If you need people from outside your organization to be able to sign-in, you need to choose "Multitenant" mode.
    - In the redirect URI, select "Web" and enter "https://localhost:{port}/signin-oidc. If you are using Visual Studio, the port is "44325" if you use the code of this repo as is.

    **Note:** This is for development purpose. You can also put the development domain, if any. You can modify/add more URI later on in the Authentication panel of your AAD app.

    **Note:** In a production environment, 'localhost' should not be used in the possible redirect URIs, since it can open security breaches.
1. Click 'Register'.
1. In the overview panel, take note of "Application (client) ID" under 'ClientId' and take note of "Directory (tenant) ID" under 'TenantId'.
1. Go to "Certificates & secrets" and create a new Client Secret.

   **Note:** Take caution in the creation of the new Client Secret. This value is sensitive and you will only be able to see it once. Note this value under 'ClientSecret'.
    - In our scenario, the Client Secret is use to ask an access token on-behalf of the user to use Microsoft Graph API.
1. In the Token Configuration panel, select the claims you want AAD to give to your application. In our case, we need the email claim by the following:
    - Click "Add optional claim", select "ID", "email" and then click "Add". The system will prompt you stating that you need to add the API permissions for email. Check the checkbox and click "Add".
1. Go to the "API Permissions" panel.
1. This panel is made to specify which scopes you want the user's access token to have when the server request an access token. In AAD, this panel is used for the "static" permissions. You application can also use incremental permissions. You are also able to mix static and incremental permissions.

    **Note:** The demo code use the incremental approach for the Microsoft Graph API call in the 'PrivateController'.
1. The AAD application is now setup.

## ASP.NET Core MVC code setup
1. Open the solution.
1. Go to the appsettings.json file and fill the **TenantId**, **ClientId**, and **ClientSecret** fields with the values you took notes from the app creation section.
1. Start the application!
\
The first time you log into the web application, you will see a consent screen from Microsoft about the static permissions that were used in the "API Permissions" panel.
After that, the first time you click on the button to go to the private page, you will get another Microsoft consent screen.
This is normal behavior because we are using incremental consent. That is, we ask the user to consent to the web application to request an access token on their behalf with that permission. This is performed the first time they reach a page that requires that specific permission.
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
