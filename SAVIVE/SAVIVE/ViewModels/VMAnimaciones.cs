using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SAVIVE.ViewModels;

namespace SAVIVE.ViewModels
{
    public class VMAnimaciones:BaseViewModel
    {
        #region VARIABLES
        bool _estadoRotar = true;
        #endregion
        #region CONSTRUCTOR
        public VMAnimaciones(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region OBJETOS
        public bool EstadoRotar
        {
            get { return _estadoRotar; }
            set { SetValue(ref _estadoRotar, value); }
        }
        #endregion
        #region PROCESOS
        public void RotarInfinito()
        { 
            EstadoRotar= true; 
        }
        //public void DetenerRotar()
        //{
        //    EstadoRotar= false;
        //}
        #endregion
        #region COMANDOS
        public ICommand RotarInfinitocommand => new Command(RotarInfinito);
        //public ICommand Detenercommand => new Command(DetenerRotar);
        #endregion

    }
}
