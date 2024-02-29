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
    public class BrandService
    {
        private AppDbContext _appDb;

        public BrandService()
        {
             _appDb = new AppDbContext();
        }

        public void Create(Brand entity)
        {
            if (_appDb.Brands.Any(x => x.Name == entity.Name))
                throw new EntityDublicateException("Brand already exists by no: " + entity.Name);

            _appDb.Brands.Add(entity);
            _appDb.SaveChanges();
        }


        public Brand GetById(int id)
        {
            var entity = _appDb.Brands.FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Brand not found");
            return entity;
        }


        public List<Brand> GetAll()
        {
            return _appDb.Brands.ToList();  
        }

        public void Delete(int id)
        {
            var entity = _appDb.Brands.FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Brand not found");
            _appDb.Brands.Remove(entity);
            _appDb.SaveChanges();
        }

        public void Update(int id, Brand entity)
        {
            var existEntity = _appDb.Brands.FirstOrDefault(x => x.Id == id);
            if (existEntity == null) throw new EntityNotFoundException("Brand not found");
            existEntity.Name = entity.Name;
            _appDb.SaveChanges();
        }
    }
}
