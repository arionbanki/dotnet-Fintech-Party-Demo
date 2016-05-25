# DotNet sýnidæmi fyrir Fintech partý Arion banka hf. í júní 2016 
Demo biðlari .NET, auðkennir og kallar á API með einfaldri virkni.

01 - Til að geta sótt gögn frá Fintech þjónustunum þarf að setja inn developer key. Ef þú hefur ekki fengið slíkan þarftu að skrá þig á https://arionapi-sandbox.portal.azure-api.net

02 - Lykillinn þarf að fara inn í Constants.cs skránna:<br>
// Azure Developer key - needed to contact the webapi's in Azure<br>
public static string OcpApimSubscriptionKey => "[YourAzureDeveloperKeyGoesHere]";

03 - Sækja um OAuth2 ClientId:<br>
https://arionapi-identityserver3-sandbox.azurewebsites.net/clientregistration?clientId=[ClientId]&redirectpath=[ClientRedirectUrl]&flowType=code

<br>
*[ClientId] er nafnið sem menn vilja gefa sínum OAuth2 biðlara.
*[ClientRedirectUrl] er slóðin sem menn vilja vera beint inn á eftir innskráningu með sínum biðlara

- Taka ClientID, ClientSecret og ClientRedirectUrl að ofan ( úr 03 ) og nota í lið 04 að neðan

04 - Setja inn ClientID, ClientSecret og ClientRedirectUrl í Constants.cs skránna:<br>
// The registered id for the OAuth2 client:<br>
public static string ClientId => "[ClientId]";

// The registered secret for the OAuth2 client:<br>
public static string ClientSecret => "[Secret]";
        
// The registered redirect url for the OAuth2 client:<br>
public static string ClientRedirectUrl => "[RedirectUrl]";

05 - Taka frá notanda til að geta auðkennt sig inn, það er gert á eftirfarandi slóð:<br>
https://arionapi-identityserver3-sandbox.azurewebsites.net/NewUser?clientId=[ClientId]

<br>
*[ClientId] er nafnið sem menn völdu sér á sinn OAuth2 biðlara í lið 03 að ofan.<br>
- Muna eða skrifa niður notandanafn/lykilorð sem menn fengu úthlutað til innskráningar, og nota þegar menn skrá sig inn :-)
