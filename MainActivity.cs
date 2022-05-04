using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace minesweeper
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, Android.Views.View.IOnClickListener
    {

        EditText username;
        EditText password;

        Button loginButton;
        Button registerButton;

        Dialog d;

        Button submit;
        Button submitR;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            loginButton = FindViewById<Button>(Resource.Id.loginButton);
            registerButton = FindViewById<Button>(Resource.Id.registerButton);

            loginButton.SetOnClickListener(this);
            registerButton.SetOnClickListener(this);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnClick(View v)
        {
            if (v == loginButton)
            {
                CreateLoginDialog();
            }
            if (v == registerButton)
            {
                CreateRegisterDialog();
            }
            if (v == submit)
            {
                Con.UserT log = new Con.UserT();
                log.username = username.Text.ToLower();
                log.password = password.Text;
                switch (Con.db.Login(log))
                {
                    case -1:
                        Toast.MakeText(this, "Password incorrect", ToastLength.Short).Show();
                        break;
                    case 0:
                        Toast.MakeText(this, "Connected", ToastLength.Short).Show();
                        Intent i = new Intent(this, typeof(Menu));
                        StartActivity(i);
                        break;
                    case 1:
                        Toast.MakeText(this, "User doesn't exist", ToastLength.Short).Show();
                        break;
                    default:
                        Toast.MakeText(this, "UNKNOWN ERROR", ToastLength.Short).Show();
                        break;
                }
            }
            if (v == submitR)
            {
                Con.UserT reg = new Con.UserT();
                reg.username = username.Text.ToLower();
                reg.password = password.Text;
                if (Con.db.Register(reg))
                {
                    Toast.MakeText(this, "Connected", ToastLength.Short).Show();
                    Intent i = new Intent(this, typeof(Menu));
                    StartActivity(i);
                }
                else
                {
                    Toast.MakeText(this, "User exists Already", ToastLength.Short).Show();
                }
            }
        }

        public void CreateLoginDialog()
        {
            d = new Dialog(this);
            d.SetContentView(Resource.Layout.loginLayout);
            d.SetTitle("Login");
            d.SetCancelable(true);
            submit = d.FindViewById<Button>(Resource.Id.submitLogin);
            username = d.FindViewById<EditText>(Resource.Id.username);
            password = d.FindViewById<EditText>(Resource.Id.password);
            submit.SetOnClickListener(this);
            d.Show();
        }

        public void CreateRegisterDialog()
        {
            d = new Dialog(this);
            d.SetContentView(Resource.Layout.loginLayout);
            d.SetTitle("Register");
            d.SetCancelable(true);
            submitR = d.FindViewById<Button>(Resource.Id.submitLogin);
            username = d.FindViewById<EditText>(Resource.Id.username);
            password = d.FindViewById<EditText>(Resource.Id.password);
            submitR.SetOnClickListener(this);
            d.Show();
        }
    }
}
