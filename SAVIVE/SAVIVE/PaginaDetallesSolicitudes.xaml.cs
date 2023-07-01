using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaDetallesSolicitudes : ContentPage
    {
        public PaginaDetallesSolicitudes()
        {
            InitializeComponent();
        }

        private void btn_aceptar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaginaRevisionArchivos());
        }

        private void btn_rechazar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaginaRechazoSolicitud());
            
        }
    }
}