// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Web;

namespace MvcApplicationDemo.Services
{
    public interface IServerVariablesProvider
    {
        IEnumerable<KeyValuePair<string, string>> GetRequestData(HttpRequestBase request);
    }
}