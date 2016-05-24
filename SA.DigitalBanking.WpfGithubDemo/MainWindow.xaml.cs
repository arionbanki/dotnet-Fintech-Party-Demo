using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Windows;
using Newtonsoft.Json.Linq;
using Sample;
using Thinktecture.IdentityModel.Client;
using Thinktecture.Samples;
using WpfClient;

namespace SA.DigitalBanking.WpfGithubDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginWebView _login;
        AuthorizeResponse _response;

        private string accessToken;

        public MainWindow()
        {
            InitializeComponent();

            _login = new LoginWebView();
            _login.Done += _login_Done;

            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _login.Owner = this;
        }

        void _login_Done(object sender, AuthorizeResponse e)
        {
            _response = e;
            Textbox1.Text = e.Raw;
        }

        private void LoginWithProfileButton_Click(object sender, RoutedEventArgs e)
        {
            RequestToken("financial", "code");
        }

        private void RequestToken(string scope, string responseType)
        {
            var client = new OAuth2Client(new Uri(Constants.AuthorizeEndpoint));
            var startUrl = client.CreateAuthorizeUrl(
                clientId: Constants.ClientId,
                responseType: responseType,
                scope: scope,
                redirectUri: Constants.ClientRedirectUrl, 
                state: "random_state",
                nonce: "random_nonce");

            _login.Show();
            _login.Start(new Uri(startUrl), new Uri(Constants.ClientRedirectUrl));
        }

        private async void UseCodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_response != null && _response.Values.ContainsKey("code"))
            {
                var client = new OAuth2Client(
                    new Uri(Constants.TokenEndpoint),
                    Constants.ClientId,
                    Constants.ClientSecret);

                var response = await client.RequestAuthorizationCodeAsync(
                    _response.Code,
                    Constants.ClientRedirectUrl);

                Textbox1.Text = response.Json.ToString();
                
                accessToken = response.AccessToken;
                this.textBoxAuthHeader.Text = "Bearer " + response.AccessToken;
            }
        }

        private void ShowAccessTokenButton_Click(object sender, RoutedEventArgs e)
        {
            var viewer = new IdentityTokenViewer();
            viewer.IdToken = accessToken;
            viewer.Show();
        }
        private async void CallCoreCreditcardServiceButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            string ocpApimSubscriptionKey = Constants.OcpApimSubscriptionKey;

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ocpApimSubscriptionKey);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            //var uri = "https://arionapi-sandbox.azure-api.net/accounts/v1/getall?" + queryString;
            var uri = "https://arionapi-sandbox.azure-api.net/creditcards/v1/creditCards";

            var response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                Textbox1.Text = JObject.Parse(json).ToString();
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
        }

        private async void CallClaimsServiceButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            string ocpApimSubscriptionKey = Constants.OcpApimSubscriptionKey;

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ocpApimSubscriptionKey);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            //var uri = "https://arionapi-sandbox.azure-api.net/accounts/v1/getall?" + queryString;
            var uri = "https://arionapi-sandbox.azure-api.net/claims/v1/claims";

            var response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                Textbox1.Text = JObject.Parse(json).ToString();
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
        }

        private async void CallCoreAccountServiceButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            string ocpApimSubscriptionKey = Constants.OcpApimSubscriptionKey;

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ocpApimSubscriptionKey);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            // Get a single account:
            //var uri = "https://arionapi-sandbox.azure-api.net/accounts/v1/accounts/" + "XXXXXXXXXXXXX";
            // Get all accounts:
            var uri = "https://arionapi-sandbox.azure-api.net/accounts/v1/accounts";

            var response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                Textbox1.Text = JObject.Parse(json).ToString();
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(this.textBoxAuthHeader.Text);
        }
    }
}