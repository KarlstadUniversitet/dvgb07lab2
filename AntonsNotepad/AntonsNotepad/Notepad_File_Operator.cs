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
        public Notepad_File_Operator()
        {

        }

        Stream openFileStream;

        internal void openFile(Form form, TextBox textbox)
        {
            if (openFileStream != null)
            {
                saveFileBeforeOpenNew(textbox);
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\Users";
            openFileDialog.Filter = "txt files (*txt)|*.txt|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((openFileStream = openFileDialog.OpenFile()) != null)
                    {
                        using (openFileStream)
                        {
                            StreamReader sr = new StreamReader(openFileStream);
                            form.Text = System.IO.Path.GetFileName(openFileDialog.FileName);
                            textbox.Text = sr.ReadToEnd();
                            sr.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        internal void newFile(Form form, TextBox textbox)
        {
            if (openFileStream != null)
            {
                saveFileBeforeOpenNew(textbox);
            }
            form.Text = "doc.1";
            textbox.Text = "";
        }

        internal void saveFileBeforeOpenNew(TextBox textbox)
        {
            string MessageBoxText = "If you don't save file before opening a new all changed will be lost. Do you want to save file?";
            string caption = "Save file?";
            DialogResult dialogResult = MessageBox.Show(MessageBoxText, caption, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                saveFile(textbox);
                openFileStream.Close();
                textbox.Text = "";
            }
            else if (dialogResult == DialogResult.No)
            {
                openFileStream.Close();
                textbox.Text = "";
            }
        }

        internal void saveFileAs(Form form, TextBox textbox)
        {
            Stream stream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "txt files (*txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.FileName = form.Text;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveFileDialog.OpenFile()) != null)
                {
                    using (stream)
                    {
                        StreamWriter sw = new StreamWriter(stream);
                        sw.Write(textbox.Text);
                        sw.Close();
                        stream.Close();
                    }
                }
            }
        }

        internal void saveFile(TextBox textbox)
        {
            Stream stream = openFileStream;
            if (stream != null)
            {
                StreamWriter sw = new StreamWriter(stream);
                sw.Write(textbox);
                sw.Close();
            }
        }

    }
}
