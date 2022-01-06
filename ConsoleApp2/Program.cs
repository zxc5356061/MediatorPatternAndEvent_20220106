using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    /// <summary>
    /// Mediator pattern using event.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            ICartService cart = new CartService();//Here to decide which class to be called
            IOrderService order = new OrderService();//Here to decide which class to be called
            IStockService stock = new StockService();//Here to decide which class to be called

            var mediator = new CartMediator(cart, order, stock);//call mediator and give it interfaces

            int productId = 55;
            cart.Checkout(productId);        
        }
    }

    public class CartMediator
    {
        private readonly ICartService _cart;
        private readonly IOrderService _order;
        private readonly IStockService _stock;

        public CartMediator(ICartService cart, IOrderService order, IStockService stock)//constructor
        {
            _order = order;
            _stock = stock;
            _cart = cart;

            _cart.RequestCheckout += _Cart_RequestCheckout;
        }

        private void _Cart_RequestCheckout(ICartService sender, int productId)
        {
            _order.PlaceOrder(productId);
            _stock.Update(productId, 1);
            _cart.EmptyCart();
            
        }
    }

    public class CartService : ICartService//implement interface
    {
        public event CheckoutEventHandler RequestCheckout;

        public void Checkout(int productId)
        {
            OnRequestCheckout(productId);
        }

        protected virtual void OnRequestCheckout(int productId)
        {
            if(RequestCheckout != null)
            {
                RequestCheckout(this, productId);
            }
        }

        public void EmptyCart()
        {
            Console.WriteLine("購物車已經被清空。");
        }
    }

    public class OrderService : IOrderService//implement interface
    {
        public void PlaceOrder(int productId)
        {
            Console.WriteLine($"OrderService建立了一筆商品編號{productId}的訂單。");
        }
    }

    public class StockService : IStockService//implement interface
    {
        public void Update(int productId, int quantity)
        {
            Console.WriteLine($"編號{productId}的商品數量要減少{quantity}個。");
        }
    }


    public delegate void CheckoutEventHandler(ICartService sender, int productId);//write delegate here to implement event
    public interface ICartService
    {
        event CheckoutEventHandler RequestCheckout;
        void Checkout(int productId);
        void EmptyCart();
    }

    public interface IOrderService
    {
        void PlaceOrder(int productId);
    }

    public interface IStockService
    {
        void Update(int productId, int v);
    }
}
