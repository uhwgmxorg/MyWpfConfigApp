using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace MyWpfConfigApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region INotify Changed Properties  
        private string message;
        public string Message
        {
            get { return message; }
            set { SetField(ref message, value, nameof(Message)); }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            Title += Globals.BuildString;
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// Button_1_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            Message = "Button 1 Click";
        }

        /// <summary>
        /// Button_RestoreDefault_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_RestoreDefault_Click(object sender, RoutedEventArgs e)
        {
            Message = "Restore Default Values";
            Properties.Settings.Default.Reset();
        }

        /// <summary>
        /// Button_Reload_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Reload_Click(object sender, RoutedEventArgs e)
        {
            Message = "Reload Last Values";
            Properties.Settings.Default.Reload();
        }

        /// <summary>
        /// Button_Save_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            Message = "Save Values";
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Button_Close_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        /// <summary>
        /// Lable_Message_MouseDown
        /// Clear Message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lable_Message_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Message = "";
        }

        /// <summary>
        /// Window_Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Window_Closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// SetField
        /// for INotify Changed Properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #endregion

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }

    public static class Globals
    {
        public static string Revison { get; } = "0";
        public static string ExeDir
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        public static string Exe
        {
            get
            {
                return $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.exe";
            }
        }
        public static string BuildString
        {
            get
            {
                System.DateTime buildDate = System.IO.File.GetLastWriteTime(System.IO.Path.Combine(ExeDir, Exe));
#if DEBUG
                return $"    Debug Version {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}  Revison {Globals.Revison} - Build-Date {buildDate}";
#else
                return $"    Release Version {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}  Revison {Globals.Revison} - Build-Date {buildDate}";
#endif
            }
        }
    }
}
