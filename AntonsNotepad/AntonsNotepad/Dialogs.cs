using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadForm
{
    class Dialogs
    {
        /*
         * Creates a OpenFileDialog and puts the user in c:\\Users. If a dialog is opened later
         * RestoreDirectory makes it appears in the same directory where user opened file. 
         * If the dialog browser returns OK the dialog is returned. 
         * <returns>
         * openFileDialog
         * IF ERROR: null
         * </returns>*/
        internal OpenFileDialog openDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\Users";
            openFileDialog.Filter = "txt files (*txt)|*.txt|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog;
            }

            return null;
        }

        /*
         * Creates a SaveFileDialog and if dialog returns true the dialog is returned.
         * <returns>
         * SaveFileDialog
         * if Error: null
         * </returns>*/
        internal SaveFileDialog saveDialog(Form form)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "txt files (*txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.FileName = form.Text;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog;
            }

            return null;
        }

        /*
         * Promptes the user with a message that ask the user to save. Options are 'yes', 'no' or 'cancel'.
         * returns the answer. 
         * <returns>
         * DialogResult
         * </returns>*/
        internal DialogResult saveBeforeNewDialog()
        {
            string MessageBoxText = "If you don't save file before opening a new all changed will be lost. Do you want to save file?";
            string caption = "Save file?";
            return MessageBox.Show(MessageBoxText, caption, MessageBoxButtons.YesNoCancel);
        }
    }
}
