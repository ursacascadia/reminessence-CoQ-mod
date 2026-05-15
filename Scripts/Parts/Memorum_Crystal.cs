using System;
using XRL;
using XRL.Core;
using XRL.Messages;

namespace XRL.World.Parts {

    [Serializable]
    public class UrsaCascadia_Memorum_Crystal : IPart
    {
        bool IsRealityDistortionBased = true;
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

        public override bool HandleEvent(BeforeDieEvent E)
        {
            CrushCrystal();
            return false;
        }

        public bool CrushCrystal()
        {
            // if (!IComponent<GameObject>.CheckRealityDistortionUsability(ParentObject, null, ParentObject.Holder, ParentObject))
            // {
            //     XRL.UI.Popup.ShowFail("Reminessence crystal affected by normality.");
            //     return false;
            // } Doesnt work.
            if (XRL.XRLGame.LoadCurrentGame("Crystal") == null)
            {
                XRL.UI.Popup.ShowFail("The crystal resists breaking. Nothing happens.");
                return false;
            }
            XRL.UI.Popup.Show("The crystal disentegrates. Reminiscense flashes before your eyes.");
            return true;
        }
    }
}