using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace MonoBench
{
    public class MonoCounterMonitor : MonoBehaviour
    {
        MonoMonitor monitor;

        // Use this for initialization
        void Start()
        {
            MonoBench.MonoCounterConfig.GetConfig();
            monitor = new MonoMonitor();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                monitor.GetCounterData();
                UnityEngine.Debug.Log("Getting data");
            }
        }
    }

    class MonoMonitor
    {
        private PerformanceCounterCategory[] categories;
        List<PerformanceCounter> perfCounters;

        public MonoMonitor()
        {
            perfCounters = new List<PerformanceCounter>();
            foreach (MonoBench.MonoCounterConfig.Counter c in MonoBench.MonoCounterConfig.Counters)
            {
                perfCounters.Add(new PerformanceCounter(c.categoryName, c.counterName, false));
            }
        }

        public void GetCounterData()
        {
            Thread thread = new Thread(new ThreadStart(GetData));
            thread.Start();
        }

        private void GetData()
        {
            try
            {
                foreach (PerformanceCounter counter in perfCounters)
                {
                    UnityEngine.Debug.Log("" + counter.CounterName + ": " + counter.RawValue.ToString());
                    UnityEngine.Debug.Log("Value of performance counter " + counter.CategoryName + "/" + counter.CounterName + ": " + counter.RawValue + ", Next Sample: " + counter.NextSample().RawValue + ", Next Value: " + counter.NextValue());

                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log("Exception :" + ex.Message);
                throw;
            }

            UnityEngine.Debug.Log("Thread going to bed............................");
            System.Threading.Thread.Sleep(1000);
            UnityEngine.Debug.Log("Thread going to waking.........................");
        }
    }
}