﻿using System;
using System.Collections.Generic;

namespace Test.Models;

public partial class User
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
