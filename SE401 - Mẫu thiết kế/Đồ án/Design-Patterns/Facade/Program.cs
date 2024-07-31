using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    internal class Program
    {
        //client
        static void Main(string[] args)
        {
            ShopFacade.getInstance().buyProductByCashWithFreeShipping("1234@gmail.com");
            ShopFacade.getInstance().buyProductByPaypalWithStandardShipping("5678@gmail.com");
        }

        //subsystem
        public class AccountService
        {
            public void GetAccout(string email)
            {
                Console.WriteLine("Getting the account of " + email);
            }
        }

        public class EmailService
        {
            public void SendMail(string mailTo)
            {
                Console.WriteLine("Sending an email to " + mailTo);
            }
        }

        public class PaymentService
        {
            public void PaymentByPaypal()
            {
                Console.WriteLine("Payment by Paypal");
            }
            public void PaymentByCreditCard()
            {
                Console.WriteLine("Payment by Credit Card");
            }
            public void PaymentByEBankingAccount()
            {
                Console.WriteLine("Payment by E-banking account");
            }
            public void PaymentByCash()
            {
                Console.WriteLine("Payment by cash");
            }
        }

        public class ShippingService
        {
            public void FreeShipping()
            {
                Console.WriteLine("Free Shipping");
            }

            public void StandardShipping()
            {
                Console.WriteLine("Standard Shipping");
            }

            public void ExpressShipping()
            {
                Console.WriteLine("Express Shipping");
            }
        }

        //facade
        public class ShopFacade
        {
            private static ShopFacade _instance;

            private AccountService accountService;
            private PaymentService paymentService;
            private ShippingService shippingService;
            private EmailService emailService;

            private ShopFacade()
            {
                accountService = new AccountService();
                paymentService = new PaymentService();
                shippingService = new ShippingService();
                emailService = new EmailService();
            }

            public static ShopFacade getInstance()
            {
                if (_instance == null)
                    _instance = new ShopFacade();
                return _instance;
            }

            public void buyProductByCashWithFreeShipping(string email)
            {
                accountService.GetAccout(email);
                paymentService.PaymentByCash();
                shippingService.FreeShipping();
                emailService.SendMail(email);
                Console.WriteLine("Done\n");
            }

            public void buyProductByPaypalWithStandardShipping(string email)
            {
                accountService.GetAccout(email);
                paymentService.PaymentByPaypal();
                shippingService.StandardShipping();
                emailService.SendMail(email);
                Console.WriteLine("Done\n");
            }
        }
    }
}
