using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using System.Text.RegularExpressions;
using SAVIVE.Clases;
using Xamarin.Essentials;
using Plugin.Geolocator;

namespace SAVIVE.Generic
{
    public static class Generics
    {
        public static async Task<List<T>> ListarDatos<T>(string url)
        { 
            HttpClient cliente = new HttpClient();
            var rpta  = await cliente.GetAsync(url);
            if (!rpta.IsSuccessStatusCode) 
                return new List<T>();
            else
            { 
                var result = await rpta.Content.ReadAsStringAsync();
                List<T> l = JsonConvert.DeserializeObject<List<T>>(result);
                return l;
            }
                
        }

        private static readonly Regex regex = new Regex(@"\s+");

        public static string EliminarEspacios(this string str)
        {
            return regex.Replace(str, String.Empty);
        }

        public static List<SolicitudCLS> Filtrar_listas(List<SolicitudCLS> solicitudes,int estado, string nombre)
        {            
            List<SolicitudCLS> sol_ = new List<SolicitudCLS>();

            if (nombre == null) //fitro solo para estado 
            {
                for (int k = 0; k < solicitudes.Count; k++)
                {
                    if (solicitudes.ElementAt(k).estado == estado)
                    {
                        sol_.Add(new SolicitudCLS
                        {
                            idsolicitud = solicitudes.ElementAt(k).idsolicitud,
                            autorizacion = solicitudes.ElementAt(k).autorizacion,
                            justificacion = solicitudes.ElementAt(k).justificacion,
                            fecha = solicitudes.ElementAt(k).fecha,
                            solicitante = solicitudes.ElementAt(k).solicitante,
                            estado = solicitudes.ElementAt(k).estado
                        });
                    }

                }

            }
            else //filtro para nombres
            {
                string _nombre = Generic.Generics.EliminarEspacios(nombre);
                for (int k = 0; k < solicitudes.Count; k++)
                {

                    string _autorizacion = Generic.Generics.EliminarEspacios(solicitudes.ElementAt(k).autorizacion);

                    if (_nombre.Length <= _autorizacion.Length)
                    {
                        if (_autorizacion.Substring(0, _nombre.Length).ToLower() == _nombre.ToLower() && solicitudes.ElementAt(k).estado == estado)
                        {
                            sol_.Add(new SolicitudCLS
                            {
                                idsolicitud = solicitudes.ElementAt(k).idsolicitud,
                                autorizacion = solicitudes.ElementAt(k).autorizacion,
                                justificacion = solicitudes.ElementAt(k).justificacion,
                                fecha = solicitudes.ElementAt(k).fecha,
                                solicitante = solicitudes.ElementAt(k).solicitante,
                                estado = solicitudes.ElementAt(k).estado
                            });
                        }
                    }

                }

            }
            return sol_;
        }

        public static async Task<List<double>> ObtenerUbicacion()
        {
            List<double> coordenadas = null;

            try
            {
                coordenadas = new List<double>();
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);

                coordenadas.Add(location.Latitude);
                coordenadas.Add(location.Longitude);
            }
            catch (Exception ex)
            {
                coordenadas = null;

            }

            return coordenadas;
        }

    }
}
