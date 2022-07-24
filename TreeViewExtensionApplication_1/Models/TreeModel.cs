using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewExtensionApplication_1.Models
{
    public class TreeModel : BindableObject
    {
        private string name;
        /// <summary>
        /// 节点名字
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private bool? isCheck;
        /// <summary>
        /// 是否被选中（注：true是钩，false是空的，null是方块）
        /// </summary>
        public bool? IsCheck
        {
            get => isCheck;
            set
            {
                isCheck = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public TreeModel Parent { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public ObservableCollection<TreeModel> Children { get; set; } = new ObservableCollection<TreeModel>();
    }
}
