using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using SAVIVE.Generic;

namespace SAVIVE.Models
{
    public class CategoriaModel : BaseBinding
    {
        //private bool _IsLoading;

        private ImageSource _Imagen;

        public ImageSource Imagen
        {
            get { return _Imagen; }
            set { SetValue(ref _Imagen, value); }
        }

    }
}
