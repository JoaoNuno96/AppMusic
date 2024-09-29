namespace AppMusic.Services.PaymentServices
{
    interface IPayment
    {
        public double Tax(double amount);
        public double Fee(double amount);
    }
}
