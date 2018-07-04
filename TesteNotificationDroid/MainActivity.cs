using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V4.App;
using System.Threading;

namespace TesteNotificationDroid
{
    [Activity(Label = "TesteNotificationDroid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        private static readonly int ButtonClickNotification = 9999;




        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

        }


        protected override void OnStop()
        {

            Thread.CurrentThread.Name = "Notificacao";
            Thread t1 = new Thread(new ThreadStart(run));
            t1.Start();

            void run()
            {
                bool a = true;

                while (a)
                {
                    Thread.Sleep(10000);
                    Bundle valueSend = new Bundle();
                    valueSend.PutString("sendContent", "Teste Sucesso");

                    Intent newIntent = new Intent(this, typeof(Second_Activity));
                    newIntent.PutExtras(valueSend);

                    Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
                    stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(Second_Activity)));
                    stackBuilder.AddNextIntent(newIntent);

                    PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

                    NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                        .SetAutoCancel(true)
                        .SetContentIntent(resultPendingIntent)
                        .SetContentTitle("Notificação")
                        .SetSmallIcon(Resource.Drawable.icon)
                        .SetContentText("Clique aqui");

                    NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                    notificationManager.Notify(ButtonClickNotification, builder.Build());

                }
            }


            base.OnStop();

        }
    }
}

