# DotNet sýnidæmi fyrir Fintech partý Arion banka hf. í júní 2016 
Demo biðlari .NET, auðkennir og kallar á API með einfaldri virkni.

01 - Til að geta sótt gögn frá Fintech þjónustunum þarf að setja inn developer key. Ef þú hefur ekki fengið slíkan þarftu að skrá þig á https://arionapi-sandbox.portal.azure-api.net

02 - Lykillinn þarf að fara inn í Constants.cs skránna:
// Azure Developer key - needed to contact the webapi's in Azure
public static string OcpApimSubscriptionKey => "[YourAzureDeveloperKeyGoesHere]";

03 - Sækja um OAuth2 ClientId:
https://arionapi-identityserver3-sandbox.azurewebsites.net/clientregistration?clientId=[ClientId]&redirectpath=[ClientRedirectUrl]&flowType=code

- Taka ClientID, ClientSecret og ClientRedirectUrl að ofan ( úr 03 ) og nota í lið 04 að neðan

04 - Setja inn ClientID, ClientSecret og ClientRedirectUrl í Constants.cs skránna:
// The registered id for the OAuth2 client:
public static string ClientId => "[ClientId]";

// The registered secret for the OAuth2 client:
public static string ClientSecret => "[Secret]";
        
// The registered redirect url for the OAuth2 client:
public static string ClientRedirectUrl => "[RedirectUrl";

05 - Taka frá notanda til að geta auðkennt sig inn, það er gert á eftirfarandi slóð:
https://arionapi-identityserver3-sandbox.azurewebsites.net/NewUser?clientId=[ClientId]

06 - Muna username/passowrd sem menn fengu úthlutað til innskráningar :-)
