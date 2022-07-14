namespace HotelBooking.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(IApplicationBuilder app)
        {
            return app;
        }
    }
}