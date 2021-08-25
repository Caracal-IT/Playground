using System;
using System.Threading.Tasks;

namespace Playground.PaymentEngine.Extensions {
    public static class WebExtensions {
        public static async Task<T> ExecuteAsync<T>(Func<Task<T>> executeAsync) {
            return await executeAsync();
        }
    }
}