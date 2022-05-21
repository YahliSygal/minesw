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
        public ImageView image { get; set; }

        public Cell(Context context) : base(context)
        {
            IsBomb = false;
            IsFlagged = false;
            surrounded = 0;
        }

        public override void SetImageResource(int resId)
        {

            if (resId == 1)
            {
                switch (this.surrounded)
                {
                    case 0:
                        resId = Resource.Drawable.blank;
                        break;
                    case 1:
                        resId = Resource.Drawable.one;
                        break;
                    case 2:
                        resId = Resource.Drawable.two;
                        break;
                    case 3:
                        resId = Resource.Drawable.three;
                        break;
                    case 4:
                        resId = Resource.Drawable.four;
                        break;
                    case 5:
                        resId = Resource.Drawable.five;
                        break;
                    case 6:
                        resId = Resource.Drawable.six;
                        break;
                    case 7:
                        resId = Resource.Drawable.seven;
                        break;
                    case 8:
                        resId = Resource.Drawable.eight;
                        break;
                    default:
                        resId = Resource.Drawable.bomb;
                        break;
                }
            }
            else if (resId == 2)
            {
                resId = Resource.Drawable.covered;
            }
            else
            {
                resId = Resource.Drawable.flagged;
            }
            base.SetImageResource(resId);
        }
    }
}