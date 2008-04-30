#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
#endregion


namespace Frogger.Utils
{

    public enum Sounds
    {
        Car,
        Frog,
        Collision,
        Cow
    }

    public static class Sound
    {
        private static AudioEngine engine;
        private static WaveBank wavebank;
        private static SoundBank soundbank;

        private static string[] cueNames = new string[]
        {
            "Car",
            "Frog",
            "Collision",
            "Cow"
        };


  
        public static Cue Play(Sounds sound)
        {
            Cue returnValue = soundbank.GetCue(cueNames[(int)sound]);
            returnValue.Play();
            return returnValue;
        }


        public static void Stop(Cue cue)
        {
            cue.Stop(AudioStopOptions.Immediate);
        }


        public static void Initialize()
        {
            engine = new AudioEngine(@"audio\Frogger.xgs");
            wavebank = new WaveBank(engine, @"audio\Frogger.xwb");
            soundbank = new SoundBank(engine, @"audio\Frogger.xsb");
        }


        public static void Update()
        {
            engine.Update();
        }


        public static void Shutdown()
        {
            soundbank.Dispose();
            wavebank.Dispose();
            engine.Dispose();
        }
    }
}
