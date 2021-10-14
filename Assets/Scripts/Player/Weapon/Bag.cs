using Mirror;
using UnityEngine;

namespace MobileFPS.PlayerWeapon
{
    public class Bag : NetworkBehaviour
    {
        [SerializeField] Weapon _mainWeapon;

        public Weapon MainWeapon => _mainWeapon;
    }
}
