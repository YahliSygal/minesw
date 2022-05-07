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

        MediaPlayer player;

        Button easy;
        Button normal;
        Button hard;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.menu);

            // Create your application here

            easy = FindViewById<Button>(Resource.Id.easyDif);
            normal = FindViewById<Button>(Resource.Id.normalDif);
            hard = FindViewById<Button>(Resource.Id.hardDif);

            //volume = FindViewById<ImageView>(Resource.Id.volumeMenu);


            //volume.Click += this.Volume_Click;
            //startPlayer();

            player = new MediaPlayer();

            easy.SetOnClickListener(this);
            normal.SetOnClickListener(this);
            hard.SetOnClickListener(this);

        }

        public void OnClick(View v)
        {
            Intent i = new Intent(this, typeof(score));
            if (v == easy)
            {
                i.PutExtra("dif", "easy");
            }
            else if (v == normal)
            {
                i.PutExtra("dif", "nor");
            }
            else if (v == hard)
            {
                i.PutExtra("dif", "hard");
            }
            StartActivity(i);
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

        public void startPlayer()
        {
            this.player = Con.gPlayer;
            Con.gCon = this;
            
            var intent = new Intent(MusicService.ActionPlay);
            StartService(intent);
        }

        //public void stopPlayer()
        //{
        //    var intent = new Intent(MusicService.ActionStop);
        //    StartService(intent);
        //}
    }
}