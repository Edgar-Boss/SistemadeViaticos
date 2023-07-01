using Plugin.Geolocator;
using SAVIVE.Clases;
using SAVIVE.Generic;
using SAVIVE.Models;
using SAVIVE.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class paginaVerReportesUbicacion : ContentPage
    {
        public  List<UbicacionCLS> ListaUbicaciones = new List<UbicacionCLS>();

        double lati, longi;
        public paginaVerReportesUbicacion()
        {
            Localizar();
            InitializeComponent();
            obtener_nombres();
            

        }
        private async void obtener_nombres()
        {
            
            List<UbicacionCLS> l = await Generics.ListarDatos<UbicacionCLS>("http://www.sumate.somee.com/api/ubicacion");
            ListaUbicaciones = l;
            BindingContext = new DUbicacionVIewModel(ListaUbicaciones);

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame indice = (Frame)sender;

            ListaUbicaciones.ForEach(i => 
            {
                if (i.Idubicacion == indice.TabIndex)
                {

                    lati = Convert.ToDouble(i.Latitud);
                    longi = Convert.ToDouble(i.Longitud);
                    MapLaunchOptions options = new MapLaunchOptions { Name = "Mi posicionactual" };
                    Xamarin.Essentials.Map.OpenAsync(lati, longi, options);
                }

            });
            

            
        }

        private async void Localizar()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            if (!locator.IsListening)
            {
                await locator.StartListeningAsync(TimeSpan.FromSeconds(1), 50);
            }
            locator.PositionChanged += async (cambio, args) =>
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best);
                    var location = await Geolocation.GetLocationAsync(request);

                    lati = location.Latitude;
                    longi = location.Longitude;
                }
                catch (Exception ex)
                {
                    await DisplayAlert(null, "error mapas", "ok");
                }

                
            };

        }


    }
}