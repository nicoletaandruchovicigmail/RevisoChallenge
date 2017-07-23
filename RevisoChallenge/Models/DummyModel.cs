using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RevisoChallenge.Models
{
    public class DummyModel
    {
        public DummyModel()
        {
                
        }
        public DummyModel(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}