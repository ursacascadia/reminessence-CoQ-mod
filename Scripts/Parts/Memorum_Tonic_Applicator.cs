using System;
using XRL;
using XRL.Core;
using XRL.Messages;

namespace XRL.World.Parts {

    [Serializable]
    public class UrsaCascadia_Memorum_Tonic_Applicator : IPart
    {
        public override void Register(GameObject Object, IEventRegistrar Registrar)
        {
            Registrar.Register("ApplyTonic");
            base.Register(Object, Registrar);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "ApplyTonic")
            {
                GameObject subject = E.GetGameObjectParameter("Subject");
                XRLCore.Core.Game.SaveGame("Crystal", "Crystal realigning", true, true);
                // Make the crystal and give it to the subject AFTER saving
                GameObject crystal = GameObjectFactory.Factory.CreateObject("UrsaCascadia_MemorumCrystal");
                subject.ReceiveObject(crystal);
                return true;
            }
            return base.FireEvent(E);
        }
    }
}