﻿using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thinktecture.IdentityServer.Core.Extensions
{
    public static class OwinExtensions
    {
        public static string GetBaseUrl(this IDictionary<string, object> env, string host = null)
        {
            var ctx = new OwinContext(env);
            var request = ctx.Request;
            
            if (host.IsMissing())
            {
                host = "https://" + request.Host.Value;
            }

            var baseUrl = new Uri(new Uri(host), ctx.Request.PathBase.Value).AbsoluteUri;
            if (!baseUrl.EndsWith("/")) baseUrl += "/";

            return baseUrl;
        }

        public static string GetIdentityServerLogoutUrl(this IDictionary<string, object> env)
        {
            return env.GetIdentityServerBaseUrl() + Constants.RoutePaths.Logout;
        }
        
        public static string GetIdentityServerBaseUrl(this IDictionary<string, object> env)
        {
            return env[Constants.OwinEnvironment.IdentityServerBaseUrl] as string;
        }

        public static void SetIdentityServerBaseUrl(this IDictionary<string, object> env, string value)
        {
            env[Constants.OwinEnvironment.IdentityServerBaseUrl] = value;
        }
    }
}