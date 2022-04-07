using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DebuggingFiles.Classes
{
    /// <summary>
    /// <see cref="INotifyPropertyChanged"/> allows in this case <see cref="Beat"/> property to be
    /// updated from review form to the main form via <see cref="OnPropertyChanged"/>, same may be
    /// done with other properties in this case if needed.
    /// </summary>
    public class DataItem  : INotifyPropertyChanged
    {
        private string _beat;
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public int District { get; set; }

        public string Beat
        {
            get => _beat;
            set
            {
                _beat = value;
                OnPropertyChanged();
            }
        }

        public int Grid { get; set; }
        public string Description { get; set; }
        public int NcicCode { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool Inspect { get; set; }
        
        /// <summary>
        /// References will show zero (not true, try removing it)
        /// </summary>
        public string Line => $"{Id},{Date},{Address},{District},{Beat}," + 
                              $"{Grid},{Description},{NcicCode},{Latitude},{Longitude}";
        public override string ToString() => Id.ToString();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
