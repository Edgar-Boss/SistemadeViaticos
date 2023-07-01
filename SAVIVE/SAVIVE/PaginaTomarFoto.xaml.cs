using Plugin.Media;
using Plugin.Media.Abstractions;
using SAVIVE.Models;
using SAVIVE.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaTomarFoto : ContentPage
    {
       
        public PaginaTomarFoto()
        {
            InitializeComponent();
            BindingContext=new FotoViewModel();   
        }

    }
}