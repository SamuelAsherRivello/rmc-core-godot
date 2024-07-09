using System.Collections.Generic;
using Godot;
using RMC.Core.Utilities;
using RMC.Mingletons;

namespace RMC.Core.Audio
{
    /// <summary>
    /// This workflow is for a Singleton that is a Node
    ///
    /// Where you want to add it to the Mingleton in the _Ready method
    /// </summary>
    public partial class AudioManager : Node
    {
        private readonly List<AudioStreamPlayer3D> _audioStreamPlayer3Ds = new List<AudioStreamPlayer3D>();
        private readonly Dictionary<string, AudioStream> _audioStreams = new Dictionary<string, AudioStream>();

        public override async void _Ready()
        {
            base._Ready();
            
            // Make AudioManager a Singleton
            await Mingleton.InstantiateAsync();
            Mingleton.Instance.AddSingleton<AudioManager>(this);
            
            // Add all children to list. It's WHERE to play.
            foreach (Node child in GetChildren())
            {
                if (child is AudioStreamPlayer3D audioStreamPlayer3D)
                {
                    _audioStreamPlayer3Ds.Add(audioStreamPlayer3D);
                }
            }
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            Mingleton.Instance.RemoveSingleton<AudioManager>();
        }

        public void PlayAudio(string fileName, float pitchScale = 1.0f)
        {   
            AudioStreamPlayer3D audioStreamPlayer3D = GetAvailableAudioStreamPlayer3D();

            if (audioStreamPlayer3D == null)
            {
                return;
            }
            audioStreamPlayer3D.Stream = GetOrCreateCachedAudioStream(fileName);
            audioStreamPlayer3D.PitchScale = pitchScale;
            audioStreamPlayer3D.Play();
        }
        
        public AudioStreamPlayer3D GetAvailableAudioStreamPlayer3D ()
        {
            // Find non-busy player. It's WHERE to play.
            foreach (AudioStreamPlayer3D audioStreamPlayer3D in _audioStreamPlayer3Ds)
            {
                if (!audioStreamPlayer3D.Playing)
                {
                    return audioStreamPlayer3D;
                }
            }
            return null;
        }
        

        private AudioStream GetOrCreateCachedAudioStream(string fileName)
        {
            // Find/creat a cached Stream. It's WHAT to play
            if (!_audioStreams.TryGetValue(fileName, out AudioStream audioStream))
            {
                audioStream = LoadAudioStreamByFilename(fileName);
                _audioStreams.Add(fileName, audioStream);
            }

            return audioStream;
        }
        
        
        /// <summary>
        /// Load MP3 from local files system by name
        /// </summary>
        /// <param name="fileName">Such as "Coin01.mp3"</param>
        /// <returns></returns>
        private AudioStream LoadAudioStreamByFilename(string fileName)
        {
            string filePath = FileAccessUtility.FindFileOnceInResources(fileName);
            AudioStream audioStreamMp3 = GD.Load<AudioStream>(filePath);
            return audioStreamMp3;
        }
    }
}