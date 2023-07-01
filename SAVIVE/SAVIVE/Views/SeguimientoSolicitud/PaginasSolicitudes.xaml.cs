using Newtonsoft.Json;
using SAVIVE.Clases;
using SAVIVE.ViewModels;
using SAVIVE.SeguimientoSolicitud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAVIVE.Validar_Internet;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginasSolicitudes : TabbedPage
    {
        public List<SolicitudCLS> solicitudes = new List<SolicitudCLS>();
        int filtrado = 0;
        public PaginasSolicitudes()
        {
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("4B8673"); //cambio de color de barra de navegacion
            InitializeComponent();
            P_conexion.BindingContext = new VMComprobarConexion(Navigation);
            Agregar_datos();

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += TapGestureRecognizer_Tapped;
            frm_activo.GestureRecognizers.Add(tap);
            frm_pendiente.GestureRecognizers.Add(tap);
            frm_aprobado.GestureRecognizers.Add(tap);
        }
        private void Clic_frame(object sender, EventArgs e)
        {
            Frame indice = (Frame)sender;
            solicitudes.ForEach(i => {

                if (i.idsolicitud == indice.TabIndex)
                {
                    Navigation.PushAsync(new PaginaDatosSolicitud(i));  
                }
            });
        }
        private void btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaginaDetallesSolicitudes());
        }
        private async void Agregar_datos()
        {
            try
            {
                await Obtener_solicitudes();
                Agregar_lista_observable();
            }
            catch (Exception ex) 
            {
                await DisplayAlert(null,"Error al obtener datos","ok");
            }
        }
        public void Agregar_lista_observable()
        {
            BindingContext = new SolicitudViewModel(solicitudes);
            cv_comprobar.BindingContext = new SolicitudViewModel(Generic.Generics.Filtrar_listas(solicitudes, 2, null));
        }
        private async Task Obtener_solicitudes()
        {

            HttpClient cliente = new HttpClient();
            var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/solicitud");

            var result = await rpta.Content.ReadAsStringAsync();
            List<SolicitudCLS> l = JsonConvert.DeserializeObject<List<SolicitudCLS>>(result);
            solicitudes = l;
            
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame indice = (Frame)sender;
            if (indice.TabIndex == filtrado)
            {
                BindingContext = new SolicitudViewModel(solicitudes);
                filtrado = 0;
            }
            else
            {
                filtrado = indice.TabIndex;
                BindingContext = new SolicitudViewModel(Generic.Generics.Filtrar_listas(solicitudes, indice.TabIndex, null));
            }
        }
        private void btn_buscar_Clicked(object sender, EventArgs e)
        {
            cv_comprobar.BindingContext = new SolicitudViewModel(Generic.Generics.Filtrar_listas(solicitudes,2, ent_busc.Text));
        }
        private void Click_frame_comprobar(object sender, EventArgs e)
        {
            Frame indice = (Frame)sender;
            solicitudes.ForEach(i => 
            {
                if (i.idsolicitud == indice.TabIndex)
                {
                    Navigation.PushAsync(new PaginaDatosComisionados(i));
                }
            });

        }
        private void ent_busc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            cv_comprobar.BindingContext = new SolicitudViewModel(Generic.Generics.Filtrar_listas(solicitudes,2, ent_busc.Text));   
        }
    }
}