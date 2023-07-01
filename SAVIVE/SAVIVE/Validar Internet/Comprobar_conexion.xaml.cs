using SAVIVE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAVIVE.Validar_Internet;

namespace SAVIVE
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Comprobar_conexion : ContentPage
	{
		public Comprobar_conexion ()
		{
			InitializeComponent ();
			BindingContext = new VMComprobarConexion(Navigation);
			
			
			
		}

       
    }
}