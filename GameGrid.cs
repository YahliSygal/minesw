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
using Xamarin.Essentials;

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

        Cell[,] grid = new Cell[10, 10];
        //int[,] gridRep = new int[10, 10];

        Button uncoverB;
        Button Back;

        TextView BombsRemained;

        ImageView volume;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GameGrid);

            // Create your application here

            volume = FindViewById<ImageView>(Resource.Id.volumeGame);
            BombsRemained = FindViewById<TextView>(Resource.Id.BombsRemained);
            Back = FindViewById<Button>(Resource.Id.BackButton);
            Back.Click += this.Back_Click;
            uncoverB = FindViewById<Button>(Resource.Id.uncoverB);
            uncoverB.Click += this.UncoverB_Click;

            dif = Intent.GetStringExtra("dif");
            assignGrid();

            if (Con.gPlayer.IsPlaying)
            {

                volume.SetImageResource(Resource.Drawable.unmuted);
            }
            else
            {
                volume.SetImageResource(Resource.Drawable.muted);
            }

            volume.Click += this.Volume_Click;
            
        }

        private void Volume_Click(object sender, EventArgs e)
        {
            if (Con.gPlayer.IsPlaying)
            {
                volume.SetImageResource(Resource.Drawable.muted);
                stopPlayer();
            }
            else
            {
                volume.SetImageResource(Resource.Drawable.unmuted);
                startPlayer();
            }
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
                    this.grid[i, j].image = FindViewById<ImageView>(Resource.Id.tile00 + i * 10 + j);
                    this.grid[i, j].image.SetOnClickListener(this);
                    this.grid[i, j].image.SetOnLongClickListener(this);
                    //this.gridRep[i, j] = 0;
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
            for (int i = 0; i < bombs; i++)
            {
                flag = false;
                do
                {
                    temp = rnd.Next(0, 101);
                    if (CheckValidity(ind, temp))
                    {
                        flag = true;
                        this.grid[temp / 10, temp % 10].IsBomb = true;
                    }
                } while (!flag);
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this.grid[i, j].IsBomb)
                    {
                        continue;
                    }
                    this.grid[i, j].surrounded = NumOfBombs(i, j);
                }
            }
            this.covered = CountBombs();
            BombsRemained.Text = Convert.ToString(uncoveredRight + uncoveredWrong) + "/" + Convert.ToString(covered + uncoveredWrong + uncoveredRight);
            this.created = true;
        }

        private int NumOfBombs(int row, int col)
        {
            int cou = 0;
            if (col == 9)
            {
                if (this.grid[row, col - 1].IsBomb)
                    cou++;
                if (row == 0)
                {
                    if (this.grid[1, 8].IsBomb)
                        cou++;
                    if (this.grid[1, 9].IsBomb)
                        cou++;
                    return cou;
                }
                if (row == 9)
                {
                    if (this.grid[8, 8].IsBomb)
                        cou++;
                    if (this.grid[8, 9].IsBomb)
                        cou++;
                    return cou;
                }
                if (this.grid[row + 1, 8].IsBomb)
                    cou++;
                if (this.grid[row + 1, 9].IsBomb)
                    cou++;
                if (this.grid[row - 1, 8].IsBomb)
                    cou++;
                if (this.grid[row - 1, 9].IsBomb)
                    cou++;
                return cou;
            }

            if (col == 0)
            {
                if (this.grid[row, col + 1].IsBomb)
                    cou++;
                if (row == 0)
                {
                    if (this.grid[1, 0].IsBomb)
                        cou++;
                    if (this.grid[1, 1].IsBomb)
                        cou++;
                    return cou;
                }
                if (row == 9)
                {
                    if (this.grid[8, 0].IsBomb)
                        cou++;
                    if (this.grid[8, 1].IsBomb)
                        cou++;
                    return cou;
                }
                if (this.grid[row + 1, 0].IsBomb)
                    cou++;
                if (this.grid[row + 1, 1].IsBomb)
                    cou++;
                if (this.grid[row - 1, 0].IsBomb)
                    cou++;
                if (this.grid[row - 1, 1].IsBomb)
                    cou++;
                return cou;
            }

            if (row == 0)
            {
                if (this.grid[row + 1, col].IsBomb)
                    cou++;
                if (this.grid[0, col + 1].IsBomb)
                    cou++;
                if (this.grid[0, col - 1].IsBomb)
                    cou++;
                if (this.grid[1, col + 1].IsBomb)
                    cou++;
                if (this.grid[1, col - 1].IsBomb)
                    cou++;
                return cou;
            }

            if (row == 9)
            {
                if (this.grid[row - 1, col].IsBomb)
                    cou++;
                if (this.grid[8, col + 1].IsBomb)
                    cou++;
                if (this.grid[8, col - 1].IsBomb)
                    cou++;
                if (this.grid[9, col + 1].IsBomb)
                    cou++;
                if (this.grid[9, col - 1].IsBomb)
                    cou++;
                return cou;
            }

            if (this.grid[row - 1, col - 1].IsBomb)
                cou++;
            if (this.grid[row - 1, col].IsBomb)
                cou++;
            if (this.grid[row - 1, col + 1].IsBomb)
                cou++;
            if (this.grid[row, col - 1].IsBomb)
                cou++;
            if (this.grid[row, col + 1].IsBomb)
                cou++;
            if (this.grid[row + 1, col - 1].IsBomb)
                cou++;
            if (this.grid[row + 1, col].IsBomb)
                cou++;
            if (this.grid[row + 1, col + 1].IsBomb)
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
            if (this.grid[getIndex(temp) / 10, getIndex(temp) % 10].IsBomb && !this.grid[getIndex(temp) / 10, getIndex(temp) % 10].IsFlagged)
            {
                this.grid[getIndex(temp) / 10, getIndex(temp) % 10].IsFlagged = true;
                temp.SetImageResource(0);
                uncoveredRight++;
                covered--;
            }
            else if (!this.grid[getIndex(temp) / 10, getIndex(temp) % 10].IsBomb && !this.grid[getIndex(temp) / 10, getIndex(temp) % 10].IsFlagged)
            {
                this.grid[getIndex(temp) / 10, getIndex(temp) % 10].IsFlagged = true;
                temp.SetImageResource(0);
                uncoveredWrong++;
                covered--;
            }
            else if (this.grid[getIndex(temp) / 10, getIndex(temp) % 10].IsBomb && this.grid[getIndex(temp) / 10, getIndex(temp) % 10].IsFlagged)
            {
                this.grid[getIndex(temp) / 10, getIndex(temp) % 10].IsFlagged = false;
                temp.SetImageResource(2);
                uncoveredRight--;
                covered++;
            }
            else
            {
                this.grid[getIndex(temp) / 10, getIndex(temp) % 10].IsFlagged = false;
                temp.SetImageResource(2);

                uncoveredWrong--;
                covered++;
            }
            Vibration.Vibrate(TimeSpan.FromMilliseconds(300));
            BombsRemained.Text = Convert.ToString(uncoveredRight + uncoveredWrong) + "/" + Convert.ToString(covered + uncoveredWrong + uncoveredRight);


            if (covered == 0 && uncoveredWrong == 0)
            {
                GameWon();
            }
            
            return true;
        }

        public bool UncoverTile(int ind)
        {
            this.grid[ind / 10, ind % 10].SetImageResource(1);
            if (this.grid[ind / 10, ind % 10].IsBomb)
            {
                GameLost();
                return false;
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
            Con.difComp = this.dif;
            Con.bombsComp = uncoveredRight;
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
                    this.grid[i, j].SetImageResource(1);
                }
            }
        }

        public int CountBombs()
        {
            int cou = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this.grid[i, j].IsBomb)
                        cou++;
                }
            }
            return cou;
        }

        protected override void OnResume()
        {
            base.OnResume();
            Con.gCon = this;
        }

        public void startPlayer()
        {
            if (Con.gPlayer == null)
            {
                Con.gPlayer = new MediaPlayer();
            }
            Con.gCon = this;


            var intent = new Intent(this, typeof(MusicService));
            intent.SetAction("com.xamarin.action.PLAY");
            StartService(intent);
        }

        public void stopPlayer()
        {
            var intent = new Intent(this, typeof(MusicService));
            intent.SetAction("com.xamarin.action.MUTE");
            StartService(intent);
        }
    }

    
}