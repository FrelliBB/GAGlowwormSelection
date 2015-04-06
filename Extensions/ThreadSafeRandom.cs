using System;
using System.Threading;

namespace GlowwormSelection.Extensions
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random CurrentThreadRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }

        public static void InitializeSeed(int seed)
        {
            Local = new Random(seed);
        }
    }
}
