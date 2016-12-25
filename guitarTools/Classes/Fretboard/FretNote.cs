﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace guitarTools
{
    /// <summary>
    /// FretNote is an object which represents a note on the fretboard
    /// It is created and managed by the Fretboard object
    /// </summary>

    class FretNote
    {
        #region Properties
        private int Index { get; set; }
        private bool IsActive { get; set; }
        private string[] MusicKeys = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        private Label noteText;
        private Border noteBody;
        private Viewbox box; 
        #endregion

        public FretNote(int index, double size, bool isActive, Point xy, Grid grid)
        {
            #region Defining variables
            Index = index;
            IsActive = isActive;
            #endregion

            #region Creating objects visual representation
            //Creating a Border
            noteBody = new Border()
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Background = IsActive ? Brushes.DarkBlue : Brushes.Crimson, //Inline IF ELSE operation
                Opacity = 0.5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = size,
                Height = size,
                CornerRadius = new CornerRadius((size * 2) / 4) //Calculating radius based on the Width and Height of the border
            };

            //Creating a Label
            noteText = new Label()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.White,
                Content = MusicKeys[Index]
            };

            //Creating a Viewbox
            box = new Viewbox()
            {
                //Parenting the elements accordingly: Label -> Viewbox -> Border
                Child = noteText
            };

            noteBody.Child = box;

            Grid.SetColumn(noteBody, (int)xy.X);
            Grid.SetRow(noteBody, (int)xy.Y);

            //Adding the created object to the grid
            grid.Children.Add(noteBody); 
            #endregion
        }

        public void NewRoot(int ShiftBy)
        {
            // Updates Label content to suit the new root
            noteText.Content = MusicKeys[(new IntLimited(Index + ShiftBy, 0, 12)).GetValue];
        }
    }
}