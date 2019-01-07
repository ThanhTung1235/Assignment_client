using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
    public sealed partial class ChangeInfo : Page
    {
        private Student _currentStudent;
        public ChangeInfo()
        {
            this._currentStudent = new Student();
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            GetInfo();
        }

        public async void GetInfo()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("token.txt");
            var content = await FileIO.ReadTextAsync(file);
            TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + tokenResponse.accessToken);

            var responseApi = httpClient.GetAsync(APIHandle.GET_INFO_USER +tokenResponse.OwnerId);
            var responseContent = await responseApi.Result.Content.ReadAsStringAsync();
            Debug.WriteLine("Day la list student "+responseContent);
            Student student = JsonConvert.DeserializeObject<Student>(responseContent);
            this.Email.Text = student.Email;
            this.Phone.Text = student.Phone;
            this.Address.Text = student.Address;
            this.Name.Text = student.Name;
            var DoB = student.DoB.Substring(0, 10);
            //this.txt_birthday.Text = DoB;
            //this.txt_birthday.Text = student.DoB;
            int gender_user = student.Gender;
        }
        private async void Do_Submit(object sender, RoutedEventArgs e)
        {
            string email = this.Email.Text;
            string name = this.Name.Text;
            string phone = this.Phone.Text;
            string address = this.Address.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            Debug.WriteLine(match.Value);
            if (match.Success)
            {
                SendInfoChange();
            }
            else
            {
                this.email.Text = "Email phải nhập đúng định dạng 'info@email.com'";
            }
            if (email.Length == 0)
            {
                this.email.Text = "Không được bỏ trống trường này'";
            }
            else
            {
                SendInfoChange();
                //this.email.Text = "";
            }
            if (name.Length == 0)
            {
                this.name.Text = "Không được bỏ trống trường này'";
            }
            else
            {
                SendInfoChange();
                this.name.Text = "";
            }
            if (phone.Length == 0)
            {
                this.phone.Text = "Không được bỏ trống trường này'";
            }
            else
            {
                SendInfoChange();
                this.phone.Text = "";
            }
            if (address.Length == 0)
            {
                this.address.Text = "Không được bỏ trống trường này'";
            }
            else
            {
                SendInfoChange();
                this.address.Text = "";
            }
        }

        public async void SendInfoChange()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("token.txt");
            var content = await FileIO.ReadTextAsync(file);
            TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);

            HttpClient httpClient = new HttpClient();
            this._currentStudent.Email = this.Email.Text;
            this._currentStudent.Name = this.Name.Text;
            this._currentStudent.Phone = this.Phone.Text;
            this._currentStudent.Address = this.Address.Text;
            string jsonUser = JsonConvert.SerializeObject(_currentStudent);

            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + tokenResponse.accessToken);
            StringContent stringContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            Debug.WriteLine("Thong tin thay doi " + jsonUser);
            var response = httpClient.PostAsync(APIHandle.CHANGE_INFO_USER, stringContent);
            var responseText = await response.Result.Content.ReadAsStringAsync();
            if (response.Result.StatusCode == HttpStatusCode.OK)
            {
                this.success.Text = "Thay đổi thông tin thành công";
                MessageDialog messageDialog = new MessageDialog("Thay đổi thông tin thành công");
                messageDialog.ShowAsync();
                this.Frame.Navigate(typeof(Views.StudentPage));
                Debug.WriteLine("Change success!!!");
            }
            else
            {
                Debug.WriteLine("Change fail " + response.Result.StatusCode);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            this._currentStudent.Gender = Int32.Parse(radio.Tag.ToString());
        }

        private void BirthdayPicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            this._currentStudent.DoB = sender.Date.Value.ToString("yyyy-MM-dd");
        }

        private void btn_back(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.StudentPage));
        }
    }
}
