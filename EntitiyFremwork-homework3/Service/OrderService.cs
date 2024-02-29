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
    public class OrderService
    {
        public AppDbContext _apdb;


        public OrderService()
        {
            _apdb = new AppDbContext();
        }


        public void Create(Order entity)
        {
            Order order = _apdb.Orders.Include(x => x.Product).FirstOrDefault(x => x.Id == entity.ProductId);
            if (order == null) throw new EntityNotFoundException("Product not found");
            _apdb.Orders.Add(entity);
            _apdb.SaveChanges();
        }

        public Order GetById(int id)
        {
            Order entity = _apdb.Orders.Include(x => x.Product).FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Order not found");
            return entity;
        }

        public List<Order> GetAll()
        {
            return _apdb.Orders.Include(x => x.Product).ToList();
        }

        public void Update(int id, Order entity)
        {
            Order existEntity = _apdb.Orders.Find(id);

            if (existEntity == null) throw new EntityNotFoundException("Order not found");

            if (entity.ProductId != existEntity.ProductId)
            {
                var product = _apdb.Products.Include(x => x.Orders).FirstOrDefault(x => x.Id == entity.ProductId);

                if (product == null) throw new EntityNotFoundException("Product not found");
            }

            existEntity.ProductId = entity.ProductId;
            existEntity.Count = entity.Count;
            _apdb.SaveChanges();
        }

        public void Delete(int id)
        {
            Order entity = _apdb.Orders.Find(id);

            if (entity == null) throw new EntityNotFoundException("Order not found");

            _apdb.Orders.Remove(entity);
            _apdb.SaveChanges();
        }

    }
}
