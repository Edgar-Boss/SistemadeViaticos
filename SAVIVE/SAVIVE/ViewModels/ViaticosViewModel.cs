using SAVIVE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


namespace SAVIVE.ViewModels
{
    public class ViaticosViewModel
    {
        public ObservableCollection<ViaticosModel> Viaticos { get; set; }

        public ViaticosViewModel(List<PaginaViaticos.Viatico> lista_viaticos)
        {
            Viaticos = new ObservableCollection<ViaticosModel>();

            
            for (int k = 0; k < lista_viaticos.Count; k++)
            {
                Viaticos.Add(new ViaticosModel
                {
                    Destino = lista_viaticos[k].Destino,
                    Tarifa = lista_viaticos[k].Tarifa,
                    No_dias ="Dias: " + lista_viaticos[k].n_dias,
                });
            }
        }
    }
}
