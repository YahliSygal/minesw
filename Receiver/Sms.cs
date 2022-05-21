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
using Xamarin.Essentials;

namespace minesweeper.Receiver
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { "game.completed.successfully" })]
    public class Sms : BroadcastReceiver
    {
        public override async void OnReceive(Context context, Intent intent)
        {
            string text = "I completed a minewseeper game on " + Con.difComp + " difficulty with " + Con.bombsComp.ToString() + " Bombs";
            System.Diagnostics.Debug.Write("poli: here");
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }
    }
}