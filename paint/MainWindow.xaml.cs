// Import necessary namespaces
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Ink;

// Namespace declaration
namespace paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // URI for temporarily storing the loaded image
        public Uri temp_imageUri;

        // Default drawing attributes for the pen
        private readonly DrawingAttributes PenAttributes = new()
        {
            Color = Colors.Black,
            Height = 2,
            Width = 2
        };

        // Default drawing attributes for the highlighter
        private readonly DrawingAttributes HighlighterAttributes = new()
        {
            Color = Colors.Yellow,
            Height = 10,
            Width = 2,
            IgnorePressure = true,
            IsHighlighter = true,
            StylusTip = StylusTip.Rectangle
        };

        // Event handler for the "Load Image" button
        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog to select an image
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Load the selected image
                    Uri imageUri = new Uri(openFileDialog.FileName);
                    temp_imageUri = imageUri;
                    ImageSource imageSource = new BitmapImage(imageUri);
                    Canvas.Background = new ImageBrush(imageSource);

                    // Reset the ink canvas attributes
                    Canvas.EditingMode = InkCanvasEditingMode.Ink;
                    Canvas.DefaultDrawingAttributes = PenAttributes;
                }
                catch (Exception ex)
                {
                    // Display an error message if image loading fails
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Event handler for the "Save Image" button
        private void SaveImage_Click(object sender, RoutedEventArgs e)
        {
            if (Canvas.Background is ImageBrush imageBrush && imageBrush.ImageSource is BitmapImage bitmapImage)
            {
                // Create a RenderTargetBitmap to render the InkCanvas
                var renderTargetBitmap = new RenderTargetBitmap(
                    (int)Canvas.ActualWidth,
                    (int)Canvas.ActualHeight,
                    96,
                    96,
                    PixelFormats.Pbgra32);

                renderTargetBitmap.Render(Canvas);

                // Save the combined image with paint
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png",
                    Title = "Save Image with Paint"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                        {
                            encoder.Save(stream);
                        }

                        MessageBox.Show("Image saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image with paint: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No image to save. Please load an image first.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            Canvas.DefaultDrawingAttributes = PenAttributes;
        }

        #region Editing Mode

        // Event handler for the "Select" button
        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            SetEditingMode(EditingMode.Select);
        }

        // Event handler for the "Pen" button
        private void PenBtn_Click(object sender, RoutedEventArgs e)
        {
            SetEditingMode(EditingMode.Pen);
        }

        // Event handler for the "Highlighter" button
        private void HighlighterBtn_Click(object sender, RoutedEventArgs e)
        {
            SetEditingMode(EditingMode.Highlighter);
        }

        // Event handler for the "Eraser" button
        private void EraserBtn_Click(object sender, RoutedEventArgs e)
        {
            SetEditingMode(EditingMode.Eraser);
        }

        // Event handler for the "Load Saved Changes" button
        private void LoadSavedChangesButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog to select a saved changes file
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Paint File|*.paint",
                Title = "Open Saved Changes"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Assuming openFileDialog.FileName is the full path to the saved changes file
                string savedChangesFile = openFileDialog.FileName;

                // Call the method to load the saved changes
                LoadSavedChanges(savedChangesFile);
            }
        }

        // Method to load saved changes
        private void LoadSavedChanges(string savedChangesFile)
        {
            try
            {
                // Load the original image
                var originalImageUri = temp_imageUri;
                var originalImage = new BitmapImage(originalImageUri);

                Canvas.Background = new ImageBrush(originalImage);

                // Load the saved ink strokes
                using (var stream = new FileStream(savedChangesFile + ".strokes", FileMode.Open))
                {
                    StrokeCollection strokes = new StrokeCollection(stream);
                    Canvas.Strokes = strokes;
                }

                MessageBox.Show("Changes loaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Display an error message if loading changes fails
                MessageBox.Show($"Error loading changes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for the "Save Changes" button
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Open a save dialog to specify the save location and format
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Paint File|*.paint",
                Title = "Save Changes"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Save the original image
                    var originalImageUri = temp_imageUri;
                    var originalImage = new BitmapImage(originalImageUri);

                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(originalImage));

                    using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        encoder.Save(stream);
                    }

                    // Save the ink strokes separately
                    using (var stream = new FileStream(saveFileDialog.FileName + ".strokes", FileMode.Create))
                    {
                        Canvas.Strokes.Save(stream);
                    }

                    MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    // Display an error message if saving changes fails
                    MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Method to set the editing mode based on the selected mode
        private void SetEditingMode(EditingMode mode)
        {
            PenBtn.IsChecked = false;
            EraserBtn.IsChecked = false;

            switch (mode)
            {
                case EditingMode.Select:
                    Canvas.EditingMode = InkCanvasEditingMode.Select;
                    break;

                case EditingMode.Pen:
                    PenBtn.IsChecked = true;
                    Canvas.EditingMode = InkCanvasEditingMode.Ink;
                    Canvas.DefaultDrawingAttributes = PenAttributes;
                    break;

                case EditingMode.Eraser:
                    Canvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Pen

        // Event handler for the pen color picker
        private void PenColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (IsLoaded)
                PenAttributes.Color = PenColorPicker.SelectedColor ?? Colors.Black;
        }

        // Event handler for the thickness slider
        private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsLoaded)
            {
                PenAttributes.Width = ThicknessSlider.Value;
                PenAttributes.Height = ThicknessSlider.Value;
            }
        }

        #endregion

        #region Highlighter

        // Event handler for the yellow radio button

        #endregion

        #region Eraser Type

        #endregion
    }

    // Enumeration for different editing modes
    public enum EditingMode
    {
        Select, Pen, Highlighter, Eraser
    }
}
