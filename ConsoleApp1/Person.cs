using System.Collections.Generic;

namespace Sandbox
{
    public class Person
    {
        public Person()
        {
            Cars = new HashSet<Car>();
        }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
