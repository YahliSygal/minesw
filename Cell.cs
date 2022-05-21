using Android.App;
using Android.Content;
using Android.Graphics;
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
    class Cell : ImageView
    {
        public bool IsBomb { get; set; }
        public bool IsFlagged { get; set; }
        public int surrounded { get; set; }

        public Cell(Context context) : base(context)
        {
            IsBomb = false;
            IsFlagged = false;
            surrounded = 0;
        }
    }
}