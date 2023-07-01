using SAVIVE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


namespace SAVIVE.ViewModels
{

    public class PasajeViewModel
    {
        public ObservableCollection<PasajeModel> Pasaje { get; set; }

        public PasajeViewModel(List<PaginaPasajes.Pasaje> Pasajes)
        {
            Pasaje = new ObservableCollection<PasajeModel>();

            for (int k = 0; k < Pasajes.Count; k++)
            {
                Pasaje.Add(new PasajeModel
                {
                    Destino = Pasajes[k].Destino,
                    Transporte = Pasajes[k].Transporte,
                    Tarifa = Pasajes[k].Tarifa,
                    Ida_v = Pasajes[k].ida_v,
                });
            }

        }
    }
}
