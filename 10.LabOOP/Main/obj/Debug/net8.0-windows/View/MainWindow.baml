   M S B A M L   `   `   `  ����?  ;Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MainMain  5   	       Main.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Main     5   	   -   5          � Title$  Threads��5   	   $	��450��6   $	��450��6)   $	��450��66   $	��620��6F   $	��620��6R   ���#1f1f1f � 	Resources 5         btnCtrlTemp �� 
TargetType ��Name �� Triggers P� Property P� Value �� Property �� Value  ButtonChooseThreadStl	 �� 
TargetType ! ButtonChooseThreadPriorityStl  ButtonStartStopThreadStl
 �� BasedOn��%�  &	        &	 �    7   &	 >    7   &	 �    7  7   '��     '�� �    '�� �    '��     �� 5   
   " ��6.   .R�5      �� $ border��6   #��z��6%   #��z��5      ._�5      �� $��Center=�6#   $��Center=�6@   5       5      P� 5       � {�$ False��6/   . �5      ��  � �� �#55555563   �� 5       � �� �#2f2f2f63   5      P� 5       �@J�IsMouseOver$
 True��61   . �5      ��  � �� �#7d7d7d63   5      5      �� 5   
   "	 ��6.   .�5      ��  � �� �#ffffff68   �� 5       � ��$ 16(�66   �� 5       � ��$ Cascadia Code)�68   �� 5        � ��$ 5 0 5 0q�64   �� 5!       � ��$	 130��63   �� 5"       � ��$ 80��66   �� 5#       � ��$ 30��64   �� 5$       � �� �#4d4d4d68   �� 5%       � ��$ 0q�6=   �� 5&       � ��8   6)   5'      �� 5(   
   "	 ��67   .�5)      ��  � �� �#ffffff6+   �� 5*       � ��$ 16(�6)   �� 5+       � ��$ Cascadia Code)�6+   �� 5,       � ��$ 5 2 5 2q�6'   �� 5/       � ��$ 25��6'   �� 50       � �� �#4d4d4d6+   �� 51       � ��$ 0q�60   �� 52       � ��8   6)   53      �� 54   
   8
   "	 ��6b   .�55      ��  � ��$ 60��64   �� 56       � ��8  6)   57      �� 58   
   "	 ��6   .�59      ��  � ��$ 20(�6)   �� 5:       � ��$
 Bold"�6+   �� 5;       � ��$ Calibri)�6+   �� 5<       � �� �#ffffff68   5=      �� 5>   
   "	 ��6   .�5?      ��  � ��$ 14(�6)   �� 5@       � ��$ Calibri)�6+   �� 5A       � ��$ Italic$�6*   �� 5B       � ��$ 10 0 0 0q�6'   �� 5C       � �� �#ffffff68   5D      �� 5E   
   "	 ��6   .�5F      ��  � �� �#7160e86+   5G      �� 5H   
   "	 ��6   .�5I      ��  � ��$ Cascadia Code)�6+   �� 5J       � ��$ 10(�6)   5K      5L      .��5M      � $��10q�6    � RowDefinitions 5N   
   �� 5O      $��35 �6   �� 5P      $��60 �6   �� 5Q      $��35 �6   �� 5R      $	��150 �6   �� 5S      $��35 �6   �� 5T      $��1* �6    � ColumnDefinitions 5V   
   �� 5W      $	��280��6   �� 5X      $��15 �6   �� 5Y      $	��280��6   .+�5\   
   �� $��0��6   $��0��6   $��Choose thread��6-   �� 5]   
   $��0��6   $��2��6   $��File output��6-   �� 5^   
   $��2��6   $��0��6   $��Choose priority��6-   �� 5_   
   $��4��6   $��0��6   $��Start/Stop threads��6-   �� 5`   
   $��4��6   $��2��6   $��Thread priority state��6-   �� 5b   
   $��1��6   $��0��6"    �� Orientation$ Vertical=�62   .+�5c      �� $ 
Horizontal=�6   $��40��62   $��Center=�6>   .+�5d      �� $��WriteThread��6   #��[ 6/   $��WriteThread��5e   Q   ��6   �� *%#threadViewModel.ChooseThreadCommand+6Q   �� 5f      $��
ReadThread��6   #��[ 6.   $��
ReadThread��5g   Q   ��6   �� *%#threadViewModel.ChooseThreadCommand+6Q   5h      �� 5i      ��6   �� *+)threadViewModel.TextBlockInfoChosenThread+5j      �� 5l   
   $��3��6   $��0��6"   $ Vertical=�62   $��Stretch=�6I   .+�5m      �� $��Lowest��6   #��[ 6&   $��Lowest��5n   U   ��6   �� *-+threadViewModel.ChangeThreadPriorityCommand+{�5o      �� *20threadViewModel.TextBlockInfoChosenThreadPressed+�� 5p      $��BelowNormal��6   #��[ 6+   $��BelowNormal��5q   U   ��6   �� *-+threadViewModel.ChangeThreadPriorityCommand+{�5r      �� *20threadViewModel.TextBlockInfoChosenThreadPressed+�� 5s      $��Normal��6   #��[ 6&   $��Normal��5t   U   ��6   �� *-+threadViewModel.ChangeThreadPriorityCommand+{�5u      �� *20threadViewModel.TextBlockInfoChosenThreadPressed+�� 5v      $��AboveNormal��6   #��[ 6+   $��AboveNormal��5w   U   ��6   �� *-+threadViewModel.ChangeThreadPriorityCommand+{�5x      �� *20threadViewModel.TextBlockInfoChosenThreadPressed+�� 5y      $��Highest��6   #��[ 6'   $��Highest��5z   U   ��6   �� *-+threadViewModel.ChangeThreadPriorityCommand+{�5{      �� *20threadViewModel.TextBlockInfoChosenThreadPressed+5|      �� 5~   
   $��5��6   $��0��6"   $ 
Horizontal=�62   $	��Top=�6K   $��Center=�6c   .+�5      �� $��Start��6   #��[ 6%   $��Start��5�   R   ��6   �� **(threadViewModel.StartOrStopThreadCommand+6R   �� 5�      $
��Stop��6   #��[ 6$   $
��Stop��5�   R   ��6   �� **(threadViewModel.StartOrStopThreadCommand+6R   5�      �� 5�   
   $��1��6   $��6��6'   $��Center=�68   $��3��6U   �� 5�   
   $��1��6   $��2��6   $��3��6/    �� TextWrapping$
 Wrap=�6y   ��6@   �� *threadViewModel.filer.file+ �� Mode$ OneWay=�6y   �� 5�   
   $��5��6   $��2��6"   .+�5�      �� ��6   �� *-+threadViewModel.TextBlockInfoWriterPriority+�� 5�      ��6   �� *-+threadViewModel.TextBlockInfoReaderPriority+5�      5�      5�      