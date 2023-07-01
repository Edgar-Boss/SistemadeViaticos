using Android.Telephony.Data;
using Java.Security;
using Plugin.Media.Abstractions;
using Plugin.XamarinFormsSaveOpenPDFPackage;
using SAVIVE.Clases;
using SAVIVE.Generic;
using SAVIVE.Models;
using SAVIVE.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVIVE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaComprobarViaticos : ContentPage
    {
        FileResult[] imagenes_stream = new FileResult[3];
        string[] imagenes_paths = new string[3];
        bool ban_foto = false;
        List<DViaticoCLS> lista_viaticos;
        List<DPasajaeCLS> lista_pasajes;
        SolicitudCLS solicitud;
        int id;
        SolicitudViaticosCLS solicitud_viatico;
        string concepto;
        DViaticoCLS viatico;
        DPasajaeCLS pasaje;
        public PaginaComprobarViaticos(SolicitudCLS solicitud, int id, string concepto)
        {
            InitializeComponent();
            
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += TapGestureRecognizer_Tapped;
            frm_pdf.GestureRecognizers.Add(tap);
            frm_xml.GestureRecognizers.Add(tap);
            frm_ticket.GestureRecognizers.Add(tap);
            ent_concepto.Text = concepto;
            this.solicitud = solicitud;
            this.id = id;
            this.concepto = concepto;
            Obtener_solicitudes();

        }

        private async void btn_adjuntar_pdf_Clicked(object sender, EventArgs e)
        {



            var file = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] {"image/jpeg" , "application/pdf" } },
                })
            });

            if (file == null)
                return;


            lbl_nombre_pdf.Text = file.FileName;
            imagenes_stream[0] = file;
            imagenes_paths[0] = file.FullPath;
        }


        private async void btn_adjuntar_xml_Clicked(object sender, EventArgs e)
        {

            //FileResult  file = await FilePicker.PickAsync();
            var file = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] {"image/jpeg" , "application/pdf" } },
                })
            });

            if (file == null)
                return;
            lbl_nombre_xml.Text = file.FileName;
            imagenes_stream[1] = file;
            imagenes_paths[1] = file.FullPath;
        }

        private async void btn_adjuntar_ticket_Clicked(object sender, EventArgs e)//clic seleccionar archivo
        {
            //var file = await FilePicker.PickAsync();
            var file = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] {"image/jpeg", "application/pdf" } },
                })
            });
            if (file == null)
                return;
            lbl_nombre_ticket.Text = file.FileName;


            imagenes_stream[2] = file;
            imagenes_paths[2] = file.FullPath;

            ban_foto = false;

        }
        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e) //clic ver archivo seleccionado
        {
            Frame obj = (Frame)sender;

            if (imagenes_stream[int.Parse(obj.StyleId)] == null)//campo vacio
            {
                if (ban_foto)
                {

                    if (imagenes_paths[int.Parse(obj.StyleId)] != null)
                        await Navigation.PushAsync(new PaginaVisualizarImagenes(imagenes_paths[int.Parse(obj.StyleId)]));
                    else
                        await DisplayAlert("", "Aun no se ha seleccionado un archivo", "ok");
                }
                else
                {
                    await DisplayAlert("", "Aun no se ha seleccionado un archivo", "ok");
                }

            }
            else if (imagenes_stream[int.Parse(obj.StyleId)].FileName.EndsWith(".pdf")) //hace referencia aun pdf
            {


                var stream = await imagenes_stream[int.Parse(obj.StyleId)].OpenReadAsync();

                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);

                    await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView("myFile.pdf", "application/pdf", memoryStream, PDFOpenContext.InApp);
                }
            }
            else //hace referencia a una imagen
            {

                await Navigation.PushAsync(new PaginaVisualizarImagenes(imagenes_paths[int.Parse(obj.StyleId)]));
            }

        }

        private void evento_vaciar_campos(object sender, EventArgs e)
        {
            Button boton = (Button)sender;

            switch (boton.StyleId)
            {
                case "0": lbl_nombre_pdf.Text = "";
                    imagenes_stream[0] = null;
                    imagenes_paths[0] = null;
                    break;
                case "1": lbl_nombre_xml.Text = "";
                    imagenes_stream[1] = null;
                    imagenes_paths[1] = null;
                    break;
                case "2": lbl_nombre_ticket.Text = "";
                    imagenes_stream[2] = null;
                    imagenes_paths[2] = null;
                    break;
            }
        }

        private void propiedad_cambiada_lbl(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            Label id = (Label)sender;

            switch (id.StyleId)
            {
                case "0":
                    if (lbl_nombre_pdf.Text == "" || lbl_nombre_pdf.Text == null)
                    {
                        btn_eliminar_pdf.IsEnabled = false;
                    }
                    else
                    {
                        btn_eliminar_pdf.IsEnabled = true;
                    }
                    break;
                case "1":
                    if (lbl_nombre_xml.Text == "" || lbl_nombre_xml.Text == null)
                    {
                        btn_eliminar_mxl.IsEnabled = false;
                    }
                    else
                    {
                        btn_eliminar_mxl.IsEnabled = true;
                    }
                    break;
                case "2":
                    if (lbl_nombre_ticket.Text == "" || lbl_nombre_ticket.Text == null)
                    {
                        btn_eliminar_ticket.IsEnabled = false;
                    }
                    else
                    {
                        btn_eliminar_ticket.IsEnabled = true;
                    }
                    break;
            }
        }

        private void btn_tomar_foto_Clicked(object sender, EventArgs e)
        {

            TomarFoto();



        }
        public async void TomarFoto()
        {
            var camara = new StoreCameraMediaOptions();
            camara.Name = "Foto_prueba";
            camara.PhotoSize = PhotoSize.Full;
            camara.SaveToAlbum = true;
            camara.Directory = "SAVIVE";


            Plugin.Media.Abstractions.MediaFile foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(camara);

            if (foto != null)
            {

                int ind = foto.Path.LastIndexOf("/");
                lbl_nombre_ticket.Text = foto.Path.Substring(ind + 1);
                imagenes_paths[2] = foto.Path;
                imagenes_stream[2] = null;
                ban_foto = true;

            }


        }

        private async void Obtener_solicitudes()
        {
            List<DViaticoCLS> l = await Generics.ListarDatos<DViaticoCLS>("http://www.sumate.somee.com/api/DViaticos");
            lista_viaticos = l;
            List<DPasajaeCLS> ll = await Generics.ListarDatos<DPasajaeCLS>("http://www.sumate.somee.com/api/dpasajes");
            lista_pasajes = ll;
            FiltrarDatos();

        }

        private void FiltrarDatos()
        {
            if (concepto == "Viaticos")
            {
                lista_viaticos.ForEach(i =>
                {
                    if (solicitud.idsolicitud == i.Idsolicitud && i.Iddestino == id)
                    {
                        viatico = i;
                    }
                });
                lbl_costo.Text = viatico.Costo.ToString();
                BindingContext = viatico;
            }
            if (concepto == "Pasajes")
            {
                lista_pasajes.ForEach(i =>
                {
                    if (solicitud.idsolicitud == i.Idsolicitud && i.Iddestino == id)
                    {
                        pasaje = i;
                    }
                });
                lbl_costo.Text = pasaje.Tarifa.ToString();
                BindingContext = pasaje;
            }
            
        }
    }
}



