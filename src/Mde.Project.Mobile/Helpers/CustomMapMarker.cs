using Mde.Project.Core.Entities;
using Syncfusion.Maui.Maps;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mde.Project.Mobile.Helpers
{
    public class CustomMapMarker : MapMarker, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Farm Farm { get; set; }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set 
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
