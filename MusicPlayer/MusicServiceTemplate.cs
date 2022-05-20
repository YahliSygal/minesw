using Android.App;
using Android.Content;
using Android.OS;

namespace minesweeper
{
    abstract class MusicServiceTemplate : Service
    {
        public const string ActionPlay = "com.xamarin.action.PLAY";
        public const string ActionStop = "com.xamarin.action.MUTE";


        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }

        protected abstract void Play();

        protected abstract void Stop();

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}