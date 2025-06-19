using Enums;
using Photon.Realtime;

namespace Auxiliars
{

    public class PickupRequest
    {
        public Player Player;
        public double Timestamp;
        public PowerUpType Type;
    }
}