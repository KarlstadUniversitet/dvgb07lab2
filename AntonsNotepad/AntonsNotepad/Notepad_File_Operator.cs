using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadForm
{
    class Notepad_File_Operator
    {
        Boolean isFileOpen = false;
        Dialogs dialog = new Dialogs();
        /* INTERNAL FUNCTIONS */

        /*
         * First of the functions checks if a file is already being worked on in the notepad. If so
         * the saveFileBeforeOpenNew() function is called. Next step the user is prompt to choose a 
         * file to open. After that the text from file is applied in the notepad textarea and title is 
         * changed based on filename. 
         * IF ERROR: an message with exception is messaged to the user. 
         * <returns></returns> */
        internal void openFile(Form form, TextBox textbox)
        {
            if (isFileOpen)
            {
                saveFileBeforeOpenNew(form, textbox);
            }
            try
            {
                OpenFileDialog oFD = dialog.openDialog();
                string filename = oFD.FileName;
                Stream stream = oFD.OpenFile();
                using (stream)
                {
                    StreamReader sr = new StreamReader(stream);
                    using (sr)
                    {
                        form.Text = System.IO.Path.GetFileName(filename);
                        textbox.Text = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                isFileOpen = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        internal void newFile(Form form, TextBox textbox)
        {
            if (isFileOpen)
            {
                saveFileBeforeOpenNew(form, textbox);
            }
            form.Text = "doc.1";
            textbox.Text = "";
        }

        internal void saveFileAs(Form form, TextBox textbox)
        {
            SaveFileDialog sFD = dialog.saveDialog(form);
            try {
                Stream stream = sFD.OpenFile();
                if (stream != null)
                {
                    StreamWriter sw = new StreamWriter(stream);
                    using (sw)
                    {
                        sw.Write(textbox.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: could not cave file: " + ex.Message);
            }
        }

        internal void saveFile(Form form, TextBox textbox)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(form.Text))
                {
                    sw.Write(textbox.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not save file: " + ex.Message);
            }
        }

        /* PRIVATE FUNCTIONS */

        private void saveFileBeforeOpenNew(Form form, TextBox textbox)
        {
            string MessageBoxText = "If you don't save file before opening a new all changed will be lost. Do you want to save file?";
            string caption = "Save file?";
            DialogResult dialogResult = MessageBox.Show(MessageBoxText, caption, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                saveFile(form, textbox);
            }
        }
    }
}
