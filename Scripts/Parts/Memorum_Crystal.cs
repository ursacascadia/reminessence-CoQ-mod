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
            if (ID == GetInventoryActionsEvent.ID){
                return true;
            }
            if (ID == InventoryActionEvent.ID)
            {
                return true;
            }
            if (ID == BeforeDieEvent.ID)
            {
                return true;
            }
            if (ID == TakenEvent.ID)
            {
                return true;
            }
            if (ID == DroppedEvent.ID)
            {
                return true;
            }
            return base.WantEvent(ID, cascade);
        }

        public override bool HandleEvent(GetInventoryActionsEvent E)
        {
            E.AddAction("Crush", "Crush", COMMAND_NAME, null, 'C', FireOnActor: false, 10);
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(InventoryActionEvent E)
        {
            if (E.Command == COMMAND_NAME)
            {
                CrushCrystal();
                E.RequestInterfaceExit();
            }
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(TakenEvent E)
        {
            E.Actor.RegisterEvent(this, BeforeDieEvent.ID);
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(DroppedEvent E)
        {
            E.Actor.UnregisterEvent(this, BeforeDieEvent.ID);
            return base.HandleEvent(E);
        }

        public override bool AllowStaticRegistration()
        {
            return true;
        }
        
        // public override void Register(GameObject Object, IEventRegistrar Registrar)
        // {
        //     Registrar.Register("BeforeDie");
        //     base.Register(Object, Registrar);
        // }

        // public override bool FireEvent(Event E)
        // {
        //     if (E.ID == "BeforeDie")
        //     {
        //         return CrushCrystal();
        //     }
        //     return base.FireEvent(E);
        // }

        public override bool HandleEvent(BeforeDieEvent E)
        {
            // XRL.Messages.MessageQueue.AddPlayerMessage("[Debug: Died, so activating crystal.]");
            CrushCrystal();
            return false;
        }

        public bool CrushCrystal()
        {
            // XRL.Messages.MessageQueue.AddPlayerMessage("[Debug: Crystal crush attempt.]");
            // PlayWorldSound("Sounds/Interact/sfx_interact_curlingIron_press");
            if (XRL.XRLGame.LoadCurrentGame("Crystal") == null)
            {
                XRL.UI.Popup.ShowFail("The crystal disentegrates... but nothing happens.");
                return false;
            }
            XRL.UI.Popup.Show("The crystal disentegrates. Reminiscense flashes before your eyes.");
            return true;
        }
    }
}