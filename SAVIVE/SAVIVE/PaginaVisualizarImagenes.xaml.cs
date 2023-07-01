using Plugin.XamarinFormsSaveOpenPDFPackage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaVisualizarImagenes : ContentPage
    {
        public  PaginaVisualizarImagenes(string img)
        {
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("4B8673"); //cambio de color de barra de navegacion
            InitializeComponent();

            mostrar_img(img);

        }
        private void mostrar_img(string img)
        {

            //var stream = await img.OpenReadAsync();

            //ImageToDisplay.Source = ImageSource.FromStream(() => stream);
            ImageToDisplay.Source = ImageSource.FromFile(img);
        }


    }
}