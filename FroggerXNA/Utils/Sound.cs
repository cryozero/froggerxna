// Sound.cs
// Part of "Microbe Patrol" Version 1.0 -- January 15, 2007
// Copyright 2007 Michael Anderson


#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
#endregion


namespace Frogger.Utils
{
    /// <summary>
    /// An enum for all of the sounds.
    /// </summary>
    public enum Sounds
    {
        Music,
        Car,
        Frog
    }

    /// <summary>
    /// Abstracts away the sounds for a simple interface using the Sounds enum.
    /// </summary>
    public static class Sound
    {
        private static AudioEngine engine;
        private static WaveBank wavebank;
        private static SoundBank soundbank;

        private static string[] cueNames = new string[]
        {
            "Music",
            "Car",
            "Frog"
        };


        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="sound">Which sound to play</param>
        /// <returns>XACT cue to be used if you want to stop this particular looped sound. Can be ignored for one shot sounds</returns>
        public static Cue Play(Sounds sound)
        {
            Cue returnValue = soundbank.GetCue(cueNames[(int)sound]);
            returnValue.Play();
            return returnValue;
        }


        /// <summary>
        /// Stops a previously playing cue.
        /// </summary>
        /// <param name="cue">The cue to stop that you got returned from Play(sound)</param>
        public static void Stop(Cue cue)
        {
            cue.Stop(AudioStopOptions.Immediate);
        }


        /// <summary>
        /// Starts up the sound code.
        /// </summary>
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


        /// <summary>
        /// Shuts down the sound code tidily.
        /// </summary>
        public static void Shutdown()
        {
            soundbank.Dispose();
            wavebank.Dispose();
            engine.Dispose();
        }
    }
}
