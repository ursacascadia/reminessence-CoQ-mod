using System;
using XRL;
using XRL.Core;
using XRL.Wish;
using XRL.Messages;

namespace UrsaCascadia.Memorum {
    [HasWishCommand]
    public class Wishes {
        [WishCommand]
        public static void LoadCrystal() {
            if (XRL.XRLGame.LoadCurrentGame("Crystal") == null)
            {
                XRL.Messages.MessageQueue.AddPlayerMessage("[Debug: Command failed.]");
            }
        }
        [WishCommand]
        public static void SaveCrystal() {
            XRLCore.Core.Game.SaveGame("Crystal", "Crystal realigning", true, true);
        }
    }
}