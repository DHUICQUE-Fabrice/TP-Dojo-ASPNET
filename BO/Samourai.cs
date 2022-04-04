using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Samourai
    {
        public int Id { get; set; }
        public int Force { get; set; }
        public string Nom { get; set; }
        public virtual Arme Arme { get; set; } = null;
    }
}
