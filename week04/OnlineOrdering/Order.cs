using System.Collections.Generic;
using System.Text;

public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetTotalCost()
    {
        double total = 0;
        foreach (Product p in _products)
        {
            total += p.GetTotalCost();
        }

        double shippingCost = _customer.LivesInUSA() ? 5.0 : 35.0;
        return total + shippingCost;
    }

    public string GetPackingLabel()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("--- Packing Label ---");
        foreach (Product p in _products)
        {
            sb.AppendLine($"{p.GetName()} (ID: {p.GetProductId()})");
        }
        return sb.ToString();
    }

    public string GetShippingLabel()
    {
        return $"--- Shipping Label ---\n{_customer.GetShippingLabel()}";
    }
}