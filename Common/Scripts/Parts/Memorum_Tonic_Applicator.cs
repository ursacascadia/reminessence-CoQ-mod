using System;
using XRL;
using XRL.Core;
using XRL.Messages;
using XRL.World.Effects;

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
                GameObject actor = E.GetGameObjectParameter("Actor");
                GameObject subject = E.GetGameObjectParameter("Subject");

                if (subject.IsPlayer())
                {
                    XRL.UI.Popup.Show("A pang shoots through your arm to your palm.");
                    UrsaCascadia_Memorum_Tonic_Effect effect = new UrsaCascadia_Memorum_Tonic_Effect();
                    subject.ApplyEffect(effect);
                    subject.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality");
                    return true;
                }
                else
                {
                    GameObject crystal = GameObjectFactory.Factory.CreateObject("UrsaCascadia_MemorumCrystal");
                    subject.ReceiveObject(crystal);
                    return true;
                }

            }
            return base.FireEvent(E);
        }
    }
}