using Android.Graphics;
using SAVIVE.Clases;
using SAVIVE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static SAVIVE.PaginaCrearSolicitud;

namespace SAVIVE.ViewModels
{
    public class SolicitudViewModel
    {
        public ObservableCollection<Solicitud> Solicitudes { get; set; }

        public SolicitudViewModel(List<SolicitudCLS> lista_solicitudes) 
        {
            Solicitudes = new ObservableCollection<Solicitud>();

           
            lista_solicitudes.ForEach(i =>
            {
                string col = "white";
                if (i.estado == 1)
                    col = "#26BBDEFB";
                else if (i.estado == 2)
                    col = "#0AE0AA00";
                else if(i.estado == 3)
                    col = "#33E3FAE4";

                Solicitudes.Add(new Solicitud
                {
                    IdSolicitud = i.idsolicitud,
                    Autorizacion = i.autorizacion,
                    Justificacion = i.justificacion,
                    Fecha = i.fecha.Date,
                    Solicitante = i.solicitante,
                    estado= i.estado,
                    color = col
                }); ;

            });

            

            
            }
    }
}
