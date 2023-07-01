using Newtonsoft.Json;
using SAVIVE.Clases;
using SAVIVE.Generic;
using SAVIVE.Models;
using SAVIVE.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVIVE.SeguimientoSolicitud
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaDatosSolicitud : TabbedPage
    {
        public List<SolicitudCLS> solicitudes = new List<SolicitudCLS>();
        public List<SolicitudEmpleadoCLS> sol_emp= new List<SolicitudEmpleadoCLS>();
        public List<PersonaCLS> Nombres_empleados = new List<PersonaCLS>();
        public List<DEmpleadosCLS> Detalles_empelados = new List<DEmpleadosCLS>();
        public List<DPasajaeCLS> Detalles_pasajes = new List<DPasajaeCLS>();
        public List<DViaticoCLS> Viaticos= new List<DViaticoCLS>();


        List<DViaticoCLS> lista_viaticos = new List<DViaticoCLS>();
        List<DEmpleadosCLS> lista_empleados = new List<DEmpleadosCLS>();
        List<DPasajaeCLS> lista_pasajes = new List<DPasajaeCLS>();
        
        SolicitudCLS solicitud;

        public PaginaDatosSolicitud(SolicitudCLS solicitud)
        {
            this.solicitud = solicitud;
            InitializeComponent();
            Mostrar_todo();
           
            
        }

        private async Task Obtener_sol_pas()
        {
            HttpClient cliente = new HttpClient();
            var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/dpasajes");
            var result = await rpta.Content.ReadAsStringAsync();
            List<DPasajaeCLS> l = JsonConvert.DeserializeObject<List<DPasajaeCLS>>(result);
            Detalles_pasajes = l;
        }

       

        private async Task Obtener_sol_empl()
        {
            HttpClient cliente = new HttpClient();
            var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/DEmpleado");
            var result = await rpta.Content.ReadAsStringAsync();
            List<DEmpleadosCLS> l = JsonConvert.DeserializeObject<List<DEmpleadosCLS>>(result);
            Detalles_empelados = l;


            
        }

        private async Task Obtener_sol_dest()
        {
            HttpClient cliente = new HttpClient();
            var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/DViaticos");
            var result = await rpta.Content.ReadAsStringAsync();
            List<DViaticoCLS> l = JsonConvert.DeserializeObject<List<DViaticoCLS>>(result);

            Viaticos = l;

        }
        private async Task  Mostrar_pasajes()
        {
            await Obtener_sol_pas();
            Detalles_pasajes.ForEach(i =>
            {
                if (i.Idsolicitud == solicitud.idsolicitud)
                {
                    lista_pasajes.Add(new DPasajaeCLS
                    { 
                        Idsolicitud= i.Idsolicitud,
                        Iddestino=i.Iddestino,
                        Transporte=i.Transporte,
                        Tarifa=i.Tarifa,
                        Idavuelta=i.Idavuelta,
                        Destino=i.Destino

                    });
                }

            });
        }

        private async Task Mostarr_viaticos()
        {
            await Obtener_sol_dest();

            Viaticos.ForEach(i =>
            {
                if (i.Idsolicitud == solicitud.idsolicitud)
                {
                    lista_viaticos.Add(new DViaticoCLS
                    {
                        Idsolicitud = i.Idsolicitud,
                        Iddestino = i.Iddestino,
                        Zona = i.Zona,
                        Costo = i.Costo,
                        Dias = i.Dias,
                        Destino = i.Destino
                    });
                }
            });
           

        }

        private async Task Mostrar_datos()
        {
            await Obtener_sol_empl();

           

            Detalles_empelados.ForEach(i =>
            {
                if (i.idsolicitud == solicitud.idsolicitud)
                {
                    lista_empleados.Add(new DEmpleadosCLS 
                    {
                        idsolicitud = i.idsolicitud,
                        Autorizacion = i.Autorizacion,
                        Justifiacion = i.Justifiacion,
                        Fecha = i.Fecha,
                        Solicitante = i.Solicitante,
                        Estado = i.Estado,
                        Idempleado = i.Idempleado,
                        DJustificacion = i.DJustificacion,
                        Nombre = i.Nombre + " " + i.Paterno + " " + i.Materno,
                        Paterno = i.Paterno,
                        Materno = i.Materno,
                        Puesto = i.Puesto,
                        Area = i.Area
                    });
                }
            });
            BindingContext = new DSolicitudViewModel(lista_empleados);

        }

        private async void Mostrar_todo()
        {
            await Mostarr_viaticos();
            await Mostrar_datos();
            await Mostrar_pasajes();
            Mostrar_detalles();
            cv_pasajes.BindingContext = new DPasajeViewModel(lista_pasajes);
            cv_viaticos.BindingContext = new DViaticosViewModel(lista_viaticos);
            cv_empleados.BindingContext = new DSolicitudViewModel(lista_empleados);

        }

        private void Mostrar_detalles()
        {
            lbl_no_s.Text = solicitud.idsolicitud.ToString();
            ent_nombre.Text = lista_empleados.ElementAt(0).Nombre;
            ent_area.Text = lista_empleados.ElementAt(0).Area;
            lbl_fecha.Text = solicitud.fecha.ToString();
            ent_autorizar.Text = solicitud.autorizacion;
            edt_observaciones.Text = solicitud.justificacion;

            float total =0;
            lista_pasajes.ForEach(i =>
            {
                int i_v = 1;
                if (i.Idavuelta == true)
                    i_v = 2;

                total += (float)i.Tarifa * i_v;
            });

            lista_viaticos.ForEach(i =>
            {
                total += (float)i.Costo * i.Dias;
            });

            lbl_total.Text = total.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
        }
        
    }
}





