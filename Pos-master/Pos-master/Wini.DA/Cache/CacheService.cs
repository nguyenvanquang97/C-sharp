// ---------------------------------------------------
// <copyright file="CacheService.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

namespace Wini.DA.Cache
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using DocumentFormat.OpenXml.EMMA;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using StackExchange.Redis;


    /// <summary>
    /// Cache redis.
    /// </summary>
    public class CacheService : ICacheService
    {

        private IDatabase _db;

        public CacheService()
        {
            ConfigureRedis();
        }

        private void ConfigureRedis()
        {
            _db = ConnectionHelper.Connection.GetDatabase();


        }


        /// <inheritdoc/>
        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        /// <inheritdoc/>
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }

        /// <inheritdoc/>
        public object RemoveData(string key)
        {
            bool isKeyExist = _db.KeyExists(key);

            if (isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }


        public async Task RemoveWithWildCardAsync(string keyRoot)
        {
            await foreach (var key in GetKeysAsync(keyRoot + "*"))
            {
                _db.KeyDelete(key);
            }
        }

        private async IAsyncEnumerable<string> GetKeysAsync(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(pattern));
            var endpoints = ConnectionHelper.Connection.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = ConnectionHelper.Connection.GetServer(endpoint);
                await foreach (var key in server.KeysAsync(pattern: pattern))
                {
                    yield return key.ToString();
                }
            }
        }

    }
}
