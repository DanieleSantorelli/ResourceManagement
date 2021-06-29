using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsInspector.Data.Models.ApiModels
{
    public class ApiResponseBody
    {
        public int count { get; set; }

        public List<Object> value { get; set; }
    }
}
