   M S B A M L   `   `   `  ����M  IWindowsMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WindowsMediaPlayerWindowsMediaPlayer  5   	   f bGalaSoft.MvvmLight.Platform, Version=5.2.0.37226, Culture=neutral, PublicKeyToken=5f873c45e98af8a1lMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.PlatformGalaSoft.MvvmLight.Command 5   	   #    WindowsMediaPlayer.MainWindow   5   	   U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:WindowsMediaPlayer     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   WcmdMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.Platform  5   	     � Title$  Pas Persona Player��5   	   $��550.411��6$   $��1013.799��65   ���	#FFD8D8D8 � DataContext 5	   	   �� *PlayerViewModel+   Locator �� Source# [   � 	Resources 5        FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  TabItem.Static.Background �� EndPoint	 �� 
StartPoint
 � Color � Offset  TabItem.Static.Border �� Color   TabItem.MouseOver.Background  TabItem.MouseOver.Border  TabItem.Disabled.Background  TabItem.Disabled.Border  TabItem.Selected.Border 	 TabItem.Selected.Background 
 TabItemStylePlayer �� 
TargetType �� 
TargetType �Name ��Name J� Opacity ��Name �� RelativeSource �� AncestorType �� Triggers ^� 
Conditions �� Binding �� Value �� 
TargetName��%�!  &	       &	 �     &	 B    &	 c    &	 �    &	     &	 =    &	 ^    &		     &	
 �    7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $	 0,1&�6@   $		 0,0&�6O   .��5      � $
 #F0F0F0��6   $	 0.0Q�6+   � 5      $
 #E5E5E5��6   $	 1.0Q�6+   5      �� 5   
   $ #ACACAC��68   �� 5   
   $	 0,1&�6C   $		 0,0&�6R   .��5      � $
 #ECF4FC��6   $	 0.0Q�6+   � 5      $
 #DCECFC��6   $	 1.0Q�6+   5      �� 5    
   $ #7EB4EA��6;   �� 5!   
   $ #F0F0F0��6>   �� 5"   
   $ #D9D9D9��6:   �� 5#   
   $ #ACACAC��6:   �� 5$   
   $ #FFFFFF��6>   �� 5%   
   " ��6+   .�5&      ��  �@�FocusVisualStyle8   61   �� 5'       � ��
 �   �6+   �� 5(       � ��8  6+   �� 5)       � ��8  6,   �� 5*       � ��$ 0q�6'   �� 5+       � ��$ 6,2,6,2q�6(   �� 5,       � ��$ Stretch=�6;   �� 5-       � ��$ Stretch=�69   �� 5.       � �� 5/      �� 50      " ��6&   .R�51      � $ templateRoot��6    . .+�52      �� $ 
mainBorder��6%   #��z��69   $��1,1,1,0q�6e   #��z��6   $��0q�6�   ._�53   "   �� $ innerBorder��6)   8�� $��1,1,1,0q�6u   ���	#FF999999$��-1q�6�   $ 0Q�6�   54      �� 55      $ contentPresenter��6/   $��Header��6I   |�.  #��z��6�   ��. # z 6/  ��6r   �� *HorizontalContentAlignment+ �� " ����6k  �� *VerticalContentAlignment+ �� " ��56       57      ^� 58       59   "   �� 5:   &    60   �� *IsMouseOver+ �� *Self+ true6v   �� 5;   &    60   �� *TabStripPlacement+ �� " �� Left6�   .0�5=   "   �� $ 
mainBorder��6?   8   � ��6W   �� 5>   "   $ 
mainBorder��6@   8   � ��6X   �� 5?   "   $ innerBorder��6D    � ��$ 1,1,0,1q�6]   �� 5@   "   $ 
mainBorder��6D    � ��$ 1,1,0,1q�6\   5A      ^� 5B       5C   "   �� 5D   &    60   �� *IsMouseOver+ �� *Self+ true6v   �� 5E   &    60   �� *TabStripPlacement+ �� " ��
 Bottom6�   .0�5G   "   �� $ 
mainBorder��6?   8   � ��6W   �� 5H   "   $ 
mainBorder��6@   8   � ��6X   �� 5I   "   $ innerBorder��6D    � ��$ 1,0,1,1q�6]   �� 5J   "   $ 
mainBorder��6D    � ��$ 1,0,1,1q�6\   5K      ^� 5L       5M   "   �� 5N   &    60   �� *IsMouseOver+ �� *Self+ true6v   �� 5O   &    60   �� *TabStripPlacement+ �� " ��	 Right6�   .0�5Q   "   �� $ 
mainBorder��6?   8   � ��6W   �� 5R   "   $ 
mainBorder��6@   8 	  � ��6X   �� 5S   "   $ innerBorder��6D    � ��$ 0,1,1,1q�6]   �� 5T   "   $ 
mainBorder��6D    � ��$ 0,1,1,1q�6\   5U      ^� 5V       5W   "   �� 5X   &    60   �� *IsMouseOver+ �� *Self+ true6v   �� 5Y   &    60   �� *TabStripPlacement+ �� " �� Top6�   .0�5[   "   �� $ 
mainBorder��6?   8 
  � ��6W   �� 5\   "   $ 
mainBorder��6@   8   � ��6X   �� 5]   "   $ innerBorder��6D    � ��$ 1,1,1,0q�6]   �� 5^   "   $ 
mainBorder��6D    � ��$ 1,1,1,0q�6\   5_      ^� 5`       5a   "   �� 5b   &    60   �� *	IsEnabled+ �� *Self+	 false6t   �� 5c   &    60   �� *TabStripPlacement+ �� " �� Left6�   .0�5e   "   �� $ contentPresenter��6<    �@J�Opacity$
 0.56Q�6Z   �� 5f   "   $ 
mainBorder��6?   8   � ��6W   �� 5g   "   $ 
mainBorder��6@   8   � ��6X   �� 5h   "   $ innerBorder��6D    � ��$ 1,1,0,1q�6]   �� 5i   "   $ 
mainBorder��6D    � ��$ 1,1,0,1q�6\   5j      ^� 5k       5l   "   �� 5m   &    60   �� *	IsEnabled+ �� *Self+	 false6t   �� 5n   &    60   �� *TabStripPlacement+ �� " ��
 Bottom6�   .0�5p   "   �� $ contentPresenter��6<    �@J�Opacity$
 0.56Q�6Z   �� 5q   "   $ 
mainBorder��6?   8   � ��6W   �� 5r   "   $ 
mainBorder��6@   8   � ��6X   �� 5s   "   $ innerBorder��6D    � ��$ 1,0,1,1q�6]   �� 5t   "   $ 
mainBorder��6D    � ��$ 1,0,1,1q�6\   5u      ^� 5v       5w   "   �� 5x   &    60   �� *	IsEnabled+ �� *Self+	 false6t   �� 5y   &    60   �� *TabStripPlacement+ �� " ��	 Right6�   .0�5{   "   �� $ contentPresenter��6<    �@J�Opacity$
 0.56Q�6Z   �� 5|   "   $ 
mainBorder��6?   8   � ��6W   �� 5}   "   $ 
mainBorder��6@   8   � ��6X   �� 5~   "   $ innerBorder��6D    � ��$ 0,1,1,1q�6]   �� 5   "   $ 
mainBorder��6D    � ��$ 0,1,1,1q�6\   5�      ^� 5�       5�   "   �� 5�   &    60   �� *	IsEnabled+ �� *Self+	 false6t   �� 5�   &    60   �� *TabStripPlacement+ �� " �� Top6�   .0�5�   "   �� $ contentPresenter��6<    �@J�Opacity$
 0.56Q�6Z   �� 5�   "   $ 
mainBorder��6?   8   � ��6W   �� 5�   "   $ 
mainBorder��6@   8   � ��6X   �� 5�   "   $ innerBorder��6D    � ��$ 1,1,1,0q�6]   �� 5�   "   $ 
mainBorder��6D    � ��$ 1,1,1,0q�6\   5�      ^� 5�       5�   "   �� 5�   &    60   �� *
IsSelected+ �� *Self+	 false6u   �� 5�   &    60   �� *TabStripPlacement+ �� " �� Left6�   .0�5�   "   �� $ innerBorder��6D    � ��$ 1,1,0,1q�6]   �� 5�   "   $ 
mainBorder��6D    � ��$ 1,1,0,1q�6\   5�      ^� 5�       5�   "   �� 5�   &    60   �� *
IsSelected+ �� *Self+ true6u   �� 5�   &    60   �� *TabStripPlacement+ �� " �� Left6�   .0�5�   "   ��  �@K�ZIndex$ 1��6A   �� 5�   "    � ��$ 
-2,-2,0,-2q�6;   �� 5�   "   $ innerBorder��6<    �@J�Opacity$ 1Q�6U   �� 5�   "   $ innerBorder��6D    � ��$ 1,1,0,1q�6]   �� 5�   "   $ 
mainBorder��6D    � ��$ 1,1,0,1q�6\   5�      ^� 5�       5�   "   �� 5�   &    60   �� *
IsSelected+ �� *Self+	 false6u   �� 5�   &    60   �� *TabStripPlacement+ �� " ��
 Bottom6�   .0�5�   "   �� $ innerBorder��6D    � ��$ 1,0,1,1q�6]   �� 5�   "   $ 
mainBorder��6D    � ��$ 1,0,1,1q�6\   5�      ^� 5�       5�   "   �� 5�   &    60   �� *
IsSelected+ �� *Self+ true6u   �� 5�   &    60   �� *TabStripPlacement+ �� " ��
 Bottom6�   .0�5�   "   ��  �@K�ZIndex$ 1��6A   �� 5�   "    � ��$ 
-2,0,-2,-2q�6;   �� 5�   "   $ innerBorder��6<    �@J�Opacity$ 1Q�6U   �� 5�   "   $ innerBorder��6D    � ��$ 1,0,1,1q�6]   �� 5�   "   $ 
mainBorder��6D    � ��$ 1,0,1,1q�6\   5�      ^� 5�       5�   "   �� 5�   &    60   �� *
IsSelected+ �� *Self+	 false6u   �� 5�   &    60   �� *TabStripPlacement+ �� " ��	 Right6�   .0�5�   "   �� $ innerBorder��6D    � ��$ 0,1,1,1q�6]   �� 5�   "   $ 
mainBorder��6D    � ��$ 0,1,1,1q�6\   5�      ^� 5�       5�   "   �� 5�   &    60   �� *
IsSelected+ �� *Self+ true6u   �� 5�   &    60   �� *TabStripPlacement+ �� " ��	 Right6�   .0�5�   "   ��  �@K�ZIndex$ 1��6A   �� 5�   "    � ��$ 
0,-2,-2,-2q�6;   �� 5�   "   $ innerBorder��6<    �@J�Opacity$ 1Q�6U   �� 5�   "   $ innerBorder��6D    � ��$ 0,1,1,1q�6]   �� 5�   "   $ 
mainBorder��6D    � ��$ 0,1,1,1q�6\   5�      ^� 5�       5�   "   �� 5�   &    60   �� *
IsSelected+ �� *Self+	 false6u   �� 5�   &    60   �� *TabStripPlacement+ �� " �� Top6�   .0�5�   "   �� $ innerBorder��6D    � ��$ 1,1,1,0q�6]   �� 5�   "   $ 
mainBorder��6D    � ��$ 1,1,1,0q�6\   5�      ^� 5�       5�   "   �� 5�   &    60   �� *
IsSelected+ �� *Self+ true6u   �� 5�   &    60   �� *TabStripPlacement+ �� " �� Top6�   .0�5�   "   ��  �@K�ZIndex$ 1��6A   �� 5�   "    � ��$ 
-2,-2,-2,0q�6;   �� 5�   "   $ innerBorder��6<    �@J�Opacity$ 1Q�6U   �� 5�   "   $ innerBorder��6D    � ��$ 1,1,1,0q�6]   �� 5�   "   $ 
mainBorder��6D    � ��$ 1,1,1,0q�6\   5�      5�      5�      5�      5�      .��5�      � .+�5�   
   �� $��89��6   $��Bottom=�6    $��-11,0,-17,0q�6;   ��5�      �� 5�      $ Black��6"    �� Opacity$ 0Q�60   ��5�      �� 5�       �� ImageSource$ assets/backplayer.png��6   5�         WindowsMediaPlayer.Search  5�   
   $��0,0,0,9q�6   $
��Auto��6(   $
��Auto��66   �� 5�   
   $��	0,27,0,87q�6   ���	#FF999999��6?   Z� .  (System.Windows.Interactivity.Interaction   Triggers 5�      /  )System.Windows.Interactivity.EventTrigger  5�         	EventName$ SelectionChanged��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5�      /  )GalaSoft.MvvmLight.Command.EventToCommand     PassEventArgsToCommand .     Command  6)   �� *
TabChanged+! �� Mode$! OneWay=�6U   5�      .@�5�      �� $��Player��6   ���	#FF999999���	#FF999999#��� 
 6U   5�      �� 5�      $��	Libraries��6   
�������
�������.��5�      � .+�5�      $   WindowsMediaPlayer.LibraryView  $��10,-3,525,-12q�6(   %   WindowsMediaPlayer.PlaylistView  5�      $��0,-3,10,-32q�6)   " J� RenderTransformOrigin$" 0.684,0.427&�6>   $��Right=�6b   $	��515��6~   5�      5�      5�      #   WindowsMediaPlayer.PlayerView  5�   
   $��0,0,10,0q�6   $��68��6-   $
��Auto��69   $��Bottom=�6F   "	   WindowsMediaPlayer.MediaView	  5�   
   $��10,60,10,73q�6   5�      5�      