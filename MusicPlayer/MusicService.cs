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
using System.Threading;

namespace minesweeper
{
    [Service]
    [IntentFilter(new[] { ActionPlay, ActionStop })]
    class MusicService : Service
    {
        public const string ActionPlay = "com.xamarin.action.PLAY";
        public const string ActionStop = "com.xamarin.action.MUTE";


        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            switch (intent.Action)
            {
                case ActionPlay: this.Play(); break;
                case ActionStop: this.Stop(); break;
            }
            //Set sticky as we are a long running operation
            return StartCommandResult.Sticky;
        }

        private void Play()
        {
            Con.gPlayer = MediaPlayer.Create(Con.gCon, Android.Net.Uri.Parse("file://mSource.mp3"));
            Con.gPlayer.SetDataSource("mSource");
            Con.gPlayer.Prepared += (sender, args) => Con.gPlayer.Start();
            Con.gPlayer.Error += (sender, args) =>
            {
                //playback error
                System.Diagnostics.Debug.Write("Error in playback resetting: " + args.What);
                this.Stop();//this will clean up and reset properly.
            };
        }

        private void Stop()
        {
            Con.gPlayer.Pause();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}