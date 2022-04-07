using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SidesMpcLibrary.Models
{
    /// <summary>
    /// Represents a single key/value from configuration file
    /// </summary>
    public class SettingItem : INotifyPropertyChanged
    {
        private string _value;
        /// <summary>
        /// Key in file
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of key
        /// </summary>
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Expected type as when read in all values are strings
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// Is Value a folder
        /// </summary>
        public bool IsPath { get; set; }
        public override string ToString()
        {
            return $"{Name} = {Value}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
