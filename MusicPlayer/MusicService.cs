using Android.App;
using Android.Content;
using Android.Media;

namespace minesweeper
{
    [Service]
    [IntentFilter(new[] { ActionPlay, ActionStop })]
    class MusicService : MusicServiceTemplate
    {

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

        protected override void Play()
        {
            Con.gPlayer = MediaPlayer.Create(Con.gCon, Resource.Raw.mSource);
            Con.gPlayer.Prepared += (sender, args) => Con.gPlayer.Start();
            Con.gPlayer.Error += (sender, args) =>
            {
                //playback error
                System.Diagnostics.Debug.Write("Error in playback resetting: " + args.What);
                this.Stop();//this will clean up and reset properly.
            };
        }

        protected override void Stop()
        {
            Con.gPlayer.Stop();
        }
    }
}