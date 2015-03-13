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
                using (StreamReader sr = new StreamReader(stream))
                {
                    textbox.Text = sr.ReadToEnd();
                    form.Text = System.IO.Path.GetFileName(filename);
                    textChanged = false;
                }
                isFileOpen = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        /*
         * First of this function checks if another file is in progress and that is has been changed.
         * If so the function saveFileBeforeOpenNew will be called. After this operation notepad is
         * "reseted" to make it look like we opened a new file. 
         * <returns></returns>
         */
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

        /*
         * This function will first of prompt the user for where to save the file and what to name it. 
         * After this a StreamWriter is opened to this file and the content in notepad textbox is
         * written to it. 
         * IF ERROR: prompts the message "ERROR: could not save file:" and exception message. 
         * <returns></returns> */
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

        /* The path to the file we have opened has been saved and that + the filename we use
         * to now what file to write to. Because we get the filename from the form title(stupid solution)
         * we have to check if there is an '*' sign in the end which it probably is. If so the last sign is deleted. 
         * Afterwords we save the writes the data to file. 
         * IF ERROR: prompts the user the message "Error: could nto save file:" + exception message. 
         * <returns></returns> */
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

        /*
         * First of checks if a file is open and if the text has been changed. If so the user is prompt/asked 
         * if he/she want s to save, not save or cancel the operation. After this check is made the notepad information
         * is "reseted" to nothing. 
         * <returns>if Cancel was pressed when user is prompted</returns> */
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

        /*
         * Calles the saveBeforeNewDialog in the Dialogs-klass. Result is saved in a DialogResult variable. 
         * If Yes was pressed the function saveFile is called.
         * <returns>A string of what the user pressed in prompt</returns>*/
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
