﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Assignment.Domain.Models
{
    public class Result
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
