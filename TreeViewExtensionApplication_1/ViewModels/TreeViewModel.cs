using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TreeViewExtensionApplication_1.Common;
using TreeViewExtensionApplication_1.Models;

namespace TreeViewExtensionApplication_1.ViewModels
{
    public class TreeViewModel : BindableObject
    {
        /// <summary>
        /// 目录树
        /// </summary>
        public ObservableCollection<TreeModel> Trees { get; set; }

        /// <summary>
        /// 存储勾选的节点
        /// </summary>
        public List<TreeModel> SelectItem { get; set; } = new List<TreeModel>();

        /// <summary>
        /// 点击节点命令
        /// </summary>
        public DelegateCommand<TreeModel> ClickCommand { get; private set; }

        /// <summary>
        /// 展示命令
        /// </summary>
        public DelegateCommand ShowCommand { get; private set; }

        public TreeViewModel()
        {
            Trees = new ObservableCollection<TreeModel>();
            ClickCommand = new DelegateCommand<TreeModel>(Click);
            ShowCommand = new DelegateCommand(Show);
            CreateTree();
        }

        /// <summary>
        /// 初始化生成目录树
        /// </summary>
        public void CreateTree()
        {
            for (int i = 0; i < 3; i++)
            {
                TreeModel treeModel = new TreeModel()
                {
                    Name = $"我是第{i}",
                    IsCheck = false,
                };
                for (int j = 0; j < 5; j++)
                {
                    treeModel.Children.Add(new TreeModel()
                    {
                        Name = $"我是第{i}的第{j}个孩子",
                        IsCheck = false,
                        Parent = treeModel,
                    });
                }
                Trees.Add(treeModel);
            }
        }

        public void Click(TreeModel param)
        {
            try
            {
                var result = Trees.FirstOrDefault(x => x.Name == param.Name);

                if (result != null)
                {
                    SelectAll(result);
                }
                else
                {
                    Select(param);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="param"></param>
        private void SelectAll(TreeModel param)
        {
            try
            {
                int count = 0;
                List<TreeModel> lists = new List<TreeModel>();

                foreach (var item in param.Children) //这里是获取之前点击了几个孩子
                {
                    if (item.IsCheck == true)
                    {
                        count++;
                    }
                    else
                    {
                        lists.Add(item);
                    }
                }

                if (count == param.Children.Count) //相等表示之前是全选状态，现在为取消全选，否则反之
                {
                    param.IsCheck = false;
                    foreach (var item in param.Children)
                    {
                        var result = SelectItem.FirstOrDefault(x => x.Name == item.Name);
                        if (result != null)
                        {
                            SelectItem.Remove(result);
                            item.IsCheck = false;
                        }
                    }
                }
                else
                {
                    param.IsCheck = null;
                    foreach (var item in lists)
                    {
                        var result = SelectItem.FirstOrDefault(x => x.Name == item.Name);
                        if (result == null)
                        {
                            SelectItem.Add(item);
                            item.IsCheck = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 单选
        /// </summary>
        /// <param name="param"></param>
        private void Select(TreeModel param)
        {
            try
            {
                var result = SelectItem.FirstOrDefault(x => x.Name == param.Name);

                var value = param.Parent;

                if (result != null)//表示之前点击过，现在是取消选择，否则反之
                {
                    var list = SelectItem.Where(x => x.Parent == value).ToList();
                    if (list.Count == value.Children.Count)//数量相等表示取消全选
                    {
                        value.IsCheck = false;
                    }

                    SelectItem.Remove(result);
                    param.IsCheck = false;
                }
                else
                {
                    SelectItem.Add(param);
                    var list = SelectItem.Where(x => x.Parent == value).ToList();
                    if (list.Count == value.Children.Count)//数量相等表示全选
                    {
                        value.IsCheck = null;
                    }

                    param.IsCheck = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Show()
        {
            MessageBox.Show($"已选择{SelectItem.Count}节点");
        }
    }
}
