   M S B A M L   `   `   `  ????L  HBCSH2_Semestralka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:BCSH2_Semestralka.ViewModelBCSH2_Semestralka.ViewModel  5   	   5clr-namespace:BCSH2_SemestralkaBCSH2_Semestralka  5	   N   $    BCSH2_Semestralka.View.AppView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?	viewModel)clr-namespace:BCSH2_Semestralka.ViewModel     5   	   :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     5   	   ^ ZICSharpCode.AvalonEdit, Version=6.1.3.50, Culture=neutral, PublicKeyToken=9cc39be672370310?
avalonedit.http://icsharpcode.net/sharpdevelop/avalonedit  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	   	   .controlsclr-namespace:BCSH2_Semestralka  6N     ? Title$  MiniKotlinInterpreter??5   	   $	??550??6'   $
??1200??64   $	??420??5   	   $	??600??6    ? 
ResizeMode$ 	CanResize=?5   	   .??5      ? $??10q?6    ? RowDefinitions 5   
   ?? 5      $??1.75* ?6   ?? 5      $??* ?6    ? ColumnDefinitions 5   
   ?? 5      $??3* ?6   ?? 5      $??* ?6   .+?5   
   ?? $??1??6   $??	10,0,0,10q?6%   .+?5      ?? $??File name: ??6   ?? 5      ??6   ??  ?? Path$ SaveFileName? ?? Mode$ OneWay=??? 5      $??10(?6J   $??Stretch=?6X   ??6   ?? $ SaveFilePath?$ OneWay=?6X   ?? 5      $??0,10,0,0q?6   $??	Open file??6'   ??6;   ?? *
OpenFile+?? 5      $??0,10,0,0q?6   $??	Save file??6'   ??6;   ?? *
SaveFile+?? 5       $??0,10,0,0q?6   $??Save file as??6'   ??6>   ?? *
SaveFileAs+?? 5!      $??0,10,0,0q?6   $??New file??6'   ??6:   ?? *	NewFile+?? 5"      $??0,10,0,0q?6    ?? Orientation$ 
Horizontal=?6+   $??Center=?6D   .+?5#      ? $??23??6   $	??Top=?6#   .+?5$      ?? -   5        $??3,2,13,3q?5$   D   ??6   ?? *
TextSize+$ TwoWay=?6V   ?? 5%      $??10(?6   $??0,-4,0,0q?6+   $	??▲??6>   $??10??6J   $??	33,2,1,13q?6x   ??6U   ?? *ChangeSizePlus+6x   ?? 5&      $??10(?6   $??0,-4,0,0q?6+   $	??▼??6>   $??10??6J   $??	33,12,1,3q?6y   ??6U   ?? *ChangeSizeMinus+6y   5'      5(      5)      ?? 5*   
   $??1??6   $??1??6%   $??	10,10,0,0q?62   $??Stretch=?6E   $??Center=?6c   .+?5+      ?? $??Compile??6   $??22(?6D   ??6(   ?? *	Compile+6D   ?? 5,      $	??Run??6   $??22(?6<   $??0,15,0,0q?6J   ??6$   ?? *Run+6J   ?? 5-      $
??Stop??6   $??22(?6>   $??0,15,0,0q?6L   ??6%   ?? *Stop+6L   5.      ?? 50   
   $??1q?6   
???   ?$??	0,0,10,10q?69   ._?51      '  !ICSharpCode.AvalonEdit.TextEditor  -   5          Name$ 
textEditor??55         WordWrap$
 True??52      	   ShowLineNumbers$
	 True??53      
   LineNumbersForeground

 ?   ?   SyntaxHighlightingM  GICSharpCode.AvalonEdit.Highlighting.HighlightingDefinitionTypeConverter$	 XML 57         
IsReadOnly 56      ?? *CodeReadOnly+$ OneWay=???58      ?? *
TextSize+$ OneWay=?   Text. 6<   .  (System.Windows.Interactivity.Interaction   	Behaviors 59      +   %BCSH2_Semestralka.AvalonEditBehaviour  5:         GiveMeTheText 63   ?? *	InputText+$ TwoWay=? ?? UpdateSourceTrigger$ PropertyChanged=?. 5;   +   5<      5=      ?? 5>   
   -   5        $??createOrLoadLabel??5>   b   $??Create or load a file.??6   $??Center=?6{   $??Center=?6?   y?61   ?? *PromptVisible+$ OneWay=?6?   ?? 5?   
   $??1??6   $??1q?6   
???   ?$??	0,10,10,0q?6F   ._?5@      ?? -   5         ??Name$ ScrollableOutput??5@      .??5A      ?? -   5         ??Name$ TextBoxScroll??5B   G   $??Stretch=?6    ?? TextWrapping$
 Wrap=?63   $??Stretch=?5C   b    ?? AcceptsReturn$
 True??5D   @   ??5C      ?? *
OutputText+$ TwoWay=?$ PropertyChanged=???5D      ?? *
TextSize+$ OneWay=? ?? 
IsReadOnly 6U   ?? *OutputReadOnly+$ OneWay=?5E      5F      5G      5H      