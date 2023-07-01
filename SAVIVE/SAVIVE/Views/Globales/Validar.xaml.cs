using SAVIVE.Validar_Internet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAVIVE.ViewModels;
using System.ComponentModel;

namespace SAVIVE.Views.Globales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Validar : ContentView
    {
        public Validar()
        {
            InitializeComponent();
            //BindingContext = new VMComprobarConexion(Navigation);
            //var x = new VMAnimaciones(Navigation);
            //x.RotarInfinito();
            //BindingContext = x;
            //Frame.BindingContext = x;

            
            
        }

    }
}