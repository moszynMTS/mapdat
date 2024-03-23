﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace IdentityServer4.Quickstart.UI
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "Pole Login jest wymagane")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Pole Hasło jest wymagane")]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}