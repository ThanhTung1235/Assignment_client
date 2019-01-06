using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentPage : Page
    {

        private ObservableCollection<Student> listStudent;
        internal ObservableCollection<Student> ListStudent
        {
            get => listStudent;
            set => listStudent = value;
        }
        private int _currentIndex;
        public StudentPage()
        {
            this.ListStudent = new ObservableCollection<Student>();
            this.ListStudent.Add(new Student()
            {
                Rollnumber = 1,
                Name = "Giàng A Tống",
                Email ="info@gmail.com",
            });
            this.ListStudent.Add(new Student()
            {
                Rollnumber = 1,
                Name = "Giàng A Tống",
                Email = "info@gmail.com",
            });
            this.ListStudent.Add(new Student()
            {
                Rollnumber = 1,
                Name = "Giàng A Tống",
                Email = "info@gmail.com",
            });
            this.ListStudent.Add(new Student()
            {
                Rollnumber = 1,
                Name = "Giàng A Tống",
                Email = "info@gmail.com",
            });


            this.InitializeComponent();
            this.GetInfoUser();
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
                Debug.WriteLine(result);

                StorageFile storageFile = await folder.CreateFileAsync("info.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(storageFile, result);

                var arr = JObject.Parse(result)["studentClassRooms"].ToObject<Clazz[]>();
                for (int i = 0; i < arr.Length; i++)
                {
                    Debug.WriteLine("Day la lop " + arr[i].ClassRoomId);
                    //handle
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
                //throw;
            }


        }
        private async void btn_change_info(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.ChangeInfo));
        }

        private void showDetails(object sender, TappedRoutedEventArgs e)
        {
            if (this.studentList.Visibility == Visibility.Collapsed && this.titleTable.Visibility == Visibility.Collapsed)
            {
                this.studentList.Visibility = Visibility.Visible;
                this.titleTable.Visibility = Visibility.Visible;
            }
            else
            {
                this.studentList.Visibility = Visibility.Collapsed;
                this.titleTable.Visibility = Visibility.Collapsed;
            }
        }
    }
}
