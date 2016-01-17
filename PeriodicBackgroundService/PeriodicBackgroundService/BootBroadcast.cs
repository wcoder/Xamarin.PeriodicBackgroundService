using Android.App;
using Android.Content;

namespace PeriodicBackgroundService
{
	[BroadcastReceiver]
	[IntentFilter(new[] { Intent.ActionBootCompleted })]
	public class BootBroadcast : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			MainActivity.SetAlarmForBackgroundServices(context);
		}
	}
}

