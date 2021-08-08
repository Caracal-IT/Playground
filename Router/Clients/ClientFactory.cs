namespace Router.Clients {
    public interface ClientFactory {
        Client Create(string name);
    }
}