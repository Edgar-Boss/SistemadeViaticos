using Java.Util.Functions;
using SAVIVE.Clases;
using SAVIVE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static SAVIVE.PaginaCrearSolicitud;

namespace SAVIVE.ViewModels
{
    public class DUbicacionVIewModel
    {
        public ObservableCollection<UbicacionCLS> ubicaciones { get; set; }

        public DUbicacionVIewModel(List<UbicacionCLS> _ubicaciones)
        {
            ubicaciones = new ObservableCollection<UbicacionCLS>();

            _ubicaciones.ForEach(i =>
            {
                ubicaciones.Add(new UbicacionCLS
                {
                    Idubicacion = i.Idubicacion,
                    IdSolicitud = i.IdSolicitud,
                    IdEmpleado = i.IdEmpleado,
                    Fecha = i.Fecha,
                    Latitud = i.Latitud,
                    Longitud = i.Longitud,
                    Ubicacion = i.Ubicacion
                });
            });
        }
    }
}
