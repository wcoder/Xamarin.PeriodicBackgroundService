using Android.Content;

namespace PeriodicBackgroundService
{
	[BroadcastReceiver]
	class AlarmReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			var backgroundServiceIntent = new Intent(context, typeof(PeriodicBackgroundService));
			context.StartService(backgroundServiceIntent);
		}
	}
}

