using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Humanizer;
using SAVIVE.Clases;
using SAVIVE.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using SAVIVE.Validar_Internet;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaSubirSol : ContentPage
    {

        List<SolicitudCLS> solicitudes = new List<SolicitudCLS>();
        List<PersonaCLS> Nombres_empleados = new List<PersonaCLS>();
        readonly List<PaginaCrearSolicitud.Empleado> empleados;
        readonly List<PaginaViaticos.Viatico> viaticos;
        readonly List<PaginaPasajes.Pasaje> pasajes;

        public PaginaSubirSol(List<PaginaCrearSolicitud.Empleado> empleados, List<PaginaViaticos.Viatico> viaticos, List<PaginaPasajes.Pasaje> pasajes)
        {
            this.empleados = empleados;
            this.viaticos = viaticos;
            this.pasajes = pasajes;

            DateTime dia_hoy = DateTime.Today;
            InitializeComponent();
            barra_progreso4.ProgressTo(1, 1000, Easing.Linear);
            P_conexion.BindingContext = new VMComprobarConexion(Navigation);
            pk_autorizar.SelectedIndex = 1;
            DateTimeFormatInfo dtinfo = new CultureInfo("es-MX", false).DateTimeFormat;
            
            
            lbl_fecha.Text ="Heroica Puebla de Zaragoza a "+  dtinfo.GetDayName(dia_hoy.DayOfWeek) + "\r\n " + dia_hoy.Day.ToString() + " de "+ dtinfo.GetMonthName(dia_hoy.Month)+ " del "+ dia_hoy.Year.ToString();
            ent_nombre.Text = empleados.ElementAt(0).Nombre +" " + empleados.ElementAt(0).Appaterno + " " + empleados.ElementAt(0).Apmaterno;
            Colocar_datos(int.Parse(empleados.ElementAt(0).Numero));
            mostrar_autorizados();
            Obtener_solicitudes();
        }

        

        private int[] numero_letra(float numero)
        {
            int[] arr_num = new int[2];
            arr_num[0] = Convert.ToInt32( Math.Floor(numero));

            arr_num[1] = Convert.ToInt32((Math.Round((Decimal)numero % 1, 2, MidpointRounding.AwayFromZero)) * 100);
           
            return arr_num;
            
        }
        private async Task obtener_nombres()
        {
            List<PersonaCLS> l = await Generics.ListarDatos<PersonaCLS>("http://www.sumate.somee.com/api/persona");
            Nombres_empleados = l;
        }
        private async void Obtener_solicitudes()
        {
            //List<SolicitudCLS> l = await Generics.ListarDatos<SolicitudCLS>("http://edgardiazortiz-001-site1.atempurl.com/api/solicitud");
            //solicitudes = l;

            HttpClient cliente = new HttpClient();
            var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/solicitud");

            var result = await rpta.Content.ReadAsStringAsync();
            List<SolicitudCLS> l = JsonConvert.DeserializeObject<List<SolicitudCLS>>(result);
            solicitudes = l;

        }
        private int buscar_numero(int busc)
        {
            int ind = -1;
            for (int k = 0; k < Nombres_empleados.Count(); k++)
            {
                
                 if (Nombres_empleados.ElementAt(k).noemp == busc)
                    ind=k;
            }
            return ind;
        }
        private async void Colocar_datos(int num)
        {
            await obtener_nombres();
            int[] arr_num = new int[2];
            float total_sum = 0;
            int ind = buscar_numero(num);
            if (ind != -1)
            {
                ent_area.Text = Nombres_empleados.ElementAt(ind).area;
                ent_direccion.Text = Nombres_empleados.ElementAt(ind).direccion;
                total_sum = (Calcular_viaticos() + Calcular_pasajes());
                lbl_total.Text = total_sum.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
            }
            arr_num = numero_letra(total_sum);
            lbl_numero_letra.Text = arr_num[0].ToWords() + " pesos con " + arr_num[1].ToWords() + " centavos 00/100 M.N.";
        }
        private async void mostrar_autorizados()
        {
            
            List<AutorizarCLS> l = await Generics.ListarDatos<AutorizarCLS>("http://www.sumate.somee.com/api/autorizar");
            

            for (int i = 0; i < l.Count(); i++)
            {
                pk_autorizar.Items.Add(l.ElementAt(i).nombre + " " + l.ElementAt(i).appaterno + " " + l.ElementAt(i).apmaterno);
            }

        }
        private float Calcular_pasajes()
        {
            float total = 0;
            float aux = 0;
            int id_v = 0;
            pasajes.ForEach(k =>
            {
                if (k.ida_v == "Ida y regreso")
                    id_v = 2;
                else
                    id_v = 1;
                
                aux = float.Parse(k.Tarifa) * id_v;

                
                total += aux;

            });

            return total;
        }
        private float Calcular_viaticos()
        {
            float total = 0;
            float aux = 0;
            viaticos.ForEach(k => 
            {
                //aux = float.Parse( k.Tarifa) * float.Parse(k.n_dias);
                aux = float.Parse(k.Tarifa) * float.Parse(k.n_dias);
                
                total += aux;
                
            });
            return total;
        }

        bool ban_sol = true;
        bool ban_soli_e = true;
        bool ban_sol_p = true;
        bool ban_sol_v = true;

        private async Task Subir_solicitud(int idsol)
        {

            HttpClient cliente = new HttpClient();
            SolicitudCLS soli = new SolicitudCLS();

            soli.idsolicitud = idsol;
            soli.autorizacion = pk_autorizar.SelectedItem.ToString();
            soli.justificacion = edt_observaciones.Text;
            soli.fecha = DateTime.Now;
            soli.solicitante = int.Parse(empleados.ElementAt(0).Numero);
            soli.estado = 1;
            soli.total = (decimal)(Calcular_viaticos() + Calcular_pasajes());
            

            var cadena = JsonConvert.SerializeObject(soli);
            var body = new StringContent(cadena, Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("http://www.sumate.somee.com/api/solicitud", body);

            if (!response.IsSuccessStatusCode)
                ban_sol = false;
            else
            {
                int respuesta = int.Parse(await response.Content.ReadAsStringAsync());

                if (respuesta == 0)
                    ban_sol = false;   
            } 
        }


        
        private async Task Subir_sul_e(int idsol)
        {
            HttpClient cliente = new HttpClient();
            SolicitudEmpleadoCLS soliv = new SolicitudEmpleadoCLS();

            for (int k = 0; k < empleados.Count(); k++)
            {
                soliv.IdSolicitud = idsol;
                soliv.IdEmpleado= int.Parse(empleados.ElementAt(k).Numero);
                soliv.Justificacion = empleados.ElementAt(k).Justificaion;
                
                var cadena = JsonConvert.SerializeObject(soliv);
                var body = new StringContent(cadena, Encoding.UTF8, "application/json");
                var response = await cliente.PostAsync("http://www.sumate.somee.com/api/solicitudempleado", body);


                if (!response.IsSuccessStatusCode)
                    ban_soli_e = false;
                else
                {
                    int respuesta = int.Parse(await response.Content.ReadAsStringAsync());

                    if (respuesta == 0)
                        ban_soli_e = false;
                    
                }

            }

        }

        

        private async Task Subir_sol_p(int id_sol)
        {
            HttpClient cliente = new HttpClient();
            SolicitudPasajesCLS solip = new SolicitudPasajesCLS();

            for (int k = 0; k < pasajes.Count(); k++)
            {

                solip.IdSolicitud = id_sol;
                solip.IdDestino = pasajes.ElementAt(k).IdDestino;
                solip.Transporte= pasajes.ElementAt(k).Transporte;
                solip.Tarifa=decimal.Parse( pasajes.ElementAt(k).Tarifa);
                if (pasajes.ElementAt(k).ida_v == "Solo ida")
                {
                    solip.IdaVuelta = false;
                }
                else
                {
                    solip.IdaVuelta = true;
                }

                var cadena = JsonConvert.SerializeObject(solip);
                var body = new StringContent(cadena, Encoding.UTF8, "application/json");
                var response = await cliente.PostAsync("http://www.sumate.somee.com/api/Solicitudpasaje", body);


                if (!response.IsSuccessStatusCode) 
                    ban_sol_p = false;
                else
                {
                    int respuesta = int.Parse(await response.Content.ReadAsStringAsync());

                    if (respuesta == 0)
                        ban_sol_p = false;
                   
                }

            }

        }

        private async Task Subir_sol_v(int idsol)
        {
            HttpClient cliente = new HttpClient();
            SolicitudViaticosCLS soliv = new SolicitudViaticosCLS();



            for (int k = 0; k < viaticos.Count(); k++)
            {

               

                soliv.IdSolicitud = idsol;
                soliv.IdDestino = viaticos.ElementAt(k).Iddestino;
                soliv.Zona = "noreste";
                soliv.Costo = decimal.Parse( viaticos.ElementAt(k).Tarifa);
                soliv.Dias = int.Parse(viaticos.ElementAt(k).n_dias);

                var cadena = JsonConvert.SerializeObject(soliv);
                var body = new StringContent(cadena, Encoding.UTF8, "application/json");
                var response = await cliente.PostAsync("http://www.sumate.somee.com/api/SolicitudViatico", body);


                if (!response.IsSuccessStatusCode)
                {
                    
                    ban_sol_v = false; }
                else
                {
                    int respuesta = int.Parse(await response.Content.ReadAsStringAsync());

                    if (respuesta == 0)
                    {
                       
                        ban_sol_v = false; 
                    }

                }

            }

        }

        private async void btn_aceptar_Clicked(object sender, EventArgs e)
        {

            if (pk_autorizar.SelectedIndex != -1 && edt_observaciones.Text != null && edt_observaciones.Text != "")
            {
                int idsolicitud;
                if (solicitudes.Count() == 0)
                    idsolicitud = 1;
                else
                    idsolicitud = solicitudes.Last().idsolicitud + 1;

                ban_sol = true;
                ban_soli_e = true;
                ban_sol_p = true;
                ban_sol_v = true;

                await Subir_solicitud(idsolicitud);
                await Subir_sol_v(idsolicitud);
                await Subir_sul_e(idsolicitud);
                await Subir_sol_p(idsolicitud);

                if (ban_sol == true && ban_soli_e == true && ban_sol_p == true && ban_sol_v == true)
                {
                    //await Navigation.PushAsync(new PaginaSolicitudRealizada(idsolicitud,empleados,lbl_total.Text,lbl_numero_letra.Text));
                    Navigation.InsertPageBefore(new PaginaSolicitudRealizada(idsolicitud, empleados, lbl_total.Text, lbl_numero_letra.Text), Navigation.NavigationStack[2]);
                    Eliminar_paginas();
                }
                else
                {
                    Navigation.InsertPageBefore(new PaginaSolicitudNoRealizada(ban_sol, ban_soli_e, ban_sol_p, ban_sol_v), Navigation.NavigationStack[2]);
                    Eliminar_paginas();
                }       
            }
            else
            {
                await DisplayAlert(null, "faltan campos por llenar", "ok");
            }  
        }
        private void Eliminar_paginas()
        {
            Navigation.RemovePage(Navigation.NavigationStack[3]);
            Navigation.RemovePage(Navigation.NavigationStack[3]);
            Navigation.RemovePage(Navigation.NavigationStack[3]);
            Navigation.PopAsync();    
        }

    }

}