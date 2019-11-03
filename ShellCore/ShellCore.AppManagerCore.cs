using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shell.Core
{
    public partial class ShellCore
    {
        public List<ShellTask> ShellTaskList;

        public ShellAppManager ShellAppManager;

        // starts a WPF Shell Task. (8.0.134.0+)

        // TODO: Multithreading. This will make it a lot easier to recover from crashes.
        public ShellTask InitTask(string[] Arguments, string WindowTitle, int sizeX, int sizeY) // Creates a shell task.
        {
            ShellTask ShellTask = new ShellTask(Arguments, "ShellTask", WindowTitle);


            ShellTaskList.Add(ShellTask);
            ShellAppManager.OpenTask(new Form[1] { ShellTask.WinForm });
			
            return ShellTask;
        }

        public ShellTask GetTaskByTid(int tid)
        {
            foreach (ShellTask ShellTask in ShellTaskList)
            {
                if (ShellTask.TaskID == tid) // does it equal the task id we have?
                {
                    // if so, return the task with that tid
                    return ShellTask;
                }
            }
            return null;
        }

        public void CloseTask(int tid)
        {
            ShellTask ShellTask = GetTaskByTid(tid);

            ShellTask.Close();
        }

    }

    public class ShellTask
    {
        public Application LocalApp { get; set; }
        public string[] Arguments { get; set; }
        public string Name { get; set; }
        public int TaskID { get; set; }
        public string WindowTitle { get; set; }
        public Form WinForm { get; set; }
        public Task TaskThread { get; set; } // Multithreaded Shell
        public delegate void InitSafe(Form form);
        public ShellTask(string[] Arguments, string Name, string WindowTitle)
        {
            Random Random = new Random();
            this.TaskID = Random.Next(100000, 999999);
            this.Arguments = Arguments;
            this.Name = Name;
            this.WindowTitle = WindowTitle;

        }
        public void Close()
        {
            this.Arguments = null;
            this.Name = null;
            this.TaskID = -1;
            this.WindowTitle = null;
            this.WinForm.Close();
        }


        public void AppInit()
        {
            WinForm = new Form();
            WinForm.Text = WindowTitle; // set the window title
            WinForm.Show();
            Application.Run(WinForm);
        }

        public void Run()
        {
            Action action = () => AppInit();
            TaskThread = new Task(action);
            // create a new thread and run
            TaskThread.Start(); // hopefully.
        }
    }

    // app context.
    public class ShellAppManager : ApplicationContext
    {
        internal int openTasks;

        public void OpenTask(params Form[] tasks)
        {
            foreach (Form shltask in tasks)
            {
                
                openTasks++;
            }
            
            
        }


    }
}
		
