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
        internal Boolean isFileOpen { get; set; }
        String filePath;
        Dialogs dialog = new Dialogs();
        internal Boolean textChanged { get; set; }

        public Notepad_File_Operator()
        {
            textChanged = false;
        }
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
            if (isFileOpen && textChanged)
            {
                if (saveFileBeforeOpenNew(form, textbox) == "Cancel")
                {   // Cancel was pressed
                    return;
                }
            }
            try
            {
                OpenFileDialog oFD = dialog.openDialog();
                String filename = oFD.FileName;
                FileInfo fInfo = new FileInfo(oFD.FileName);
                filePath = fInfo.DirectoryName;
                Stream stream = oFD.OpenFile();
                using (stream)
                {
                    StreamReader sr = new StreamReader(stream);
                    using (sr)
                    {
                        textbox.Text = sr.ReadToEnd();
                        form.Text = System.IO.Path.GetFileName(filename);
                        textChanged = false;
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
            if (isFileOpen && textChanged)
            {
                if (saveFileBeforeOpenNew(form, textbox) == "Cancel")
                {   // Cancel was pressed
                    return;
                }
            }
            form.Text = "dok.1";
            textbox.Text = "";
            isFileOpen = true;
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
            String file = filePath + "\\" + form.Text;
            if (form.Text[form.Text.Length - 1] == '*')
            {
                file = file.TrimEnd(file[file.Length - 1]);
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.Write(textbox.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not save file: " + ex.Message);
            }
        }

        internal void closeFile(Form form, TextBox textbox)
        {
            if (isFileOpen && textChanged)
            {
                if (saveFileBeforeOpenNew(form, textbox) == "Cancel")
                {   // Cancel was pressed
                    return;
                }
            }
            form.Text = "Antons Notepad";
            textbox.Text = "";
            isFileOpen = false;
            filePath = "";
        }

        /* PRIVATE FUNCTIONS */

        private String saveFileBeforeOpenNew(Form form, TextBox textbox)
        {
            DialogResult dialogResult = dialog.saveBeforeNewDialog();
            if (dialogResult == DialogResult.Yes)
            {
                saveFile(form, textbox);
                return "Yes";
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return "Cancel";
            }

            return "No";
        }

        
    }
}
