using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WpfApplication1
{
    public class SimpleViewModel : WpfApplication1.CommonBase
    {
        string response;
 /*       string btn;

        public string Btn1
        {

            get { return this.btn; }

            set
            {
                if ( this.btn == value )
                    return;

                this.btn = value;
                RaisePropertyChanged( "Response" );
            }
        }

   */
        public string Response
        {

            get { return this.response; }

            set
            {
                if ( this.response == value )
                    return;

                this.response = value;
                RaisePropertyChanged( "Response" );
            }
        }
    }
}
