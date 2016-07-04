using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace BackTask
{
    class BgTool
    {
        public const string taskName = "ExampleTask";
        public static BackgroundTaskRegistration RegisterBackgroundTask(
                                                string taskEntryPoint,
                                                string name,
                                                IBackgroundTrigger trigger,
                                                IBackgroundCondition condition)
        {

            //Check for existing registrations of this background task.



            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {

                if (cur.Value.Name == taskName)
                {
                    // 
                    // The task is already registered.
                    // 

                    return (BackgroundTaskRegistration)(cur.Value);
                }
            }


            //Register the background task.
            //if (TaskRequiresBackgroundAccess(name))
            //{
            //    // If the user denies access, the task will not run.
            //    var requestTask = BackgroundExecutionManager.RequestAccessAsync();
            //}
            var requestTask = BackgroundExecutionManager.RequestAccessAsync();
            var builder = new BackgroundTaskBuilder();

            builder.Name = name;
            builder.TaskEntryPoint = taskEntryPoint;
            builder.SetTrigger(trigger);

            if (condition != null)
            {

                builder.AddCondition(condition);
            }

            BackgroundTaskRegistration task = builder.Register();

            return task;
        }

        //private static bool TaskRequiresBackgroundAccess(string name)
        //{
        //    if ((name == "TimeTriggeredTask") ||
        //(name == "ApplicationTriggerTask"))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
