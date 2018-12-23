using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using StudentApp.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignIn : Page
    {
        public SignIn()
        {
            this.InitializeComponent();
            ((Storyboard)Resources["GradientAnimation"]).Begin();
        }
        
        private async void Button_submit(object sender, RoutedEventArgs e)
        {
            var txt_email = this.Email.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(txt_email);
            if (match.Success)
            {
                this.email.Text = "";
                //Debug.WriteLine("True");
                Dictionary<string, string> login_handle = new Dictionary<string, string>();
                login_handle.Add("email", this.Email.Text);
                login_handle.Add("password", this.Password.Password);
                //HttpClient httpClient = new HttpClient();
                //StringContent stringContent = new StringContent(JsonConvert.SerializeObject(login_handle), System.Text.Encoding.UTF8, "application/json");
                //var respon = httpClient.PostAsync(APIHandle.LOGIN_API, stringContent).Result;
                //var responContext = await respon.Content.ReadAsStringAsync();
                //if (respon.StatusCode == HttpStatusCode.OK)
                //{
                //    Debug.WriteLine("Login success");
                //    // server tra ve token va sau do client se luu token ra file va se chuyen huong nguoi dung sang frame StudentPage
                //    var currentFrame = Window.Current.Content as Frame;
                //    currentFrame.Navigate(typeof(Views.StudentPage));
                //}
                Debug.WriteLine(login_handle);
            }
            else
            {
                this.email.Text = "Bạn phải nhập đúng định dạng email vd:'info@mail.com'";
                return;
            }

            
        }
    }
}
