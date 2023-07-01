using SAVIVE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SAVIVE.ViewModels
{
    public  class DPasajeViewModel
    {
        public ObservableCollection<DPasajaeCLS> DPasajes { get; set; }

        public DPasajeViewModel(List<DPasajaeCLS> _dpasajes) 
        {
            DPasajes = new ObservableCollection<DPasajaeCLS>();

            _dpasajes.ForEach(i =>
            {
                string iv = "Sólo ida";
                if (i.Idavuelta == true)
                    iv = "ida y vuelta";

                DPasajes.Add(new DPasajaeCLS
                {
                    Idsolicitud = i.Idsolicitud,
                    Iddestino = i.Iddestino,
                    Transporte = i.Transporte,
                    Tarifa = i.Tarifa,
                    Idavuelta = i.Idavuelta,
                    Destino = i.Destino,
                    _Ida = iv,
                });
                
            });
        }
    }
}
