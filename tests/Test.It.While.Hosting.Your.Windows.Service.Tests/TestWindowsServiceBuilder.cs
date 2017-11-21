using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests
{
    public class TestWindowsServiceBuilder : DefaultWindowsServiceBuilder
    {
        public override IWindowsService Create(ITestConfigurer configurer)
        {
            var app = new TestWindowsServiceApp(configurer.Configure);

            return new TestConsoleApplicationWrapper(app);
        }

        private class TestConsoleApplicationWrapper : IWindowsService
        {
            private readonly TestWindowsServiceApp _app;

            public TestConsoleApplicationWrapper(TestWindowsServiceApp app)
            {
                _app = app;
            }
            
            public int Start(params string[] args)
            {
                return _app.Start(args);
            }

            public int Stop()
            {
                return 0;
            }
        }
    }
}