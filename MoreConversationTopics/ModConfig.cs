﻿using System;
namespace MoreConversationTopics
{
    public class ModConfig
    {
        //public bool ExampleBoolean { get; set; }
        public int WeddingDuration { get; set; }
        public int LuauDuration { get; set; }
        public int BirthDuration { get; set; }

        public ModConfig()
        {
            // this.ExampleBoolean = true;
            this.WeddingDuration = 7;
            this.LuauDuration = 7;
            this.BirthDuration = 7;
        }
    }
}