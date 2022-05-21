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

namespace minesweeper
{
    [Activity(Label = "score")]
    public class score : Activity
    {

        TextView scorePlayer;
        Button BackToMenu;
        Button Share;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Score);
            // Create your application here

            scorePlayer = FindViewById<TextView>(Resource.Id.scorePlayer);
            BackToMenu = FindViewById<Button>(Resource.Id.backToMenuButton);
            Share = FindViewById<Button>(Resource.Id.shareButton);

            Con.db.AddScore();
            scorePlayer.Text = Convert.ToString(Con.db.Getscore());

            BackToMenu.Click += this.BackToMenu_Click;
            Share.Click += this.Share_Click;
        }

        private void Share_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent("game.completed.successfully");
            this.SendBroadcast(intent);
        }

        private void BackToMenu_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(Menu));
            StartActivity(i);
        }
    }
}