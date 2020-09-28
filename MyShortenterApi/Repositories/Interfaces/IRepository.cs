﻿using System.Threading.Tasks;

namespace MyShortenterApi.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<bool> DoesKeyExist(string key);

        Task Add(string key, string url);

        Task<string> GetUrl(string key);
    }
}