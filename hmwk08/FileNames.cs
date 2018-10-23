using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Collections;

namespace hmwk08
{
    public class FileNames : ViewModelBase, INotifyDataErrorInfo
    {
        private ObservableCollection<string> filePaths;
        private string directoryPath;

        private ObservableCollection<string> fileNames;
        private string directoryName;

        private ObservableCollection<BitmapImage> pictures;

        public ObservableCollection<BitmapImage> Pictures
        {
            get { return pictures; }
            set
            {
                if (pictures == value)
                    return;
                pictures = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> FilePaths
        {
            get { return filePaths; }
            set
            {
                if (filePaths == value)
                    return;
                filePaths = value;
                NotifyPropertyChanged();
            }
        }

        public string DirectoryPath
        {
            get { return directoryPath; }
            set {
                if (directoryPath == value)
                    return;
                directoryPath = value;
                NotifyPropertyChanged();
                if (isDirectoryNameValid(value))
                {
                    GetFileNames(value);
                }
            }
        }

        public ObservableCollection<string> FileNamez
        {
            get { return fileNames; }
            set
            {
                if (fileNames == value)
                    return;
                fileNames = value;
                NotifyPropertyChanged();
            }
        }

        public string DirectoryName
        {
            get { return directoryName; }
            set
            {
                if (directoryName == value)
                    return;
                directoryName = value;
                NotifyPropertyChanged();
            }
        }

        public FileNames()
        {
            GetFileNames(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        }

        private BitmapImage GetThumbnail(string fileName)
        {
            byte[] buffer = File.ReadAllBytes(fileName);
            MemoryStream memoryStream = new MemoryStream(buffer);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.DecodePixelWidth = 80;
            bitmap.DecodePixelHeight = 60;
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }

        public void GetFileNames(string path)
        {
            this.directoryPath = path;

            DirectoryInfo directory = new DirectoryInfo(path);
            DirectoryName = directory.Name;
            var files = directory.EnumerateFiles();

            var apictures = new ObservableCollection<BitmapImage>();
            var filePaths = new ObservableCollection<string>();
            var fileNames = new ObservableCollection<string>();

            foreach (var file in files)
            {
                if (file.FullName.Contains(".jpg") || file.FullName.Contains(".png"))
                {
                    filePaths.Add(file.FullName);
                    fileNames.Add(Path.GetFileName(file.FullName));
                    apictures.Add(GetThumbnail(file.FullName));
                }
            }

            this.filePaths = filePaths;
            this.FileNamez = fileNames;
            this.Pictures = apictures;
        }

        //data validation

        private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();

        public bool HasErrors => _errors.Count() > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) ||
                !_errors.ContainsKey(propertyName)) return null;
            return _errors[propertyName];
        }

        public void AddError(string propertyName, string error, bool isWarning)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                if (isWarning) _errors[propertyName].Add(error);
                else _errors[propertyName].Insert(0, error);
                RaiseErrorsChanged(propertyName);
            }
        }

        public void RemoveError(string propertyName, string error)
        {
            if (_errors.ContainsKey(propertyName) &&
                _errors[propertyName].Contains(error))
            {
                _errors[propertyName].Remove(error);
                if (_errors[propertyName].Count == 0) _errors.TryRemove(propertyName, out var as1);
                RaiseErrorsChanged(propertyName);
            }
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private const string DIRECTORY_ERROR = "Directory MUST contain :\\";
        public bool isDirectoryNameValid(string value)
        {
            bool isValid = false;
            if (value.Contains(":\\"))
            {
                RemoveError(nameof(DirectoryPath), DIRECTORY_ERROR);
                return isValid = true;
            }
            else AddError(nameof(DirectoryPath), DIRECTORY_ERROR, false);
            return isValid;
        }
    }
}
