using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.Email.DataProvider;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using StudentApp.Entity;
using StudentApp.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignIn : Page
    {
        private static Student currentLogin;
        public SignIn()
        {
            this.InitializeComponent();
        }

        private async void Button_submit(object sender, RoutedEventArgs e)
        {
            string email = this.Email.Text;
            string password = this.Password.Password;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                Login();
            }
            else
            {
                this.email.Text = "Eamil phải nhập đúng định dạng 'info@email.com'";
            }
            if (email.Length == 0)
            {
                this.email.Text = "Không được bỏ trống trường này'";
            }
            else
            {
                Login();
                this.email.Text = "";
            }
            if (password.Length == 0)
            {
                this.password.Text = "Không được bỏ trống trường này'";
            }
            else
            {
                Login();
                this.password.Text = "";
            }
        }

        public async void Login()
        {
            try
            {
                Dictionary<string, string> login_handle = new Dictionary<string, string>();
                login_handle.Add("email", this.Email.Text);
                login_handle.Add("password", this.Password.Password);
                HttpClient httpClient = new HttpClient();
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(login_handle), System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(APIHandle.LOGIN_API, stringContent).Result;
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    Debug.WriteLine("Login Success");
                    Debug.WriteLine(responseContent);
                    TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent); //read token
                    StorageFolder folder = ApplicationData.Current.LocalFolder;// save token file
                    StorageFile storageFile = await folder.CreateFileAsync("token.txt", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(storageFile, responseContent);
                    var rootFrame = Window.Current.Content as Frame;
                    rootFrame.Navigate(typeof(MainPage));

                }
                else
                {
                    Debug.WriteLine("Login Fail");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debug.WriteLine(e);
            }
            
        }
        public static async void DoLogin()
        {
            currentLogin = new Student();
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            if (await folder.TryGetItemAsync("token.txt") != null)
            {
                StorageFile file = await folder.GetFileAsync("token.txt");
                var tokenContent = await FileIO.ReadTextAsync(file);

                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(tokenContent);
                Debug.WriteLine("token la" + token.accessToken);
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(MainPage));
                Debug.WriteLine("Da dang nhap thanh cong");
            }
            else
            {
                Debug.WriteLine("File doesn't exist");
            }
        }

        private void Sigin_Loaded(object sender, RoutedEventArgs e)
        {
            //DoLogin();
        }
    }
}
