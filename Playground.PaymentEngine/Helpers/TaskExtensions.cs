using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.PaymentEngine.Helpers {
    public static class TaskExtensions {
        public static Task WhenAll(this IEnumerable<Task> tasks, int maxConcurrentRequests) {
            SemaphoreSlim semaphore = new(initialCount: 1, maxConcurrentRequests);
            return Task.WhenAll(tasks.Select(Process));

            async Task Process(Task task) {
                await semaphore.WaitAsync();
                try { await Task.WhenAll(task); }
                finally { semaphore.Release(); }
            }
        }
        
        public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks, int maxConcurrentRequests) {
            SemaphoreSlim semaphore = new(initialCount: 1, maxConcurrentRequests);
            return Task.WhenAll(tasks.Select(Process));

            async Task<T> Process(Task<T> task) {
                await semaphore.WaitAsync();
                try { return await task; }
                finally { semaphore.Release(); }
            }
        }
    }
}