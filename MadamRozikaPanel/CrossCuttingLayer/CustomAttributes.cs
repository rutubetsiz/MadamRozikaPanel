using System;

namespace MadamRozikaPanel.CrossCuttingLayer
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DatabaseField : Attribute
    {
        public string Name { get; set; }
        public DatabaseField(string FieldName)
        {
            this.Name = FieldName;
        }
    }
}
