using AirlineBookingSystem.Payments.Core.Entities;

namespace AirlineBookingSystem.Payments.Core.Repositories
{
    public interface IPaymentRepository
    {
        Task CreateAsync(Payment payment);
        Task RefundPaymentAsync(Guid id);
    }
}
