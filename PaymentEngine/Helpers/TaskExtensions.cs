using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentEngine.Helpers {
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
    }
}