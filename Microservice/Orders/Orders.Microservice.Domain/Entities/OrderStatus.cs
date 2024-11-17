namespace Orders.Microservice.Domain.Entities
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Packaged,
        Performed,
        Canceled
    }
}
