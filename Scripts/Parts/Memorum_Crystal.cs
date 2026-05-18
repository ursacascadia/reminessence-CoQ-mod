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
            if (CrushCrystal()) {
                return false;
            }
            return base.HandleEvent(E);
        }

        public bool CrushCrystal()
        {
            // Check if reality is stabilized (enough for the tomb of the eaters high floors to stop the crystal, but only while held--tomb normality only applies to creatures.)
            if (XRL.UI.Options.GetOption("Option_UrsaCascadia_SaveTonic_NormalityAffected") == "Yes" && !IComponent<GameObject>.CheckRealityDistortionUsability(ParentObject, ParentObject.GetCurrentCell(), ParentObject.Holder, ParentObject, null, 35))
            {
                XRL.UI.Popup.ShowFail("A local normality lattice prevents destruction of a reminessence crystal.");
                return false;
            }
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