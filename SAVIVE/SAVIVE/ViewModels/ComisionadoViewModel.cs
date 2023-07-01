using Android.App;
using SAVIVE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SAVIVE.ViewModels
{
    public class ComisionadoViewModel
    {
        public ObservableCollection<Comisionado> Comisionados { get; set; }

        public ComisionadoViewModel(List<PaginaCrearSolicitud.Empleado> Empleados)
        {
            Comisionados = new ObservableCollection<Comisionado>();

            string en;
            for (int k = 0; k < Empleados.Count; k++)
            {
                en = "True";
                if (k==0)   
                    en = "False";
                
                Comisionados.Add(new Comisionado
                {
                    Name = Empleados[k].Nombre + " " + Empleados[k].Appaterno + " " + Empleados[k].Apmaterno,
                    Puesto = Empleados[k].Puestos,
                    Justificacion = Empleados[k].Justificaion,
                    N_emp = "No." + Empleados[k].Numero,
                    Image = "User.png",
                    Id = k.ToString(),
                    Edit = "editing",
                    Enable = en,

                });

            }

        }
    }
}
