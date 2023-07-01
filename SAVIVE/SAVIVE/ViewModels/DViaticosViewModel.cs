using SAVIVE.Clases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SAVIVE.ViewModels
{
    public class DViaticosViewModel
    {
        public ObservableCollection<DViaticoCLS> DViaticos { get; set; }

        public DViaticosViewModel(List<DViaticoCLS> _DViaticos)
        {
            DViaticos = new ObservableCollection<DViaticoCLS>();


            _DViaticos.ForEach(i =>
            {
                DViaticos.Add(new DViaticoCLS
                {
                    Idsolicitud = i.Idsolicitud,
                    Iddestino = i.Iddestino,
                    Zona = i.Zona,
                    Costo = i.Costo,
                    Dias= i.Dias,
                    Destino= i.Destino

                });
            });

            
        }


    }
}
