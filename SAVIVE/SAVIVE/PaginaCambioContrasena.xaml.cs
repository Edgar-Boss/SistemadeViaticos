using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaCambioContrasena : ContentPage
    {
        public PaginaCambioContrasena()
        {
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("4B8673"); //cambio de color de barra de navegacion
            InitializeComponent();
            //NavigationPage.SetIconColor(this, Color.FromHex("7FB77E"));
        }



        


        async private void btn_aceptar_Clicked(object sender, EventArgs e)
        {
            if (revisar_contrasenas())
            {

                var answer = await DisplayAlert("", "¿Seguro que desea cambiar su cuntraseña?", "Yes", "No");

                if (answer)
                {
                    await Navigation.PushAsync(new PaginaCambioRealizado());
                }
                else
                {
                    await Navigation.PushAsync(new PaginaCambioNoRealizado());
                }
                //DisplayActionSheet("¿Desea cambiar la contaseña?","No","Si");
                //Navigation.PushAsync(new PaginaCambioRealizado());
            }
            else 
            {
                await DisplayAlert("", lbl_conf_pass.Text + " : " + lbl_new_pass.Text , "ok");
            }
        }
        private bool revisar_contrasenas()
        {
            return ent_new_pass.Text == ent_conf_pass.Text;
        }
    }
}