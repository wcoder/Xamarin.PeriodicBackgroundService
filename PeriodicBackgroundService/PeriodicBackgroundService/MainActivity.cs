using Android;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace PeriodicBackgroundService.Android
{
	[Activity(Label = "PeriodicBackgroundService", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);
			
			button.Click += delegate
			{
				button.Text = string.Format("{0} clicks!", count++);
			};
					
			SetAlarmForBackgroundServices(this);
		}

		public static void SetAlarmForBackgroundServices(Context context)
		{
			var alarmIntent = new Intent(context.ApplicationContext, typeof(AlarmReceiver));
			var broadcast = PendingIntent.GetBroadcast(context.ApplicationContext, 0, alarmIntent, PendingIntentFlags.NoCreate);
			if (broadcast == null)
			{
				var pendingIntent = PendingIntent.GetBroadcast(context.ApplicationContext, 0, alarmIntent, 0);
				var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
				alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime(), 15000, pendingIntent);
			}
		}
	}
}


