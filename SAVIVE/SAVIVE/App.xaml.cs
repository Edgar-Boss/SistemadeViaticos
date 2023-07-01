using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAVIVE.Views;


namespace SAVIVE
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            //MainPage= new NavigationPage(new Comprobar_conexion());
            //MainPage = new NavigationPage(new PaginaReportesUbicación());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
