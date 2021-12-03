using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }

        // In our example, we want the Weapon to be the dependent side of the relationship. 
        // There must be a Character for this Weapon. It cannot exist without one. 
        // The Character on the other hand can exist without a Weapon.
        public Character Character { get; set; }
        public int CharacterId { get; set; }
    }
}