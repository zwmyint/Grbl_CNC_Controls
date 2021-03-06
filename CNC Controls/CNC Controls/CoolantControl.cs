﻿/*
 * CoolantControl.cs - part of CNC Controls library
 *
 * 2018-09-23 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2018, Io Engineering (Terje Io)
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

· Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

· Redistributions in binary form must reproduce the above copyright notice, this
list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

· Neither the name of the copyright holder nor the names of its contributors may
be used to endorse or promote products derived from this software without
specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CNC_Controls
{
    public partial class CoolantControl : UserControl
    {
        public bool silent = false;

        public delegate void CommandGeneratedHandler(string command);
        public event CommandGeneratedHandler CommandGenerated;

        public CoolantControl()
        {
            InitializeComponent();

            this.chkFlood.Tag = ((char)GrblConstants.CMD_COOLANT_FLOOD_OVR_TOGGLE).ToString();
            this.chkFlood.CheckedChanged += new EventHandler(chkCoolant_CheckedChanged);

            this.chkMist.Tag = ((char)GrblConstants.CMD_COOLANT_MIST_OVR_TOGGLE).ToString();
            this.chkMist.CheckedChanged += new EventHandler(chkMist_CheckedChanged);
        }

        public bool EnableControl
        {
            get { return this.chkFlood.Enabled; }
            set
            {
                this.chkFlood.Enabled = this.chkMist.Enabled = value;
            }
        }

        public bool MistOn
        {
            get { return this.chkMist.Checked; }
            set { silent = true; this.chkMist.Checked = value; silent = false; }
        }
        public bool FloodOn
        {
            get { return this.chkFlood.Checked; }
            set { silent = true; this.chkFlood.Checked = value; silent = false; }
        }

        public string FloodCommand { get { return string.Format((string)this.chkFlood.Tag, FloodOn ? "1" : "0"); } set { this.chkFlood.Tag = value; } }
        public string MistCommand { get { return string.Format((string)this.chkMist.Tag, MistOn ? "1" : "0"); } set { this.chkMist.Tag = value; } }

        void chkCoolant_CheckedChanged(object sender, EventArgs e)
        {
            if (!silent)
                CommandGenerated((string)this.chkFlood.Tag);
        }

        void chkMist_CheckedChanged(object sender, EventArgs e)
        {
            if (!silent)
                CommandGenerated((string)this.chkMist.Tag);
        }
    }
}
