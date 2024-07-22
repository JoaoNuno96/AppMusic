namespace AppMusic.Services.PaymentServices
{
    class PaypalService : IPayment
    {
        public double tax(double amount)
        {
            return amount * 1.2;
        }

        public double fee(double amount)
        {
            return amount * 1.2;
        }
    }
}
