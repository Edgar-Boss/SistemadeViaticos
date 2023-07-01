using Android.Provider;
using Newtonsoft.Json;
using SAVIVE.Clases;
using SAVIVE.Validar_Internet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Provider.CalendarContract;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaMenu : ContentPage
    {
        int noemp = 0;
        string Nombre;
        string Appaterno;
        string Apmaterno;
        string Puesto;
        string Area;
        int contador = 0;
        NavigationPage navigationPage = Application.Current.MainPage as NavigationPage;
        public PaginaMenu(int noemp)
        {
            this.noemp = noemp;
            obtener_datos();
            InitializeComponent();
            BindingContext = new VMComprobarConexion(Navigation);
            NavigationPage.SetHasBackButton(this, false);
            
            navigationPage.BarBackgroundColor = Color.FromHex("4B8673"); //  3A89C9

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += TapGestureRecognizer_Tapped;
            
            frm_crarsol.GestureRecognizers.Add(tap);
            frm_seguimiento.GestureRecognizers.Add(tap);
            frm_ubicacion.GestureRecognizers.Add(tap);
            frm_pass.GestureRecognizers.Add(tap);
        }

        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {    
            Frame indice = (Frame)sender;
            
            switch (int.Parse(indice.StyleId))
            {
                case 0:
                    
                    await Navigation.PushAsync(new PaginaCrearSolicitud(noemp,Nombre,Appaterno,Apmaterno,Puesto));
                    break;
                case 1:
                    await Navigation.PushAsync(new PaginasSolicitudes());
                    break;
                case 2:
                    await Navigation.PushAsync(new PaginaReportesUbicación());
                    break;
                case 3:
                    await Navigation.PushAsync(new PaginaCambioContrasena());
                    break;
               

            }
        }

        
        private async void obtener_datos()
        {

            try
            {
                HttpClient cliente = new HttpClient();
                var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/persona");

                var result = await rpta.Content.ReadAsStringAsync();
                List<PersonaCLS> l = JsonConvert.DeserializeObject<List<PersonaCLS>>(result);

                for (int k = 0; k < l.Count(); k++)
                {
                    if (l.ElementAt(k).noemp == noemp)
                    {
                        Nombre = l.ElementAt(k).nombre;
                        Appaterno = l.ElementAt(k).appaterno;
                        Apmaterno = l.ElementAt(k).apmaterno;
                        noemp = l.ElementAt(k).noemp;
                        Puesto = l.ElementAt(k).puesto;
                        Area= l.ElementAt(k).area;

                    }
                }
                titulo.Text = titulo.Text + Nombre + " " + Appaterno + " " + Apmaterno;
                lbl_puesto.Text = Area;
               

            }
            catch(Exception e)
            {
                await DisplayAlert(null,e.Message,"ok");
                
            
            }
        }

        

        protected  override bool OnBackButtonPressed()
        {

            return true;
        }

        private async void btn_close_Clicked(object sender, EventArgs e)
        {
            var res = await DisplayActionSheet("Cerrar sesión", "No", "Si", "¿Desea cerrar sesión?");

            if (res == "Si")
                await Navigation.PopAsync();
            
            
        }

      
    }
}