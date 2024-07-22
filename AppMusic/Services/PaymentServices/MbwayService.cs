namespace AppMusic.Services.PaymentServices
{
    class MbwayService : IPayment
    {
        public double fee(double amount)
        {
            return amount * 1.1;
        }

        public double tax(double amount)
        {
            return amount * 1.2;
        }
    }
}
