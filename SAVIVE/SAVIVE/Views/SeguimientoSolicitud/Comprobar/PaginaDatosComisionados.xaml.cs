using Humanizer;
using Microcharts;
using Microcharts.Forms;
using Newtonsoft.Json;
using SAVIVE.Clases;
using SAVIVE.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SAVIVE.PaginaViaticos;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaDatosComisionados : ContentPage
    {


        List<DEmpleadosCLS> Lista_Empelados = new List<DEmpleadosCLS>();
        List<DPasajaeCLS> Lista_Pasajes = new List<DPasajaeCLS>();
        List<DViaticoCLS> Lista_Viaticos = new List<DViaticoCLS>();

        List<DEmpleadosCLS> Empleados = new List<DEmpleadosCLS>();
        List<DPasajaeCLS> Pasajes = new List<DPasajaeCLS>();
        List<DViaticoCLS> Viaticos = new List<DViaticoCLS>();

        SolicitudCLS solicitud;
       
        public PaginaDatosComisionados(SolicitudCLS solicitud)
        {
            InitializeComponent();
            obtener_datos(solicitud.idsolicitud);
            BindingContext = solicitud;
            this.solicitud = solicitud;
            
        }
        private async void obtener_datos(int id)
        {
            await Obtener_sol_dest();
            await Obtener_sol_empl();
            await Obtener_sol_pas();
            filtrar_datos(id);
            Mostrar_grafica();
  
        }
        private void Mostrar_grafica()
        {
            
            decimal comprobado = solicitud.comprobado;
            decimal comprobado_porc = (comprobado * 100)/solicitud.total;
            float tot_poc = (float)(100 - comprobado_porc);
            var entries = new[]
            {
                new ChartEntry(tot_poc)
                {
                    Color = SKColor.Parse("#19AEF9"),
                },
                new ChartEntry((float)comprobado_porc)
                {
                    Color = SKColor.Parse("#A8F4B4"),
                },
            };

            var chart = new Microcharts.DonutChart
            {
                Entries = entries,
                HoleRadius = 0.8f
            };
            var chartview = new ChartView { Chart = chart };
            chart_.Chart = chartview.Chart;
            chart_.Chart.BackgroundColor = SKColor.Parse("#00FFFFFF");
            lbl_total.Text = solicitud.total.ToString();
            lbl_comprobado.Text = solicitud.comprobado.ToString();
        }
        private void filtrar_datos(int id)
        {
            Lista_Viaticos.ForEach(i =>
            {
                if (i.Idsolicitud == id)
                { 
                    Viaticos.Add(i);
                    
                }
            });
            Lista_Pasajes.ForEach(i =>
            {
                if (id == i.Idsolicitud)
                { 
                    Pasajes.Add(i);
                }
            });
            Lista_Empelados.ForEach(i =>
            {
                if (id == i.idsolicitud)
                {
                    Empleados.Add(i);
                }
            });


            Empleados.ElementAt(0).Nombre += " " + Empleados.ElementAt(0).Paterno + " " + Empleados.ElementAt(0).Materno;
            ent_nombre.BindingContext = Empleados.ElementAt(0);
            lbl_just.BindingContext = solicitud;
            lbl_puesto.BindingContext = Empleados.ElementAt(0);
        }
        private async Task Obtener_sol_pas()
        {
            HttpClient cliente = new HttpClient();
            var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/dpasajes");
            var result = await rpta.Content.ReadAsStringAsync();
            List<DPasajaeCLS> l = JsonConvert.DeserializeObject<List<DPasajaeCLS>>(result);
            Lista_Pasajes = l;
        }
        private async Task Obtener_sol_empl()
        {
            HttpClient cliente = new HttpClient();
            var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/DEmpleado");
            var result = await rpta.Content.ReadAsStringAsync();
            List<DEmpleadosCLS> l = JsonConvert.DeserializeObject<List<DEmpleadosCLS>>(result);
            Lista_Empelados = l;
        }
        private async Task Obtener_sol_dest()
        {
            HttpClient cliente = new HttpClient();
            var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/DViaticos");
            var result = await rpta.Content.ReadAsStringAsync();
            List<DViaticoCLS> l = JsonConvert.DeserializeObject<List<DViaticoCLS>>(result);
            Lista_Viaticos = l;
        }
        private void btn_aceptar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaginaHospedajeMT(solicitud));
        }

    }
}