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
using System.Threading;

namespace minesweeper
{
    [Activity(Label = "GameGrid")]
    public class GameGrid : Activity, Android.Views.View.IOnClickListener, Android.Views.View.IOnLongClickListener
    {
        string dif = "nor";


        bool created = false;

        int uncoveredRight;
        int uncoveredWrong;
        int covered;

        ImageView[,] grid = new ImageView[10, 10];
        int[,] gridRep = new int[10, 10];

        Button uncoverB;
        Button Back;

        TextView BombsRemained;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GameGrid);

            // Create your application here

            BombsRemained = FindViewById<TextView>(Resource.Id.BombsRemained);
            Back = FindViewById<Button>(Resource.Id.BackButton);
            Back.Click += this.Back_Click;
            uncoverB = FindViewById<Button>(Resource.Id.uncoverB);
            uncoverB.Click += this.UncoverB_Click;

            dif = Intent.GetStringExtra("dif");
            assignGrid();
            
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(Menu));
            StartActivity(i);
        }

        private void UncoverB_Click(object sender, EventArgs e)
        {
            UncoverGrid();
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

        private void CreateGrid(int ind)
        {
            bool flag = false;
            Random rnd = new Random();
            int temp = 0;
            if (ind == 101)
            {
                return;
            }

            int bombs = Con.NORMAL_DIFFICULTY;
            if (dif == "easy")
            {
                bombs = Con.EASY_DIFFICULTY;
            }
            if (dif == "nor")
            {
                bombs = Con.NORMAL_DIFFICULTY;
            }
            if (dif == "hard")
            {
                bombs = Con.HARD_DIFFICULTY;
            }
            this.covered = bombs;
            BombsRemained.Text = Convert.ToString(uncoveredRight + uncoveredWrong) + "/" + Convert.ToString(covered + uncoveredWrong + uncoveredRight);
            for (int i = 0; i < bombs; i++)
            {
                flag = false;
                do
                {
                    temp = rnd.Next(0, 101);
                    if (CheckValidity(ind, temp))
                    {
                        flag = true;
                        this.gridRep[temp / 10, temp % 10] = 99;
                    }
                } while (!flag);
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this.gridRep[i, j] == 99)
                    {
                        continue;
                    }
                    this.gridRep[i, j] = NumOfBombs(i, j);
                }
            }
            this.created = true;
        }

        private int NumOfBombs(int row, int col)
        {
            int cou = 0;
            if (col == 9)
            {
                if (this.gridRep[row, col - 1] == 99)
                    cou++;
                if (row == 0)
                {
                    if (this.gridRep[1, 8] == 99)
                        cou++;
                    if (this.gridRep[1, 9] == 99)
                        cou++;
                    return cou;
                }
                if (row == 9)
                {
                    if (this.gridRep[8, 8] == 99)
                        cou++;
                    if (this.gridRep[8, 9] == 99)
                        cou++;
                    return cou;
                }
                if (this.gridRep[row + 1, 8] == 99)
                    cou++;
                if (this.gridRep[row + 1, 9] == 99)
                    cou++;
                if (this.gridRep[row - 1, 8] == 99)
                    cou++;
                if (this.gridRep[row - 1, 9] == 99)
                    cou++;
                return cou;
            }

            if (col == 0)
            {
                if (this.gridRep[row, col + 1] == 99)
                    cou++;
                if (row == 0)
                {
                    if (this.gridRep[1, 0] == 99)
                        cou++;
                    if (this.gridRep[1, 1] == 99)
                        cou++;
                    return cou;
                }
                if (row == 9)
                {
                    if (this.gridRep[8, 0] == 99)
                        cou++;
                    if (this.gridRep[8, 1] == 99)
                        cou++;
                    return cou;
                }
                if (this.gridRep[row + 1, 0] == 99)
                    cou++;
                if (this.gridRep[row + 1, 1] == 99)
                    cou++;
                if (this.gridRep[row - 1, 0] == 99)
                    cou++;
                if (this.gridRep[row - 1, 1] == 99)
                    cou++;
                return cou;
            }

            if (row == 0)
            {
                if (this.gridRep[row + 1, col] == 99)
                    cou++;
                if (this.gridRep[0, col + 1] == 99)
                    cou++;
                if (this.gridRep[0, col - 1] == 99)
                    cou++;
                if (this.gridRep[1, col + 1] == 99)
                    cou++;
                if (this.gridRep[1, col - 1] == 99)
                    cou++;
                return cou;
            }

            if (row == 9)
            {
                if (this.gridRep[row - 1, col] == 99)
                    cou++;
                if (this.gridRep[8, col + 1] == 99)
                    cou++;
                if (this.gridRep[8, col - 1] == 99)
                    cou++;
                if (this.gridRep[9, col + 1] == 99)
                    cou++;
                if (this.gridRep[9, col - 1] == 99)
                    cou++;
                return cou;
            }

            if (this.gridRep[row - 1, col - 1] == 99)
                cou++;
            if (this.gridRep[row - 1, col] == 99)
                cou++;
            if (this.gridRep[row - 1, col + 1] == 99)
                cou++;
            if (this.gridRep[row, col - 1] == 99)
                cou++;
            if (this.gridRep[row, col + 1] == 99)
                cou++;
            if (this.gridRep[row + 1, col - 1] == 99)
                cou++;
            if (this.gridRep[row + 1, col] == 99)
                cou++;
            if (this.gridRep[row + 1, col + 1] == 99)
                cou++;

            return cou;
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
                CreateGrid(getIndex(temp));
            }
            UncoverTile(getIndex(temp));
            
        }

        public bool OnLongClick(View v)
        {
            if (!created)
            {
                OnClick(v);
                return true;
            }
            ImageView temp = (ImageView)v;
            if (this.gridRep[getIndex(temp) / 10, getIndex(temp) % 10] == 99)
            {              
                this.gridRep[getIndex(temp) / 10, getIndex(temp) % 10] = 999;
                temp.SetImageResource(Resource.Drawable.flagged);
                uncoveredRight++;
                covered--;
            }
            else if (this.gridRep[getIndex(temp) / 10, getIndex(temp) % 10] < 9)
            {
                this.gridRep[getIndex(temp) / 10, getIndex(temp) % 10]++;
                this.gridRep[getIndex(temp) / 10, getIndex(temp) % 10] *= 10;
                temp.SetImageResource(Resource.Drawable.flagged);
                uncoveredWrong++;
                covered--;
            }
            else if (this.gridRep[getIndex(temp) / 10, getIndex(temp) % 10] == 999)
            {
                this.gridRep[getIndex(temp) / 10, getIndex(temp) % 10] = 99;
                temp.SetImageResource(Resource.Drawable.covered);
                uncoveredRight--;
                covered++;
            }
            else
            {
                this.gridRep[getIndex(temp) / 10, getIndex(temp) % 10] /= 10;
                this.gridRep[getIndex(temp) / 10, getIndex(temp) % 10]--;
                uncoveredWrong--;
                covered++;
            }

            BombsRemained.Text = Convert.ToString(uncoveredRight + uncoveredWrong) + "/" + Convert.ToString(covered + uncoveredWrong + uncoveredRight);


            if (covered == 0 && uncoveredWrong == 0)
            {
                GameWon();
            }

            return true;
        }

        public bool UncoverTile(int ind)
        {
            if (this.gridRep[ind / 10, ind % 10] == 99)
            {
                this.grid[ind / 10, ind % 10].SetImageResource(Resource.Drawable.bomb);
                GameLost();
                return false;
            }
            int i = ind / 10;
            int j = ind % 10;

            switch (this.gridRep[i, j])
            {
                case 0:
                    this.grid[i, j].SetImageResource(Resource.Drawable.blank);
                    break;
                case 1:
                    this.grid[i, j].SetImageResource(Resource.Drawable.one);
                    break;
                case 2:
                    this.grid[i, j].SetImageResource(Resource.Drawable.two);
                    break;
                case 3:
                    this.grid[i, j].SetImageResource(Resource.Drawable.three);
                    break;
                case 4:
                    this.grid[i, j].SetImageResource(Resource.Drawable.four);
                    break;
                case 5:
                    this.grid[i, j].SetImageResource(Resource.Drawable.five);
                    break;
                case 6:
                    this.grid[i, j].SetImageResource(Resource.Drawable.six);
                    break;
                case 7:
                    this.grid[i, j].SetImageResource(Resource.Drawable.seven);
                    break;
                case 8:
                    this.grid[i, j].SetImageResource(Resource.Drawable.eight);
                    break;
            }
            return true;
        }

        public void GameLost()
        {
            UncoverGrid();
            Toast.MakeText(this, "Game Lost", ToastLength.Short).Show();
        }

        public void GameWon()
        {
            UncoverGrid();
            Thread.Sleep(10000);
            Toast.MakeText(this, "Game Won", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(score));
            StartActivity(i);
        }

        public void UncoverGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this.gridRep[i, j] > 8 && this.gridRep[i, j] < 91)
                    {
                        this.gridRep[i, j] /= 10;
                        this.gridRep[i, j]--;
                    }
                    switch (this.gridRep[i, j])
                    {
                        case 0:
                            this.grid[i, j].SetImageResource(Resource.Drawable.blank);
                            break;
                        case 1:
                            this.grid[i, j].SetImageResource(Resource.Drawable.one);
                            break;
                        case 2:
                            this.grid[i, j].SetImageResource(Resource.Drawable.two);
                            break;
                        case 3:
                            this.grid[i, j].SetImageResource(Resource.Drawable.three);
                            break;
                        case 4:
                            this.grid[i, j].SetImageResource(Resource.Drawable.four);
                            break;
                        case 5:
                            this.grid[i, j].SetImageResource(Resource.Drawable.five);
                            break;
                        case 6:
                            this.grid[i, j].SetImageResource(Resource.Drawable.six);
                            break;
                        case 7:
                            this.grid[i, j].SetImageResource(Resource.Drawable.seven);
                            break;
                        case 8:
                            this.grid[i, j].SetImageResource(Resource.Drawable.eight);
                            break;
                        case 99:
                            this.grid[i, j].SetImageResource(Resource.Drawable.bomb);
                            break;
                    }
                }
            }
        }
    }

    
}