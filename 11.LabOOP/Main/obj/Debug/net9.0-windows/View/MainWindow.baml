   M S B A M L   `   `   `  ����?  ;Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MainMain  5   	   K GClassIerarchyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ClassIerarchyLib;assembly=ClassIerarchyLibClassIerarchyLib 5   	       Main.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Main     5   	   MClassLib8clr-namespace:ClassIerarchyLib;assembly=ClassIerarchyLib     5   	   ���#F2F5F8  � Title$  Collection App��5
   	   $	��750��6    $	��880��6-    � 	Resources 5      ��%    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��420 �6   �� 5      $	��440 �6    � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��4.5* �6   �� 5      $
��0.5* �6   �� 5      $��2* �6   .+�5   
   c� $��0��6   $��0��6   $��2��6/   ���#F2F5F8.@�5      �� $
��Save��6   $��16(�6H   $��Arial Rounded)�6V   $��0 0 5 0q�6q   ��6$   �� *SaveFileCommand+6q   �� 5      $
��Load��6   $��16(�6H   $��Arial Rounded)�6V   $��0 0 5 0q�6q   ��6$   �� *LoadFileCommand+6q   5       �� 5"   
   $��1��6   $��0��6   $��Options��6-   $��10 0 0 0q�6?   �� 5#   
   $��1��6   $��1��6   $��
Collection��6-   $��10 0 0 0q�6B   � 5%   
   $��2��6   $��0��6   $��10q�6,    5&      �� 5'      $��1* �6    �� 5(      $��1* �6    �� 5)      $��1* �6    �� 5*      $��1* �6    �� 5+      $��1* �6    �� 5,      $��1* �6    .+�5.      �� $��0��6   $��Create empty collection��6"   ��6D   �� *CreateEmptyCollectionCommand+�� 5/      $��1��6   $��Create auto colleciton��6"   ��6C   �� *CreateAutoCollectionCommand+�� 50      $��2��6   $��Add unit��6"   ��65   �� *AddUnitCommand+�� 51      $��3��6   $��Change unit��6"   ��68   �� *ChangeUnitCommand+�� 52      $��4��6   $��Delete unit��6"   ��68   �� *DeleteUnitCommand+�� 53      $��5��6   $��Query��6"   ��62   �� *UseLINQCommand+54      �� 56   
   $��3��6   $��0��6   $��Journal��6-   $��10 0 0 0q�6?   �� 58   
   $��4��6   $��0��59      $��5q�5:      $��Disabled=�5<      ��5;      �� *	Journal+5=      �� 5?   
   $��2��6   $��1��5@      $��3��5A      $��5q�5B      $��Disabled=�5D      ��5C      �� *
Collection+5E      5F      5G      