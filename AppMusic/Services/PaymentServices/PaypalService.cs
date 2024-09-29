namespace AppMusic.Services.PaymentServices
{
    class PaypalService : IPayment
    {
        public double Tax(double amount)
        {
            return amount * 1.2;
        }

        public double Fee(double amount)
        {
            return amount * 1.2;
        }
    }
}
