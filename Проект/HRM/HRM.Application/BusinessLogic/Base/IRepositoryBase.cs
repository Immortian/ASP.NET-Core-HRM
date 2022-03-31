﻿namespace HRM.Application.BuisnessLogic.Base
{
    public interface IRepositoryBase<T>
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
    }
}
