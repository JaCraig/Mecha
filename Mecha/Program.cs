using System;
using System.Diagnostics.CodeAnalysis;

namespace Mecha
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class TempAttribute : Attribute
    { }

    internal class Program
    {
        public static void NullableTest([DisallowNull] string something)
        {
        }

        private static void Main(string[] args)
        {
            var Temp = new Exception[2];
            Temp[0] = ThrowTest(10);
            Temp[1] = ThrowTest(10);
            Console.WriteLine(SameException(Temp[0], Temp[1]));
            var Method = typeof(Program).GetMethod(nameof(NullableTest));
            var Parameters = Method?.GetParameters();
            var Attributes = Parameters?[0].Member.CustomAttributes;
            Console.WriteLine(Attributes);
        }

        private static bool SameException(Exception? exception1, Exception? exception2)
        {
            if (exception1 is null && exception2 is null)
                return true;
            if (exception1 is null || exception2 is null)
                return false;
            return exception1.StackTrace == exception2.StackTrace
                && exception1.Message == exception2.Message
                && SameException(exception1.InnerException, exception2.InnerException)
                && exception1.Source == exception2.Source;
        }

        private static Exception ThrowTest(int time)
        {
            try
            {
                throw new ArgumentException(nameof(time));
            }
            catch (Exception e) { return e; }
        }
    }
}