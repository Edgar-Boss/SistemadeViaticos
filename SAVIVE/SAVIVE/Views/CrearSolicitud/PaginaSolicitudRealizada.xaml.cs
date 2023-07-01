using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaSolicitudRealizada : ContentPage
    {
        public PaginaSolicitudRealizada(int n_s, List<PaginaCrearSolicitud.Empleado> empleados,string total,string num_letra)
        {

            
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);


            lbl_total.Text = total;
            lbl_nombre.Text = empleados.ElementAt(0).Nombre + " " + empleados.ElementAt(0).Appaterno + " " + empleados.ElementAt(0).Apmaterno;
            lbl_id_sol.Text = n_s.ToString();


            DateTime dia_hoy = DateTime.Today;
            DateTimeFormatInfo dtinfo = new CultureInfo("es-MX", false).DateTimeFormat;
            lbl_fecha.Text = "Heroica Puebla de Zaragoza a " + dtinfo.GetDayName(dia_hoy.DayOfWeek) + "\r\n " + dia_hoy.Day.ToString() + " de " + dtinfo.GetMonthName(dia_hoy.Month) + " del " + dia_hoy.Year.ToString();

            lbl_num_letra.Text = num_letra;

          

        }

        

        private void btn_aceptar_Clicked(object sender, EventArgs e)
        {
            
            Navigation.PopAsync();

        }

        private void btn_aceptarR_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("",Navigation.NavigationStack.Count().ToString(),"ok");
        }
    }
}