using Mirror;
using UnityEngine;

namespace 脚本.玩家.枪械
{
    public class Bag : NetworkBehaviour
    {
        [SerializeField] Weapon _mainWeapon;
        [SerializeField] Weapon _knife;
        public           Weapon MainWeapon => _mainWeapon;
        public           Weapon Knife      => _knife;
        
    }
}
