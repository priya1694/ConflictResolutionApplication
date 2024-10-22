using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using System.Collections.ObjectModel;
using ConflictResolutionApplication.ViewModels;
using ConflictResolutionApplication.Models;

namespace ConflictResolutionApplication
{
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }

        public MainWindow(MainViewModel mainViewModel)
        {
            this.InitializeComponent();
            ViewModel = mainViewModel;

            root.DataContext = ViewModel;
        }

        private void HeaderButton_Click(object obj, RoutedEventArgs e)
        {
            var button = obj as Button;
            if (button != null && button.CommandParameter is string columnName)
            {
                var viewModel = (MainViewModel)root.DataContext;
                viewModel.SortTrips(columnName);
            }
        }


        private object GetPropertyValue(Trip trip, string propertyName)
        {
            return typeof(Trip).GetProperty(propertyName)?.GetValue(trip, null);
        }
    }
}
