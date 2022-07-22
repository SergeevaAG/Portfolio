using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory2
{
    public class FullTable
    {
        public FullTable(string Id, string Name, string Desc, string Source, string Obj, string Conf, string Integ, string Access)
        {
            this.Id = Id;
            this.Name = Name;
            this.Desc = Desc;
            this.Source = Source;
            this.Obj = Obj;
            this.Conf = Conf;
            this.Integ = Integ;
            this.Access = Access;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Source { get; set; }
        public string Obj { get; set; }
        public string Conf { get; set; }
        public string Integ { get; set; }
        public string Access { get; set; }
    }
}
