using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Domain.Models
{
    public class ImportFileCommand
    {
        public string Path { get; set; }
        public string Extension { get; set; }       
    }
}
