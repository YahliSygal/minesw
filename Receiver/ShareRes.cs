using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace minesweeper
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { "game.completed.successfully" })]
    public class ShareRes : BroadcastReceiver
    {

        public ShareRes()
        {

        }

        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }

        public override async void OnReceive(Context context, Intent intent)
        {
            await this.ShareText("I completed a minewseeper game on " + Con.difComp + " difficulty with " + Con.bombsComp.ToString() + " Bombs");

        }
    }
}