using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V4.App;
using System.Threading;
using System;
using TesteNotificationDroid.ModelsHorarios;

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
            bool a = true;
            while (a){
                VerificaNotificacao();
                Thread.Sleep(3600000);
            }

            base.OnStop();
        }



        public void VerificaNotificacao()
        {
            //Thread.CurrentThread.Name = "verifica";
            Thread t1 = new Thread(new ThreadStart(run));
            t1.Start();

            void run()
            {
                
                TimeSpan comeco = new TimeSpan(14, 40, 00); //Define inicio do disparo da thread 00:00:00
                TimeSpan fim = new TimeSpan(15, 32, 00); //Define o fim do intervalo da thread
                TimeSpan agora = DateTime.Now.TimeOfDay; //Obtem a horario atual

                if ((agora >= comeco) && (agora < fim)) // condicao para exibir a notificacao dentro do intervalo definido
                {
                    

                    Bundle valueSend = new Bundle();
                    valueSend.PutString("sendContent", "Teste Sucesso"); //Destino do tapped da notificação

                    Intent newIntent = new Intent(this, typeof(Second_Activity));
                    newIntent.PutExtras(valueSend);

                    Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
                    stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(Second_Activity)));
                    stackBuilder.AddNextIntent(newIntent);

                    PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

                    NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                        .SetAutoCancel(true)
                        .SetContentIntent(resultPendingIntent)
                        .SetContentTitle("SESC TREINO") // Define titulo da notificação;
                        .SetSmallIcon(Resource.Drawable.icon) //Define ícone da notificacao;
                        .SetContentText("Você tem um novo aviso. Clique aqui"); // Texto da notificação

                    NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                    notificationManager.Notify(ButtonClickNotification, builder.Build());

                }


            }
         }

      }


}

