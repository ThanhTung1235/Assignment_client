using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using StudentApp.Entity;
using Windows.System;
using Newtonsoft.Json;
using StudentApp.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailsClazz : Page
    {
        private ObservableCollection<Student> litStudent;
        internal ObservableCollection<Student> ListStudent
        {
            get => litStudent;
            set => litStudent = value;
        }
        public DetailsClazz()
        {
            this.litStudent = new ObservableCollection<Student>();
            //this.litStudent.Add(new Student()
            //{
            //    Id = 11,
            //    Name = "giàng a tống",
            //    Email = "info@gmail.com",
            //});
            //this.litStudent.Add(new Student()
            //{
            //    Id = 11,
            //    Name = "giàng a tống",
            //    Email = "info@gmail.com",
            //});
            //this.litStudent.Add(new Student()
            //{
            //    Id = 11,
            //    Name = "giàng a tống",
            //    Email = "info@gmail.com",
            //});
            //this.litStudent.Add(new Student()
            //{
            //    Id = 11,
            //    Name = "giàng a tống",
            //    Email = "info@gmail.com",
            //});
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Clazz) e.Parameter;

            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync(APIHandle.GET_CLAZZ + parameters.ClassRoomId);
            var result = await response.Result.Content.ReadAsStringAsync();
            ObservableCollection<Student> students = JsonConvert.DeserializeObject<ObservableCollection<Student>>(result);
            foreach (var student in students)
            {
                litStudent.Add(student);
            }
        }



        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.DetailsFrame.Navigate(typeof(Views.StudentPage));
        }

    }
}
