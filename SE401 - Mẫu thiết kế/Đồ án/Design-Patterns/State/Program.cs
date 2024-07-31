using System;

namespace State
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VendingMachineContext machine = new VendingMachineContext(new IdleState());
            bool flag = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Select mode:");
                Console.WriteLine("\t1. Insert money");
                Console.WriteLine("\t2. Select product");
                Console.WriteLine("\t3. Delete product");
                Console.WriteLine("\t4. Dispense product");
                Console.WriteLine("\t5. Refund money");
                Console.WriteLine("\n\t0. Exit");
                Console.WriteLine("\nSelect mode:");

                if (!int.TryParse(Console.ReadLine(), out int mode))
                {
                    Console.WriteLine("Invalid mode. Try again");
                }
                else switch (mode)
                    {
                        case 1:
                            machine.InsertMoney();
                            break;
                        case 2:
                            machine.SelectProduct();
                            break;
                        case 3:
                            machine.DeleteProduct();
                            break;
                        case 4:
                            machine.DispenseProduct();
                            break;
                        case 5:
                            machine.RefundMoney();
                            break;
                        case 0:
                            flag = false;
                            break;
                    }
                Console.ReadKey();
            }
            while (flag);
        }

        public class Product
        {
            public string Name { get; set; }
            public double Price { get; set; }
        }

        //State interface
        public interface IVendingMachineState
        {
            void SetContext(VendingMachineContext context);
            void InsertMoney();
            void SelectProduct();
            void DeleteProduct();
            void DispenseProduct();
            void RefundMoney();
        }

        //Context
        public class VendingMachineContext
        {
            private IVendingMachineState State { get; set; }
            public double Balance { get; set; }
            public Product Product { get; set; }

            public VendingMachineContext(IVendingMachineState state)
            {
                ChangeState(state);
            }

            public void ChangeState(IVendingMachineState state)
            {
                State = state;
                State.SetContext(this);
            }

            public void InsertMoney()
            {
                State.InsertMoney();
            }

            public void SelectProduct()
            {
                State.SelectProduct();
            }

            public void DeleteProduct()
            {
                State.DeleteProduct();
            }

            public void DispenseProduct()
            {
                State.DispenseProduct();
            }

            public void RefundMoney()
            {
                State.RefundMoney();
            }
        }

        //ConcreteState
        public class IdleState : IVendingMachineState
        {
            private VendingMachineContext Context { get; set; }

            public void DeleteProduct()
            {
                Console.WriteLine("Please insert money first.");
            }

            public void DispenseProduct()
            {
                Console.WriteLine("Please insert money and select product first.");
            }

            public void InsertMoney()
            {
                Console.WriteLine("How much? ");
                if (!int.TryParse(Console.ReadLine(), out int money))
                    Console.WriteLine("Money invalid");
                Console.WriteLine($"Inserted {money}");
                Context.Balance += money;
                Console.WriteLine($"Current balance: {Context.Balance}");
                Context.ChangeState(new HasMoney());
            }

            public void RefundMoney()
            {
                Console.WriteLine("No money to refund.");
            }

            public void SelectProduct()
            {
                Console.WriteLine("Please insert money first.");
            }

            public void SetContext(VendingMachineContext context)
            {
                Context = context;
            }
        }

        //ConcreteState
        public class HasMoney : IVendingMachineState
        {
            private VendingMachineContext Context { get; set; }

            public void DeleteProduct()
            {
                Console.WriteLine("Please select product first.");
            }

            public void DispenseProduct()
            {
                Console.WriteLine("Please select product first.");
            }

            public void InsertMoney()
            {
                Console.WriteLine("How much? ");
                if (!int.TryParse(Console.ReadLine(), out int money))
                    Console.WriteLine("Money invalid");
                Console.WriteLine($"Inserted {money}");
                Context.Balance += money;
                Console.WriteLine($"Current balance: {Context.Balance}");
            }

            public void RefundMoney()
            {
                Console.WriteLine($"Refunding: {Context.Balance}");
                Context.ChangeState(new IdleState());
            }

            public void SelectProduct()
            {
                Console.WriteLine("What product? ");
                string product = Console.ReadLine();
                Context.Product = new Product
                {
                    Name = product,
                    Price = new Random().Next(1, 11)
                };
                Console.WriteLine($"Selected product: {Context.Product.Name}. Price: {Context.Product.Price}");
                Context.ChangeState(new SelectedProduct());
            }

            public void SetContext(VendingMachineContext context)
            {
                Context = context;
            }
        }

        //ConcreteState
        public class SelectedProduct : IVendingMachineState
        {
            private VendingMachineContext Context { get; set; }

            public void DeleteProduct()
            {
                Console.WriteLine($"Removed product {Context.Product.Name}");
                Context.Product = null;
                Context.ChangeState(new HasMoney());
            }

            public void DispenseProduct()
            {
                if (Context.Balance >= Context.Product.Price)
                {
                    Context.Balance -= Context.Product.Price;
                    Console.WriteLine($"Dispensing product: {Context.Product.Name}. Price: {Context.Product.Price}.");
                    Console.WriteLine($"Current balance: {Context.Balance}.");
                    Context.Product = null;
                    if (Context.Balance > 0)
                        Context.ChangeState(new HasMoney());
                    else
                        Context.ChangeState(new IdleState());
                }
                else
                    Console.WriteLine($"The remaining: {Context.Balance} is insufficient to purchase the product: {Context.Product.Name}.");
            }

            public void InsertMoney()
            {
                Console.WriteLine("How much? ");
                if (!int.TryParse(Console.ReadLine(), out int money))
                    Console.WriteLine("Money invalid");
                Console.WriteLine($"Inserted {money}");
                Context.Balance += money;
                Console.WriteLine($"Current balance: {Context.Balance}");
            }

            public void RefundMoney()
            {
                Console.WriteLine($"Refunding: {Context.Balance}");
                Context.ChangeState(new IdleState());
            }

            public void SelectProduct()
            {
                Console.WriteLine("Product has already been selected.");
            }

            public void SetContext(VendingMachineContext context)
            {
                Context = context;
            }
        }
    }
}
