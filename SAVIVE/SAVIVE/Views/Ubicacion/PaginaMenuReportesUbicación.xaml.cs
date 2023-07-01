using SAVIVE.Views.Ubicacion;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAVIVE.Generic;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaReportesUbicación : ContentPage
    {
        List<double> coordenadas = new List<double>();



        public PaginaReportesUbicación()
        {
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("4B8673"); // Cambio de color de la barra de navegación

            InitializeComponent();

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += TapGestureRecognizer_Tapped;
            frm_crarsol.GestureRecognizers.Add(tap);
            frm_seguimiento.GestureRecognizers.Add(tap);

            InicializarAsync();
        }

        private async void InicializarAsync()
        {

            coordenadas = await Generics.ObtenerUbicacion();
           
            if(coordenadas==null)
            {
                await DisplayAlert(null,"Error al inicializar mapa, por favor verifica que la ubicacion este activada","ok");
                await Navigation.PopAsync();
            }

            
            
        }

        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame indice = (Frame)sender;

            switch (int.Parse(indice.StyleId))
            {
                case 0:
                    await Navigation.PushAsync(new paginaVerReportesUbicacion());
                    break;
                case 1:
                    await Navigation.PushAsync(new PaginaEnviarReporte(coordenadas));
                    break;
               

            }
        }
    }
}