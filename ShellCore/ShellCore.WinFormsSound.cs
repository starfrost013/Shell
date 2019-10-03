using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
// WAV APIs for ShellCore 8.0
namespace Shell.Core
{
    partial class ShellCore
    {
        public SoundPlayer sndPlayer; // still considering having a single object and the shell app not having to import System.Media
        public SoundPlayer WinLoadSound(string soundPath)
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = soundPath;
            try
            {
                soundPlayer.LoadAsync();
                return soundPlayer;
            }
            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return null;
            }
            catch (TimeoutException)
            {
                ElmThrowException(51);
                return null;
            }
        }

        public void WinPlaySound(string soundPath)
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = soundPath;
            try
            {
                soundPlayer.LoadAsync();
                soundPlayer.Play();
            }
            catch (FileNotFoundException)
            {
                ElmThrowException(31);
            }
            catch (TimeoutException)
            {
                ElmThrowException(51);
            }

            return;
        }

        // probably should've used a delegate here, but oh well.
        public SoundPlayer WinPlaySound(string soundPath, bool n0001 = true)
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = soundPath;
            try
            {
                soundPlayer.LoadAsync();
                soundPlayer.Play();
                return soundPlayer;
            }
            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return null;
            }
            catch (TimeoutException)
            {
                ElmThrowException(51);
                return null;
            }

            
        }

        public void WinStopSound(SoundPlayer soundPlayer)
        {
            soundPlayer.Stop();
        }

        public void WinPlaySystemSound(SystemSound systemSound)
        {
            systemSound.Play();
        }
    }
}
