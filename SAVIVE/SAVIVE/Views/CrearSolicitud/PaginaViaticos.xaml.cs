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



namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaViaticos : ContentPage
    {
        float total = 0;
        float aux_total = 0;
        List<DestinoCLS> destinos;
        float costo = 0;
        string destino = "";
        List<PaginaCrearSolicitud.Empleado> empleados;

        public struct Viatico
        {
            public int Iddestino;
            public string Destino;
            public string Tarifa;
            public string n_dias;
        }
        public List<Viatico> list_Viaticos = new List<Viatico>();


        public PaginaViaticos(List<PaginaCrearSolicitud.Empleado> empleados)
        {
            InitializeComponent();
            barra_progreso2.ProgressTo(1, 1000, Easing.Linear);
            obtener_datos();
            P_conexion.BindingContext = new VMComprobarConexion(Navigation);
            this.empleados = empleados;
        }

        

        private void btn_agregar_Clicked(object sender, EventArgs e)
        {
            if (buscar_repeticiones())
            {
                DisplayAlert("","destino ya agregado","ok");
            }
            else if (!validar_campos())
            {
                DisplayAlert("","Faltan datos por agregar","ok");
            }
            else
            {
                int id = buscar_id(destino);

                list_Viaticos.Add(new Viatico
                {
                    Iddestino = id,
                    Destino = destino,
                    Tarifa = costo.ToString(), //"C", CultureInfo.CreateSpecificCulture("es-MX")
                    n_dias = ent_ndias.Text,
                }); 

                total = aux_total;

                lbl_subtot.Text = total.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                lbl_tarifa.Text = "$0.00";
                lbl_subcon.Text = "$0.00";

                //ent_destino.Text = string.Empty;
                ent_tarhot.Text = string.Empty;
                ent_ndias.Text = string.Empty;
                BindingContext = new ViaticosViewModel(list_Viaticos);


                pk_destino.SelectedIndex = -1;
                ent_tarhot.Text = "";
                ent_otro.Text = "";
                ent_ndias.Text = "";
            }

        }

        private void entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Entry ent = sender as Entry;

            if ((ent.Text != "" && ent.Text != null) )
            {
                costo = float.Parse(ent.Text);
                if (ent_ndias.Text != "" && ent_ndias.Text != null)
                {
                    int dias = int.Parse(ent_ndias.Text);
                    float tarifa = costo;//float.Parse(ent_tarhot.Text);
                    float subtotcon = dias * tarifa;
                    aux_total = subtotcon + total;

                    lbl_tarifa.Text = tarifa.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    lbl_subcon.Text = subtotcon.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    lbl_subtot.Text = (total + subtotcon).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                }
                btn_agregar.IsEnabled = validar_campos();
            }
        }

        private void entrynd_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Entry ent = sender as Entry;
            
            if (ent.Text != "" && ent.Text != null)
            {
                int dias = int.Parse(ent_ndias.Text);
                float tarifa = costo;
                float subtotcon = dias * tarifa;
                aux_total = subtotcon + total;

                lbl_tarifa.Text = tarifa.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                lbl_subcon.Text = subtotcon.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                lbl_subtot.Text = (total + subtotcon).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
            }
            btn_agregar.IsEnabled = validar_campos();
        }

        public async void  obtener_datos()
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

                ent_tarhot.Text = destinos.ElementAt(pk.SelectedIndex).costo.ToString();
                destino = destinos.ElementAt(pk.SelectedIndex).destino;

                if (rb_tarifahotel.IsChecked)
                    costo = (float)destinos.ElementAt(pk.SelectedIndex).costo;
                
                btn_agregar.IsEnabled = validar_campos();
                actualizar_calculos();
            }
        }

        private int buscar_id(string destino)
        {
            int iddestino=0;
            destinos.ForEach(i =>
            {
                if (i.destino == destino)
                {
                    iddestino = i.iddestino;
                }
            });

            return iddestino;
        }

        private void rb_tarifahotel_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            ent_tarhot.IsEnabled = rb.IsChecked;
            
            if (rb.IsChecked)
            {
                if (ent_tarhot.Text != "" && ent_tarhot.Text != null)
                    costo = float.Parse(ent_tarhot.Text);
                else
                    costo = 0;
            }

            btn_agregar.IsEnabled = validar_campos();
            actualizar_calculos();

        }

        private void rb_otro_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            ent_otro.IsEnabled = rb.IsChecked;
            if (rb.IsChecked)
            {
                if (ent_otro.Text != "" && ent_otro.Text != null)
                    costo = float.Parse(ent_otro.Text);
                else
                    costo = 0;
            }
            btn_agregar.IsEnabled = validar_campos();
            actualizar_calculos();
        }

        public void actualizar_calculos()
        {

            if (ent_ndias.Text != "" && ent_ndias.Text != null)
            {
                int dias = int.Parse(ent_ndias.Text);
                float tarifa = costo;
                float subtotcon = dias * tarifa;
                aux_total = subtotcon + total;

                lbl_tarifa.Text = tarifa.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                lbl_subcon.Text = subtotcon.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                lbl_subtot.Text = (total + subtotcon).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
            }
        }

        public bool validar_campos()
        {
            bool validar= true;
            
            if (rb_otro.IsChecked && (ent_otro.Text == "" || ent_otro.Text == null))
            {
                validar = false;
            }
            else if (rb_tarifahotel.IsChecked && (ent_tarhot.Text == "" || ent_tarhot.Text == null))
            {
                validar = false;
            }
            else if (ent_ndias.Text == "" || ent_ndias.Text == null)
            {
                validar = false;
            }
            else if (pk_destino.SelectedIndex == -1)
            {
                validar = false;
            }

            return validar;
        }

        private void ent_otro_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            btn_agregar.IsEnabled = validar_campos();
        }

        private bool buscar_repeticiones()
        {
            for (int k = 0; k < list_Viaticos.Count; k++)
            {
                if (list_Viaticos.ElementAt(k).Destino == destino)
                {
                    return true;
                }
            }
            return false;
        }
        private void btn_aceptar_Clicked(object sender, EventArgs e)
        {
            if (list_Viaticos.Count == 0)
                DisplayAlert(null,"aun no ha agregado destinos","ok");
            else
                Navigation.PushAsync(new PaginaPasajes(empleados,list_Viaticos));
        }
    }
}