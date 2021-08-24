using OnlineShopping.Data.Repositories;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Data.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        public ProjectContext Context = null;
        private IRepository<Admin> AdminRepository;
        private IRepository<Customer> CustomerRepository;
        private IRepository<Item> ItemRepository;
        private IRepository<UnitOfMeasure> UnitOfMeasureRepository;
        private IRepository<Tax> TaxRepository;
        private IRepository<Discount> DiscountRepository;
        private IRepository<Cart> CartRepository;
        private IRepository<CartItem> CartItemRepository;

        public IRepository<Admin> _AdminRepository 
        {
            get
            {
                if (AdminRepository == null)
                {
                    AdminRepository = new GenericRepository<Admin>(Context);
                }
                return AdminRepository;
            }
        }


        public IRepository<Customer> _CustomerRepository
        {
            get
            {
                if (CustomerRepository == null)
                {
                    CustomerRepository = new GenericRepository<Customer>(Context);
                }
                return CustomerRepository;
            }
        }
        public IRepository<Item> _ItemRepository
        {
            get
            {
                if (ItemRepository == null)
                {
                    ItemRepository = new GenericRepository<Item>(Context);
                }
                return ItemRepository;
            }
        }
        public IRepository<UnitOfMeasure> _UnitOfMeasureRepository
        {
            get
            {
                if (UnitOfMeasureRepository == null)
                {
                    UnitOfMeasureRepository = new GenericRepository<UnitOfMeasure>(Context);
                }
                return UnitOfMeasureRepository;
            }
        }
        public IRepository<Tax> _TaxRepository
        {
            get
            {
                if (TaxRepository == null)
                {
                    TaxRepository = new GenericRepository<Tax>(Context);
                }
                return TaxRepository;
            }
        }
        public IRepository<Discount> _DiscountRepository
        {
            get
            {
                if (DiscountRepository == null)
                {
                    DiscountRepository = new GenericRepository<Discount>(Context);
                }
                return DiscountRepository;
            }
        }
        public IRepository<Cart> _CartRepository
        {
            get
            {
                if (CartRepository == null)
                {
                    CartRepository = new GenericRepository<Cart>(Context);
                }
                return CartRepository;
            }
        }
        public IRepository<CartItem> _CartItemRepository
        {
            get
            {
                if (CartItemRepository == null)
                {
                    CartItemRepository = new GenericRepository<CartItem>(Context);
                }
                return CartItemRepository;
            }
        }

        public UnitOfWork(ProjectContext context)
        {
            Context = context;
        }

        public bool SaveChanges()
        {
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var result = await Context.SaveChangesAsync();
                return Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
