using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class FileStorageData
    {
        public string Data { get; set; }
        public List<FileStorageHolder> FileStorageHolder { get; set; }

        public FileStorageData()
        {
            Data = string.Empty;
            FileStorageHolder = null;
        }

        public FileStorageData(string data, List<FileStorageHolder> fileStorageHolder = null)
        {
            Data = data;
            if (fileStorageHolder != null)
            {
                FileStorageHolder = fileStorageHolder;
            }
        }
    }
}
