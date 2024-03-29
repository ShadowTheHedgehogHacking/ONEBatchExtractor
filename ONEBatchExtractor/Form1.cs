﻿using HeroesONE_R.Structures;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ONEBatchExtractor {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            var dialog = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;
            string[] foundOnes = Directory.GetFiles(dialog.SelectedPath, "*.one", SearchOption.AllDirectories);
            for (int i = 0; i < foundOnes.Length; i++) {
                byte[] readFile = File.ReadAllBytes(foundOnes[i]);
                Archive currentONE = Archive.FromONEFile(ref readFile);
                for (int j = 0; j < currentONE.Files.Count; j++) {
                    var newFntFilePath = dialog.SelectedPath + "\\OUTPUT\\" + foundOnes[i].Split(dialog.SelectedPath + '\\')[1];
                    var parent = Directory.GetParent(newFntFilePath).FullName;
                    var fulldir = newFntFilePath.Substring(0, newFntFilePath.Length - 4);
                    Directory.CreateDirectory(fulldir);
                    currentONE.Files[j].WriteToFile(fulldir + '\\' + currentONE.Files[j].Name);
                }
            }
            MessageBox.Show("DONE");
        }
    }
}
