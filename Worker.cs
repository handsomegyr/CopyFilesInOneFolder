#region Using directives

using System.Collections.Generic;
using System.IO;

#endregion

namespace CopyFilesInOneFolder
{
    public class Worker
    {

        public static bool merge(string selectedPath, List<FileEntry> excelFileList, System.ComponentModel.BackgroundWorker backgroundWorker)
        {

            string dir = selectedPath + "mergeAll";
            if(Directory.Exists(dir)){
                Directory.Delete(dir,true);
            }
            DirectoryInfo di = Directory.CreateDirectory(dir);
            
            int i = 0;
            foreach (var f in excelFileList)
            {
                i++;
                if (backgroundWorker.CancellationPending)
                {
                    // Return without doing any more work.
                    return false;
                }

                if (backgroundWorker.WorkerReportsProgress)
                {
                    backgroundWorker.ReportProgress(i);
                }

                if (!f.IsSelected) continue;
                f.Status = "处理中";
                File.Copy(f.Path, di.FullName + "/" + f.Name, true);
                f.Status = "处理完成";
            }
            return true;

        }

        
    }
}
