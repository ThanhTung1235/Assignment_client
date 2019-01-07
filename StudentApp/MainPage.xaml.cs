using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StudentApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string _currentTag = " ";
        public MainPage()
        {
            this.InitializeComponent();
            this.Frame_content.Navigate(typeof(Views.StudentPage));
        }

        private void Btn_bar_OnClick(object sender, RoutedEventArgs e)
        {
            this.Student_splitview.IsPaneOpen = !this.Student_splitview.IsPaneOpen;
        }

        public async void Logout()
        {
            StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("token.txt");
            if (storageFile != null)
            {
                await storageFile.DeleteAsync();
            }
        }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (_currentTag == radioButton.Tag.ToString())
            {
                return;
            }

            switch (radioButton.Tag.ToString())
            {
                case "Home":
                    _currentTag = "Home";
                    this.Frame_content.Navigate(typeof(Views.StudentPage));
                    break;
                case "Info":
                    _currentTag = "Info";
                    this.Frame_content.Navigate(typeof(Views.ChangeInfo));
                    break;
                case "Logout":
                    _currentTag = "Logout";
                    Logout();
                    var rootFrame = Window.Current.Content as Frame;
                    rootFrame.Navigate(typeof(Views.SignIn));
                    break;
            }

        }
    }
}
