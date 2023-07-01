using SAVIVE.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SAVIVE.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAVIVE.Models;
using SAVIVE.ViewModels;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaHospedajeMT : ContentPage
    {
        SolicitudCLS solicitud;
        public PaginaHospedajeMT(SolicitudCLS solicitud)
        {
            this.solicitud = solicitud;
            InitializeComponent();
            Obtener_solicitudes();  
        }

        List<DViaticoCLS> lista_viaticos;
        List<DPasajaeCLS> lista_pasajes;

        private void btn_aceptar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaginaAutorizarSolicitud());
        }

        
        private async void Obtener_solicitudes()
        {
            List<DViaticoCLS> l = await Generics.ListarDatos<DViaticoCLS>("http://www.sumate.somee.com/api/DViaticos");
            lista_viaticos = l;
            List<DPasajaeCLS> ll = await Generics.ListarDatos<DPasajaeCLS>("http://www.sumate.somee.com/api/dpasajes");
            lista_pasajes = ll;
            AgregarListaObservable();
        }

        private void AgregarListaObservable()
        {
            cv_viaticos.BindingContext = new DViaticosViewModel(FiltrarDatos_viaticos());
            cv_pasajes.BindingContext = new DPasajeViewModel(FiltrarDatos_pasaje());
            
        }
        private List<DPasajaeCLS> FiltrarDatos_pasaje()
        {
            

            List<DPasajaeCLS> lista_ = new List<DPasajaeCLS>();
            lista_pasajes.ForEach(i=>
            {
                if (i.Idsolicitud == solicitud.idsolicitud)
                {
                    lista_.Add(i);
                }
            });

            return lista_;  
        }
        private List<DViaticoCLS> FiltrarDatos_viaticos()
        {
            List<DViaticoCLS> lista = new List<DViaticoCLS>();

            lista_viaticos.ForEach(i =>
            {
                if (i.Idsolicitud == solicitud.idsolicitud)
                {
                    lista.Add(i);
                }
            });

            return lista;
        }

        private void Hospedaje_Tapped(object sender, EventArgs e)
        {
            Frame index = (Frame)sender;
            Navigation.PushAsync(new PaginaComprobarViaticos(solicitud,index.TabIndex,"Viaticos"));
           
        }

        private void Pasajes_Tapped(object sender, EventArgs e)
        {
            Frame index = (Frame)sender;
            Navigation.PushAsync(new PaginaComprobarViaticos(solicitud,index.TabIndex,"Pasajes"));
        }
    }
}