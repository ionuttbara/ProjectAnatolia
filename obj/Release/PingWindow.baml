   M S B A M L   `   `   `  ����   ProjectAnatolia     ProjectAnatolia.PingWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$
  Ping��$	��200��$	��400�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � ColumnDefinitions �� $��0* ��� .+��� $��2��.+��� $#��Enter IP address or hostname:���� -   $��txtPingAddress��$	��300��$��0,5,0,10q���  �� Orientation$ 
Horizontal=�$��0,5,0,10q�.+��� -   $��btnPing��$��
Start Ping��$	��100��$��0,0,10,0q��� -   $��btnStop��$��	Stop Ping��$	��100��{�.  �� -   $��txtPingResult��$��80��$��0,5,0,0q� �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�