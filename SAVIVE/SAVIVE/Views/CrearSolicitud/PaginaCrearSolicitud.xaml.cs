using Android.Content;
using Java.Lang;
using Newtonsoft.Json;
using SAVIVE.Clases;
using SAVIVE.Validar_Internet;
using SAVIVE.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaCrearSolicitud : ContentPage
    {
        string nombre = "";
        int numero_emp = -1;
        string puesto = "";
        string paterno = "";
        string materno = "";
 
       public struct Empleado
       {
            public string Nombre;
            public string Appaterno;
            public string Apmaterno;
            public string Puestos;
            public string Justificaion;
            public string Numero;
       }

        public List<Empleado> list_Empleados = new List<Empleado>();
        public List<PersonaCLS> Nombres_empleados = new List<PersonaCLS>();
        public PaginaCrearSolicitud(int noemp,string Nombre,string Appaterno,string Apmaterno,string Puesto)
        {
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("#4B8673"); //cambio de color de barra de navegacion  3A89C9
            InitializeComponent();
            P_conexion.BindingContext = new VMComprobarConexion(Navigation);
            obtener_nombres();
            barra_progreso1.ProgressTo(1,1000,Easing.Linear);
            agregar_comisionado(noemp,Nombre,Appaterno,Apmaterno,Puesto, " ");
 
        }

        public void agregar_comisionado(int no_emp_,string nomb,string paterno_,string materno_, string puesto_, string justif)
        {
            if (!buscar_numero(no_emp_.ToString()))
            {
                list_Empleados.Add(new Empleado
                {
                    Nombre = nomb,
                    Appaterno = paterno_,
                    Apmaterno = materno_,
                    Justificaion = justif,
                    Numero = no_emp_.ToString(),
                    Puestos = puesto_
                });

                BindingContext = new ComisionadoViewModel(list_Empleados);//muesta en pantalla los elemntos 
                ent_buscar.Text = System.String.Empty;
                ent_justificacion.Text = System.String.Empty;
                pk_nombres.SelectedIndex = -1;

            }
            else
                DisplayAlert("", "Usuario ya agregado", "ok");

        }
        private void btn_agregar_Clicked(object sender, EventArgs e)
        {
            if (pk_nombres.SelectedIndex != -1 && ent_justificacion.Text != "" && ent_justificacion.Text != null)
                agregar_comisionado(numero_emp,nombre,paterno,materno,puesto,ent_justificacion.Text);
            else
                DisplayAlert("", "faltan campos por llenar", "ok");
        }
        private async void obtener_nombres()
        {
            HttpClient cliente = new HttpClient();
            var rpta = await cliente.GetAsync("http://www.sumate.somee.com/api/persona");

            var result = await rpta.Content.ReadAsStringAsync();
            List<PersonaCLS> l = JsonConvert.DeserializeObject<List<PersonaCLS>>(result);

            Nombres_empleados = l;

            for (int k = 0; k < Nombres_empleados.Count(); k++)
            {
                pk_nombres.Items.Add(Nombres_empleados.ElementAt(k).nombre + " " + Nombres_empleados.ElementAt(k).appaterno + " " + Nombres_empleados.ElementAt(k).apmaterno + " ");
            }
            
        }
        private void pk_nombres_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker obj = (Picker)sender;
            if (obj.SelectedIndex > -1)
            {
                nombre = Nombres_empleados.ElementAt(obj.SelectedIndex).nombre;
                paterno = Nombres_empleados.ElementAt(obj.SelectedIndex).appaterno;
                materno = Nombres_empleados[obj.SelectedIndex].apmaterno;
                numero_emp = Nombres_empleados.ElementAt(obj.SelectedIndex).noemp;
                puesto = Nombres_empleados.ElementAt(obj.SelectedIndex).puesto;

            } 
        }
        private bool buscar_numero(string busc)
        {
            bool exist = false;
            for (int k = 0; k < list_Empleados.Count(); k++)
            {
                if (list_Empleados.ElementAt(k).Numero == busc)
                    exist = true;
            }
            return exist;
        }
        private void btn_buscar_Clicked(object sender, EventArgs e)
        {

            string busc = ent_buscar.Text;
            int enc = -1; 
            for (int k = 0; k < Nombres_empleados.Count(); k++)
            {
                if (Nombres_empleados.ElementAt(k).noemp.ToString() == busc)
                    enc = k;
            }

            if (enc > -1)
            {
                pk_nombres.SelectedIndex = enc;
                numero_emp = Nombres_empleados.ElementAt(enc).noemp;
                nombre = Nombres_empleados.ElementAt(enc).nombre;
                
            }
            else
            {
                DisplayAlert("","No se encunetra el numero buscado","ok");
            }

        }
        private void btn_editar_Clicked(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            if (btn.ClassId == "0")
                DisplayAlert("", "este usuario no se puede eliminar", "ok");
            else
            {
                list_Empleados.RemoveAt(int.Parse(btn.ClassId));
                BindingContext = new ComisionadoViewModel(list_Empleados);
            }
        }
        private void btn_aceptar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaginaViaticos(list_Empleados));
        }
    }

}