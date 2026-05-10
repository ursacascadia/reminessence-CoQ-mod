using System;
using XRL;
using XRL.Core;
using XRL.Messages;

namespace XRL.World.Parts {

    [Serializable]
    public class UrsaCascadia_Memorum_Crystal : IPart
    {
        public static readonly string COMMAND_NAME = "CrushCrystal";

        public override bool WantEvent(int ID, int cascade)
        {
            if (!base.WantEvent(ID, cascade) && ID != GetInventoryActionsEvent.ID)
            {
                return ID == InventoryActionEvent.ID;
            }
            // else if (!base.WantEvent(ID, cascade) && ID != DeathEvent.ID) {
            //     return ID == DeathEvent.ID;
            // }
            return true;
        }

        public override bool HandleEvent(GetInventoryActionsEvent E)
        {
            E.AddAction("Crush", "crush", COMMAND_NAME, null, 'C', FireOnActor: false, 10);
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(InventoryActionEvent E)
        {
            if (E.Command == COMMAND_NAME)
            {
                CrushCrystal();
            }
            return base.HandleEvent(E);
        }

        public override bool AllowStaticRegistration()
        {
            return true;
        }

        public bool CrushCrystal()
        {
            // PlayWorldSound("Sounds/Interact/sfx_interact_curlingIron_press");
            if (XRL.XRLGame.LoadCurrentGame("Crystal") == null)
            {
                XRL.Messages.MessageQueue.AddPlayerMessage("[Debug: Command failed.]");
                return false;
            }
            return true;
        }

        // public override bool HandleEvent(DeathEvent E)
        // {
        //     CrushCrystal();
        //     return base.HandleEvent(E);
        // }
    }
}