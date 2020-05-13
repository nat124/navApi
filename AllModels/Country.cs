using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual List<User> Users { get; set; }
        public virtual List<State> States { get; set; }
        public virtual List<Shipping> ShippingAddresses { get; set; }

    }
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }

        public virtual List<User> Users { get; set; }
        public virtual Country Country { get; set; }
        public virtual List<Shipping> ShippingAddresses { get; set; }

    }
}
