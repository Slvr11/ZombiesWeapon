using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using InfinityScript;

namespace CustomWeapon
{
    public class CustomWeapon : BaseScript
    {
        public CustomWeapon()
        {
            PlayerConnected += CustomWeapon_PlayerConnected;
        }

        void CustomWeapon_PlayerConnected(Entity obj)
        {
            obj.SpawnedPlayer += () => OnPlayerSpawned(obj);
            obj.TakeWeapon("iw5_ump45_mp");
            obj.GiveWeapon("scrambler_mp");
        }
        public void OnPlayerSpawned(Entity player)
        {
            if (player.IsAlive && player.GetField<string>("sessionteam") == "axis")
            {
                player.TakeWeapon("iw5_ump45_mp");
                player.SetPerk("specialty_marathon", true, true);
                player.GiveWeapon("scrambler_mp");
                OnInterval(50, () =>
                    {
                        if (player.CurrentWeapon == "" || player.CurrentWeapon == null)
                        {
                            player.GiveWeapon("scrambler_mp");
                            player.SwitchToWeaponImmediate("scrambler_mp");
                        }
                        return true;
                    });
                AfterDelay(100, () =>
                    player.SwitchToWeaponImmediate("scrambler_mp"));
                //failsafe give. Sometimes initial give doesn't compute
                player.Call("setmovespeedscale", new Parameter((float)0.8));
            }
            else if (player.GetField<string>("sessionteam") == "axis")
            {
                player.TakeWeapon("iw5_ump45_mp");
                player.GiveWeapon("scrambler_mp");
                player.SwitchToWeaponImmediate("scrambler_mp");
            }
        }
    }
}

