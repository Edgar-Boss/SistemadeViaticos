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
    public partial class PaginaSolicitudNoRealizada : ContentPage
    {
        public PaginaSolicitudNoRealizada(bool s_, bool s_e, bool s_p, bool s_v)
        {
            
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            DateTime dia_hoy = DateTime.Today;
            DateTimeFormatInfo dtinfo = new CultureInfo("es-MX", false).DateTimeFormat;
            lbl_fecha.Text = "Heroica Puebla de Zaragoza a " + dtinfo.GetDayName(dia_hoy.DayOfWeek) + "\r\n " + dia_hoy.Day.ToString() + " de " + dtinfo.GetMonthName(dia_hoy.Month) + " del " + dia_hoy.Year.ToString();

            if (!s_)
            {
                edt_error.Text += "Error al cargar solicitud \n";
            }
            if (!s_e)
            {
                edt_error.Text += "Error al cargar empleados \n";
            }
            if (!s_p)
            {
                edt_error.Text += "Error al cargar pasajes \n";
            }
            if (!s_v)
            {
                edt_error.Text += "Error al cargar viaticos \n";
            }
           


        }

        private void Eliminar_paginas()
        {
            var mainPage = (Application.Current.MainPage as NavigationPage);

            mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack[2]);
            mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack[2]);
            mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack[2]);
            mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack[2]);

        }

        private void btn_aceptar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}