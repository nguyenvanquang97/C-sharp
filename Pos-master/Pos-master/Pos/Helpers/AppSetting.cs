// ---------------------------------------------------
// <copyright file="AppSetting.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

using System.Collections.Generic;

namespace Pos.Helpers
{
    /// <summary>
    /// model setting from application.json.
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// Gets or sets key for encrypt jwt.
        /// </summary>
        public string JwtKey { get; set; }

        public List<int> RoldAdminIds { get; set; }
    }
}
