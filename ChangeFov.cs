using System.Text.RegularExpressions;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Logging;

namespace ChangeFov {

    public class Main : BasePlugin {
        
        public override string ModuleName => "eboyfriends";
        public override string ModuleVersion => "6.6.6";
        public override string ModuleAuthor => "eboyfriends";

        public override void Load(bool hotReload)
        {
            Logger.LogInformation("We are loading ChangeFov!");
            
            AddCommand("css_fov", "Changes the players fov", ChangeFov);
        }

        public override void Unload(bool hotReload)
        {
            Logger.LogInformation("We are unloading ChangeFov!");

        }

        private void ChangeFov(CCSPlayerController? player, CommandInfo info) {
            if (player == null) return;

            var command = info.GetArg(0);
            var value = info.GetArg(1);
            bool isNumber = Regex.IsMatch(value, @"^\d+$");

            if (value.ToString() != null && isNumber) {
                player.DesiredFOV = Convert.ToUInt32(value);
                Utilities.SetStateChanged(player, "CBasePlayerController", "m_iDesiredFOV");
            }
            
            player.PrintToCenter($"Fov set to {value}");
        }
    }
}