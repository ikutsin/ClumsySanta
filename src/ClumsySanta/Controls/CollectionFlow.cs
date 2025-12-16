using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClumsySanta.Controls
{
    public class CollectionFlow : ItemsControl
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(CollectionFlow), null);
        public static readonly DependencyProperty SelectedItemIndexProperty = DependencyProperty.Register("SelectedItemIndex", typeof(int), typeof(CollectionFlow), new PropertyMetadata(SelectedItemIndexChanged));

        public CollectionFlow()
        {
			DefaultStyleKey = typeof(CollectionFlow);
        }

        private static void SelectedItemIndexChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CollectionFlow cf = (CollectionFlow)sender;

            if (cf.Panel != null && cf.Panel.FocusedItemIndex != cf.SelectedItemIndex)
            {
                cf.Panel.FocusedItemIndex = (int)e.NewValue;
                cf.SelectedItem = cf.Items[cf.SelectedItemIndex];
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            if (Panel != null && Panel.FocusedItemIndex != SelectedItemIndex)
            {
                Panel.FocusedItemIndex = SelectedItemIndex;
                SelectedItem = Items[SelectedItemIndex];
            }

            return base.GetContainerForItemOverride();
        }

        [Category("CollectionFlow Panel")]
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            private set { SetValue(SelectedItemProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public int SelectedItemIndex
        {
            get { return (int)GetValue(SelectedItemIndexProperty); }
            set { SetValue(SelectedItemIndexProperty, value); }
        }

        public CollectionFlowPanel Panel
        {
            get
            {
                if (GetTemplateChild("ItemsPresenter") != null)
                    return (CollectionFlowPanel)VisualTreeHelper.GetChild(GetTemplateChild("ItemsPresenter"), 0);

                return null;
            }
        }
    }
}