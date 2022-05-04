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
        int[,] gridRep = new int[10, 10];

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
                    this.gridRep[i, j] = 0;
                }
            }
        }

        private void CreateGrid(ImageView ima)
        {
            bool flag = false;
            Random rnd = new Random();
            int ind = getIndex(ima);
            int[] bombs = new int[5];
            int temp = 0;
            if (ind == 100)
            {
                return;
            }
            int row = ind / 10;
            int col = ind % 10;
            
            for (int i = 0; i < 5; i++)
            {
                flag = false;
                do
                {
                    temp = rnd.Next(0, 100);
                    if (CheckValidity(ind, temp))
                    {
                        flag = true;
                        bombs[i] = temp;
                    }
                } while (!flag);
            }
        }

        private bool CheckValidity (int ind, int bomb)
        {
            if (ind == bomb)
                return false;

            if (ind % 10 == 1)
            {
                if (bomb == ind - 10 || bomb == ind - 9)
                    return false;
                if (bomb == ind + 10 || bomb == ind + 11)
                    return false;
                if (bomb == ind + 1)
                    return false;
                return true;
            }

            if (ind % 10 == 0)
            {
                if (bomb == ind - 10 || bomb == ind - 11)
                    return false;
                if (bomb == ind + 10 || bomb == ind + 9)
                    return false;
                if (bomb == ind - 1)
                    return false;
                return true;
            }

            if (bomb == ind - 11 || bomb == ind - 10 || bomb == ind - 9)
                return false;
            if (bomb == ind + 11 || bomb == ind + 10 || bomb == ind + 9)
                return false;
            if (bomb == ind + 1 || bomb == ind - 1)
                return false;
            return true;
        }

        private int getIndex(ImageView ima)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this.grid[i, j] == ima)
                    {
                        return (i * 10 + j);
                    }
                }
            }
            return 100;
        }

        public void OnClick(View v)
        {
            ImageView temp = (ImageView)v;
            if (!created)
            {
                CreateGrid(temp);
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