﻿using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;
using System.Threading;

namespace FRS.MT940Loader.Services
{
    partial class FRSMT940LoaderWinService : ServiceBase
    {
        public ServiceHost serviceHost = null;
        public FRSMT940LoaderWinService()
        {
            // Name the Windows Service
            ServiceName = "IST FRS.MT940Loader WindowService V1.0.0.0";
            InitializeComponent();
        }

        public static void Main()
        {
            //Run(new FRSMT940LoaderWinService());

            ServiceBase[] servicesToRun = new ServiceBase[] {
                new FRSMT940LoaderWinService()
            }
            ;
            if (Environment.UserInteractive)
            {
                RunInteractive(servicesToRun);
            }
            else
            {
                Run(servicesToRun);
            }
        }

        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            // Create a ServiceHost for the FRSMT940LoaderWCFService type and provide the base address.
            serviceHost = new ServiceHost(typeof(FRSMT940LoaderWCFService));

            // Open the ServiceHostBase to create listeners and start listening for messages.
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }

        static void RunInteractive(ServiceBase[] servicesToRun)
        {
            Console.WriteLine("Services running in interactive mode.");
            Console.WriteLine();

            MethodInfo onStartMethod = typeof(ServiceBase).GetMethod("OnStart",
                BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (ServiceBase service in servicesToRun)
            {
                Console.Write("Starting {0}...", service.ServiceName);
                onStartMethod.Invoke(service, new object[] { new string[] { } });
                Console.Write("Started");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(
                "Press any key to stop the services and end the process...");
            Console.ReadKey();
            Console.WriteLine();

            MethodInfo onStopMethod = typeof(ServiceBase).GetMethod("OnStop",
                BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (ServiceBase service in servicesToRun)
            {
                Console.Write("Stopping {0}...", service.ServiceName);
                onStopMethod.Invoke(service, null);
                Console.WriteLine("Stopped");
            }

            Console.WriteLine("All services stopped.");
            // Keep the console alive for a second to allow the user to see the message.
            Thread.Sleep(1000);
        }

    }
}
