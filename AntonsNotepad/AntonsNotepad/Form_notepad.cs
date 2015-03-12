﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace NotepadForm
{
    public partial class Form_notepad : Form
    {
        Notepad_File_Operator nfo = new Notepad_File_Operator();

        public Form_notepad()
        {
            InitializeComponent();
        }

        private void textbox_filecontent_TextChanged(object sender, EventArgs e)
        {
            char lastChar = this.Text[this.Text.Length-1];
            if (lastChar != '*')
            {
                this.Text += "*";
                nfo.textChanged = true;
            }
        }

        private void newToolStripMenuItem_Clicked(object sender, System.EventArgs e)
        {
            nfo.newFile(this, textbox_filecontent);
        }


        private void openToolStripMenuItem_Clicked(object sender, System.EventArgs e)
        {
            nfo.openFile(this, textbox_filecontent);

        }

        private void saveAsToolStripMenuItem_Clicked(object sender, System.EventArgs e)
        {
            if (nfo.isFileOpen)
            {
                nfo.saveFileAs(this, textbox_filecontent);
            }
            
        }

        private void saveToolStripMenuItem_Clicked(object sender, System.EventArgs e)
        {
            if (nfo.isFileOpen)
            {
                nfo.saveFile(this, textbox_filecontent);
            }
        }


        private void closeToolStripMenuItem_Clicked(object sender, System.EventArgs e)
        {
            nfo.closeFile(this, textbox_filecontent);
        }


        private void Form_notepad_Load(object sender, EventArgs e)
        {

        }
    }
}
