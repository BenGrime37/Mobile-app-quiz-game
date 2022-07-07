using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace mobileAppAssigment.Models
{
    public class Products : INotifyPropertyChanged
    {
        private Guid _productId { get; set; }

        public Guid productId
        {
            get { return _productId; }
            set
            {
                _productId = value; OnPropertyChanged();
            }
        }
        private string _productName { get; set; }

        public string productName
        {
            get { return _productName; }
            set
            {
                _productName = value; OnPropertyChanged();
            }
        }
        private double? _productPrice { get; set; }

        public double? productPrice
        {
            get { return _productPrice; }
            set
            {
                _productPrice = value; OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
