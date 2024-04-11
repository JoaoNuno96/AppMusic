namespace AppMusic.Services.PaymentServices
{
    class MbwayService : IPayment
    {
        public double Fee(double amount)
        {
            return amount * 1.1;
        }

        public double Tax(double amount)
        {
            return amount * 1.2;
        }
    }
}
