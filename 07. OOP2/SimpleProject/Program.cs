namespace SimpleProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FoodOrder foodOrder = new FoodOrder()
            {
                Id = 1,
                Price = 200
            };

            foodOrder.Print();
            foodOrder.CalculateDliveryFee();
            foodOrder.Deliver();

            Console.WriteLine("---------");

            ProductOrder productOrder = new ProductOrder()
            {
                Id = 2,
                Price = 500
            };

            productOrder.Print();
            productOrder.CalculateDliveryFee();
            productOrder.Deliver();


        }
    }

    abstract class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public void Print()
        {
            Console.WriteLine($"Order: {Id}");
            Console.WriteLine($"Price: {Price}");
        }

        public abstract void CalculateDliveryFee();
    }

    interface IDeliverable
    {
        void Deliver();
    }

    class FoodOrder : Order, IDeliverable
    {
        public override void CalculateDliveryFee()
        {
            Console.WriteLine("Food Delivery Fee: 30 EGP");
        }

        public void Deliver()
        {
            Console.WriteLine("Food order Deliverd");
        }
    }

    class ProductOrder : Order, IDeliverable
    {
        public override void CalculateDliveryFee()
        {
            Console.WriteLine("Food Delivery Fee: 50 EGP");
        }

        public void Deliver()
        {
            Console.WriteLine("Product Order Deliverd");
        }
    }
}
