using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Notifications;

namespace SpammerDisrespect
{
    class Program
    {

        private static bool isTextSetted = false;
        private static string textSetted = "";

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += LoadingOnOnLoadingComplete;
        }

        private static void LoadingOnOnLoadingComplete(EventArgs args)
        {
            Chat.Print("Disrespect Injected");
            Notifications.Show(new SimpleNotification("Toxic", "Disrespect Injected"));
            Chat.OnInput += OnInput;
            Game.OnNotify += GameOnOnNotify;
        }

        public static void GameOnOnNotify(GameNotifyEventArgs args)
        {
            if (!isTextSetted) return;

            if (args.EventId == GameEventId.OnChampionKill || args.EventId == GameEventId.OnChampionDoubleKill || args.EventId == GameEventId.OnChampionTripleKill || args.EventId == GameEventId.OnChampionQuadraKill || args.EventId == GameEventId.OnChampionPentaKill)
            {
                var killer = ObjectManager.GetUnitByNetworkId<Obj_AI_Base>(args.NetworkId);

                if (killer.IsMe)
                {
                    Chat.Say("/masterybadge");
                    Core.DelayAction(() => Chat.Say("/l"), 0x3e8);
                    Chat.Say("/all "+ textSetted);
                }
            }
        }

        private static void OnInput(ChatInputEventArgs args)
        {
            if (args.Input.Contains("#register"))
            {
                if (args.Input.Contains("="))
                {
                    try
                    {
                        var key = args.Input.Replace("#register=", "");
                        Chat.Print("Text defined: " + key);
                        textSetted = key;
                        isTextSetted = true;
                        Notifications.Show(new SimpleNotification("Oh no !", "Text Defined: " + key));
                    }
                    catch (Exception)
                    {
                        Notifications.Show(new SimpleNotification("Error", "text"));
                        throw;
                    }
                }
                args.Input = "";
            }
        }
    }
}
