using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace OrderEntrySystem
{
    /// <summary>
    /// Interaction logic for MultiEntityView.xaml
    /// </summary>
    public partial class MultiEntityView : UserControl
    {
        public MultiEntityView()
        {
            InitializeComponent();
        }

        public GridViewColumn BuildColumn(PropertyInfo propertyInfo)
        {
            string description = DisplayUtil.GetColumnDescription(propertyInfo);

            int width = DisplayUtil.GetColumnWidth(propertyInfo);

            GridViewColumn gridColumn = new GridViewColumn();

            gridColumn.DisplayMemberBinding = new Binding(propertyInfo.Name);

            gridColumn.Header = description;
            gridColumn.Width = width;

            return gridColumn;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is IMultiEntityViewModel)
            {
                Type type = (this.DataContext as IMultiEntityViewModel).Type;

                var properties = from p in type.GetProperties()
                                 where DisplayUtil.HasColumn(p)
                                 orderby DisplayUtil.GetColumnSequence(p)
                                 select p;

                GridView gridView = new GridView();

                foreach (PropertyInfo p in properties)
                {
                    GridViewColumn gridViewColumn = this.BuildColumn(p);
                    gridView.Columns.Add(gridViewColumn);
                }

                this.entityListView.View = gridView;
            }
        }
    }
}
