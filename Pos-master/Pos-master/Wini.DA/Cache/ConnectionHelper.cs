// ---------------------------------------------------
// <copyright file="ConnectionHelper.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

namespace Wini.DA.Cache
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using StackExchange.Redis;

    public class ConnectionHelper
    {
        static ConnectionHelper()
        {
            ConnectionHelper._lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                // TODO: get from appsetting.json
                return ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisURL"]);
            });
        }
        private static Lazy<ConnectionMultiplexer> _lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyConnection.Value;
            }
        }
    }
    static class ConfigurationManager
    {
        public static IConfiguration AppSetting
        {
            get;
        }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
