   M S B A M L   `   `   `  ����H  DEncryptionApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:EncryptionAppEncryptionApp  5   	       EncryptionApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:EncryptionApp     5   	     � Title$  Шифрование��5   	   $	��450��6   $��711.2��6)    � WindowStartupLocation$ CenterScreen=�67   ��6\   Z� .��5	      � ��5
   
   �� 5       �� ImageSource$ 	s1200.jpg��6    �� Opacity$	 0.7Q�61   .+�5   
   �� -   5         ��Name$ btnDecision��5      $��Выбрать файл��6&   $
��Left=�6=   $��	24,14,0,0q�6X   $	��Top=�6k   $	��123��6�   $��38��6�   ���	#FF91E27C$
��Bold"�6�   �� 5   
   -   5         ��Name$ lb��5      $�� ��6   $
��Left=�6'   $��
268,20,0,0q�6B   $	��Top=�6V   $
��Bold"�6n   ���	#7FFCFCFC J� RenderTransformOrigin$ -2.23,0.207&�6�   
�������6�   �� 5   
   -   5        $ btnDecipher��5      $%��Дешифровать файл��6&   $
��Left=�6A   $��
24,100,0,0q�6\   $	��Top=�6p   $	��123��6�   ���	#FF91E27C$
��Bold"�6�   $��38��6�   6�   �� 5   
   -   5         ��Name$ txtOut��5      $
��Left=�6"   $	��340��6=   $��
170,57,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $:��4Область для работы с текстом��6r   $	��Top=�6�   $	��508��6�   ���	#CCFFFFFF6�   �� 5   
   -   5        $ 	btnCipher��5      $'��!Зашифровать текст��6$   $
��Left=�6@   $��	24,57,0,0q�6[   $	��Top=�6n   $	��123��6�   $��38��6�   ���	#FF91E27C$
��Bold"�6�   6�   �� 5   
   $��Выбран файл:��6   $
��Left=�6'   $��
170,20,0,0q�6B   $	��Top=�6V   ���	#7FFFFFFF$
��Bold"�6�   �� 5   
   -   5        $ btnSave��5      $#��Сохранить текст��6"   $
��Left=�6<   $��
24,144,0,0q�6W   $	��Top=�6k   $	��123��6�   $��38��6�   ���	#FF91E27C$
��Bold"�6�   6�   5      5      