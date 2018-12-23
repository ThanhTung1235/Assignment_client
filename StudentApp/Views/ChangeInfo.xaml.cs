using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        }
        private async void Do_Submit(object sender, RoutedEventArgs e)
        {
            //validate data input
            this._currentStudent.Email = this.Email.Text;
            //this._currentStudent.Name = this.Name.Text;
            this._currentStudent.Phone = this.Email.Text;
            this._currentStudent.Address = this.Email.Text;
            string jsonUser = JsonConvert.SerializeObject(_currentStudent);
            Debug.WriteLine(jsonUser);


            //HttpClient httpClient = new HttpClient();
            //StringContent stringContent = new StringContent(jsonUser,Encoding.UTF8,"application/json");
            //var response =  httpClient.PostAsync(APIHandle.CHANGE_INFO_USER,stringContent);
            //var responseText = await response.Result.Content.ReadAsStringAsync();
            //if (response.Result.StatusCode == HttpStatusCode.Created)
            //{
            //    var rootFrame = Window.Current.Content as Frame;
            //    rootFrame.Navigate(typeof(Views.StudentPage));
            //    Debug.WriteLine("Change success!!!");
            //}
            this.success.Text = "Thay đổi thông tin thành công";
            this.Frame.Navigate(typeof(Views.StudentPage));
            
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
    }
}
