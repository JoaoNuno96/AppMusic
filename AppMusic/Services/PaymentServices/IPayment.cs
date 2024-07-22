namespace AppMusic.Services.PaymentServices
{
    interface IPayment
    {
        public double tax(double amount);
        public double fee(double amount);
    }
}
