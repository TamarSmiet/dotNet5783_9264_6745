using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    public Cart AddProductToCart(Cart cart, int id);
    public Cart UpdateProductInCart(Cart cart, int id, int newAmount);
    public void PlaceOrder (Cart cart);

}
