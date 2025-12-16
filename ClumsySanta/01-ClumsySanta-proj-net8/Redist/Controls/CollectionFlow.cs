using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClumsySanta.Redist.Controls
{
    public class CollectionFlow : ItemsControl
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(CollectionFlow), null);
        public static readonly DependencyProperty SelectedItemIndexProperty = DependencyProperty.Register("SelectedItemIndex", typeof(int), typeof(CollectionFlow), new PropertyMetadata(CollectionFlow.SelectedItemIndexChanged));

        public CollectionFlow()
        {
            this.DefaultStyleKey = typeof(CollectionFlow);
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
            get { return (object)this.GetValue(CollectionFlow.SelectedItemProperty); }
            private set { this.SetValue(CollectionFlow.SelectedItemProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public int SelectedItemIndex
        {
            get { return (int)this.GetValue(CollectionFlow.SelectedItemIndexProperty); }
            set { this.SetValue(CollectionFlow.SelectedItemIndexProperty, value); }
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