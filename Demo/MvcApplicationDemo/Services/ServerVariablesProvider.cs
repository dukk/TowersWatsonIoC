// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Web;

namespace MvcApplicationDemo.Services
{
    public class ServerVariablesProvider : IServerVariablesProvider
    {
        public IEnumerable<KeyValuePair<string, string>> GetRequestData(HttpRequestBase request)
        {
            foreach (var key in request.ServerVariables.AllKeys)
                yield return new KeyValuePair<string, string>(key, request.ServerVariables[key]);

            yield break;
        }
    }
}