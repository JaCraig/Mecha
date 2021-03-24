using Mecha.Core;
using Mecha.Core.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Mecha.xUnit
{
    /// <summary>
    /// Property test case
    /// </summary>
    /// <seealso cref="XunitTestCase"/>
    public class PropertyTestCase : XunitTestCase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyTestCase"/> class.
        /// </summary>
        /// <param name="diagnosticMessageSink">The message sink used to send diagnostic messages</param>
        /// <param name="defaultMethodDisplay">Default method display to use (when not customized).</param>
        /// <param name="testMethod">The test method this test case belongs to.</param>
        /// <param name="testMethodArguments">The arguments for the test method.</param>
        public PropertyTestCase(IMessageSink diagnosticMessageSink,
                                TestMethodDisplay defaultMethodDisplay,
                                TestMethodDisplayOptions methodDisplayOptions,
                                ITestMethod testMethod,
                                object[]? testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, methodDisplayOptions, testMethod, testMethodArguments)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyTestCase"/> class.
        /// </summary>
        public PropertyTestCase()
            : base(null, TestMethodDisplay.ClassAndMethod, TestMethodDisplayOptions.None, null)
        {
        }

        /// <summary>
        /// Gets the lock object.
        /// </summary>
        /// <value>The lock object.</value>
        private object LockObject { get; } = new object();

        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <value>The manager.</value>
        private Check? Manager { get; set; }

        /// <summary>
        /// Initializes the specified output helper.
        /// </summary>
        /// <param name="outputHelper">The output helper.</param>
        public void Init()
        {
            if (Canister.Builder.Bootstrapper is null)
            {
                lock (LockObject)
                {
                    if (Canister.Builder.Bootstrapper is null)
                    {
                        new ServiceCollection().AddCanisterModules(configure => configure.RegisterMecha().AddAssembly(typeof(FuzzDataAttribute).Assembly));
                    }
                }
            }
            Manager = Canister.Builder.Bootstrapper?.Resolve<Check>();
        }

        /// <summary>
        /// Runs the asynchronous.
        /// </summary>
        /// <param name="diagnosticMessageSink">The diagnostic message sink.</param>
        /// <param name="messageBus">The message bus.</param>
        /// <param name="constructorArguments">The constructor arguments.</param>
        /// <param name="aggregator">The aggregator.</param>
        /// <param name="cancellationTokenSource">The cancellation token source.</param>
        /// <returns></returns>
        public override Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink, IMessageBus messageBus, object[] constructorArguments, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            var Test = new XunitTest(this, this.DisplayName);
            var Summary = new RunSummary
            {
                Total = 1
            };
            var OutputHelper = new TestOutputHelper();
            OutputHelper.Initialize(messageBus, Test);

            Init();
            var Timer = new ExecutionTimer();

            if (!messageBus.QueueMessage(new TestCaseStarting(this)))
                cancellationTokenSource.Cancel();

            if (!messageBus.QueueMessage(new TestStarting(Test)))
                cancellationTokenSource.Cancel();

            if (!string.IsNullOrEmpty(SkipReason))
            {
                if (!messageBus.QueueMessage(new TestSkipped(Test, SkipReason)))
                    cancellationTokenSource.Cancel();
                if (!messageBus.QueueMessage(new TestCaseFinished(this, 1, 0, 0, 1)))
                    cancellationTokenSource.Cancel();
                return Task.FromResult(new RunSummary { Skipped = 1, Total = 1 });
            }
            else
            {
                return Task.Run(RunTest);
            }
        }

        private RunSummary RunTest()
        {
            var RunMethod = TestMethod.Method.ToRuntimeMethod();
            object Target = null;
            var TestClass = TestMethod.TestClass.Class.ToRuntimeType();
            if (!TestMethod.Method.IsStatic)
            {
                Target = Test.CreateTestClass(TestClass, constructorArguments, messageBus, Timer, cancellationTokenSource);
            }
            Result Result = null;

            Timer.Aggregate(() => Manager.Run(RunMethod, Target, out Result));
            if (Target is IDisposable disposable)
                disposable.Dispose();

            RunSummary ReturnValue = new RunSummary
            {
                Time = Result?.ExecutionTime ?? 0,
                Failed = Result.Passed ? 0 : 1,
                Skipped = string.IsNullOrEmpty(SkipReason) ? 0 : 1,
                Total = 1
            };
            OutputHelper.WriteLine(Result.Output);

            IMessageSinkMessage ResultMessage = Result.Passed
                ? new TestPassed(Test, Timer.Total, Result.Output)
                : (IMessageSinkMessage)new TestFailed(Test, Timer.Total, Result.Output, Result.Exception);

            OutputHelper.Uninitialize();
            messageBus.QueueMessage(ResultMessage);
            if (!messageBus.QueueMessage(new TestFinished(Test, ReturnValue.Time, Result.Output)))
                cancellationTokenSource.Cancel();
            if (!messageBus.QueueMessage(new TestCaseFinished(this, ReturnValue.Time, ReturnValue.Total, ReturnValue.Failed, ReturnValue.Skipped)))
                cancellationTokenSource.Cancel();
            return ReturnValue
        }
    }
}