using Mirror;
using UnityEngine;

namespace Player.Weapon
{
    public class Bag : NetworkBehaviour
    {
        [SerializeField] global::Weapon _mainWeapon;
        [SerializeField] global::Weapon _knife;
        public           global::Weapon MainWeapon => _mainWeapon;
        public           global::Weapon Knife      => _knife;
        
    }
}
