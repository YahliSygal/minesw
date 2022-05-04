using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace minesweeper
{
    [Activity(Label = "Menu")]
    public class Menu : Activity, Android.Views.View.IOnClickListener
    {
        //ImageView volume;

        //MediaPlayer media;

        Button normal;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.menu);

            // Create your application here

            normal = FindViewById<Button>(Resource.Id.normalDif);

            //volume = FindViewById<ImageView>(Resource.Id.volumeMenu);


            //volume.Click += this.Volume_Click;
            //startPlayer();

            normal.SetOnClickListener(this);

        }

        public void OnClick(View v)
        {
            if (v == normal)
            {
                Intent i = new Intent(this, typeof(GameGrid));
                i.PutExtra("dif", "nor");
                StartActivity(i);
            }
        }

        //private void Volume_Click(object sender, EventArgs e)
        //{
        //    if (media.IsPlaying)
        //    {
        //        volume.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeFile("muted"));
        //        stopPlayer();
        //    }
        //    else
        //    {
        //        volume.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeFile("unmuted"));
        //        startPlayer();
        //    }
        //}

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    Con.gPlayer = media;
        //    Con.gCon = this;
        //}

        //public void startPlayer()
        //{
        //    var intent = new Intent(MusicService.ActionPlay);
        //    StartService(intent);
        //}

        //public void stopPlayer()
        //{
        //    var intent = new Intent(MusicService.ActionStop);
        //    StartService(intent);
        //}
    }
}