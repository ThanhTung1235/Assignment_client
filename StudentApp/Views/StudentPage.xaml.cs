using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
    public sealed partial class StudentPage : Page
    {
        public StudentPage()
        {
            this.InitializeComponent();
        }

        public async void GetInfoUser()
        {
            //lay token tu file ra
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("token.txt");
            var content = await FileIO.ReadTextAsync(file);
            TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);

            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync(APIHandle.GET_INFO_USER);
            var responseContext = await response.Result.Content.ReadAsStringAsync();
            Student student = JsonConvert.DeserializeObject<Student>(responseContext);
            this.txt_email.Text = student.Email;
            this.txt_phone.Text = student.Phone;
            int gender_user = student.Gender;
            switch (gender_user)
            {
                case 2:
                    this.txt_gender.Text = "Nữ";
                    break;
                case 1:
                    this.txt_gender.Text = "Nam";
                    break;
                case 0:
                    this.txt_gender.Text = "Giới tính khác";
                    break;
            }


        }
        private void btn_change_info(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.ChangeInfo));
        }
    }
}
