using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;
using Microsoft.Win32;
using System.IO;

namespace SpeechSynthesizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Speech.Synthesis.SpeechSynthesizer synthesizer;
        public MainWindow()
        {
            InitializeComponent();
            synthesizer = new System.Speech.Synthesis.SpeechSynthesizer();
        }

        private void BrowseSourceFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                SourceFileTextBox.Text = openFileDialog.FileName;
            }
        }

        private void BrowseOutputFileButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                OutputFileTextBox.Text = saveFileDialog.FileName;
            }
        }

        private void SynthesizeButton_Click(object sender, RoutedEventArgs e)
        {
            var sourceFile = SourceFileTextBox.Text;
            var outputFile = OutputFileTextBox.Text;
            string fileText = "";

            if (string.IsNullOrEmpty(sourceFile))
            {
                MessageBox.Show("Please select a source file.");
                return;
            }

            if (string.IsNullOrEmpty(outputFile))
            {
                MessageBox.Show("Please select an output file.");
                return;
            }

            try
            {
                using (StreamReader reader = new StreamReader(sourceFile))
                {
                    fileText = reader.ReadToEnd();
                }
                synthesizer.SetOutputToWaveFile(outputFile);
                synthesizer.Speak(fileText);
                MessageBox.Show("Speech synthesis completed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
