   M S B A M L   `   `   `  ����  ProjectAnatolia     ProjectAnatolia.PingWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$
  Ping��5   	   $	��200��6   $	��400��6#    � 
ResizeMode$ NoResize=�6/    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5	      .+�5   
   �� $��2��6   .+�5      �� $#��Enter IP address or hostname:��6   �� 5      -   5        $��txtPingAddress��5      $	��300��6,   $��0,5,0,10q�68   �� 5       �� Orientation$ 
Horizontal=�6   $��0,5,0,10q�62   .+�5      �� -   5        $��btnPing��5      $��
Start Ping��6(   $	��100��6=   $��0,0,10,0q�6\   �� 5      -   5        $��btnStop��5      $��	Stop Ping��6(   $	��100��6<   {�.  6[   5      �� 5      -   5        $��txtPingResult��5      $��80��6+   $��0,5,0,0q�67    �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�6Z   5      5      5      