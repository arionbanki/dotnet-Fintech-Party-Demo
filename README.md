# DotNet sýnidæmi fyrir Fintech partý Arion banka hf. í júní 2016 
Demo biðlari .NET, auðkennir og kallar á API með einfaldri virkni.

Nokkrir punktar:
----------------
Authorize endpoint - til að fá authorization-code til baka:<br>
https://arionapi-identityserver3-sandbox.azurewebsites.net/connect/authorize

Token endpoint - sendir authorization-code inn sem fékkst að ofan, til að fá access-token til baka:<br>
https://arionapi-identityserver3-sandbox.azurewebsites.net/connect/token

notar svo authorization code og OcpApimSubscriptionKey ( sem fæst í Azure portalnum hérna:<br> https://arionapi-sandbox.portal.azure-api.net/ ) til að kalla á varin WebApi

Dæmi um access token sem notendur geta notað, til að einfalda málin:<br>
------------------------------------------------------------------------<br>
"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSIsImtpZCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSJ9.eyJjbGllbnRfaWQiOiJFaWtpVGVzdENsaWVudCIsInNjb3BlIjoiZmluYW5jaWFsIiwic3ViIjoiMjExMjU3MjAyOSIsImFtciI6InBhc3N3b3JkIiwiYXV0aF90aW1lIjoxNDY0OTYxNTYyLCJpZHAiOiJpZHNydiIsImlzcyI6Imh0dHBzOi8vYXJpb25hcGktaWRlbnRpdHlzZXJ2ZXIzLXNhbmRib3guYXp1cmV3ZWJzaXRlcy5uZXQiLCJhdWQiOiJodHRwczovL2FyaW9uYXBpLWlkZW50aXR5c2VydmVyMy1zYW5kYm94LmF6dXJld2Vic2l0ZXMubmV0L3Jlc291cmNlcyIsImV4cCI6MTQ2NTMyMTU2NiwibmJmIjoxNDY0OTYxNTY2fQ.gjgfL0_Jah0sGGswSu0-_K2ibLF8QPqzgyn0j4azOS2S7Wxg_bojwwqokfVkgPLlta1DnW4xuK0vWrdJBTmWRf6jFl8iEU5je43ppJ5i5kpJWgxFnC3H9JsYhCwbbsqbGYMMKoyBd3Xo7KNmknXBnJN62IlFjVrg_Fcnqut-79uSvS6X_Jlm509G21N1GnUDMAFLCxh_utUnr6Yp7G1y7gvep4VN5L--aMpSLDbQMOYfaRNcgeSjGNASG5mC1tB0TBGqNeUdeekkUYkQxMuaxjkO2rzPkMq1YPvQGgbp29LCsEe3TaasiidrFMaK2tPJ-7MRP-D5b6BymZPXJbcZrw"       

<br>--------------------------------------------------------------------------------------------------<br>
 
Step-by-step leiðbeiningar fyrir  sýnidæmi:<br>

01 - Til að geta sótt gögn frá Fintech þjónustunum þarf að setja inn developer key. Ef þú hefur ekki fengið slíkan þarftu að skrá þig á https://arionapi-sandbox.portal.azure-api.net<br>

02 - Sækja um OAuth2 client með því að fara inn á Management Api og velja búa til nýjan client - til þess þarf að nota developer-key'inn sem menn fengu úthlutað í skrefi 01 að ofan, og setja inn í 'Ocp-Apim-Subscription-Key' svæðið í Azure viðmótinu<br>
https://arionapi-sandbox.portal.azure-api.net/docs/services/574d5a9cdbc60f015c0a5974/operations/57507fac6aa55e0e2411340e

<br>
*<b>[clientId]</b> er nafnið sem menn vilja gefa sínum OAuth2 biðlara.<br> 
*<b>[redirectpath]</b> er slóðin sem menn vilja vera beint inn á eftir innskráningu með sínum biðlara<br>
*<b>[flowType]</b> annað hvort "codeflow" eða "implicit" - í flestum tilfellum er þetta "codeflow"<br><br>

- Muna eða skrifa niður clientSecret'ið sem menn fengu úthlutað til innskráningar, og nota þegar menn skrá sig inn. ClientSecret'ið verður ekki gefið aftur upp.<br>

- Eftir að búið er að búa til OAuth2 client, skal taka <b>clientId</b>, <b>redirectpath</b> og <b>clientSecret</b> sem maður fékk uppgefið eftir að hafa búið til clientinn að ofan ( úr 02 ) og nota í lið 03 að neðan:

03 - Setja inn <b><developerKey></b> (úr lið 01 að ofan), <b>ClientID</b>, <b>ClientSecret</b> og <b>ClientRedirectUrl</b> (allt þrennt úr lið 02 að ofan) í Constants.cs skrána:<br>

// Azure Developer key - needed to contact the webapi's in Azure<br>
public static string OcpApimSubscriptionKey => "[YourAzureDeveloperKeyGoesHere]";<br>

// The registered id for the OAuth2 client:<br>
public static string ClientId => "[ClientId]";<br>

// The registered secret for the OAuth2 client:<br>
public static string ClientSecret => "[Secret]";<br>
        
// The registered redirect url for the OAuth2 client:<br>
public static string ClientRedirectUrl => "[RedirectUrl]";<br>

04 - Taka frá notanda til að geta auðkennt sig inn, það er gert á eftirfarandi slóð:<br>
https://arionapi-sandbox.portal.azure-api.net/docs/services/574d5a9cdbc60f015c0a5974/operations/574d5a9ddbc60f0fc8631c35

<br>
*<b>[clientId]</b> er nafnið sem menn völdu sér á sinn OAuth2 biðlara í lið 02 að ofan.<br>
- Muna eða skrifa niður notandanafn/lykilorð sem menn fengu úthlutað til innskráningar, og nota þegar menn skrá sig inn. Lykilorðið verður ekki gefið aftur upp.
