namespace Playground.PaymentEngine.Api.Setup {
    public static class InfrastructureSetup {
        public static void Setup(WebApplicationBuilder builder) {
            DataSetup.Setup(builder);
            RoutingSetup.Setup(builder);
            CacheSetup.Setup(builder);
        }
    }
}