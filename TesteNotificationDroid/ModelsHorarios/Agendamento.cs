using System;



namespace TesteNotificationDroid.ModelsHorarios
{
    public class Agendamento
    {

        TimeSpan comeco = new TimeSpan(01, 37, 00);
        TimeSpan fim = new TimeSpan(01, 37, 01);
        TimeSpan agora = DateTime.Now.TimeOfDay;

    }
}
