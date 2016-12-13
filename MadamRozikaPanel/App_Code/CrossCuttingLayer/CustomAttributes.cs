using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DatabaseField : Attribute
    {
        public string Name { get; set; }
        public DatabaseField(string FieldName)
        {
            this.Name = FieldName;
        }
    }
