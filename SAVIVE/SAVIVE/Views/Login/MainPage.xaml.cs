using SAVIVE.Clases;
using SAVIVE.Generic;
using SAVIVE.Validar_Internet;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SAVIVE
{
    public partial class MainPage : ContentPage
    {


        

        public List<PersonaCLS> Nombres_empleados = new List<PersonaCLS>();
        int noemp = 0;
        public MainPage()
        {
            
            InitializeComponent();
            BindingContext = new VMComprobarConexion(Navigation);
            obtener_nombres();

        }

        private async void obtener_nombres()
        {
            try
            {
                val.IsVisible= true;
                List<PersonaCLS> l = await Generics.ListarDatos<PersonaCLS>("http://www.sumate.somee.com/api/persona");
                Nombres_empleados = l;

                val.IsVisible = false;
            }
            catch (Exception ex)
            {
                obtener_nombres();
                
            }
        }
        async private void btn_ingresar_Clicked(object sender, EventArgs e)
        {

            

            if (validar_datos() && noemp != 0)
            {

                await Navigation.PushAsync(new PaginaMenu(noemp));
            }
            else
                await DisplayAlert("", "usuario y/o contraseña invalidos", "ok");
        }

        private bool validar_datos()
        {

            try
            {
                int id = -1;
                if (ent_user.Text != null && ent_user.Text != "" && ent_pass.Text != null && ent_pass.Text != "")
                {
                    string user = ent_user.Text;

                    for (int k = 0; k < Nombres_empleados.Count(); k++)
                    {
                        if (Nombres_empleados.ElementAt(k).correo == user)
                        {

                            id = k;
                            break;
                        }
                    }

                    if (id > -1)
                    {
                        if (Nombres_empleados.ElementAt(id).pass == ent_pass.Text)
                        {
                            noemp = Nombres_empleados.ElementAt(id).noemp;
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                DisplayAlert("",ex.Message.ToString(),"ok");
                return false;
            }

        }

      
    }
}
