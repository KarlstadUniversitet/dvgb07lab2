using System;
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

        /*
         * If the textbox was changed there is first of a check if a '*'-sign is already showing. 
         * If not a '*'-sign is added to the form-title. And textChanged variable in Notepad_File_Operator
         * is changed to true. 
         * <returns></returns> */
        private void textbox_filecontent_TextChanged(object sender, EventArgs e)
        {
            char lastChar = this.Text[this.Text.Length-1];
            if (lastChar != '*')
            {
                this.Text += "*";
                nfo.textChanged = true;
            }
        }

        /*
         * Calles the function newFile in Notepad_File_Operator
         * <returns></returns> */
        private void newToolStripMenuItem_Clicked(object sender, System.EventArgs e)
        {
            nfo.newFile(this, textbox_filecontent);
        }

        /*
         * Calles the function openFile in Notepad_File_Operator
         * <returns></returns> */
        private void openToolStripMenuItem_Clicked(object sender, System.EventArgs e)
        {
            nfo.openFile(this, textbox_filecontent);

        }

        /*
         * Checks if a file is opened. If so the saveFIleAs function is called in Notepad_File_Operator. 
         * <returns></returns> */
        private void saveAsToolStripMenuItem_Clicked(object sender, System.EventArgs e)
        {
            if (nfo.isFileOpen)
            {
                nfo.saveFileAs(this, textbox_filecontent);
            }
            
        }

        /*
         * Checks if a file is opened. If so the saveFile function is called in Notepad_File_Operator 
         * <returns></returns> */
        private void saveToolStripMenuItem_Clicked(object sender, System.EventArgs e)
        {
            if (nfo.isFileOpen)
            {
                nfo.saveFile(this, textbox_filecontent);
            }
        }

        /*
         * calles the function closeFile in Notepad_File_Operator
         * <returns></returns> */
        private void closeToolStripMenuItem_Clicked(object sender, System.EventArgs e)
        {
            nfo.closeFile(this, textbox_filecontent);
        }
    }
}
