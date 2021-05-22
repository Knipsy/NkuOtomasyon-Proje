﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Specification;

namespace Core.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
