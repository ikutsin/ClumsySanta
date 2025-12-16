using System;
using System.Collections.Generic;
using System.IO;
using System.Media;

namespace ClumsySanta.Redist
{

    public static class SoundPlayerHelper
    {
	    private static readonly Dictionary<string, SoundPlayer> Cache;

        static SoundPlayerHelper()
        {
	        if (OperatingSystem.IsWindows())
	        {
		        Cache = new Dictionary<string, SoundPlayer>();
	        }
        }

        public static void PlaySound(string name)
        {
	        if (!OperatingSystem.IsWindows())
	        {
		        return;
	        }

	        try
            {
                if (!Cache.TryGetValue(name, out var player))
                {
                    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    var path = Path.Combine(baseDir, "Assets", "Sounds", name + ".wav");
                    if (!File.Exists(path)) return;
                    player = new SoundPlayer(path);
                    Cache[name] = player;
                }
                player.Stop();
                player.Play();
            }
            catch
            {
                // ignore sound errors in WPF migration
            }
        }
    }
}
