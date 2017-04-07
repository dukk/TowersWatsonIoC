// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace MvcApplicationDemo.Models
{
    public class RequestData
    {
        public IEnumerable<KeyValuePair<string, string>> Data { get; set; }
    }
}