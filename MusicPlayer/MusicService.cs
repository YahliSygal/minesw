//using Android.App;
//using Android.Content;
//using Android.Media;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;

//namespace minesweeper
//{
//    [Service]
//    [IntentFilter(new[] { ActionPlay, ActionStop })]
//    class MusicService : Service
//    {
//        public const string ActionPlay = "com.xamarin.action.PLAY";
//        public const string ActionStop = "com.xamarin.action.MUTE";

//        private MediaPlayer player;

//        public override IBinder OnBind(Intent intent)
//        {
//            return null;
//        }

//        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
//        {
//            this.player = Con.gPlayer;
//            switch (intent.Action)
//            {
//                case ActionPlay: this.Play(); break;
//                case ActionStop: this.Stop(); break;
//            }
//            //Set sticky as we are a long running operation
//            return StartCommandResult.Sticky;
//        }

//        private void Play()
//        {
//            this.player = MediaPlayer.Create(Con.gCon, Android.Net.Uri.Parse("file://mSource.mp3"));
//            this.player.SetDataSource("mSource");
//            this.player.Prepared += (sender, args) => this.player.Start();
//            this.player.Error += (sender, args) => {
//                //playback error
//                System.Diagnostics.Debug.Write("Error in playback resetting: " + args.What);
//                this.Stop();//this will clean up and reset properly.
//            };
//        }

//        private void Stop()
//        {
//            this.player.Pause();
//        }

//        public override void OnDestroy()
//        {
//            base.OnDestroy();
//        }
//    }
//}