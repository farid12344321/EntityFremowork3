using Core.Entities;
using Data;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService
    {
        public AppDbContext _apdb;

        public ProductService()
        {
            _apdb = new AppDbContext();
        }


        public void Create(Product entity)
        {
            Brand brand = _apdb.Brands.Include(x => x.Products).FirstOrDefault(x => x.Id == entity.BrandId);

            if (brand == null) throw new EntityNotFoundException("Brand not found");

            _apdb.Products.Add(entity);
            _apdb.SaveChanges();
        }
        public Product GetById(int id)
        {
            Product entity = _apdb.Products.Include(x => x.Brand).FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Product not found");
            return entity;
        }

        public List<Product> GetAll()
        {
            return _apdb.Products.ToList();
        }


        public void Delete(int id)
        {
            Product entity = _apdb.Products.Find(id);

            if (entity == null) throw new EntityNotFoundException("Product not found");

            _apdb.Products.Remove(entity);
            _apdb.SaveChanges();
        }


        public void Update(int id, Product entity)
        {
            Product existEntity = _apdb.Products.Find(id);

            if (existEntity == null) throw new EntityNotFoundException("Product not found");

            if (entity.BrandId != existEntity.BrandId)
            {
                var group = _apdb.Brands.Include(x => x.Products).FirstOrDefault(x => x.Id == entity.BrandId);

                if (group == null) throw new EntityNotFoundException("Brand not found");
            }

            existEntity.BrandId = entity.BrandId;
            existEntity.Name = entity.Name;
            existEntity.Price = entity.Price;

            _apdb.SaveChanges();
        }

    }
}
