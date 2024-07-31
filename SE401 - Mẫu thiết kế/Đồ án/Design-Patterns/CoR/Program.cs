using System;

namespace CoR
{
    // Base Handler
    public abstract class PurchaseHandler
    {
        protected PurchaseHandler nextHandler;

        public void SetNextHandler(PurchaseHandler handler)
        {
            nextHandler = handler;
        }

        public virtual void HandleRequest(PurchaseRequest request)
        {
            if (nextHandler != null)
            {
                nextHandler.HandleRequest(request);
            }
        }
    }

    // Purchase Request
    public class PurchaseRequest
    {
        public string ProductName { get; set; }
        public double Amount { get; set; }

        public PurchaseRequest(string productName, double amount)
        {
            ProductName = productName;
            Amount = amount;
        }
    }

    // Concrete Handlers
    public class ManagerHandler : PurchaseHandler
    {
        public override void HandleRequest(PurchaseRequest request)
        {
            if (request.Amount <= 1000)
            {
                Console.WriteLine("Manager approves the purchase of " + request.ProductName);
            }
            else
            {
                base.HandleRequest(request);
            }
        }
    }

    public class DirectorHandler : PurchaseHandler
    {
        public override void HandleRequest(PurchaseRequest request)
        {
            if (request.Amount > 1000 && request.Amount <= 5000)
            {
                Console.WriteLine("Director approves the purchase of " + request.ProductName);
            }
            else
            {
                base.HandleRequest(request);
            }
        }
    }

    public class CEOHandler : PurchaseHandler
    {
        public override void HandleRequest(PurchaseRequest request)
        {
            if (request.Amount > 5000)
            {
                Console.WriteLine("CEO approves the purchase of " + request.ProductName);
            }
            else
            {
                base.HandleRequest(request);
            }
        }
    }

    // Client
    public class Client
    {
        public void ProcessPurchaseRequest(PurchaseRequest request, PurchaseHandler handler)
        {
            handler.HandleRequest(request);
        }
    }

    // Usage
    public class Program
    {
        public static void Main(string[] args)
        {
            PurchaseHandler managerHandler = new ManagerHandler();
            PurchaseHandler directorHandler = new DirectorHandler();
            PurchaseHandler ceoHandler = new CEOHandler();

            managerHandler.SetNextHandler(directorHandler);
            directorHandler.SetNextHandler(ceoHandler);

            Client client = new Client();

            PurchaseRequest request1 = new PurchaseRequest("Laptop", 800);
            client.ProcessPurchaseRequest(request1, managerHandler); // Manager approves the purchase of Laptop

            PurchaseRequest request2 = new PurchaseRequest("Conference Table", 3000);
            client.ProcessPurchaseRequest(request2, managerHandler); // Director approves the purchase of Conference Table

            PurchaseRequest request3 = new PurchaseRequest("Server Rack", 10000);
            client.ProcessPurchaseRequest(request3, managerHandler); // CEO approves the purchase of Server Rack
        }
    }
}
