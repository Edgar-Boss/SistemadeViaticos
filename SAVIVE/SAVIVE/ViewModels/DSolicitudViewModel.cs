using SAVIVE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SAVIVE.ViewModels
{
    public class DSolicitudViewModel
    {
        public ObservableCollection<DEmpleadosCLS> dempleados { get; set; }

        public DSolicitudViewModel(List<DEmpleadosCLS> _dempleados ) 
        {

            dempleados = new ObservableCollection<DEmpleadosCLS>();


            _dempleados.ForEach(i =>
            {
                dempleados.Add(new DEmpleadosCLS
                {
                    idsolicitud = i.idsolicitud,
                    Autorizacion = i.Autorizacion,
                    Justifiacion = i.Justifiacion,
                    Fecha = i.Fecha,
                    Solicitante = i.Solicitante,
                    Estado = i.Estado,
                    Idempleado = i.Idempleado,
                    DJustificacion = i.DJustificacion,
                    Nombre = i.Nombre,
                    Paterno = i.Paterno,
                    Materno = i.Materno,
                    Puesto = i.Puesto,
                    Area = i.Area
                });
            });

        }

    }
}
