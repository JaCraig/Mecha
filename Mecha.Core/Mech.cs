using Fast.Activator;

namespace Mecha.Core
{
    /// <summary>
    /// Main class for breaking the
    /// </summary>
    public static class Mech
    {
        /// <summary>
        /// Breaks the specified target.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool Break<TClass>(TClass target)
        {
            return true;
        }

        public static bool Break<TClass>()
        {
            var ClassType = typeof(TClass);
            if (ClassType.IsAbstract && ClassType.IsSealed) //Static class
                return false;
            return Break(FastActivator.CreateInstance(ClassType));
        }
    }
}