using System;
using System.Collections.Generic;
using System.Text;

namespace BiKafaProject.Core.DTOs
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new List<string>();
        }
        public List<String> Errors { get; set; }

        public int status { get; set; }
    }
}
