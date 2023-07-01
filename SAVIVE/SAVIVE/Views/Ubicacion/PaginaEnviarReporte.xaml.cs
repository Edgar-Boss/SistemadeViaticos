using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using System.Net.Http;
using Newtonsoft.Json;
using SAVIVE.Clases;
using SAVIVE.Generic;
using SAVIVE.ViewModels;


namespace SAVIVE.Views.Ubicacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaEnviarReporte : ContentPage
    {
        List<UbicacionCLS> ListaUbicaciones = new List<UbicacionCLS>();
        List<double> coordenadas = new List<double>();
        
        public PaginaEnviarReporte(List<double> coordenadas)
        {
            this.coordenadas = coordenadas;
            InitializeComponent();
            InicializarDatos();
            Localizar();
            
        }

        private async void InicializarDatos()
        {
            obtener_ubicaciones();
            await  AjustarMapa();
            
        }
        private async void obtener_ubicaciones()
        {
            List<UbicacionCLS> l = await Generics.ListarDatos<UbicacionCLS>("http://www.sumate.somee.com/api/ubicacion");
            ListaUbicaciones = l;
            BindingContext = new DUbicacionVIewModel(ListaUbicaciones);
            await AjustarMapa();
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
                await AjustarMapa();
            };
        }

        private async Task AjustarMapa()
        {
            coordenadas = await Generics.ObtenerUbicacion();
            if (coordenadas == null)
            {
                await DisplayAlert(null, "Error al cargar mapa, por favor verifica que la ubicacion este activada", "ok");
                Navigation.RemovePage(Navigation.NavigationStack[2]);
                await Navigation.PopAsync();
            }
            else
            {

                Position position = new Position(coordenadas[0], coordenadas[1]);
                MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.5));//--
                map.MoveToRegion(mapSpan);
                map.Pins.Clear();
                Pin boardwalkPin = new Pin
                {
                    Position = new Position(coordenadas[0], coordenadas[1]),
                    Label = "Ubicacion actual",
                    Type = PinType.Place
                };

                map.Pins.Add(boardwalkPin);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient cliente = new HttpClient();
            UbicacionCLS ubi = new UbicacionCLS();

            ubi.Idubicacion = asignar_id();
            ubi.IdSolicitud = 2;
            ubi.IdEmpleado = 112;
            ubi.Fecha = DateTime.Now;
            ubi.Latitud = Convert.ToDecimal(coordenadas[0]);
            ubi.Longitud = Convert.ToDecimal(coordenadas[1]);
            if (ent_ubi.Text != null && ent_ubi.Text != "")
            {
                ubi.Ubicacion = ent_ubi.Text;
            }
            else 
            {
                ubi.Ubicacion = "ubicacion actual";
            }
            var cadena = JsonConvert.SerializeObject(ubi);
            var body = new StringContent(cadena, Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("http://www.sumate.somee.com/api/ubicacion", body);
            await DisplayAlert(null, "Ubicacion enviada", "ok");
            await Navigation.PopAsync();
        }

        private int asignar_id()
        {
            int x = 0;
            ListaUbicaciones.ForEach(i =>
            {
                if (i.Idubicacion > x)
                {
                    x = i.Idubicacion;
                }
            });
            return x+1;
        }
    }
}