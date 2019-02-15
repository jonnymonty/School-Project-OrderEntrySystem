using OrderEntryDataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace OrderEntrySystem.Views
{
    /// <summary>
    /// Interaction logic for EntityView.xaml
    /// </summary>
    public partial class EntityView : UserControl
    {
        private Grid propertyGrid;

        private StackPanel commandPanel;

        public EntityView()
        {
            InitializeComponent();
            propertyGrid = new Grid();
            this.Content = propertyGrid;
        }

        private void BuildLabeledControl(PropertyInfo propertyInfo)
        {
            Grid grid = new Grid();
            grid.Width = 270;
            grid.Height = 23;
            grid.Margin = new Thickness(0, 0, 15, 5);

            ColumnDefinition columnDefinitionOne = new ColumnDefinition();
            columnDefinitionOne.Width = new GridLength(120);

            grid.ColumnDefinitions.Add(columnDefinitionOne);

            ColumnDefinition columnDefinitionTwo = new ColumnDefinition();
            columnDefinitionTwo.Width = new GridLength(150);

            grid.ColumnDefinitions.Add(columnDefinitionTwo);

            Binding binding = CreateBinding(propertyInfo, DisplayUtil.GetControlType(propertyInfo), DataContext);

            switch (DisplayUtil.GetControlType(propertyInfo))
            {
                case ControlType.CheckBox:
                    CheckBox checkBox = new CheckBox();
                    checkBox.SetBinding(CheckBox.IsCheckedProperty, binding);
                    Grid.SetColumn(checkBox, 2);
                    grid.Children.Add(checkBox);

                    break;
                case ControlType.ComboBox:
                    ComboBox comboBox = new ComboBox();
                    if (propertyInfo.PropertyType.IsEnum)
                    {
                        PopulateLookupComboBox(comboBox, Enum.GetValues(propertyInfo.PropertyType), new EnumToStringConverter());
                        //PopulateLookupComboBox(comboBox, RepositoryManager.GetLookupRepository(propertyInfo.PropertyType) as Array, binding.Converter);
                        //comboBox.ItemsSource = propertyInfo.PropertyType.GetEnumValues();
                    }
                    else
                    {
                        try
                        {
                            comboBox.ItemsSource = RepositoryManager.GetLookupRepository(propertyInfo.PropertyType).LookupList;
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString() + " " + propertyInfo.PropertyType.Name);
                        }
                        //ILookupRepository lookupRepository = RepositoryManager.GetLookupRepository(propertyInfo.PropertyType);
                        //comboBox.ItemsSource = lookupRepository.LookupList;
                    }
                    comboBox.SetBinding(ComboBox.IsSelectedProperty, binding);
                    Grid.SetColumn(comboBox, 2);
                    grid.Children.Add(comboBox);

                    break;
                case ControlType.DateBox:

                    break;
                case ControlType.Label:
                    TextBox disabledTextBox = new TextBox();
                    disabledTextBox.SetBinding(TextBox.IsReadOnlyProperty, binding);
                    disabledTextBox.IsReadOnly = true;
                    Grid.SetColumn(disabledTextBox, 2);

                    break;
                case ControlType.TextBox:
                    TextBox textBox = new TextBox();
                    textBox.SetBinding(TextBox.TextProperty, binding);
                    Grid.SetColumn(textBox, 2);
                    grid.Children.Add(textBox);

                    break;
                case ControlType.Button:
                    Button button = new Button();
                    button.Content = DisplayUtil.GetControlDescription(propertyInfo);
                    button.HorizontalAlignment = HorizontalAlignment.Right;
                    button.SetBinding(Button.CommandProperty, binding);
                    button.Margin = new Thickness(2);
                    button.Padding = new Thickness(5);
                    this.commandPanel.Children.Add(button);

                    break;
                case ControlType.None:

                    break;
                default:
                    // Do something...
                    break;
            }

            if (DisplayUtil.GetControlType(propertyInfo) != ControlType.Button)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                propertyGrid.RowDefinitions.Add(rowDefinition);

                Grid.SetRow(grid, this.propertyGrid.RowDefinitions.Count - 1);

                propertyGrid.Children.Add(grid);

                Label label = new Label();
                label.Content = DisplayUtil.GetControlDescription(propertyInfo);
                grid.Children.Add(label);
            }
        }

        private static void PopulateLookupComboBox(ComboBox comboBox, Array lookupObjects, IValueConverter converter)
        {
            comboBox.Items.Clear();
            foreach(var o in lookupObjects)
            {
                Binding binding = new Binding();
                binding.Path = new PropertyPath(comboBox.SelectedValuePath);
                binding.Mode = BindingMode.OneWay;
                binding.Converter = converter;

                DataTemplate dataTemplate = new DataTemplate();

                FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
                textBlockFactory.SetBinding(TextBlock.TextProperty, binding);

                dataTemplate.VisualTree = textBlockFactory;

                comboBox.ItemTemplate = dataTemplate;

                comboBox.Items.Add(o);
            }

        }

        private Binding CreateBinding(PropertyInfo propertyInfo, ControlType controlType, object source)
        {
            Binding binding = new Binding(propertyInfo.Name);

            binding.Source = source;

            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            binding.Mode = controlType == ControlType.Label || controlType == ControlType.Button ? BindingMode.OneWay : BindingMode.TwoWay;

            if (controlType == ControlType.TextBox && binding.Mode == BindingMode.TwoWay)
            {
                switch (propertyInfo.PropertyType.Name)
                {
                    case "Decimal":
                        binding.Converter = new DecimalToStringConverter();

                        break;
                    case "Double":
                        binding.Converter = new DoubleToStringConverter();

                        break;
                }
            }

            return binding;


                //Binding result = new Binding(propertyInfo.Name);
                //result.Source = source;
                //result.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                //if (propertyInfo.CanWrite || controlType != ControlType.Label)
                //{
                //    result.Mode = BindingMode.TwoWay;
                //}
                //else
                //{
                //    result.Mode = BindingMode.OneWay;
                //}

                //if (controlType == ControlType.Button)
                //{
                //    result.Mode = BindingMode.OneWay;
                //}

                //if (controlType == ControlType.TextBox && result.Mode == BindingMode.TwoWay)
                //{
                //    switch (propertyInfo.PropertyType.Name)
                //    {
                //        case "Decimal":
                //            result.Converter = new DecimalToStringConverter();

                //            break;
                //    }
                //}

                //return result;
            }

        private void userControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.propertyGrid = new Grid();
            this.Content = this.propertyGrid;
            PropertyInfo[] properties = typeof(ProductViewModel).GetProperties();

            this.commandPanel = new StackPanel();
            this.commandPanel.Orientation = Orientation.Horizontal;
            this.commandPanel.HorizontalAlignment = HorizontalAlignment.Right;
            this.commandPanel.Margin = new Thickness(2);

            var sortedPvmInfo =
                from p in properties
                where DisplayUtil.HasControl(p)
                orderby DisplayUtil.GetControlSequence(p)
                select p;

            foreach (PropertyInfo property in sortedPvmInfo)
            {
                //object object1 = ReflectionUtil.GetAttributePropertyValue(p, typeof(EntityControlAttribute), p.Name.ToString());
                if (DisplayUtil.HasControl(property))
                {
                    BuildLabeledControl(property);
                }
            }

            this.commandPanel.Orientation = Orientation.Horizontal;
            this.commandPanel.HorizontalAlignment = HorizontalAlignment.Right;
            this.commandPanel.Margin = new Thickness(2);

            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            this.propertyGrid.RowDefinitions.Add(rowDefinition);
            Grid.SetRow(commandPanel, this.propertyGrid.RowDefinitions.Count);
            this.propertyGrid.Children.Add(commandPanel);
        }
    }
}
