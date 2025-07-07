#if !USE_ORIGINAL_CODE
using Microsoft.Xna.Framework.Audio;
using SDL3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Terraria
{
	internal class SoundManager
	{
		private const string MusicError = "The number of loaded music files does not equate to the number of music entries expected.\nMusic may be played during incorrect events as a result.";
		private readonly Dictionary<string, MusicEntry> LoadedMusic;
		private readonly int MusicCount;
		private MusicEntry Song;

		internal SoundManager(string[] MusicList)
		{
			MusicCount = MusicList.Length;
			// Not listed, but this runs off of FNA's handling for the SoundEffect class, so .WAVs are expected.
			FileInfo[] MatchedSongs = GetMatchingFiles("Content/ExtMusic", MusicList);
			LoadedMusic = new Dictionary<string, MusicEntry>();

			for (int SongIdx = 0; SongIdx < MatchedSongs.Length; ++SongIdx)
			{
				MusicEntry Entry = new MusicEntry();
				if (Entry.Initialize($"Content/ExtMusic/{MatchedSongs[SongIdx]}"))
				{
					LoadedMusic.Add(MusicList[SongIdx], Entry); // Music file will be added if and only if FNA can properly initialise it.
					// This also means in regards to music IDs, it will be solely determined by the MusicList variable.
				}
			}
		}

		private static FileInfo[] GetMatchingFiles(string DirPath, string[] FileList)
		{
			DirectoryInfo Directory = new DirectoryInfo(DirPath);
			List<FileInfo> MatchedFiles = new List<FileInfo>();

			foreach (string Name in FileList)
			{
				FileInfo Match = Directory.GetFiles()
							 .FirstOrDefault(f =>
								 string.Equals(Path.GetFileNameWithoutExtension(f.Name), Name, StringComparison.OrdinalIgnoreCase));

				if (Match != null)
				{
					// We have successfully found a file in the given directory that matches a name from the approved list.
					MatchedFiles.Add(Match);
				}
			}

			return MatchedFiles.ToArray();
		}

		internal bool MatchCounted(IntPtr GameWindow)
		{
			if (LoadedMusic.Count != MusicCount)
			{
				return SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_WARNING, "Loaded music mismatch", MusicError, GameWindow);
			}
			else { return true; }
		}

		// The 4 functions below are FACTCue substitutes since an AudioEngine file is no longer in use.

		internal void Play(string Cue)
		{
			if (LoadedMusic.TryGetValue(Cue, out Song))
			{
				Song.Play();
			}
		}

		internal void Pause(string Cue)
		{
			if (LoadedMusic.TryGetValue(Cue, out Song))
			{
				Song.Pause();
			}
		}

		internal void Stop(string Cue, bool HaltAll = false)
		{
			if (HaltAll)
			{
				foreach (KeyValuePair<string, MusicEntry> Song in LoadedMusic)
				{
					Song.Value.Stop();
				}
			}
			else
			{
				if (LoadedMusic.TryGetValue(Cue, out Song))
				{
					Song.Stop();
				}
			}
		}

		internal void Resume(string Cue)
		{
			if (LoadedMusic.TryGetValue(Cue, out Song))
			{
				Song.Resume();
			}
		}

		// The 3 functions below are extensions of functionality to adjust Volume, Pitch, and Looping which is built in to the SoundEffect class... again, since the AudioEngine is no longer used.

		internal void SetVolume(string Cue, float Value)
		{
			if (LoadedMusic.TryGetValue(Cue, out Song))
			{
				Song.SetVolume(Value);
			}
		}

		internal void SetPitch(string Cue, float Value)
		{
			if (LoadedMusic.TryGetValue(Cue, out Song))
			{
				Song.SetPitch(Value);
			}
		}

		internal void SetLoop(string Cue, bool ShouldLoop)
		{
			if (LoadedMusic.TryGetValue(Cue, out Song))
			{
				Song.SetLoop(ShouldLoop);
			}
		}

		internal bool CheckState(string Cue, SoundState State)
		{
			if (LoadedMusic.TryGetValue(Cue, out Song))
			{
				return Song.GetState() == State;
			}
			return false;
		}
	}

	internal class MusicEntry
	{
		SoundEffect SfxSong;
		SoundEffectInstance SongInstance;

		internal MusicEntry() { }

		internal bool Initialize(string Path)
		{
			try
			{
				FileStream Stream = new FileStream(Path, FileMode.Open, FileAccess.Read);
				SfxSong = SoundEffect.FromStream(Stream);
				Stream.Dispose();

				SongInstance = SfxSong.CreateInstance();
			}
			catch (Exception e)
			{
				Console.WriteLine($"'{Path}' was unable to be loaded due to the below error:");
				Console.WriteLine(e.ToString());
				return false;
			}

			return true;
		}

		internal void Play()
		{
			if (SongInstance.State == SoundState.Paused)
			{
				SongInstance.Resume();
			}
			else if (SongInstance.State == SoundState.Stopped)
			{
				SongInstance.Play();
			}
			else
			{
				// This way functions as a restart as opposed to doing nothing if its already playing.
				SongInstance.Stop();
				SongInstance.Play();
			}
		}

		internal void Pause()
		{
			if (SongInstance.State == SoundState.Paused || SongInstance.State == SoundState.Stopped)
			{
				return;
			}

			SongInstance.Pause();
		}

		internal void Stop()
		{
			if (SongInstance.State == SoundState.Stopped || SongInstance.State == SoundState.Paused)
			{
				return;
			}

			SongInstance.Stop();
		}

		internal void Resume()
		{
			if (SongInstance.State == SoundState.Playing)
			{
				return;
			}

			SongInstance.Resume();
		}

		internal void SetVolume(float value)
		{
			SongInstance.Volume = value;
		}

		internal void SetPitch(float value)
		{
			SongInstance.Pitch = value;
		}

		internal void SetLoop(bool value)
		{
			SongInstance.IsLooped = value;
		}

		internal SoundState GetState()
		{
			return SongInstance.State;
		}
	}
}
#endif