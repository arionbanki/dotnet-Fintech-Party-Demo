# DotNet Demo Clients for Arion Bank hf. Hackathon API (updated October 2020)
Demo client for .NET FW, authenticates and invokes the Sandbox API with some simple invocations. Much of the rest of these instructions are in Icelandic.  

To begin with, a few notes on how OAuth2 authentication can work for the various clients:<br>

Example of an implicit client ( e.g. Javascript or Python ) requesting an access token:
----------------------------------------------------------------------------
https://arionapi-identityserver3-sandbox.azurewebsites.net/connect/authorize?response_type=token&client_id=XXXXXXXXXX&redirect_uri=https%3a%2f%2farionapi-sandbox.portal.azure-api.net%2fdocs%2fservices%2f57361a83110546175c6fec3d%2fconsole%2foauth2%2fimplicit%2fcallback&scope=financial
<br>
Plese note that different OAuth2 helper libraries might handle url-encoding on your behalf but others not. The redirectUri needs ot be url-encoded - and multiple options to help doing that manually like this website: http://meyerweb.com/eric/tools/dencoder/). Below is another example of a request:
<br>
https://arionapi-identityserver3-sandbox.azurewebsites.net/connect/authorize?response_type=token&client_id=FintechAzureApiManagement&redirect_uri=https%3a%2f%2farionapi-sandbox.portal.azure-api.net%2fdocs%2fservices%2f57361a83110546175c6fec3d%2fconsole%2foauth2%2fimplicit%2fcallback&state=aae016ca-1c17-42bc-99d2-122c8470b0d9&scope=financial
<br>

A pre-generated access token that users can use for testing - valid until 08.11.2020 at 01:21  
Please note you might need to specify it as a bearer token depending on the client you use, e.g.  'Authorization': 'Bearer eyJ0eXAiOiJKV1QiLC..[Rest of Token].'  
--------------------------------------------------------------------------------------------------------------------<br>
"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSIsImtpZCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSJ9.eyJjbGllbnRfaWQiOiJIYWNrTm92MjAyMCIsInNjb3BlIjoiZmluYW5jaWFsIiwic3ViIjoiMTYwMzIxMjAyOSIsImFtciI6InBhc3N3b3JkIiwiYXV0aF90aW1lIjoxNjAzNzE4NDU3LCJpZHAiOiJpZHNydiIsImlzcyI6Imh0dHBzOi8vYXJpb25hcGktaWRlbnRpdHlzZXJ2ZXIzLXNhbmRib3guYXp1cmV3ZWJzaXRlcy5uZXQiLCJhdWQiOiJodHRwczovL2FyaW9uYXBpLWlkZW50aXR5c2VydmVyMy1zYW5kYm94LmF6dXJld2Vic2l0ZXMubmV0L3Jlc291cmNlcyIsImV4cCI6MTYwNDc5ODQ2NCwibmJmIjoxNjAzNzE4NDY0fQ.dQEhKRGiDzOrwI4K3riBeA1S0a6ppj1FrzQxPnZ0XEhyqi8CKA-aiosJVbjADao5wOZ6AOLlvmEXXS8PWvRWJGb2FPJVKYxCUq7JvG8NWKs4XHufuRku4vO8RKGV96bFlA2uEClDH_raYvGcroAJ9ZaZwSbmpE5wFZgH6FNTq_3UJznRg5DuG2LX7EAxES39XTO1ANgXXCt-w9nfzQ665cCQ6dxpvIc2lpw_fAOImpw_2xNhWGGmZZUXKjT5oCTm1de0SqmPQNhBqUtZjw0BH24Cki0fycupG5hi1hKY8v9ZIDSZDyWoZaOi-lDEDFJff-88_Pv4Ar0T6hnlSVeZvg"       

The token above will be valid for the following demo user Travis Gamboa, to view his data ( you can get more users by requesting a new user and a token for the new user, in the links at the bottom of this page ) 

------------------------------------------------------------------------------<br>
NOTE: both code clients and implicit clients (e.g. Python, Node.js & Javascript) need to communicate with this endpoint to start the logon flow:<br>
----------------------------------------------------------------------------------------------
Authorize endpoint - til að fá authorization-code til baka:<br>
https://arionapi-identityserver3-sandbox.azurewebsites.net/connect/authorize

To do an implicit flow (e.g. Python, Node.js or Javascript):
<br>

responsetype: "token" for an implicit client and redirect_uri set as:
redirect_uri=https%3a%2f%2farionapi-sandbox.portal.azure-api.net%2fdocs%2fservices%2f57361a83110546175c6fec3d%2fconsole%2foauth2%2fimplicit%2fcallback
<br>

NOTE. Implicit clients do NOT need to connect to this endpoint, but it applies code flow clients (e.g. Java/C#/Javascript/iOS/Android ):<br>
------------------------------------------------------------------------------------------------------------------------------
Token endpoint - sends the authorization-code retrieved from before, to get an access-token issued:<br>
https://arionapi-identityserver3-sandbox.azurewebsites.net/connect/token

Then uses the  authorization code and OcpApimSubscriptionKey (which you can get in the Azure portal:<br> https://arionapi-sandbox.portal.azure-api.net/ ) to invoke pretected Api endpoints.
<br>

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
https://arionapi-sandbox.portal.azure-api.net/docs/services/574d5a9cdbc60f015c0a5974/operations/57507fac6aa55e0e24113410?

<br>
*<b>[clientId]</b> er nafnið sem menn völdu sér á sinn OAuth2 biðlara í lið 02 að ofan.<br>
- Muna eða skrifa niður notandanafn/lykilorð sem menn fengu úthlutað til innskráningar, og nota þegar menn skrá sig inn. Lykilorðið verður ekki gefið aftur upp.
