using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
using Newtonsoft.Json.Linq;
using StudentApp.Entity;
using StudentApp.Service;

namespace StudentApp.Views
{
    public sealed partial class StudentPage : Page
    {

        private ObservableCollection<Clazz> listClazz;
        private ObservableCollection<Subject> listSubjects;
        public static string result = null;
        internal ObservableCollection<Subject> ListSubject
        {
            get => listSubjects;
            set => listSubjects = value;
        }
        internal ObservableCollection<Clazz> ListClazz
        {
            get => listClazz;
            set => listClazz = value;
        }
        private int _currentIndex;

        public StudentPage()
        {
            this.ListClazz = new ObservableCollection<Clazz>();
            this.ListSubject = new ObservableCollection<Subject>();
            this.InitializeComponent();
            this.GetInfoUser();
            this.GetClazz();
            this.GetSubject();
        }



        public static async Task<string> GetApi()
        {
            try
            {
                if (result == null)
                {
                    StorageFolder folder = ApplicationData.Current.LocalFolder;
                    StorageFile file = await folder.GetFileAsync("token.txt");
                    string content = await FileIO.ReadTextAsync(file);
                    TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + tokenResponse.accessToken);
                    var response = client.GetAsync(APIHandle.GET_INFO_USER + tokenResponse.OwnerId);
                    Debug.WriteLine(APIHandle.GET_INFO_USER + tokenResponse.OwnerId);
                    var contentResponse = await response.Result.Content.ReadAsStringAsync();
                    result = contentResponse;
                    StorageFile storageFile = await folder.CreateFileAsync("info.txt", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(storageFile, contentResponse);
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async void GetClazz()
        {
            try
            {
                await GetApi();
                Debug.WriteLine("day la result   " + result);
                var arrs = JObject.Parse(result)["studentClassRooms"].ToObject<Clazz[]>();
                var json = JsonConvert.SerializeObject(arrs);
                ObservableCollection<Clazz> clazzes = JsonConvert.DeserializeObject<ObservableCollection<Clazz>>(json);
                foreach (var clazz in clazzes)
                {
                    listClazz.Add(clazz);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debug.WriteLine(e);
                throw;
            }

        }

        public async void GetInfoUser()
        {
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile file = await folder.GetFileAsync("token.txt");
                string content = await FileIO.ReadTextAsync(file);
                TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + tokenResponse.accessToken);
                var response = client.GetAsync(APIHandle.GET_INFO_USER + tokenResponse.OwnerId);
                Debug.WriteLine(APIHandle.GET_INFO_USER + tokenResponse.OwnerId);
                var result = await response.Result.Content.ReadAsStringAsync();
                var arr = JObject.Parse(result)["studentClassRooms"].ToObject<Clazz[]>();
                for (int i = 0; i < arr.Length; i++)
                {
                    //Debug.WriteLine("Day la lop " + arr[i].ClassRoomId);
                    
                    int classId = arr[i].ClassRoomId;
                    switch (classId)
                    {
                        case 1:
                            this.txt_classroom.Text = "T1708A";
                            break;
                        case 2:
                            this.txt_classroom.Text = "T1708M1";
                            break;
                        case 3:
                            this.txt_classroom.Text = "T1709M";
                            break;
                        case 4:
                            this.txt_classroom.Text = "T1609A";
                            break;
                        default:
                            break;
                    }
                }

                Student student = JsonConvert.DeserializeObject<Student>(result);
                this.txt_email.Text = student.Email;
                this.txt_phone.Text = student.Phone;
                this.txt_address.Text = student.Address;
                this.txt_name.Text = student.Name;
                var DoB = student.DoB.Substring(0, 10);
                this.txt_birthday.Text = DoB;
                this.txt_birthday.Text = student.DoB;
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debug.WriteLine(e);
            }


        }

        public async void GetSubject()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var response = httpClient.GetAsync(APIHandle.GET_INFO_COURSE).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    ObservableCollection<Subject> subjects = JsonConvert.DeserializeObject<ObservableCollection<Subject>>(contentResponse);
                    foreach (var subject in subjects)
                    {
                        listSubjects.Add(subject);
                    }
                }
                else
                {
                    this.Error.Visibility = Visibility.Visible;
                    this.Content.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.Error.Visibility = Visibility.Visible;
                this.Content.Visibility = Visibility.Collapsed;
                //throw;
            }
            
            

        }
        private async void btn_change_info(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.ChangeInfo));
        }

        private void Clazz_choose(object sender, TappedRoutedEventArgs e)
        {
            _currentIndex = this.clazzList.SelectedIndex;
            var parameters = new Clazz();
            parameters.ClassRoomId = this.ListClazz[_currentIndex].ClassRoomId;
            this.Frame.Navigate(typeof(Views.DetailsClazz), parameters);
        }

        private void Subject_choose(object sender, TappedRoutedEventArgs e)
        {
            _currentIndex = this._listSubject.SelectedIndex;
            var parameters = new Subject();
            parameters.Id = this.ListSubject[_currentIndex].Id;
            parameters.Name = this.ListSubject[_currentIndex].Name;
            this.Frame.Navigate(typeof(Views.MarkDetails), parameters);
        }
    }
}
