namespace Playground.Core.Events {
    public interface EventHub {
        void Publish(string message);
    }
}