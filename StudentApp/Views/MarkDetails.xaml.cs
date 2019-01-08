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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MarkDetails : Page
    {
        private ObservableCollection<Mark> listMarks;
        internal ObservableCollection<Mark> ListMarks
        {
            get => listMarks;
            set => listMarks = value;
        }

        public MarkDetails()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Subject)e.Parameter;

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync("token.txt");
            string content = await FileIO.ReadTextAsync(file);
            Student student = JsonConvert.DeserializeObject<Student>(content);
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://fptmanagersutdent.azurewebsites.net/api/MarksAPI?studentId=" + student.Id + "&courseId=" + parameters.Id).Result;
            var responseContent = await response.Content.ReadAsStringAsync();
            ObservableCollection<Mark> marks =  JsonConvert.DeserializeObject<ObservableCollection<Mark>>(responseContent);
            foreach (var mark in marks)
            {
                
            }
            
        }

        private void back_button(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.StudentPage));
        }
    }
}
