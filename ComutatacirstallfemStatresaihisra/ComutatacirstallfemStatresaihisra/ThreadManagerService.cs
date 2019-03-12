﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ComutatacirstallfemStatresaihisra
{
    /// <summary>
    ///  进程服务
    /// </summary>
    class ThreadManagerService
    {
        public void RunBackgroundTask(BackgroundTask task)
        {
            lock (_obj)
            {
                BackgroundTaskList.Add(task);
                Log("添加任务 " + task.Name);
            }
        }

        public void RemoveBackgroundTask(BackgroundTask task)
        {
            lock (_obj)
            {
                BackgroundTaskList.Remove(task);
                Log("删除");
            }
        }

        [Conditional("DEBUG")]
        private void Log(string str)
        {
            Debug.WriteLine(str);
        }

        public void Start()
        {
            if (_thread == null)
            {
                _thread = new Thread(Run);
                _thread.Start();
            }

            _isStop = false;
        }

        public void Stop()
        {
            _isStop = true;
        }

        private readonly object _obj = new object();

        private TimeSpan MaxDelayTime { get; } = TimeSpan.FromSeconds(1);
        private TimeSpan MinDelayTime { get; } = TimeSpan.FromMilliseconds(5);

        private TimeSpan[] WaitDelayTimeList { get; } = new TimeSpan[]
        {
            TimeSpan.FromMilliseconds(5),
            TimeSpan.FromMilliseconds(10),
            TimeSpan.FromMilliseconds(20),
            TimeSpan.FromMilliseconds(50),
            TimeSpan.FromMilliseconds(100),
            TimeSpan.FromMilliseconds(200),
            TimeSpan.FromMilliseconds(200),
            TimeSpan.FromMilliseconds(200),
            TimeSpan.FromMilliseconds(200),
            TimeSpan.FromMilliseconds(200),
        };

        private void Run()
        {
            try
            {
                int waitCount = 0;
                while (!_isStop)
                {
                    List<BackgroundTask> backgroundTaskList;

                    lock (_obj)
                    {
                        backgroundTaskList = BackgroundTaskList.ToList();
                    }

                    if (backgroundTaskList.Count == 0)
                    {
                        Console.WriteLine("没有任务自己暂停一下" + waitCount);
                        if (waitCount < WaitDelayTimeList.Length)
                        {
                            Thread.Sleep(WaitDelayTimeList[waitCount]);
                            waitCount++;
                        }
                        else
                        {
                            Thread.Sleep(MaxDelayTime);
                            waitCount++;
                        }

                        continue;
                    }

                    waitCount = 0;

                    var minDelayTime = TimeSpan.MaxValue;
                    foreach (var backgroundTask in backgroundTaskList)
                    {
                        var lastTime = (DateTime.Now - backgroundTask.LastStartTime);

                        if (lastTime >= backgroundTask.DelayTime)
                        {
                            backgroundTask.Action();

                            Console.WriteLine(
                                $"{DateTime.Now} {DateTime.Now.Millisecond} 运行 {backgroundTask.Name} 距离上次运行 {(lastTime).Milliseconds} 误差{lastTime.Milliseconds - backgroundTask.DelayTime.Milliseconds}");

                            backgroundTask.LastStartTime = DateTime.Now;

                            var delayTime = backgroundTask.DelayTime;
                            if (delayTime < minDelayTime)
                            {
                                minDelayTime = delayTime;
                            }
                        }
                        else
                        {
                            var delayTime = backgroundTask.DelayTime - (DateTime.Now - backgroundTask.LastStartTime);
                            if (delayTime < minDelayTime)
                            {
                                minDelayTime = delayTime;
                            }
                        }
                    }

                    if (minDelayTime > MaxDelayTime)
                    {
                        minDelayTime = MaxDelayTime;
                    }
                    else if (minDelayTime < MinDelayTime)
                    {
                        minDelayTime = MinDelayTime;
                    }

                    Thread.Sleep(minDelayTime);
                }
            }
            finally
            {
                _thread = null;
            }
        }

        private bool _isStop;

        private Thread _thread;

        private List<BackgroundTask> BackgroundTaskList { get; } = new List<BackgroundTask>();
    }
}