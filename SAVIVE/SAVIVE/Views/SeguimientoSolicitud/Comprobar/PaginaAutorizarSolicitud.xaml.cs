using SAVIVE.Clases;
using SAVIVE.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaAutorizarSolicitud : ContentPage
    {
        public PaginaAutorizarSolicitud()
        {
            InitializeComponent();
            mostrar_autorizados();
        }

        private async void mostrar_autorizados()
        {
            List<AutorizarCLS> l = await Generics.ListarDatos<AutorizarCLS>("http://www.sumate.somee.com/api/autorizar");

            for (int i = 0; i < l.Count(); i++)
            {
                pk_autorizar.Items.Add(l.ElementAt(i).nombre + " " + l.ElementAt(i).appaterno + " " + l.ElementAt(i).apmaterno);
            }
        }
    }
}