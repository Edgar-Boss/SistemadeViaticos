using Newtonsoft.Json;
using SAVIVE.Clases;
using SAVIVE.Validar_Internet;
using SAVIVE.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SAVIVE.PaginaViaticos;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPasajes : ContentPage
    {
        string transporte = "Autobus";
        string ida_v = "Solo ida";
        int ida_ = 1;
        float total = 0;
        float aux_tot = 0;
  
        List<DestinoCLS> destinos;
        string destino = "";
        int id_destino ;
        float costo = 0;

        List<PaginaCrearSolicitud.Empleado> empleados;
        List<PaginaViaticos.Viatico> viaticos;
        public struct Pasaje
        {
            public int IdDestino; 
            public string Destino;
            public string Transporte;
            public string Tarifa;
            public string ida_v;
        }

        public List<Pasaje> list_Pasajes = new List<Pasaje>();
        public PaginaPasajes(List<PaginaCrearSolicitud.Empleado> empleados, List<PaginaViaticos.Viatico> viaticos)
        {
          
            InitializeComponent();
            obtener_datos();
            barra_progreso3.ProgressTo(1, 1000, Easing.Linear);
            P_conexion.BindingContext = new VMComprobarConexion(Navigation);
            this.empleados = empleados;
            this.viaticos = viaticos;
        }
        private void btn_agregar_Clicked(object sender, EventArgs e)
        {
            if (buscar_rep())
            {
                DisplayAlert("", "Destino ya agregado", "ok");
            }
            else
            {
                if (pk_destino.SelectedIndex == -1 || ent_tarifa.Text == null || ent_tarifa.Text == "")
                {
                    DisplayAlert("", "campo vacio", "ok");
                }
                else
                {
                    float aux = float.Parse(ent_tarifa.Text);

                    list_Pasajes.Add(new Pasaje
                    {
                        IdDestino = id_destino,
                        Destino = pk_destino.SelectedItem.ToString(),
                        Transporte = transporte,
                        Tarifa = aux.ToString(),
                        ida_v = ida_v
                    });
                    total = aux_tot;

                    aux = float.Parse(ent_tarifa.Text);
                    float sub_tot = aux * ida_;

                    lbl_monto.Text = ent_tarifa.Text;
                    lbl_subtot_conc.Text = sub_tot.ToString();

                    //ent_Desino.Text = String.Empty;
                    ent_tarifa.Text = String.Empty;


                    lbl_monto.Text = "$0.00";
                    lbl_subtot_conc.Text = "$0.00";
                    lbl_subtot.Text = total.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    pk_destino.SelectedIndex = -1;
                    BindingContext = new PasajeViewModel(list_Pasajes);
                }
            }
        }
        private void Rdb_checked(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.IsChecked)
                transporte = radioButton.Content.ToString();
            
        }
        private void Rdb_checked_ida(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.IsChecked)
            {
                ida_v = radioButton.Content.ToString();
                ida_ = int.Parse( radioButton.StyleId);
            }

            if (ent_tarifa.Text != "" && ent_tarifa.Text != null)
                calcular_gastos(float.Parse(ent_tarifa.Text), ida_);
        }
        private void ent_tarifa_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            if (entry.Text != "" && entry.Text != null) 
                calcular_gastos(float.Parse(entry.Text), ida_);
        }
        private void calcular_gastos(float tarifa, int ida)
        {
            float subcon = tarifa * ida;
            aux_tot = total + subcon;
            lbl_subtot_conc.Text = subcon.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
            lbl_monto.Text = tarifa.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
            lbl_subtot.Text = aux_tot.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
        }
        public async void obtener_datos()
        {
            HttpClient cliente = new HttpClient();
            var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/destino");

            var result = await rpta.Content.ReadAsStringAsync();
            List<DestinoCLS> l = JsonConvert.DeserializeObject<List<DestinoCLS>>(result);
            destinos = l;
            for (int i = 0; i < l.Count(); i++)
            {
                pk_destino.Items.Add(l.ElementAt(i).destino);
            }
        }
        private void pk_destino_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker pk = (Picker)sender;
            if (pk.SelectedIndex != -1)
            {

                ent_tarifa.Text = destinos.ElementAt(pk.SelectedIndex).autobus.ToString();
                destino = destinos.ElementAt(pk.SelectedIndex).destino;
                id_destino = destinos.ElementAt(pk.SelectedIndex).iddestino;

                if (rdb_autobus.IsChecked)
                    costo = (float)destinos.ElementAt(pk.SelectedIndex).autobus;
  
            }
        }
        private bool buscar_rep()
        {
            for (int k = 0; k < list_Pasajes.Count(); k++)
            {
                if (list_Pasajes.ElementAt(k).Destino == destino)
                    return true;
            }
            return false;
        }
        private void btn_aceptar_Clicked(object sender, EventArgs e)
        {

            if (list_Pasajes.Count() == 0)
                DisplayAlert(null, "Aun no se han agregado pasajes", "ok");
            else
              Navigation.PushAsync(new PaginaSubirSol(empleados,viaticos,list_Pasajes));

        }
    }
}