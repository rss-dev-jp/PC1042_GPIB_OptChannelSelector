
using RssDev.Common.ModelUtility;
namespace RssDev.Common.TaskUtility
{
    /// <summary>
    /// タスク情報クラス
    /// </summary>
    public class TaskInformation : BindableBase
    {
        public string TaskName { get; set; }
        public int ID { get; set; }
        public int Fps { get; set; }
        public int Max { get; set; }
        public int Tip { get; set; }

        public void Update()
        {
            OnPropertyChanged("TaskName");
            OnPropertyChanged("ID");
            OnPropertyChanged("Fps");
            OnPropertyChanged("Max");
            OnPropertyChanged("Tip");
        }
    }
}
