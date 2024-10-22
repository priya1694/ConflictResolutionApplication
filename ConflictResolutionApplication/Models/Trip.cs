using Microsoft.UI.Xaml.Media;

namespace ConflictResolutionApplication.Models
{
    public class Trip
    {
        public string ServiceID { get; set; }
        public string TripID { get; set; }
        public string Direction { get; set; }
        public string StartTime { get; set; }
        public string TrainType { get; set; }
        public int Length { get; set; }
        public string PostFix { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public int Conflicts { get; set; }
        public SolidColorBrush TextColor
        {
            get
            {
                return Conflicts > 0 ? new SolidColorBrush(Microsoft.UI.Colors.Red)
                                     : new SolidColorBrush(Microsoft.UI.Colors.Black);
            }
        }
    }
}
