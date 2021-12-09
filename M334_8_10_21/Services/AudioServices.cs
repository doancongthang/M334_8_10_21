using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading;
using System.Windows.Media;
using System.Threading.Tasks;
using M334_8_10_21;

namespace M334_8_10_21.Services
{
    public class AudioServices
    {
        public void Playsound1()
        {
            Uri uri = new Uri(@"D:\MLTech\Orion\Project Visual\M334_8_10_21\M334_8_10_21\M334_8_10_21\Sounds\1.mp3");
            var player = new MediaPlayer();
            player.Open(uri);
            //if(mc1.vl_speed_engine >=75)
            //while(sound_ok == true)
            {
                player.Play();
                //sound_ok = false;
            }
        }
        public void Playsound2()
        {
            Uri uri = new Uri(@"D:\MLTech\Orion\Project Visual\M334_8_10_21\M334_8_10_21\M334_8_10_21\Sounds\2.mp3");
            var player = new MediaPlayer();
            player.Open(uri);
            //if(mc1.vl_speed_engine >=75)
            //while (sound_ok == true)
            {
                player.Play();
            }
        }
        public void Playsound3()
        {
            Uri uri = new Uri(@"D:\MLTech\Orion\Project Visual\M334_8_10_21\M334_8_10_21\M334_8_10_21\Sounds\3.mp3");
            var player = new MediaPlayer();
            player.Open(uri);
            //if(mc1.vl_speed_engine >=75)
            //while (sound_ok == true)
            {
                player.Play();
                //sound_ok = false;  
            }
        }
        public void Playsound4()
        {
            Uri uri = new Uri(@"D:\MLTech\Orion\Project Visual\M334_8_10_21\M334_8_10_21\M334_8_10_21\Sounds\4.mp3");
            var player = new MediaPlayer();
            player.Open(uri);
            //if(mc1.vl_speed_engine >=75)
            //while (sound_ok == true)
            {
                player.Play();
                //sound_ok = false;
            }
        }
    }
}
