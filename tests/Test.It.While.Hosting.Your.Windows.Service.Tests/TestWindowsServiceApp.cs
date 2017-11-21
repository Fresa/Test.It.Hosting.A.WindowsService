﻿using System;
using System.Linq;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests
{
    public class TestWindowsServiceApp
    {
        private static TestWindowsServiceApp _app;
        private readonly SimpleServiceContainer _serviceContainer;

        public TestWindowsServiceApp(Action<IServiceContainer> reconfigurer)
        {
            _serviceContainer = new SimpleServiceContainer();
            _serviceContainer.RegisterSingleton<ITestApp>(() => new TestApp());

            reconfigurer(_serviceContainer);
            _serviceContainer.Verify();
        }

        public int Start(params string[] args)
        {
            if (args.Any(s => s != "start"))
            {
                throw new Exception("No signal for start.");
            }

            var app = _serviceContainer.Resolve<ITestApp>();
            app.HaveStarted = true;
            return 0;
        }

        public static void Main(params string[] args)
        {
            _app = new TestWindowsServiceApp(container => { });
            _app.Start(args);
        }
    }
}