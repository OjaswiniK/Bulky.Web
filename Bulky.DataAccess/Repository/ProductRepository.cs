using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductReporsitory
    {
        private ApplicationDBContext _dbContext;       

        public ProductRepository(ApplicationDBContext dBContext) : base(dBContext) 
        {        
            _dbContext = dBContext;          
        }        

        public void Update(Product obj)
        {
            _dbContext.Products.Update(obj);
        }
    }
}
