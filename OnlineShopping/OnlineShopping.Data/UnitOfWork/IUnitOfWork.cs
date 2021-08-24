using OnlineShopping.Data.Repositories;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
        bool SaveChanges();
        void Dispose(bool disposing);

        IRepository<Admin> _AdminRepository { get; }
        IRepository<Customer> _CustomerRepository { get; }
        IRepository<Item> _ItemRepository { get; }
        IRepository<UnitOfMeasure> _UnitOfMeasureRepository { get; }
        IRepository<Tax> _TaxRepository { get; }
        IRepository<Discount> _DiscountRepository { get; }
        IRepository<Cart> _CartRepository { get; }
        IRepository<CartItem> _CartItemRepository { get; }
    }
}
