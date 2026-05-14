using System;
using XRL;
using XRL.Core;
using XRL.Messages;

namespace XRL.World.Effects {

    [Serializable]
    public class UrsaCascadia_Memorum_Tonic_Effect : ITonicEffect, ITierInitialized
    {
        private bool GaveCrystal = false;
        private bool SavedGame = false;

        public UrsaCascadia_Memorum_Tonic_Effect()
        {
            base.Duration = 12;
        }

        public override int GetEffectType()
        {
            return 4;
        }

        public override bool IsTonic()
        {
            return true;
        }

        public void Initialize(int Tier)
        {
            Duration = 12;
        }

        public override bool UseStandardDurationCountdown()
        {
            return true;
        }

        public override string GetDescription()
        {
            return "{{remiscient|reminessence}} tonic";
        }

        public override string GetStateDescription()
        {
            return "under the effects of {{remiscient|reminessence}} tonic";
        }

        public override string GetDetails()
        {
            return "Forms a crystal memory of this moment.";
        }

        public override bool Apply(GameObject Object)
        {
            if (Object.IsPlayer())
            {
                return true;
            }
            return false;
        }


        public override void Remove(GameObject Object)
        {
            base.Remove(Object);
        }

        public override void Register(GameObject Object, IEventRegistrar Registrar)
        {
            Registrar.Register("EndAction");
            base.Register(Object, Registrar);
        }

        public override bool WantEvent(int ID, int cascade)
        {
            return base.WantEvent(ID, cascade) || ID == AfterGameLoadedEvent.ID;
        }

        public override bool HandleEvent(AfterGameLoadedEvent E) {
            if (XRL.UI.Options.GetOption("Option_UrsaCascadia_SaveTonic_PersistentCheckpoint") == "Yes")
            {
                return GetBasisGameObject().RemoveEffect(this);
            }
            return false;
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "EndAction")
            {
                if (base.Duration == 6 && !SavedGame)
                {
                    SavedGame = true;
                    XRLCore.Core.Game.SaveGame("Crystal", "Crystal realigning", true, true);
                    XRL.UI.Popup.Show("The feeling in your arm intensifies, flowing from your mind to your palm like a river gushing under your flesh.");
                }
                if (base.Duration == 3 && !GaveCrystal) {
                    GaveCrystal = true;
                    GameObject crystal = GameObjectFactory.Factory.CreateObject("UrsaCascadia_MemorumCrystal");
                    XRL.UI.Popup.Show("A small crystal sprouts from your palm, enveloped by blood. The refraction shifts as the crystalline lattice realigns, then settles.");
                    base.Object.ReceiveObject(crystal);
                }
            }
            return base.FireEvent(E);
        }

        public override void ApplyAllergy(GameObject target){}
    }

}