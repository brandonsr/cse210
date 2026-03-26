class Program
{
    static void Main(string[] args)
    {
        // Order 1 — US customer
        Address address1 = new Address("123 Maple St", "Rexburg", "ID", "USA");
        Customer customer1 = new Customer("Alice Johnson", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Wireless Mouse", "WM-001", 29.99, 2));
        order1.AddProduct(new Product("USB Hub", "UH-042", 15.50, 1));
        order1.AddProduct(new Product("HDMI Cable", "HC-010", 9.99, 3));

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Order Total: ${order1.GetTotalCost():F2}");
        Console.WriteLine();

        // Order 2 — International customer
        Address address2 = new Address("45 Calle Principal", "Guatemala City", "Guatemala", "Guatemala");
        Customer customer2 = new Customer("Carlos Rivera", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Mechanical Keyboard", "KB-200", 79.99, 1));
        order2.AddProduct(new Product("Monitor Stand", "MS-305", 34.99, 2));

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Order Total: ${order2.GetTotalCost():F2}");
    }
}