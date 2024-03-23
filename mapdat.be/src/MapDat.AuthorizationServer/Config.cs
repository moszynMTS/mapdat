// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthorizationServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("api1", "My API"),
                new ApiResource("IdentityServerApi", "IdentityServerApi"),
                new ApiResource("MapDatTestApi", "MapDatTestApi"),
                new ApiResource("BugReportingApi", "BugReportingApi")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = false,

                    RedirectUris = {"http://localhost:5002/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},

                    AllowedScopes = new List<string>{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },

                    AlwaysIncludeUserClaimsInIdToken = true,

                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },
                // LOCALHOST CONTROL PANEL CLIENT
                new Client
                {
                    ClientId = "identity_client",
                    ClientName = "Local identity client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,

                    RedirectUris =           { "http://localhost:4200/#/authorised/tasks" },
                    PostLogoutRedirectUris = { "http://localhost:4200/#/authorised/tasks" },
                    AllowedCorsOrigins =     { "http://localhost:4200" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    }
                },
                // MapDat TEST CONTROL PANEL CLIENT
                new Client
                {
                    ClientId = "MapDat_test_controlpanel",
                    ClientName = "MapDat test identity client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,

                    RedirectUris =           { "https://MapDat-identity-controlpanel.devitmcode.pl/#/authorised/tasks" },
                    PostLogoutRedirectUris = { "https://MapDat-identity-controlpanel.devitmcode.pl/#/authorised/tasks" },
                    AllowedCorsOrigins =     { "https://MapDat-identity-controlpanel.devitmcode.pl" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    }
                },
                // MapDat TEST CLIENT
                new Client
                {
                    ClientId = "MapDat_test",
                    ClientName = "MapDat test client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,

                    RedirectUris =           { "https://MapDat.devitmcode.pl/#/authorised/tasks", "http://localhost:4200/#/authorised/tasks"},
                    PostLogoutRedirectUris = { "https://MapDat.devitmcode.pl/#/authorised/tasks", "http://localhost:4200/#/authorised/tasks"},
                    AllowedCorsOrigins =     { "https://MapDat.devitmcode.pl", "http://localhost:4200"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "MapDatTestApi"
                    }
                },
                // BUG REPORTING CLIENT FOR MapDat
                new Client
                {
                    ClientId = "bug_reporting",
                    ClientName = "Bug reporting client for MapDat",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,

                    RedirectUris =           { "https://MapDat-bugreporting.devitmcode.pl/#/authorised/tasks"},
                    PostLogoutRedirectUris = { "https://MapDat-bugreporting.devitmcode.pl/#/authorised/tasks"},
                    AllowedCorsOrigins =     { "https://MapDat-bugreporting.devitmcode.pl"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "BugReportingApi"
                    }
                }
            };

        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("offline_access","Offline Access",new string[]{"offline_access" })
            };
    }
}