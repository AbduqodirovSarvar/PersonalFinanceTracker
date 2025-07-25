﻿using Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Services.Redis
{
    public class RedisCacheService(IConnectionMultiplexer redis) : IRedisCacheService
    {
        private readonly IDatabase _db = redis.GetDatabase();

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, json, expiry);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value.HasValue
                ? JsonSerializer.Deserialize<T>(value!)
                : default;
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
    }
}
