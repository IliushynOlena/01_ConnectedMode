using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_CRUD_Interface
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public float Cost { get; set; }
        public string Producer { get; set; }
        public float Price { get; set; }
        public override string ToString()
        {
            return $"{Name,15}  {Type,15} {Quantity,5} {Cost,5} {Producer,15} {Price,5}";
        }
    }
}
