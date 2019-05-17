using System.Text;
using System.Windows;
using System;
using System.Timers;
using Ninject;

namespace WordDefinitionApp
{
    using DictServiceReference;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string DEFINITION = "Definition";
        private const string DICTIONARY = "Dictionary";
        private const string COLON = ": ";
        private const string BTN_NAME = "Copy Definition";
        private const string BTN_NAME_DONE = "Copied!";
        
        private readonly Timer timer = new Timer(1);
        private readonly StandardKernel kernel;
        private readonly FindDefinitionQueryDispatcher dispatcher;
        private delegate void UpdateEvent(ElapsedEventArgs e);
        private static string inputWord = String.Empty;
       // public DictServiceSoapClient dictServiceClient { get; set; }
        public MainWindow()
        {
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            InitializeComponent();
            txtDefinition.Focusable = true;
            //   dictServiceClient = new DictServiceSoapClient(EndpointConfigurationName);
            //  dictServiceClient.Open();
            kernel = new StandardKernel();
            Register.RegisterDependencies(kernel);

            dispatcher = new FindDefinitionQueryDispatcher(kernel);
            System.Windows.Input.FocusManager.SetFocusedElement(this, tbxWord);
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await Dispatcher.BeginInvoke(new UpdateEvent(UpdateContent), e);
        }

        private void UpdateContent(ElapsedEventArgs e)
        {
            pbLoading.Value = e.SignalTime.Millisecond/10;
        }

        private void BtnGetDefinition_Click(object sender, RoutedEventArgs e)
        {
            if (inputWord.Equals(tbxWord.Text.Trim()))
            {
                return;
            }

            inputWord = tbxWord.Text.Trim();
            if (string.IsNullOrEmpty(inputWord))
            {
                txtDefinition.Text = "Input the word to definition!";
                return;
            }
            btnCopyDefinition.Content = BTN_NAME;
            pbLoading.Visibility = Visibility.Visible;
            timer.Enabled = true;
            timer.Start();
            btnGetDefinition.IsEnabled = false;
            GetDefinition(inputWord);
        }

        private async void GetDefinition(string inputWord)
        {
            FindDefinitionQuery query = new FindDefinitionQuery(inputWord);         
            WordDefinition wordDefinition = await dispatcher.Execute(query);
                //= await dictServiceClient.DefineAsync(inputWord);
            Definition[] definitions = wordDefinition.Definitions;
            btnGetDefinition.IsEnabled = true;
            timer.Stop();
            timer.Enabled = false;
            pbLoading.Visibility = Visibility.Collapsed;
            txtDefinition.Text = GetFormatedDefinition(definitions);
        }

        private string GetFormatedDefinition(Definition[] definitions)
        {
            if (definitions is null || definitions.Length == 0)
            {
                return "No "+DEFINITION;
            }

            StringBuilder definitionBuilder = new StringBuilder();
            foreach (Definition item in definitions)
            {
                definitionBuilder.AppendFormat("{0}{1}{2}{3}",DICTIONARY, COLON, item.Dictionary.Name.Trim(), System.Environment.NewLine);
                definitionBuilder.AppendFormat("{0}{1}{2}{3}", DEFINITION, COLON, item.WordDefinition.Trim(), System.Environment.NewLine);
                definitionBuilder.AppendLine("-=->");
            }

            return definitionBuilder.ToString();
        }

        private void BtnCopyDefinition_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtDefinition.Text);
            btnCopyDefinition.Content = BTN_NAME_DONE;
        }
    }
}
