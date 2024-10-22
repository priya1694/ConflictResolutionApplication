using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using ConflictResolutionApplication.Models;
using System.Linq;
using System;
using System.Windows.Input;
using ConflictResolutionApplication.Helpers;



namespace ConflictResolutionApplication.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public ICommand EnableAutomaticConflictResolutionCommand { get; }

        private ObservableCollection<Trip> _trips;
        public ObservableCollection<Trip> Trips
        {
            get => _trips;
            set
            {
                _trips = value;
                OnPropertyChanged();
            }
        }
        private string _currentSortColumn;
        private bool _isAscending;

        private ObservableCollection<string> _services;
        public ObservableCollection<string> Services
        {
            get => _services;
            set
            {
                _services = value;
                OnPropertyChanged();
            }
        }



        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush _statusColor;
        public SolidColorBrush StatusColor
        {
            get => _statusColor;
            set
            {
                _statusColor = value;
                OnPropertyChanged();
            }
        }
       
        public MainViewModel()
        {
            StatusMessage = "Automatic Conflict Resolution Disabled";
            StatusColor = new SolidColorBrush(Colors.Red);

            EnableAutomaticConflictResolutionCommand = new RelayCommand(EnableAutomaticConflictResolution);



            LoadData();

        }
       
        private void LoadData()
        {
            Services = new ObservableCollection<string> { "01", "02", "03" };

            Trips = new ObservableCollection<Trip>
            {
                new Trip { ServiceID = "03", TripID = "0304", Direction = "R", StartTime = "06-29-24 12:14", TrainType = "Default", Length = 0, PostFix = "None", StartLocation = "PVO1_DAR", EndLocation = "P1C_SKU", Conflicts = 2 },
                new Trip { ServiceID = "01", TripID = "0101", Direction = "L", StartTime = "06-28-24 06:12", TrainType = "Default", Length = 0, PostFix = "G", StartLocation = "PVO1_DAR", EndLocation = "P1C_SKU", Conflicts = 0 },
                new Trip { ServiceID = "01", TripID = "0102", Direction = "R", StartTime = "06-28-24 06:12", TrainType = "Freight", Length = 0, PostFix = "None", StartLocation = "PVO1_DAR", EndLocation = "P1C_SKU", Conflicts = 0 },
                new Trip { ServiceID = "01", TripID = "0103", Direction = "L", StartTime = "06-28-24 06:24", TrainType = "Passenger", Length = 0, PostFix = "None", StartLocation = "PVO1_DAR", EndLocation = "P1C_SKU", Conflicts = 0 },
                new Trip { ServiceID = "01", TripID = "0104", Direction = "R", StartTime = "06-28-24 06:24", TrainType = "Default", Length = 0, PostFix = "None", StartLocation = "PVO1_DAR", EndLocation = "P1C_SKU", Conflicts = 0 },
                new Trip { ServiceID = "02", TripID = "0201", Direction = "L", StartTime = "06-28-24 09:48", TrainType = "Passenger", Length = 0, PostFix = "None", StartLocation = "PVO1_DAR", EndLocation = "P1C_SKU", Conflicts = 0 },
                new Trip { ServiceID = "02", TripID = "0202", Direction = "R", StartTime = "06-28-24 09:48", TrainType = "Freight", Length = 0, PostFix = "None", StartLocation = "PVO1_DAR", EndLocation = "P1C_SKU", Conflicts = 0 },
                new Trip { ServiceID = "03", TripID = "0301", Direction = "L", StartTime = "06-28-24 12:12", TrainType = "Passenger", Length = 0, PostFix = "None", StartLocation = "PVO1_DAR", EndLocation = "P1C_SKU", Conflicts = 1 },
                new Trip { ServiceID = "03", TripID = "0302", Direction = "R", StartTime = "06-28-24 12:12", TrainType = "Freight", Length = 0, PostFix = "None", StartLocation = "PVO1_DAR", EndLocation = "P1C_SKU", Conflicts = 1 },
                new Trip { ServiceID = "03", TripID = "0303", Direction = "L", StartTime = "06-28-24 12:14", TrainType = "Passenger", Length = 0, PostFix = "None", StartLocation = "PVO1_DAR", EndLocation = "P1C_SKU", Conflicts = 2 }
            };
        }
        public void SortTrips(string columnName)
        {
            if (_currentSortColumn == columnName)
            {
                _isAscending = !_isAscending; 
            }
            else
            {
                _isAscending = true; 
            }

            _currentSortColumn = columnName;

            var sortedTrips = _isAscending
                ? Trips.OrderBy(t => GetPropertyValue(t, columnName)).ToList()
                : Trips.OrderByDescending(t => GetPropertyValue(t, columnName)).ToList();

            Trips.Clear();
            foreach (var trip in sortedTrips)
            {
                Trips.Add(trip);
            }
        }

        private object GetPropertyValue(Trip trip, string propertyName)
        {
            return propertyName switch
            {
                "ServiceID" => trip.ServiceID,
                "TripID" => trip.TripID,
                "Direction" => trip.Direction,
                "StartTime" => DateTime.Parse(trip.StartTime), // Convert to DateTime for correct sorting
                "TrainType" => trip.TrainType,
                "Length" => trip.Length,
                "Postfix" => trip.PostFix,
                "StartLocation" => trip.StartLocation,
                "EndLocation" => trip.EndLocation,
                "Conflicts" => trip.Conflicts,
            };
        }


        private void EnableAutomaticConflictResolution()
        {
            StatusMessage = "Automatic Conflict Resolution Enabled";
            StatusColor = new SolidColorBrush(Colors.Green);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
