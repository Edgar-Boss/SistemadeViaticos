using Android.Graphics;
using Android.OS;
using Plugin.Media.Abstractions;
using SAVIVE.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SAVIVE.ViewModels
{
    public class FotoViewModel:FotoModel
    {
        public Plugin.Media.Abstractions.MediaFile foto;
        public Command CapturarComando { get; set; }
        public Command CapturarPath { get; set; }
        
        public FotoViewModel()
        {
            //CapturarComando = new Command(TomarFoto);
            //CapturarPath = new Command(getPath);
            //TomarFoto();
        }

        public string getPath()
        {
            return foto.Path;
        }
        public async void TomarFoto() 
        {
            var camara = new StoreCameraMediaOptions();
            camara.Name = "Foto_prueba";
            camara.PhotoSize = PhotoSize.Full;
            camara.SaveToAlbum = true;
            camara.Directory = "SAVIVE";


            foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(camara);


            if (foto != null)
            {
                
                //return foto.path
                //Path = foto.Path;
                //Fotito = ImageSource.FromStream(() =>
                //{

                //    return foto.GetStream();
                //});
            }

        }

    }
}
