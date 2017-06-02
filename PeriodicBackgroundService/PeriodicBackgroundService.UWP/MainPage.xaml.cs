using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml.Controls;

namespace PeriodicBackgroundService.UWP
{
    public sealed partial class MainPage : Page
    {
	    private const string BackgroundTaskName = "MyBackgroundTask";


		public MainPage()
        {
            InitializeComponent();

			Register();
        }


		private async Task Register()
		{
			BackgroundExecutionManager.RemoveAccess();

			await BackgroundExecutionManager.RequestAccessAsync();

			var builder = new BackgroundTaskBuilder();

			builder.Name = BackgroundTaskName;
			builder.TaskEntryPoint = "PeriodicBackgroundService.UWP.BackgroundTasks.ExampleBackgroundTask";
			builder.SetTrigger(new SystemTrigger(SystemTriggerType.InternetAvailable, false));

			BackgroundTaskRegistration task = builder.Register();

			task.Completed += Task_Completed;

			Debug.WriteLine("[PeriodicBackgroundService] Background task registered");
		}

		private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
		{
			var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
			var key = BackgroundTaskName;
			var message = settings.Values[key];

			// Run your background task code here

			Debug.WriteLine("[PeriodicBackgroundService] Background task completed");
		}

		private void Unregister()
		{
			var taskName = BackgroundTaskName;

			foreach (var task in BackgroundTaskRegistration.AllTasks)
				if (task.Value.Name == taskName)
					task.Value.Unregister(true);
		}

		private bool IsRegistered()
		{
			var taskName = BackgroundTaskName;

			foreach (var task in BackgroundTaskRegistration.AllTasks)
				if (task.Value.Name == taskName)
					return true;

			return false;
		}
	}
}
