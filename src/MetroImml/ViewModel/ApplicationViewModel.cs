using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Imml.Scene.Container;
using MetroImml.Common;
using MetroImml.Input;

namespace MetroImml.ViewModel
{
    public class ApplicationViewModel : BindableBase
    {
        #region Properties

        private ImmlDocumentViewModel _SelectedDocument;

        public ImmlDocumentViewModel SelectedDocument
        {
            get { return _SelectedDocument; }
            set { base.SetProperty(ref _SelectedDocument, value); }
        }

        #endregion

        #region Commands

        private readonly ICommand _NewCommand;

        public ICommand NewCommand
        {
            get { return _NewCommand; }
        }

        private readonly ICommand _SaveCommand;

        public ICommand SaveCommand
        {
            get { return _SaveCommand; }
        }

        private readonly ICommand _OpenCommand;

        public ICommand OpenCommand
        {
            get { return _OpenCommand; }
        }

        #endregion

        public ApplicationViewModel()
        {
            this.SelectedDocument = new ImmlDocumentViewModel(new ImmlDocument());

            _NewCommand = new CommandBase
            {
                CanExecuteDelegate = c => true
            };

            _OpenCommand = new CommandBase
            {
                CanExecuteDelegate = c => true
            };

            _SaveCommand = new CommandBase
            {
                CanExecuteDelegate = c => !this.SelectedDocument.IsReadOnly && this.SelectedDocument.HasUnsavedChanges
            };
        }
    }
}
