using System;
using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talisman.Domain.Abstract;
using Talisman.Domain.Entities;
using System.IO;

namespace Talisman.Domain.Concrete
{
    
    public class Result
    {
        private bool success;
        private string errmess;
        private object value; 
        public Result(bool succ, string err = null, object val=null)
        {
            success = succ;
            errmess = err;
            value = val;
        }
        public bool Success { get { return success; } }
        public string Errormessage { get { return errmess; } }
        public object Value { get { return value; } }

    }
    public class Result<T>
    {
        private bool success;
        private string errmess;
        private List<T> value;
        public Result(bool succ, List<T> list, string err = "")
        {
            success = succ;
            errmess = err;
            value = list;
        }
        public bool Success { get { return success; } }
        public string Errormessage { get { return errmess; } }
        public List<T> Value { get { return value; } }
    }
   

    
}

