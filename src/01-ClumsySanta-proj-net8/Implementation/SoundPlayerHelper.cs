using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace ClumsySanta.Implementation
{
    public static class SoundPlayerHelper
    {
        static Dictionary<string,SoundEffect> cache = new Dictionary<string, SoundEffect>();

        public static void PlaySound(string name)
        {
            if (!cache.ContainsKey(name))
            {
                var info = App.GetResourceStream(
                new Uri(String.Format("Assets/Sounds/{0}.wav", name), UriKind.Relative));
                FrameworkDispatcher.Update();
                SoundEffect effect = SoundEffect.FromStream(info.Stream);

                cache.Add(name, effect);
            }

            var i = cache[name].CreateInstance();
            FrameworkDispatcher.Update();
            i.Play();
        }
    }
}
