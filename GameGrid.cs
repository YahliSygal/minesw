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
    [Activity(Label = "GameGrid")]
    public class GameGrid : Activity, Android.Views.View.IOnClickListener, Android.Views.View.IOnLongClickListener
    {
        string dif = "nor";


        bool created = false;

        ImageView[,] grid = new ImageView[10, 10];

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GameGrid);

            // Create your application here


            dif = Intent.GetStringExtra("dif");
            assignGrid();
            
        }

        private void assignGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    this.grid[i, j] = FindViewById<ImageView>(Resource.Id.tile00 + i * 10 + j);
                    this.grid[i, j].SetOnClickListener(this);
                    this.grid[i, j].SetOnLongClickListener(this);
                }
            }
        }

        private void CreateGrid(View v)
        {

        }

        public void OnClick(View v)
        {
            ImageView temp = (ImageView)v;
            if (!created)
            {
                CreateGrid(v);
            }
        }

        public bool OnLongClick(View v)
        {
            if (!created)
            {
                OnClick(v);
                return true;
            }

            return true;
        }
    }

    
}