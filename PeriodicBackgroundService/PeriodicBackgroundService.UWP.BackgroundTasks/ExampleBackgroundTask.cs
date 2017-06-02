using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace PeriodicBackgroundService.UWP.BackgroundTasks
{
	public sealed class ExampleBackgroundTask : IBackgroundTask
	{
		private BackgroundTaskDeferral _deferral;

		public async void Run(IBackgroundTaskInstance taskInstance)
		{
			_deferral = taskInstance.GetDeferral();


			// Run your background task code here

			for (int i = 0; i < 100; i++)
			{
				Debug.WriteLine($"[PeriodicBackgroundService] Background task working! i = {i}");
				await Task.Delay(TimeSpan.FromSeconds(1));
			}


			_deferral.Complete();
		}
	}
}
