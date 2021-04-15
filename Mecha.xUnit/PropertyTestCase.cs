using Mecha.Core;
using Mecha.Core.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
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
        /// <param name="methodDisplayOptions">The method display options.</param>
        /// <param name="testMethod">The test method this test case belongs to.</param>
        /// <param name="testMethodArguments">The arguments for the test method.</param>
        public PropertyTestCase(IMessageSink? diagnosticMessageSink,
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
        /// Gets or sets the cancellation token source.
        /// </summary>
        /// <value>The cancellation token source.</value>
        private CancellationTokenSource? CancellationTokenSource { get; set; }

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
        /// Gets or sets the message bus.
        /// </summary>
        /// <value>The message bus.</value>
        private IMessageBus? MessageBus { get; set; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        private Options? Options { get; set; }

        /// <summary>
        /// Gets or sets the output helper.
        /// </summary>
        /// <value>The output helper.</value>
        private TestOutputHelper? OutputHelper { get; set; }

        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        /// <value>The test.</value>
        private XunitTest? Test { get; set; }

        /// <summary>
        /// Gets or sets the timer.
        /// </summary>
        /// <value>The timer.</value>
        private ExecutionTimer? Timer { get; set; }

        /// <summary>
        /// Initializes the specified output helper.
        /// </summary>
        public void Init(IMessageBus messageBus, CancellationTokenSource cancellationTokenSource)
        {
            if (Canister.Builder.Bootstrapper is null)
            {
                lock (LockObject)
                {
                    if (Canister.Builder.Bootstrapper is null)
                    {
                        new ServiceCollection().AddCanisterModules(configure => configure?.RegisterMecha()?.AddAssembly(typeof(PropertyAttribute).Assembly));
                    }
                }
            }
            Manager = Canister.Builder.Bootstrapper?.Resolve<Check>();

            Test = new XunitTest(this, DisplayName);
            CancellationTokenSource = cancellationTokenSource;
            MessageBus = messageBus;
            OutputHelper = new TestOutputHelper();
            OutputHelper.Initialize(messageBus, Test);
            var RunTimeMethod = Method.ToRuntimeMethod();
            var PropertyAttribute = RunTimeMethod.GetCustomAttributes(typeof(PropertyAttribute), true).FirstOrDefault() as PropertyAttribute;
            Options = new Options()
            {
                GenerationCount = PropertyAttribute?.GenerationCount ?? 10,
                MaxDuration = PropertyAttribute?.MaxDuration ?? int.MaxValue,
                Verbose = PropertyAttribute?.Verbose ?? false,
                MaxShrinkCount = PropertyAttribute?.MaxShrinkCount ?? 10
            };
            if (Options.GenerationCount == 0)
                Options.GenerationCount = 10;
            if (Options.MaxDuration == 0)
                Options.MaxDuration = 1000;
            if (Options.MaxShrinkCount == 0)
                Options.MaxShrinkCount = 10;
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
            var TempMethod = Method.ToRuntimeMethod();

            Init(messageBus, cancellationTokenSource);
            Timer = new ExecutionTimer();

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

        /// <summary>
        /// Runs the test.
        /// </summary>
        /// <returns></returns>
        private RunSummary RunTest()
        {
            var RunMethod = TestMethod.Method.ToRuntimeMethod();
            object? Target = null;
            var TestClass = TestMethod.TestClass.Class.ToRuntimeType();
            if (!TestMethod.Method.IsStatic)
            {
                Target = Test.CreateTestClass(TestClass, TestMethodArguments, MessageBus, Timer, CancellationTokenSource);
            }
            Result? Result = null;

            Timer?.Aggregate(async () =>
            {
                Result = await Manager.RunAsync(RunMethod, Target, Options).ConfigureAwait(false);
            });
            if (Target is IDisposable disposable)
                disposable.Dispose();

            RunSummary ReturnValue = new RunSummary
            {
                Time = Result?.ExecutionTime ?? 0,
                Failed = Result?.Passed == true ? 0 : 1,
                Skipped = string.IsNullOrEmpty(SkipReason) ? 0 : 1,
                Total = 1
            };
            OutputHelper?.WriteLine(Result?.Output);

            IMessageSinkMessage ResultMessage = Result?.Passed == true
                ? new TestPassed(Test, Timer?.Total ?? 0, Result?.Output)
                : (IMessageSinkMessage)new TestFailed(Test, Timer?.Total ?? 0, Result?.Output, Result?.Exception);

            OutputHelper?.Uninitialize();
            MessageBus?.QueueMessage(ResultMessage);
            if (MessageBus?.QueueMessage(new TestFinished(Test, ReturnValue.Time, Result?.Output)) == false)
                CancellationTokenSource?.Cancel();
            if (MessageBus?.QueueMessage(new TestCaseFinished(this, ReturnValue.Time, ReturnValue.Total, ReturnValue.Failed, ReturnValue.Skipped)) == false)
                CancellationTokenSource?.Cancel();
            return ReturnValue;
        }
    }
}