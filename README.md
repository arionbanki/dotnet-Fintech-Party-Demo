# DotNet sýnidæmi fyrir Fintech partý Arion banka hf. í júní 2016 (uppfært í september 2020)
Demo biðlari .NET, auðkennir og kallar á API með einfaldri virkni.

<br><br>Fyrzt nokkrir punktar varðandi OAuth2 auðkenningarmál fyrir hina ýmsu clienta og atriði varðandi Fintech:<br>

Dæmi um hvernig implicit client ( t.d. Javascript eða Python ) myndi kalla til að fá tóka:
----------------------------------------------------------------------------
https://arionapi-identityserver3-sandbox.azurewebsites.net/connect/authorize?response_type=token&client_id=XXXXXXXXXX&redirect_uri=https%3a%2f%2farionapi-sandbox.portal.azure-api.net%2fdocs%2fservices%2f57361a83110546175c6fec3d%2fconsole%2foauth2%2fimplicit%2fcallback&scope=financial
<br>
annað dæmi ( athugið að sum OAuth2 helper library url-encoda sjálf fyrir mann, önnur ekki - redirectUri'ið þarf að vera url-encodað - hægt er að nota þetta tól til að encoda url'ið: http://meyerweb.com/eric/tools/dencoder/ )
<br>
https://arionapi-identityserver3-sandbox.azurewebsites.net/connect/authorize?response_type=token&client_id=FintechAzureApiManagement&redirect_uri=https%3a%2f%2farionapi-sandbox.portal.azure-api.net%2fdocs%2fservices%2f57361a83110546175c6fec3d%2fconsole%2foauth2%2fimplicit%2fcallback&state=aae016ca-1c17-42bc-99d2-122c8470b0d9&scope=financial
<br>

Dæmi um harðkóðaðan access token sem notendur geta notað, til að einfalda málin - tókinn gildir til 04.20.2020 kl 23:43<br>
--------------------------------------------------------------------------------------------------------------------<br>
"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSIsImtpZCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSJ9.eyJjbGllbnRfaWQiOiJXcGZUZXN0Q2xpZW50Iiwic2NvcGUiOiJmaW5hbmNpYWwiLCJzdWIiOiIwNjEyMjQyMDM5IiwiYW1yIjoicGFzc3dvcmQiLCJhdXRoX3RpbWUiOjE2MDE0OTQ3MTMsImlkcCI6Imlkc3J2IiwiaXNzIjoiaHR0cHM6Ly9hcmlvbmFwaS1pZGVudGl0eXNlcnZlcjMtc2FuZGJveC5henVyZXdlYnNpdGVzLm5ldCIsImF1ZCI6Imh0dHBzOi8vYXJpb25hcGktaWRlbnRpdHlzZXJ2ZXIzLXNhbmRib3guYXp1cmV3ZWJzaXRlcy5uZXQvcmVzb3VyY2VzIiwiZXhwIjoxNjAxODU1MDA2LCJuYmYiOjE2MDE0OTUwMDZ9.K_TIWHL_i37xdZclP6Dn02XGfDGHeK0CEfSBxiUQ4jiKKdLtWUZvsPFRbx5OZ-trZwr68_OXvSy6RuSKK_v_ligtg4QWhy84oHOBQeBLZtIfWUFPdOC9h2sjNoYPr073qW617rJvSxUArnaiCvFNFu7UoshwaftA9v_Uy6ssBnFA8TfDV4Jab_E_O5_FayOgYd2OKVs4gexkyVpZndp6JIiDXvhWbTuQXDUD6TNOF4fJwa0_tBNKdLVzgHd8zRwys4Ki_YrTAzk5jrPcXpPhnS83JtVheZDJXpAfDl4Kkm27N9j40hQuwQ5IXrVZXAZCwn8D2bYxN-ys4YDS6fQXBQ"       


------------------------------------------------------------------------------<br>
ATH: bæði codeclients og implicitclient ( t.d. Python, Node.js og Javascript ) þurfa þennan endapunkt til að hefja innskráningu:<br>
----------------------------------------------------------------------------------------------
Authorize endpoint - til að fá authorization-code til baka:<br>
https://arionapi-identityserver3-sandbox.azurewebsites.net/connect/authorize

ath. til að gera implicit flow ( t.d. fyrir Python, Node.js og Javascript! ):
<br>

responsetype: "token" fyrir implicit client'a og redirect_uri sett svona:
redirect_uri=https%3a%2f%2farionapi-sandbox.portal.azure-api.net%2fdocs%2fservices%2f57361a83110546175c6fec3d%2fconsole%2foauth2%2fimplicit%2fcallback
<br>

ATH. Implicit client'ar þurfa EKKI að nota þennan endapunkt, en code clients ( t.d. java/C#/java/iOS/Android ) nota hann:<br>
------------------------------------------------------------------------------------------------------------------------------
Token endpoint - sendir authorization-code inn sem fékkst að ofan, til að fá access-token til baka:<br>
https://arionapi-identityserver3-sandbox.azurewebsites.net/connect/token

notar svo authorization code og OcpApimSubscriptionKey ( sem fæst í Azure portalnum hérna:<br> https://arionapi-sandbox.portal.azure-api.net/ ) til að kalla á varin WebApi
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
