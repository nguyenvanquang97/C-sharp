// ---------------------------------------------------
// <copyright file="ICacheService.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

namespace Wini.DA.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Cache service.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Get Data using key.
        /// </summary>
        /// <typeparam name="T">Type of class.</typeparam>
        /// <param name="key">key unique store in cache.</param>
        /// <returns>object of T.</returns>
        T GetData<T>(string key);

        /// <summary>
        /// Set Data with Value and Expiration Time of Key.
        /// </summary>
        /// <typeparam name="T">Type of class.</typeparam>
        /// <param name="key">key unique store in cache.</param>
        /// <param name="value">instan stored.</param>
        /// <param name="expirationTime">time expire.</param>
        /// <returns>status.</returns>
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);

        /// <summary>
        /// Remove Data.
        /// </summary>
        /// <param name="key">key unique store in cache.</param>
        /// <returns>object.</returns>
        object RemoveData(string key);

        Task RemoveWithWildCardAsync(string key);
    }
}
