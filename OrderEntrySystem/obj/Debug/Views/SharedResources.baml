   M S B A M L   `   `   `  ����K  GOrderEntrySystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:OrderEntrySystemOrderEntrySystem  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� DataType'    !OrderEntrySystem.ProductViewModel"   OrderEntrySystem.ProductView,   &OrderEntrySystem.MultiProductViewModel'   !OrderEntrySystem.MultiProductView%   OrderEntrySystem.OrderViewModel    OrderEntrySystem.OrderView*   $OrderEntrySystem.MultiOrderViewModel%   OrderEntrySystem.MultiOrderView(   "OrderEntrySystem.CustomerViewModel#	   OrderEntrySystem.CustomerView-
   'OrderEntrySystem.MultiCustomerViewModel(   "OrderEntrySystem.MultiCustomerView(   "OrderEntrySystem.LocationViewModel#   OrderEntrySystem.LocationView-   'OrderEntrySystem.MultiLocationViewModel(   "OrderEntrySystem.MultiLocationView(   "OrderEntrySystem.CategoryViewModel#   OrderEntrySystem.CategoryView-   'OrderEntrySystem.MultiCategoryViewModel(   "OrderEntrySystem.MultiCategoryView   ListViewStyle �� 
TargetType �� Property �� Value �� Path �� Mode �� Triggers ]� 
Conditions �� Property	 �� Value  WorkspacesTemplate$
 �� IsSynchronizedWithCurrentItem � Cursor  NestedCommandTemplate �� Orientation  CommandsTemplate  MainHeaderStyle  Brush_HeaderBackground �� CornerRadius �� Padding J� SnapsToDevicePixels �� 
TargetType�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      %vmclr-namespace:OrderEntrySystem  5      %�
  (��       5      *,  +)(�� 7     5      *, +)(�� n     5      *, +)(�� �     5      *, +)(�� �     5      *, +)(��     5      *,
 +)(�� J    5      *, +)(�� �    5      *, +)(�� �    5!      *, +)(�� �    5$      *, +)&	  &    &	 �    &	 U    &	 �    &	 E    7  �� 5      "    6   .R�5   
     6   5      �� 5      "   6   .R�5	   
     6    5
      �� 5      "   6   .R�5   
     6   5      �� 5      "   6   .R�5   
     6   5      �� 5      "   6   .R�5   
   	  6   5      �� 5      "  
 6   .R�5   
     6!   5      �� 5      "   6   .R�5   
     6   5      �� 5      "   6   .R�5   
     6!   5      �� 5!      "   6   .R�5"   
     6   5#      �� 5$      "   6   .R�5%   
     6!   5&      �� 5(      " ��6"   .�5)   
   ��  � ��$ Stretch=�67   �� 5*   
    �@��
IsSelected 6'   �� $ 
IsSelected�$ TwoWay=� 5+   
   ]� 5,       5-      �� 5.       �@��AlternationIndex$	 1��6I   �� 5/       �@��
IsSelected	 .  66   �� 50       �@J�IsMouseOver	 .  67   ./�52      ��  � �� �	#EEEEEEEE6/   53      55      �� 57      .R�58   
   �� $

 TrueY�6   $��4q�6R   ��6:   �� ��59      �� 5:      .R�5;      ]� .+�5<      �� $��X��6!   $��Right=�6-   $
 Hand��6D   $��4,0,0,0q�5=   !   $
��Bold"�62   $��16��6D   $��16��6P   $��Courier)�5>   !   $��9(�66   ��6C   �� $ CloseCommand��� 5?      $��Center=�6P   ��6+   �� $ DisplayName�6P   5@      5A      5C      5D      �� 5F      .R�5G   
   �� $��Right=�6/   ��6   �� ��5H      �� 5I      .R�5J      �� $ 
Horizontal=�6!   5K      ��5M      �� 5N      .R�5O      �� $��23��6c   $��75��6o   $��4,4,0,0q�6z   ��6   �� $ Command���6>   �� $ DisplayName�6z   5P      5R      5S      �� 5U      .R�5V   
   �� ��6   �� ��5W      �� 5X      .R�5Y      �� ��6   �� $ Command���6>   �� $ DisplayName�5Z      5\      5]      �� 5_      " ��6$   .�5`   
   ��  � �� 5a      �� 5b      .R�5c      �� 8��  
�������$��1q�6j   $ 5��5d      $��4q�6.   $ 4q�69    . ._�5e      �� $��14(�6$   $
��Bold"�62   
�������$��Center=�6W   #��z��6t   5f      5g      5i      �� 5j   
    � �� .  6&   �� 5k   
    � �� 5l      �� 5n      " ��6"   .R�5o      ]� .+�5p      �� $	��Top=�6+   $��Header��6@   #��z��6W   �� 5q      $��Content��6+   #��z��6C   5r      5s      5u      5v      5x      