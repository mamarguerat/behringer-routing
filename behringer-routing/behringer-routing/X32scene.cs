﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace behringer_routing
{
    class X32scene
    {
        private String scn;
        config sceneRouting;

        public struct scene
        {
            config config;

        }
        
        private struct aes50
        {
            string ch1_8;
            string ch9_16;
            string ch17_24;
            string ch25_32;
            string ch33_40;
            string ch40_48;
        }

        private struct card
        {
            public string ch1_8;
            public string ch9_16;
            public string ch17_24;
            public string ch25_32;
        }

        private struct play
        {
            public string ch1_8;
            public string ch9_16;
            public string ch17_24;
            public string ch25_32;
            public string aux1_6;
        }

        private struct routing
        {
            public string REC;
            public play IN;
            public aes50 AES50A;
            public aes50 AES50B;
            public card CARD;
            public card OUT;
            public play PLAY;
        }

        private struct userrout
        {
            public int[] IN;
            public int[] OUT;
        }

        private struct userctrl
        {
            public page a;
            public page b;
            public page c;
        }

        private struct page
        {
            public string color;
            public string encoder;
            public string button;
        }

        private struct dp48
        {
            string param;
            int[] assign;
            string[] grpname;
        }

        private struct config
        {
            public bool[] chlink;
            public bool[] auxlink;
            public bool[] fxlink;
            public bool[] buslink;
            public bool[] mtxlink;
            public bool[] mute;
            public bool[] linkcfg;
            public string mono;
            public string stereo;
            public string talk;
            public string talkA;
            public string talkB;
            public string osc;
            public userrout userrout;
            public routing routing;
            public userctrl userctrl;
            public string tape;
            public string amixenable;
        }

        public X32scene(string path)
        {
            LoadFile(path);
        }

        void LoadFile(string path)
        {
            StreamReader sr = new StreamReader(path + "\\scene.scn");
            scn = sr.ReadToEnd();
            createScene();
            
        }

        public void saveFile(string path)
        {
            createSceneFile();
            StreamWriter sw = new StreamWriter(path + "\\scene.scn", false);
            sw.Write(scn);
        }

        void createSceneFile()
        {

        }

        void createScene()
        {
            sceneRouting.chlink = new bool[16];
            sceneRouting.chlink = searchOnOff(16, "/config/chlink");
            sceneRouting.auxlink = new bool[4];
            sceneRouting.auxlink = searchOnOff(4, "/config/auxlink");
            sceneRouting.fxlink = new bool[4];
            sceneRouting.fxlink = searchOnOff(4, "/config/fxlink");
            sceneRouting.buslink = new bool[8];
            sceneRouting.buslink = searchOnOff(8, "/config/buslink");
            sceneRouting.mtxlink = new bool[3];
            sceneRouting.mtxlink = searchOnOff(3, "/config/mtxlink");
            sceneRouting.mute = new bool[6];
            sceneRouting.mute = searchOnOff(6, "/config/muste");
            sceneRouting.linkcfg = new bool[4];
            sceneRouting.linkcfg = searchOnOff(4, "/config/linkcfg");
        }

        bool[] searchOnOff(int size, string search)
        {
            bool[] onOff = new bool[size];
            int i = scn.IndexOf(search);
            i += search.Length;
            int j = 0;
            while (scn[i] != '/')
            {
                if (scn[i++] == 'O')
                {
                    if (scn[i++] == 'N')
                    {
                        onOff[j++] = true;
                    }
                    else
                    {
                        onOff[j++] = false;
                    }
                }
            }
            return onOff;
        }
    }
}
