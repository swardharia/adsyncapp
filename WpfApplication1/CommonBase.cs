using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfApplication1
{
    public class CommonBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged ( string propertyName )
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if ( handler != null )
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs( propertyName );
                handler( this , e );
            }
        }

        #endregion
    }
}
