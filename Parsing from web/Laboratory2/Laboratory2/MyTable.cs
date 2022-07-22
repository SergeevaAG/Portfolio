using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory2
{
    public class MyTable
    {
        public MyTable(string Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
